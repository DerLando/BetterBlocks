using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using RealBlocksUI.Library.Api;
using RealBlocksUI.ViewModels.Base;

namespace RealBlocksUI.ViewModels
{
    public class BlockManagerViewModel : ViewModelBase
    {
        #region Backing fields

        private BindingList<InstanceDefinitionDisplayModel> _instanceDefinitions;
        private InstanceDefinitionDisplayModel _selectedItem;

        private readonly DelegateCommand<InstanceDefinitionDisplayModel> _selectDefinitionCommand;

        private readonly IInstanceDefinitionEndpoint _instanceDefinitionEndpoint;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public BlockManagerViewModel(
            uint documentSerialNumber,
            IInstanceDefinitionEndpoint instanceDefinitionEndpoint,
            IMapper mapper
            )
            : base(documentSerialNumber)
        {
            // Set backing fields
            _instanceDefinitionEndpoint = instanceDefinitionEndpoint;
            _mapper = mapper;

            // Populate Instance definitions
            LoadInstanceDefinitions();

            // Initialize commands
            _selectDefinitionCommand = new DelegateCommand<InstanceDefinitionDisplayModel>(
                (d) => true,
                (d) =>
                {
                    SelectedItem = GetSelectedItem();
                });
        }

        private void LoadInstanceDefinitions()
        {
            var definitionList = _instanceDefinitionEndpoint.GetAll();
            var definitions = _mapper.Map<List<InstanceDefinitionDisplayModel>>(definitionList);
            InstanceDefinitions = new BindingList<InstanceDefinitionDisplayModel>(definitions);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Bound list of all Instance Definitions
        /// </summary>
        public BindingList<InstanceDefinitionDisplayModel> InstanceDefinitions
        {
            get => _instanceDefinitions;
            set
            {
                _instanceDefinitions = value;
                RaisePropertyChanged(nameof(InstanceDefinitions));
            }
        }

        public InstanceDefinitionDisplayModel SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        public DelegateCommand<InstanceDefinitionDisplayModel> SelectDefinitionCommand => _selectDefinitionCommand;

        #endregion

        #region Helper methods

        private InstanceDefinitionDisplayModel GetSelectedItem()
        {
            // if there is no collection we can't do anything
            if (InstanceDefinitions.Count == 0) return null;

            // Walk the tree
            foreach (var model in InstanceDefinitions)
            {
                // early exit
                if (model.IsSelected)
                    return model;

                var selected = GetSelectedItem(model);
                if (selected != null) return selected;
            }

            return null;
        }

        private static InstanceDefinitionDisplayModel GetSelectedItem(InstanceDefinitionDisplayModel model)
        {
            if (model.IsSelected)
                return model;

            // recursion (•_•) ( •_•)>⌐■-■ (⌐■_■)
            foreach (var childModel in model.Children)
            {
                if (childModel == null)
                    continue;

                var selected = GetSelectedItem(childModel);
                if (selected == null) continue;

                return selected;
            }

            return null;
        }
        #endregion
    }
}

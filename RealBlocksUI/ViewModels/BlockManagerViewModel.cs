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

        #endregion
    }
}

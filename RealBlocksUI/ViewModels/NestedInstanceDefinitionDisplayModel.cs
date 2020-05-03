using RealBlocksUI.Library.Api;
using RealBlocksUI.Library.Models;
using RealBlocksUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksUI.ViewModels
{
    public class NestedInstanceDefinitionDisplayModel : NestedInstanceDefinitionModel, IDisplayModel
    {
        #region Display properties

        private ObservableCollection<NestedInstanceDefinitionDisplayModel> _children;

        /// <summary>
        /// A collection of all top-level children of this
        /// </summary>
        public ObservableCollection<NestedInstanceDefinitionDisplayModel> Children
        {
            get => _children;
            set
            {
                _children = value;
                RaisePropertyChanged(nameof(Children));
                RaisePropertyChanged(nameof(CanExpand));
            }
        }

        /// <summary>
        /// Indicating if the displaymodel can expand
        /// </summary>
        public bool CanExpand => this.Model.IsAssembly;

        /// <summary>
        /// Indicating if the displaymodle is currently expanded
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return this.Children.Count(d => d != null) > 0;
            }
            set
            {
                // if the ui tells us to expand...
                if (value == true)
                    // Find all children
                    this.Expand();
                // if the ui tells us to close
                else
                    this.ClearChildren();

                RaisePropertyChanged(nameof(IsExpanded));
                RaisePropertyChanged(nameof(CanExpand));
            }

        }

        private bool _isSelected;

        /// <summary>
        /// Indicating if the displaymodel is currently selected in the UI
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                RaisePropertyChanged(nameof(IsSelected));
            }
        }

        public bool HasRoot => Root != null;

        #region Methods

        /// <summary>
        /// Removes all children from this item,
        /// Adding a dummy item to show the expand item, if required
        /// </summary>
        private void ClearChildren()
        {
            // clear items
            this.Children = new ObservableCollection<NestedInstanceDefinitionDisplayModel>();

            // show the expand arrow if we are an assembly
            if (this.Model.IsAssembly)
                this.Children.Add(null);
        }

        /// <summary>
        /// Expands this instance definition to find all its nested definitions
        /// </summary>
        private void Expand()
        {
            // We can only expand assemblies
            if (!this.Model.IsAssembly) return;

            // Find all children
            // TODO: Find a way to not new this up
            var children = GetChildren();

            this.Children = new ObservableCollection<NestedInstanceDefinitionDisplayModel>(children);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets all children using a <see cref="InstanceDefinitionEndpoint"/>
        /// </summary>
        /// <returns></returns>
        private List<NestedInstanceDefinitionDisplayModel> GetChildren()
        {
            var endpoint = new NestedDefinitionEndpoint();
            // TODO: implement endpoin

            return new List<NestedInstanceDefinitionDisplayModel>();
        
        }

        #endregion

        #endregion

        #region IDisplayModel implementation

        public event PropertyChangedEventHandler PropertyChanged;

        public void Initialize()
        {
            
        }

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}

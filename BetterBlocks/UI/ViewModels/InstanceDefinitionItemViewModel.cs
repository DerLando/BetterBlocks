using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BetterBlocks.Core;
using BetterBlocks.Data;
using BetterBlocks.UI.ViewModels.Base;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace BetterBlocks.UI.ViewModels
{
    public class InstanceDefinitionItemViewModel : BaseViewModel
    {
        #region private backing fields

        private Guid _id;
        private string _name;
        private bool _isExpanded;

        #endregion

        #region Public properties

        /// <summary>
        /// Index of the <see cref="InstanceDefinition"/> in the <see cref="InstanceDefinitionTable"/>
        /// </summary>
        public Guid Id
        {
            get => _id;
            set
            {
                if (value == _id) return;

                _id = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Id)));
            } }

        /// <summary>
        /// Given Name of the <see cref="InstanceDefinition"/>
        /// </summary>
        public string Name {
            get => _name;
            set
            {
                if (value == _name) return;

                _name = value;

                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Name)));
            } }

        /// <summary>
        /// Type of the <see cref="InstanceDefinition"/>, either Assembly or Root
        /// </summary>
        public InstanceDefinitionType Type { get; set; }

        /// <summary>
        /// List of all children contained in this item
        /// </summary>
        public ObservableCollection<InstanceDefinitionItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicates if this item can be expanded
        /// </summary>
        public bool CanExpand
        {
            get => this.Type != InstanceDefinitionType.Root;
        }

        public bool IsExpanded
        {
            get { return this.Children?.Count(i => i != null) > 0; }
            set
            {
                // if the ui tells us to expand...
                if (value == true)
                    // Finds all children
                    this.Expand();
                // if the ui tells us to close
                else
                    this.ClearChildren();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Constructor

        public InstanceDefinitionItemViewModel(Guid id, string name, InstanceDefinitionType type)
        {
            // Create commands
            this.ExpandCommand = new RelayCommand(Expand);

            // Set up fields
            this.Id = id;
            this.Name = name;
            this.Type = type;

            // Set up the children as needed
            this.ClearChildren();
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Removes all children from this item,
        /// Adding a dummy item to show the expand arrow, if required
        /// </summary>
        private void ClearChildren()
        {
            // Clear items
            this.Children = new ObservableCollection<InstanceDefinitionItemViewModel>();

            // Show the expand arrow if we are not root
            if(this.Type != InstanceDefinitionType.Root)
                this.Children.Add(null);
        }

        #endregion

        private void Expand()
        {
            // We can not expand root
            if(this.Type == InstanceDefinitionType.Root)
                return;

            // Find all children
            this.Children = 
                new ObservableCollection<InstanceDefinitionItemViewModel>(BetterBlocksPlugIn
                .Instance
                .InstanceDefinitionStructure
                .GetInstanceDefinitionContents(this.Id)
                .Select(content => new InstanceDefinitionItemViewModel(content.Id, content.Name, content.Type)));
        }

    }
}

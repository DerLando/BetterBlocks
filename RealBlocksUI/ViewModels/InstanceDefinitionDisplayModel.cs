using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using RealBlocksUI.Library.Api;
using RealBlocksUI.ViewModels.Base;
using Rhino.DocObjects;
using Rhino.DocObjects.Tables;

namespace RealBlocksUI.ViewModels
{
    public class InstanceDefinitionDisplayModel : DisplayModelBase
    {
        #region Public properties

        /// <summary>
        /// Globally unique identifier of the <see cref="InstanceDefinition"/>
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Index of the <see cref="InstanceDefinition" /> in the <see cref="InstanceDefinitionTable"/>
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Given Name of the <see cref="InstanceDefinition"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of <see cref="RhinoObject"/>s that are part of this <see cref="InstanceDefinition"/>
        /// </summary>
        public int ObjectCount { get; set; }

        /// <summary>
        /// GUIDs of all objects that are part of this definition,
        /// should be of same length as ObjectCount
        /// </summary>
        public Guid[] ObjectIds { get; set; }

        /// <summary>
        /// Determines if the definition is inserted or referenced
        /// in the Document it comes from
        /// </summary>
        public bool IsInUse { get; set; }

        /// <summary>
        /// Determines if the definition is assembled from other definitions
        /// or a 'pure' definition made up only of geometry
        /// </summary>
        public bool IsAssembly { get; set; }

        // TODO: A InstanceDefinition does not store user strings,
        // TODO: If we want to keep track of those we have to query the Inserts
        // TODO: f.e.: InstanceDefinition.GetReferences(2).ForEach(o => o.GetUserStrings())

        #endregion

        #region Display properties

        private ObservableCollection<InstanceDefinitionDisplayModel> _children;

        public ObservableCollection<InstanceDefinitionDisplayModel> Children
        {
            get => _children;
            set
            {
                _children = value;
                RaisePropertyChanged(nameof(Children));
                RaisePropertyChanged(nameof(CanExpand));
            }
        }

        public bool CanExpand => IsAssembly;

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

        #endregion

        #region Constructor

        public InstanceDefinitionDisplayModel()
        {
            Initialize();
        }

        public void Initialize()
        {
            // Create commands
            this.ExpandCommand = new RelayCommand(Expand);

            // set up children as needed
            this.ClearChildren();

            RaisePropertyChanged(nameof(CanExpand));
        }

        #endregion

        #region Commands

        /// <summary>
        /// The command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Removes all children from this item,
        /// Adding a dummy item to show the expand item, if required
        /// </summary>
        private void ClearChildren()
        {
            // clear items
            this.Children = new ObservableCollection<InstanceDefinitionDisplayModel>();

            // show the expand arrow if we are an assembly
            if(this.IsAssembly)
                this.Children.Add(null);
        }

        /// <summary>
        /// Expands this instance definition to find all its nested definitions
        /// </summary>
        private void Expand()
        {
            // We can only expand assemblies
            if(!this.IsAssembly) return;

            // Find all children
            // TODO: Find a way to not new this up
            var endpoint = new InstanceDefinitionEndpoint();
            var childrenList = endpoint.GetChildren(this.Id);
            var children = RealBlocksUIPlugIn.Mapper.Map<List<InstanceDefinitionDisplayModel>>(childrenList);

            this.Children = new ObservableCollection<InstanceDefinitionDisplayModel>(children);
        }



        #endregion
    }
}

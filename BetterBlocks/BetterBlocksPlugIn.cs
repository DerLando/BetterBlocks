using BetterBlocks.Core;
using BetterBlocks.UI.Models;
using BetterBlocks.UI.Views;
using Rhino;
using Rhino.PlugIns;
using Rhino.UI;

namespace BetterBlocks
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class BetterBlocksPlugIn : Rhino.PlugIns.PlugIn
    {
        #region Public properties

        public InstanceDefinitionStructure InstanceDefinitionStructure { get; set; }

        #endregion

        public BetterBlocksPlugIn()
        {
            Instance = this;

            // Create InstanceDefinitionStructure
            //InstanceDefinitionStructure = new InstanceDefinitionStructure(RhinoDoc.ActiveDoc.InstanceDefinitions);
            //RhinoDoc.ActiveDocumentChanged += OnActiveDocChanged;
        }

        /// <summary>
        /// Handles everything we need to do when changing documents
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnActiveDocChanged(object sender, DocumentEventArgs e)
        {
            InstanceDefinitionStructure = new InstanceDefinitionStructure(e.Document.InstanceDefinitions);
        }

        ///<summary>Gets the only instance of the BetterBlocksPlugIn plug-in.</summary>
        public static BetterBlocksPlugIn Instance
        {
            get; private set;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.
        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            // make sure our panel loads nice :)
            Panels.Show += PanelsOnShow;

            return base.OnLoad(ref errorMessage);
        }

        protected override void OnShutdown()
        {
            // clear event handlers
            Panels.Show -= PanelsOnShow;

            base.OnShutdown();
        }

        private void PanelsOnShow(object sender, ShowPanelEventArgs e)
        {
            // only do something if its actually for our panel
            if (e.PanelId == BlockManagerPanel.PanelId)
            {
                // get instance of our panel
                var panel = Panels.GetPanel<BlockManagerPanel>(e.Document);

                // check if already initialized with view-model
                if (panel.IsModelInitialized) return;

                // initialize new view-model
                panel.SetBlockTreeModel(new SearchableBlockTreeModel(new BlockTreeModel(new BlockWatcher(e.Document))));
            }

            if (e.PanelId == typeof(BlockManager).GUID)
            {
                // Initialize structure
                InstanceDefinitionStructure = new InstanceDefinitionStructure(e.Document.InstanceDefinitions);
            }
        }
    }
}
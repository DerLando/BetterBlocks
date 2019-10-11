using System;
using System.Linq;
using System.Runtime.InteropServices;
using BetterBlocks.Core;
using BetterBlocks.UI.Models;
using Eto.Forms;
using Eto.Drawing;
using Rhino.DocObjects;
using Rhino.UI;
using System.Collections.Specialized;

namespace BetterBlocks.UI.Views
{
    [Guid("961A1FA0-6719-45FC-BE00-7A48706E8C1E")]
    public class BlockManagerPanel : Panel, IPanel
    {
        // Fields
        private readonly uint _document_sn;
        private SearchableBlockTreeModel _tree_model;

        // Controls
        private TreeGridView _tg_Blocks = new TreeGridView {ContextMenu = new BlockTreeContextMenu()};
        private DynamicGroup _gB_Filter = new DynamicGroup{Title = "Filter"};
        private SearchBox _sB_Search = new SearchBox();
        private DynamicGroup _gB_Preview = new DynamicGroup {Title = "Preview"};
        private ImageView _iV_Preview = new ImageView();
        private DynamicGroup _gB_Description = new DynamicGroup {Title = "Description"};
        private Label _lbl_Description = new Label();

        // Public auto-initialized properties
        public static Guid PanelId => typeof(BlockManagerPanel).GUID;
        public bool IsModelInitialized => !(_tree_model is null);

		public BlockManagerPanel(uint documentSerialNumber)
        {
            // sn field
            _document_sn = documentSerialNumber;

            // set up event handlers
            //_tg_Blocks.CellEdited += On_tg_Blocks_CellEdited;
            _tg_Blocks.SelectedRowsChanged += On_tg_Blocks_SelectedRowsChanged;
            _sB_Search.TextChanged += On_sB_Search_TextChanged;

            // set up group boxes
            _gB_Filter.Add(_sB_Search);
            _gB_Preview.Add(_iV_Preview);
            _gB_Description.Add(_lbl_Description);

            // set up columns of treegridview
            // name column
            _tg_Blocks.Columns.Add(new GridColumn
            {
                HeaderText = "Name",
                DataCell = new TextBoxCell(0),
                Editable = true
            });

            // Object count
            _tg_Blocks.Columns.Add(new GridColumn
            {
                HeaderText = "Object Count",
                DataCell = new TextBoxCell(1)
            });

            var layout = new DynamicLayout();
            layout.Padding = 10;
            layout.Spacing = new Size(5, 5);

            layout.Add(_gB_Filter.Create(layout));
            layout.Add(_tg_Blocks);
            layout.Add(_gB_Preview.Create(layout));
            layout.Add(_gB_Description.Create(layout));
            layout.Add(null);

            Content = layout;
        }

        #region Event handlers

        private void On_sB_Search_TextChanged(object sender, EventArgs e)
        {
            _tree_model.SetSearchString(_sB_Search.Text);
        }

        private void On_tg_Blocks_SelectedRowsChanged(object sender, EventArgs e)
        {
            var def = ((TreeGridItem) _tg_Blocks.SelectedItem).Tag as InstanceDefinition;

            // set preview item
            System.Drawing.Size size = _iV_Preview.Size.IsZero ? new System.Drawing.Size(200, 100) : _iV_Preview.Size.ToDrawingSize(); 
            var image = def.CreatePreviewBitmap(Settings.BlockManagerPreviewProjection,
                Settings.BlockManagerPreviewDisplayMode, size);
            _iV_Preview.Image = image.ToEto();

            // set Description
            _lbl_Description.Text = def.Description;

            // redraw
            _iV_Preview.Invalidate();
        }

        private void On_tg_Blocks_CellEdited(object sender, GridViewCellEventArgs e)
        {
            var name = ((TreeGridItem) e.Item).Values[0].ToString();
            if (string.IsNullOrEmpty(name)) return;
            var def = ((TreeGridItem)e.Item).Tag as InstanceDefinition;
            def.Name = name;
        }

        private void On_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _tg_Blocks.DataStore = _tree_model;
            _tg_Blocks.Invalidate();
        }

        #endregion

        public void SetBlockTreeModel(SearchableBlockTreeModel model)
        {
            if (!(_tree_model is null))
            {
                // discard old handler
                _tree_model.CollectionChanged -= On_CollectionChanged;
            }

            // set new model
            _tree_model = model;

            // set up event handler
            _tree_model.CollectionChanged += On_CollectionChanged;

            // model is store of treegridview
            _tg_Blocks.DataStore = _tree_model;

            // redraw()
            _tg_Blocks.Invalidate();
        }

        #region IPanel methods
        public void PanelShown(uint documentSerialNumber, ShowPanelReason reason)
        {
            // Called when the panel tab is made visible, in Mac Rhino this will happen
            // for a document panel when a new document becomes active, the previous
            // documents panel will get hidden and the new current panel will get shown.
            Rhino.RhinoApp.WriteLine($"Panel shown for document {documentSerialNumber}, this serial number {_document_sn} should be the same");
        }

        public void PanelHidden(uint documentSerialNumber, ShowPanelReason reason)
        {
            // Called when the panel tab is hidden, in Mac Rhino this will happen
            // for a document panel when a new document becomes active, the previous
            // documents panel will get hidden and the new current panel will get shown.
            Rhino.RhinoApp.WriteLine($"Panel hidden for document {documentSerialNumber}, this serial number {_document_sn} should be the same");
        }

        public void PanelClosing(uint documentSerialNumber, bool onCloseDocument)
        {
            // Called when the document or panel container is closed/destroyed
            Rhino.RhinoApp.WriteLine($"Panel closing for document {documentSerialNumber}, this serial number {_document_sn} should be the same");
        }
        #endregion IPanel methods
    }
}

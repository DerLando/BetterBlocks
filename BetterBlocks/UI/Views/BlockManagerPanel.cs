using BetterBlocks.UI.Models;
using Eto.Drawing;
using Eto.Forms;
using Rhino.DocObjects;
using Rhino.UI;
using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace BetterBlocks.UI.Views
{
    [Guid("66F8C34D-BAF6-4029-96FF-3F74F892454C")]
    public class BlockManagerPanel : Panel, IPanel
    {
        // Fields
        private readonly uint _document_sn;

        private SearchableBlockTreeModel _tree_model;

        // Controls
        private TreeGridView _tg_Blocks = new TreeGridView();

        private DynamicGroup _gB_Search = new DynamicGroup { Title = "Search" };
        private SearchBox _sB_Search = new SearchBox();
        private DynamicGroup _gB_Preview = new DynamicGroup { Title = "Preview" };
        private ImageView _iV_Preview = new ImageView();
        private DynamicGroup _gB_Description = new DynamicGroup { Title = "Description" };
        private Label _lbl_Description = new Label();
        private GroupBox _gB_UsageFilters = new GroupBox{Text = "Filters"};
        private CheckBox _cB_Root = new CheckBox{Checked = true};
        private Label _lbl_Root = new Label{Text = "Root", VerticalAlignment = VerticalAlignment.Center};
        private CheckBox _cB_Assembly = new CheckBox{Checked = true};
        private Label _lbl_Assembly = new Label{Text = "Assembly", VerticalAlignment = VerticalAlignment.Center };
        private CheckBox _cB_InUse = new CheckBox{Checked = true};
        private Label _lbl_InUse = new Label {Text = "In use", VerticalAlignment = VerticalAlignment.Center };


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
            _tg_Blocks.ColumnHeaderClick += On_tg_Blocks_ColumnHeaderClick;
            _sB_Search.TextChanged += On_sB_Search_TextChanged;
            _cB_Root.CheckedChanged += On_cB_Root_CheckedChanged;
            _cB_Assembly.CheckedChanged += On_cB_Assembly_CheckedChanged;
            _cB_InUse.CheckedChanged += On_cB_InUse_CheckedChanged;

            // Set up context menu
            _tg_Blocks.ContextMenu = new BlockTreeContextMenu(_tg_Blocks);

            // Set up tg constraints
            _tg_Blocks.Border = BorderType.Line;
            _tg_Blocks.Height = 500;
            _tg_Blocks.AllowMultipleSelection = true;
            _tg_Blocks.AllowColumnReordering = false;
            _tg_Blocks.ShowHeader = true;

            // set up group boxes
            _gB_Search.Add(_sB_Search);
            _gB_Preview.Add(_iV_Preview);
            _gB_Description.Add(_lbl_Description);
            _gB_UsageFilters.Padding = new Padding(5);

            var filterLayout = new DynamicLayout();
            filterLayout.Padding = new Padding(5);
            filterLayout.Spacing = new Size(5, 5);

            filterLayout.AddRow(new Control[] {_lbl_Root, _cB_Root});
            filterLayout.AddRow(new Control[] {_lbl_Assembly, _cB_Assembly});
            filterLayout.AddRow(new Control[]{_lbl_InUse, _cB_InUse});

            _gB_UsageFilters.Content = filterLayout;

            #region TreeGrid columns

            // set up columns of treegridview
            // name column
            _tg_Blocks.Columns.Add(new GridColumn
            {
                HeaderText = "Name",
                DataCell = new TextBoxCell(Extensions._name_index),
            });

            // root
            _tg_Blocks.Columns.Add(new GridColumn
            {
                HeaderText = "Root",
                DataCell = new TextBoxCell(Extensions._is_root_index),
            });

            // In use
            _tg_Blocks.Columns.Add(new GridColumn
            {
                HeaderText = "In use",
                DataCell = new TextBoxCell(Extensions._is_in_use_index)
            });

            // Object count
            _tg_Blocks.Columns.Add(new GridColumn
            {
                HeaderText = "Parts Count",
                DataCell = new TextBoxCell(Extensions._part_count_index)
            });

            #endregion

            var layout = new DynamicLayout();
            layout.Padding = 10;
            layout.Spacing = new Size(5, 5);

            layout.Add(_gB_Search.Create(layout));
            layout.Add(_gB_UsageFilters);
            layout.Add(_tg_Blocks);
            layout.Add(_gB_Preview.Create(layout));
            layout.Add(_gB_Description.Create(layout));
            layout.Add(null);

            Content = layout;
        }

        private void On_tg_Blocks_ColumnHeaderClick(object sender, GridColumnEventArgs e)
        {
            int sortItem = _tg_Blocks.Columns.IndexOf(e.Column);
            switch (sortItem)
            {
                case 0:
                    _tree_model.SetSortType(BlockTreeModelSortType.Name);
                    _tree_model.SortRows();
                    break;
                default:
                    break;
            }
        }

        #region Event handlers

        private void On_cB_InUse_CheckedChanged(object sender, EventArgs e)
        {
            _tree_model.SetInUseFilter(_cB_InUse.Checked.Value);
        }

        private void On_cB_Assembly_CheckedChanged(object sender, EventArgs e)
        {
            _tree_model.SetAssemblyFilter(_cB_Assembly.Checked.Value);
        }

        private void On_cB_Root_CheckedChanged(object sender, EventArgs e)
        {
            _tree_model.SetRootFilter(_cB_Root.Checked.Value);
        }

        private void On_sB_Search_TextChanged(object sender, EventArgs e)
        {
            _tree_model.SetSearchString(_sB_Search.Text);
        }

        private void On_tg_Blocks_SelectedRowsChanged(object sender, EventArgs e)
        {
            var def = ((TreeGridItem)_tg_Blocks.SelectedItem).ToInstanceDefinition();

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
            var name = ((TreeGridItem)e.Item).Values[0].ToString();
            if (string.IsNullOrEmpty(name)) return;
            var def = ((TreeGridItem)e.Item).ToInstanceDefinition();
            def.Name = name;
        }

        private void On_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _tg_Blocks.DataStore = _tree_model;
            _tg_Blocks.Invalidate();
        }

        #endregion Event handlers

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
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
        public static Guid PanelId => typeof(BlockManagerPanel).GUID;

        private readonly uint _document_sn;
        private BlockTreeModel _tree_model;
        private TreeGridView _tg_View = new TreeGridView();

        public bool IsModelInitialized => !(_tree_model is null);

		public BlockManagerPanel(uint documentSerialNumber)
        {
            _document_sn = documentSerialNumber;

            // name column
            _tg_View.Columns.Add(new GridColumn
            {
                HeaderText = "Name",
                DataCell = new TextBoxCell(0)
            });

            // Object count
            _tg_View.Columns.Add(new GridColumn
            {
                HeaderText = "Object Count",
                DataCell = new TextBoxCell(1)
            });

			Content = new StackLayout
			{
				Padding = 10,
				Items =
				{
					"Hello World!",
					// add more controls here
                    _tg_View,
				}
			};

		}

        private void On_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _tg_View.DataStore = _tree_model;
            _tg_View.Invalidate();
        }

        public void SetBlockTreeModel(BlockTreeModel model)
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
            _tg_View.DataStore = _tree_model;

            // redraw()
            _tg_View.Invalidate();
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

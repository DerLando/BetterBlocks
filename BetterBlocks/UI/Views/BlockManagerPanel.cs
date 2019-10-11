using System;
using System.Runtime.InteropServices;
using Eto.Forms;
using Eto.Drawing;
using Rhino.UI;

namespace BetterBlocks.UI.Views
{
    [Guid("961A1FA0-6719-45FC-BE00-7A48706E8C1E")]
    public partial class BlockManagerPanel : Panel, IPanel
    {
        public static Guid PanelId => typeof(BlockManagerPanel).GUID;

        private readonly uint _document_sn;

		public BlockManagerPanel(uint documentSerialNumber)
        {
            _document_sn = documentSerialNumber;

			Content = new StackLayout
			{
				Padding = 10,
				Items =
				{
					"Hello World!",
					// add more controls here
				}
			};

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

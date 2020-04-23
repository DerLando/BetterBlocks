using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Eto.Forms;
using Eto.Drawing;
using Eto.Serialization.Xaml;
using RealBlocksUI.Library.Api;
using RealBlocksUI.ViewModels;
using Rhino.UI;

namespace RealBlocksUI.Views
{
    [Guid("6A4DAF62-14FB-4458-9793-D031AB451959")]
    public class BlockManagerPanel : Panel, IPanel
    {
        private readonly uint _documentSerialNumber;

		public BlockManagerPanel(uint documentSerialNumber)
		{
			XamlReader.Load(this);

            _documentSerialNumber = documentSerialNumber;

            DataContext = new BlockManagerViewModel(
                documentSerialNumber,
                new InstanceDefinitionEndpoint(),
                RealBlocksUIPlugIn.Mapper);
        }

        public static Guid PanelId => typeof(BlockManagerPanel).GUID;

        #region IPanel methods

        public void PanelShown(uint documentSerialNumber, ShowPanelReason reason)
        {
            // Called when the panel tab is made visible, in Mac Rhino this will happen
            // for a document panel when a new document becomes active, the previous
            // documents panel will get hidden and the new current panel will get shown.
            Rhino.RhinoApp.WriteLine($"Panel shown for document {documentSerialNumber}, this serial number {_documentSerialNumber} should be the same");
        }

        public void PanelHidden(uint documentSerialNumber, ShowPanelReason reason)
        {
            // Called when the panel tab is hidden, in Mac Rhino this will happen
            // for a document panel when a new document becomes active, the previous
            // documents panel will get hidden and the new current panel will get shown.
            Rhino.RhinoApp.WriteLine($"Panel hidden for document {documentSerialNumber}, this serial number {_documentSerialNumber} should be the same");
        }

        public void PanelClosing(uint documentSerialNumber, bool onCloseDocument)
        {
            // Called when the document or panel container is closed/destroyed
            Rhino.RhinoApp.WriteLine($"Panel closing for document {documentSerialNumber}, this serial number {_documentSerialNumber} should be the same");
        }

        #endregion IPanel methods
    }
}

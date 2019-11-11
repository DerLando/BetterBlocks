using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Rhino.DocObjects;

namespace BetterBlocks.UI.Views
{
    public class SearchableGetLayerDialog : Dialog<DialogResult>
    {
        public Layer LayerResult { get; private set; }

        private TextBox tB_Search = new TextBox();
        private Label lbl_Search = new Label {Text = "Search"};
    }
}

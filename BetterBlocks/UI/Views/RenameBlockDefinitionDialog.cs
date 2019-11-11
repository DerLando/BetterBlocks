using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using Eto.Forms;

namespace BetterBlocks.UI.Views
{
    class RenameBlockDefinitionDialog : Dialog<DialogResult>
    {
        public string NewName = "";

        private Button btn_OK = new Button{Text = "OK"};
        private Button btn_Cancel = new Button{Text = "Cancel"};
        private TextBox tB_NewName = new TextBox();

        public RenameBlockDefinitionDialog()
        {
            // initialize general properties
            Padding = new Padding(5);
            Resizable = false;
            Result = DialogResult.Cancel;
            Title = "Rename Block definition";
            WindowStyle = WindowStyle.Default;

            // initialize event handlers
            btn_OK.Click += On_btn_OK_Click;
            btn_Cancel.Click += On_btn_Cancel_Click;


            // initialize layout
            var layout = new DynamicLayout();
            layout.Add(tB_NewName);
            layout.AddSeparateRow(new[] {btn_OK, btn_Cancel});
            layout.Add(null);

            // Set content, set up Enter KeyDown
            Content = layout;
            Content.KeyDown += On_Content_KeyDown;
        }

        private void On_Content_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Keys.Enter) On_btn_OK_Click(sender, new EventArgs());
        }

        private void On_btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void On_btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tB_NewName.Text)) Close();

            NewName = tB_NewName.Text;
            Result = DialogResult.Ok;
            Close();
        }
    }
}

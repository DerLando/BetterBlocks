using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using Eto.Forms;

namespace BetterBlocks.UI.Views
{
    public class GetStringDialog : Dialog<DialogResult>
    {
        public string StringResult { get; private set; }

        private Button btn_OK = new Button { Text = "OK" };
        private Button btn_Cancel = new Button { Text = "Cancel" };
        private TextBox tB_StringResult = new TextBox();

        public GetStringDialog(string title)
        {
            // initialize general properties
            Padding = new Padding(5);
            Resizable = false;
            Result = DialogResult.Cancel;
            Title = title;
            Location = new Point(Mouse.Position);
            WindowStyle = WindowStyle.Default;

            // initialize event handlers
            btn_OK.Click += On_btn_OK_Click;
            btn_Cancel.Click += On_btn_Cancel_Click;


            // initialize layout
            var layout = new DynamicLayout();
            layout.Add(tB_StringResult);
            layout.AddSeparateRow(new[] { btn_OK, btn_Cancel });
            layout.Add(null);

            // Set content, set up Enter KeyDown
            Content = layout;
            Content.KeyDown += On_Content_KeyDown;
        }

        private void On_Content_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Keys.Enter) On_btn_OK_Click(sender, new EventArgs());
        }

        private void On_btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void On_btn_OK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tB_StringResult.Text)) Close();

            StringResult = tB_StringResult.Text;
            Result = DialogResult.Ok;
            Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using CsvHelper;
using Eto.Drawing;
using Eto.Forms;

namespace BetterBlocks.UI.Views
{
    public class BlockCountDialog : Dialog
    {
        private readonly BlockCount[] _counts;

        private GridView<BlockCount> gV_Counts = new GridView<BlockCount>();
        private Button btn_CopyToClipboard = new Button{Text = "Copy to Clipboard"};
        private Button btn_SaveAsCsv = new Button{Text = "Save as CSV"};
        private Button btn_Close = new Button{Text = "Close"};

        public BlockCountDialog(IEnumerable<BlockCount> counts)
        {
            // set counts field
            _counts = counts.ToArray();

            // set up event handlers
            btn_CopyToClipboard.Click += On_btn_CopyToClipboard_Click;
            btn_SaveAsCsv.Click += On_btn_SaveAsCsv_Click;
            btn_Close.Click += On_btn_Close_Click;

            // set up columns of counts grid
            gV_Counts.Columns.Add(new GridColumn
            {
                HeaderText = "Name",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockCount, string>(b => b.Name)}
            });

            gV_Counts.Columns.Add(new GridColumn
            {
                HeaderText = "Top Level",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockCount, string>(b => b.TopLevel.ToString())}
            });

            gV_Counts.Columns.Add(new GridColumn
            {
                HeaderText = "Nested",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockCount, string>(b => b.Nested.ToString())}
            });

            gV_Counts.Columns.Add(new GridColumn
            {
                HeaderText = "Total",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockCount, string>(b => b.Total.ToString())}
            });

            gV_Counts.DataStore = _counts;
            gV_Counts.Height = 400;

            // set up layout
            var layout = new DynamicLayout();
            layout.Padding = 10;
            layout.Spacing = new Size(5, 5);

            layout.Add(gV_Counts);
            layout.AddSeparateRow(new Control[] {btn_CopyToClipboard, btn_SaveAsCsv, btn_Close});
            layout.Add(null);

            Content = layout;
        }

        private void On_btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void On_btn_SaveAsCsv_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog())
            {
                if (sfd.ShowDialog(this) == DialogResult.Ok)
                {
                    using (var writer = new StreamWriter(sfd.FileName))
                    using (var csv = new CsvWriter(writer))
                    {
                        csv.WriteRecords(_counts);
                    }
                }
            }
            Close();
        }

        private void On_btn_CopyToClipboard_Click(object sender, EventArgs e)
        {
            var tempPath = Path.GetTempFileName();
            using (var writer = new StreamWriter(tempPath))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(_counts);
            }

            var temp = File.ReadAllText(tempPath);
            Clipboard.Instance.Text = temp;
            File.Delete(tempPath);
            Close();
        }
    }
}

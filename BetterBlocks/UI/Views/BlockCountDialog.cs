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
    public class BlockCountDialog : TableDataDialog<BlockCount>
    {
        public BlockCountDialog(IEnumerable<BlockCount> counts) : base(counts) { }

        internal override void SetupDataColumns()
        {
            // set up columns of counts grid
            gV_Data.Columns.Add(new GridColumn
            {
                HeaderText = "Name",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockCount, string>(b => b.Name) }
            });

            gV_Data.Columns.Add(new GridColumn
            {
                HeaderText = "Top Level",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockCount, string>(b => b.TopLevel.ToString()) }
            });

            gV_Data.Columns.Add(new GridColumn
            {
                HeaderText = "Nested",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockCount, string>(b => b.Nested.ToString()) }
            });

            gV_Data.Columns.Add(new GridColumn
            {
                HeaderText = "Total",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockCount, string>(b => b.Total.ToString()) }
            });

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using Eto.Forms;

namespace BetterBlocks.UI.Views
{
    public class BlockUsedByDialog : TableDataDialog<BlockUsedBy>
    {
        public BlockUsedByDialog(IEnumerable<BlockUsedBy> data) : base(data) { }

        internal override void SetupDataColumns()
        {
            gV_Data.Columns.Add(new GridColumn
            {
                HeaderText = "Name",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockUsedBy, string>(b => b.Name)}
            });

            gV_Data.Columns.Add(new GridColumn
            {
                HeaderText = "Used by",
                DataCell = new TextBoxCell { Binding = Binding.Property<BlockUsedBy, string>(b => b.UsedBy)}
            });
        }
    }
}

using System;
using System.Linq;
using BetterBlocks.UI.EtoCommands;
using Eto.Forms;
using Eto.Drawing;
using Rhino.DocObjects;

namespace BetterBlocks.UI.Views
{
	public class BlockTreeContextMenu : ContextMenu
    {

		public BlockTreeContextMenu()
        {
            var definition = (InstanceDefinition)((TreeGridItem) ((TreeGridView) FindParent(typeof(TreeGridView))).SelectedItem).Tag;

            Command select = new SelectBlockInstancesByParent(definition);

            Items.Add(select.CreateMenuItem());

        }
	}
}

using System;
using Eto.Forms;
using Eto.Drawing;

namespace BetterBlocks.UI.Views
{
	public class BlockTreeContextMenu : ContextMenu
	{
		public BlockTreeContextMenu()
        {

            Items.Add(new ButtonMenuItem{Text = "Button"});
            Items.Add(new CheckMenuItem {Text = "Check"});
            Items.Add(new SeparatorMenuItem());
            Items.Add(new RadioMenuItem {Text = "Radio"});

        }
	}
}

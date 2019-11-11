using System;
using System.Linq;
using BetterBlocks.UI.EtoCommands;
using Eto.Forms;
using Eto.Drawing;
using Rhino.DocObjects;

namespace BetterBlocks.UI.Views
{
    /// <summary>
    /// Context menu for BlockTree GridView in bbManager
    /// Handles Right-Clicks to show a context-menu from which commands can be launched
    /// </summary>
	public class BlockTreeContextMenu : ContextMenu
    {

        private readonly TreeGridView _tv_parent;
        private readonly SelectBlockInstancesByParent _selectCommand = new SelectBlockInstancesByParent();
        private readonly RenameBlockDefinitionCommand _renameCommand = new RenameBlockDefinitionCommand();

		public BlockTreeContextMenu(TreeGridView parent)
        {
            _tv_parent = parent;

            _tv_parent.SelectedItemChanged += On_tv_parent_SelectedItemChanged;

            InitializeCommands();

            Items.Add(_renameCommand.CreateMenuItem());
            Items.Add(_selectCommand.CreateMenuItem());

        }

        private void On_tv_parent_SelectedItemChanged(object sender, EventArgs e)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var definition = TryGetParentSelectedInstanceDefinition();

            _renameCommand.SetDefinition(definition);
            _selectCommand.SetDefinition(definition);
        }

        private InstanceDefinition TryGetParentSelectedInstanceDefinition()
        {
            if (_tv_parent.SelectedItem is null) return null;
            return (InstanceDefinition) ((TreeGridItem) _tv_parent.SelectedItem).Tag;
        }

    }
}

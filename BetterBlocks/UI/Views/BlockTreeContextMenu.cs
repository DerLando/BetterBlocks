using System;
using System.Collections.Generic;
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
        private readonly ChangeBlockDefinitionGeometryLayer _changeLayerCommand = new ChangeBlockDefinitionGeometryLayer();

		public BlockTreeContextMenu(TreeGridView parent)
        {
            _tv_parent = parent;

            _tv_parent.SelectedRowsChanged += On_tv_parent_SelectedItemChanged;

            InitializeCommands();

            Items.Add(_renameCommand.CreateMenuItem());
            Items.Add(_selectCommand.CreateMenuItem());
            Items.Add(_changeLayerCommand.CreateMenuItem());

        }

        private void On_tv_parent_SelectedItemChanged(object sender, EventArgs e)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var definitions = TryGetParentSelectedInstanceDefinitions();

            _renameCommand.SetDefinition(definitions);
            _selectCommand.SetDefinition(definitions);
            _changeLayerCommand.SetDefinition(definitions);
        }

        private InstanceDefinition[] TryGetParentSelectedInstanceDefinitions()
        {
            if (_tv_parent.SelectedItem is null) return null;
            int selCount = _tv_parent.SelectedItems.Count();
            InstanceDefinition[] definitions = new InstanceDefinition[selCount];

            var selected = _tv_parent.SelectedItems.ToArray();

            for (int i = 0; i < selCount; i++)
            {
                definitions[i] = (InstanceDefinition) ((TreeGridItem) selected[i]).Tag;
            }

            return definitions;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using BetterBlocks.Core;
using BetterBlocks.UI.EtoCommands;
using BetterBlocks.UI.Models;
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
        private readonly DeleteInstanceDefinition _deleteCommand = new DeleteInstanceDefinition();
        private readonly CountBlockInstances _countCommand = new CountBlockInstances();
        private readonly UsedByBlockDefinitions _usedByCommand = new UsedByBlockDefinitions();
        private readonly ExportNestedBlock _exportCommand = new ExportNestedBlock();

		public BlockTreeContextMenu(TreeGridView parent)
        {
            _tv_parent = parent;

            _tv_parent.SelectedRowsChanged += On_tv_parent_SelectedItemChanged;

            InitializeCommands();

            // Selection
            Items.Add(_selectCommand.CreateMenuItem());
            Items.Add(new SeparatorMenuItem());

            // Modify Blocks
            Items.Add(_renameCommand.CreateMenuItem());
            Items.Add(_deleteCommand.CreateMenuItem());
            Items.Add(_changeLayerCommand.CreateMenuItem());
            Items.Add(new SeparatorMenuItem());

            // Analyze Blocks
            Items.Add(_countCommand.CreateMenuItem());
            Items.Add(_usedByCommand.CreateMenuItem());
            Items.Add(new SeparatorMenuItem());

            // Export
            Items.Add(_exportCommand.CreateMenuItem());
        }

        private void On_tv_parent_SelectedItemChanged(object sender, EventArgs e)
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            var nested = TryGetParentSelectedNestedBlocks();
            var definitions = new InstanceDefinition[0];
            if (nested != null)
            {
                definitions = (from block in nested select block.Definition).ToArray();
            }

            _renameCommand.SetDefinition(definitions);
            _selectCommand.SetDefinition(definitions);
            _changeLayerCommand.SetDefinition(definitions);
            _deleteCommand.SetDefinition(definitions);
            _countCommand.SetDefinition(definitions);
            _usedByCommand.SetDefinition(definitions);
            _exportCommand.SetDefinition(definitions);

            _renameCommand.SetNested(nested);
            _selectCommand.SetNested(nested);
            _changeLayerCommand.SetNested(nested);
            _deleteCommand.SetNested(nested);
            _countCommand.SetNested(nested);
            _usedByCommand.SetNested(nested);
            _exportCommand.SetNested(nested);
        }

        private InstanceDefinition[] TryGetParentSelectedInstanceDefinitions()
        {
            if (_tv_parent.SelectedItem is null) return null;
            int selCount = _tv_parent.SelectedItems.Count();
            InstanceDefinition[] definitions = new InstanceDefinition[selCount];

            var selected = _tv_parent.SelectedItems.ToArray();

            for (int i = 0; i < selCount; i++)
            {
                definitions[i] = ((TreeGridItem) selected[i]).ToInstanceDefinition();
            }

            return definitions;
        }

        private NestedBlock[] TryGetParentSelectedNestedBlocks()
        {
            if (_tv_parent.SelectedItem is null) return null;
            int selCount = _tv_parent.SelectedItems.Count();
            NestedBlock[] nested = new NestedBlock[selCount];

            var selected = _tv_parent.SelectedItems.ToArray();

            for (int i = 0; i < selCount; i++)
            {
                nested[i] = ((TreeGridItem)selected[i]).ToNestedBlock();
            }

            return nested;
        }

    }
}

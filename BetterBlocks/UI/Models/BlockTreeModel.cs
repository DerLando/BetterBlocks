using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using Eto.Forms;
using Rhino.DocObjects;

namespace BetterBlocks.UI.Models
{
    public class BlockTreeModel : TreeGridItemCollection
    {
        private readonly BlockWatcher _block_watcher;

        public BlockTreeModel(BlockWatcher watcher)
        {
            _block_watcher = watcher;
            InitializeCollection();

            _block_watcher.CollectionChanged += OnCollectionChanged;
        }

        private void OnCollectionChanged(object sender, EventArgs e)
        {
            Clear();
            InitializeCollection();
        }

        private void InitializeCollection()
        {
            AddRange(from nested in _block_watcher.NestedBlocks select nested.ToTreeGridItem());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Drawing;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace BetterBlocks.Core
{
    /// <summary>
    /// Helper class to simplify structure of nested blocks.
    /// </summary>
    public class NestedBlock : List<NestedBlock>
    {
        public InstanceDefinition Definition { get; private set; }
        public NestedBlock Parent { get; private set; } // Can be null!
        public ChildBlockInsertionParameters RelativeInsertion { get; private set; }

        public System.Drawing.Bitmap PreviewImage { get; set; }
        public bool HasParent => Parent != null;

        /// <summary>
        /// Standard constructed for a nested block instance.
        /// This will recursively search the given rhinodoc and build
        /// a nested list of nested blocks...
        /// </summary>
        /// <param name="def"></param>
        public NestedBlock(InstanceDefinition def, NestedBlock parent = null, ChildBlockInsertionParameters relativeInsertion = null)
        {
            // store head (original definition)
            Definition = def;
            Parent = parent;
            RelativeInsertion = relativeInsertion != null ? relativeInsertion : RelativeInsertion =
                ChildBlockInsertionParameters.Identity;

            var partDefinitions = def.GetPartDefinitions();
            var partXforms = def.GetPartRelativeXforms();
            foreach (var element in partDefinitions.Zip(partXforms, (definition, xform) => new {definition, xform}))
            {
                // recursion (•_•) ( •_•)>⌐■-■ (⌐■_■)
                Add(new NestedBlock(element.definition, this, element.xform));
            }
        }

        public List<List<ReferencedBlock>> GetRootTree()
        {
            var rootTree = new List<List<ReferencedBlock>>();
            _fillRootTree(ref rootTree);
            return rootTree;
        }


        /// <summary>
        /// Gets all children of the nested block which are root meaning,
        /// determined only by geometry and not by other block definitions
        /// </summary>
        /// <returns></returns>
        private void _fillRootTree(ref List<List<ReferencedBlock>> rootTree, int recursionDepth = 0)
        {
            // On first iteration just fill with self
            if (rootTree.Count == 0)
            {
                rootTree = new List<List<ReferencedBlock>>();
                rootTree.Add(new List<ReferencedBlock>());
                rootTree[0].Add(new ReferencedBlock(this));
            }

            else
            {
                // Check if there is already a list present for the current recursion depth
                if (rootTree.Count > recursionDepth)
                {
                    // List is present, add yourself to it
                    rootTree[recursionDepth].Add(new ReferencedBlock(this));
                }
                else
                {
                    // No list present, add new List with self as content
                    rootTree.Add(new List<ReferencedBlock>());
                    rootTree[recursionDepth].Add(new ReferencedBlock(this));
                }
            }

            // if the definition is root, we don't need to check any children
            if (Definition.IsRoot()) return;

            // Recurse over children and add to rootTree
            foreach (var nested in this)
            {
                nested._fillRootTree(ref rootTree, recursionDepth + 1);
            }
        }

        public List<ReferencedBlock>[] GetRootTreeByRootDepth()
        {
            var rootTree = GetRootTree();

            for (int i = 0; i < rootTree.Count - 1; i++)
            {
                for (int j = rootTree[i].Count - 1; j >= 0; j--)
                {
                    if (rootTree[i][j].Definition.IsRoot())
                    {
                        var definition = rootTree[i][j];
                        rootTree[i].RemoveAt(j);
                        rootTree[i + 1].Add(definition);
                    }
                }
            }

            return rootTree.ToArray();
        }

        public int GetActiveIndex(NestedBlock child)
        {
            if (child.Equals(this)) return 0;
            return this.IndexOf(child);
        }

        public void CreatePreviewImage(RhinoDoc doc)
        {
            PreviewImage = BlockPreview.GeneratePreview(this, doc);
        }
    }
}

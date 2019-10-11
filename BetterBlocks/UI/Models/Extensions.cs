using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using Eto.Forms;
using Rhino.DocObjects;

namespace BetterBlocks.UI.Models
{
    public static class Extensions
    {
        public static string[] ToValues(this InstanceDefinition definition)
        {
            return new[]
            {
                definition.Name,
                definition.ObjectCount.ToString(),
            };
        }

        public static TreeGridItem ToTreeGridItem(this InstanceDefinition definition)
        {
            return new TreeGridItem
            {
                Tag = definition,
                Values = definition.ToValues()
            };
        }

        public static TreeGridItem ToTreeGridItem(this NestedBlock nested)
        {
            TreeGridItem item = nested.Definition.ToTreeGridItem();

            foreach (var nestedBlock in nested)
            {
                item.Children.Add(nestedBlock.ToTreeGridItem());
            }

            return item;
        }
    }
}

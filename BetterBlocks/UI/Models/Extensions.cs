using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                definition.IsRoot().ToString(),
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

        // If you want to implement both "*" and "?"
        public static String WildCardToRegular(this String value)
        {
            if (String.IsNullOrEmpty(value)) return String.Empty;
            return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
        }

        public static Size ToDrawingSize(this Eto.Drawing.Size size)
        {
            return new Size(size.Width, size.Height);
        }
    }
}

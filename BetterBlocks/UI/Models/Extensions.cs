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
        public static int _name_index = 0;
        public static int _is_root_index = 1;
        public static int _is_in_use_index = 2;
        public static int _part_count_index = 3;

        #region Value conversions

        /// <summary>
        /// Converts an instance definition to an array of strings,
        /// which can be used as values for a treeGridItem.
        /// <see cref="TreeGridItem"/>
        /// <seealso cref="TreeGridItemCollection"/>
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static string[] ToValues(this InstanceDefinition definition)
        {
            string[] values = new string[4];
            values[_name_index] = definition.Name;
            values[_is_root_index] = definition.IsRoot().ToString();
            values[_is_in_use_index] = definition.IsInUse().ToString();
            values[_part_count_index] = definition.PartCount().ToString();
            return values;
        }

        public static bool ToRoot(this object[] values)
        {
            return bool.Parse((string)values[_is_root_index]);
        }

        public static bool ToAssembly(this object[] values)
        {
            return !bool.Parse((string)values[_is_root_index]);
        }

        public static bool ToInUse(this object[] values)
        {
            return bool.Parse((string)values[_is_in_use_index]);
        }

        #endregion

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
            //TreeGridItem item = nested.Definition.ToTreeGridItem();
            TreeGridItem item = new TreeGridItem
            {
                Tag = nested,
                Values = nested.Definition.ToValues()
            };

            foreach (var nestedBlock in nested)
            {
                item.Children.Add(nestedBlock.ToTreeGridItem());
            }

            return item;
        }

        public static NestedBlock ToNestedBlock(this TreeGridItem item)
        {
            return (NestedBlock) item.Tag;
        }

        public static InstanceDefinition ToInstanceDefinition(this TreeGridItem item)
        {
            return item.ToNestedBlock().Definition;
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

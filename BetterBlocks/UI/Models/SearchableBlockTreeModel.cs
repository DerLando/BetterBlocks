using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Eto.Forms;

namespace BetterBlocks.UI.Models
{
    public class SearchableBlockTreeModel : TreeGridItemCollection
    {
        private readonly TreeGridItemCollection _original_collection;

        private string _search_string = "";

        public SearchableBlockTreeModel(TreeGridItemCollection originalCollection)
        {
            _original_collection = originalCollection;
            AddRange(_original_collection);
        }

        public void SetSearchString(string s)
        {
            _search_string = s.WildCardToRegular();
            Filter();
        }

        private void Filter()
        {
            Clear();

            if (string.IsNullOrEmpty(_search_string))
            {
                AddRange(_original_collection);
            }
            else
            {
                AddRange(from item in _original_collection
                    where Regex.IsMatch(((TreeGridItem)item).Values[0].ToString(), _search_string)
                    select item);
            }
        }
    }
}

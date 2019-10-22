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

        private bool _filter_root = true;
        private bool _filter_assembly = true;
        private bool _filter_in_use = true;

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

        public void SetRootFilter(bool filter)
        {
            _filter_root = filter;
            Filter();
        }

        public void SetAssemblyFilter(bool filter)
        {
            _filter_assembly = filter;
            Filter();
        }

        public void SetInUseFilter(bool filter)
        {
            _filter_in_use = filter;
            Filter();
        }

        private void Filter()
        {
            Clear();

            if (string.IsNullOrEmpty(_search_string) && (_filter_root && _filter_assembly && _filter_in_use))
            {
                AddRange(_original_collection);
            }
            else
            {
                var filtered = from item in _original_collection
                    where Regex.IsMatch(((TreeGridItem) item).Values[0].ToString(), _search_string)
                    select item;
                List<TreeGridItem> items = new List<TreeGridItem>();

                if (_filter_root && _filter_assembly && _filter_in_use)
                {
                    AddRange(filtered);
                    return;
                }

                foreach (var obj in filtered)
                {
                    var item = obj as TreeGridItem;
                    if (item is null) throw new ArgumentException("Invalid cast for treegriditem");

                    // filter
                    if (_filter_root && _filter_assembly)
                    {
                        if (item.Values.ToInUse() & !_filter_in_use) continue;
                        items.Add(item);
                    }
                    else
                    {
                        if (item.Values.ToRoot() & !_filter_root) continue;
                        if (item.Values.ToAssembly() & !_filter_assembly) continue;
                        if (item.Values.ToInUse() == _filter_in_use) items.Add(item);

                    }

                }

                AddRange(items);
            }
        }
    }
}

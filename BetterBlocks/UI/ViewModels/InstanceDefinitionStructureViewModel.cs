using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Data;

namespace BetterBlocks.UI.ViewModels
{
    public class InstanceDefinitionStructureViewModel : Rhino.UI.ViewModel
    {
        private uint _docSn;

        public ObservableCollection<InstanceDefinitionItemViewModel> Items { get; set; }


        public InstanceDefinitionStructureViewModel(uint docSn)
        {
            this._docSn = docSn;

            this.Items = new ObservableCollection<InstanceDefinitionItemViewModel>(
                BetterBlocksPlugIn
                    .Instance
                    .InstanceDefinitionStructure
                    .GetAssemblies()
                    .Select(assembly => new InstanceDefinitionItemViewModel(assembly.Id, assembly.Name, assembly.Type)
                    )
                );
        }
    }
}

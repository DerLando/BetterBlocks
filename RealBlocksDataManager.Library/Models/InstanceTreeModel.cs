using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksDataManager.Library.Models
{
    /// <summary>
    /// An abstraction around the <see cref="InstanceDefinitionTable"/>
    /// Modelling a nested tree like structure of all instance definitions
    /// and their children
    /// </summary>
    public class InstanceTreeModel
    {
        public NestedInstanceDefinitionModel[] Instances { get; set; }
    }
}

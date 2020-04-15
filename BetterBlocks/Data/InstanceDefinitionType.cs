using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBlocks.Data
{
    /// <summary>
    /// The possible types a <see cref="InstanceDefinitionItem"/> can have
    /// </summary>
    public enum InstanceDefinitionType
    {
        /// <summary>
        /// An assembly is an Instance that has other Instances nested inside it
        /// </summary>
        Assembly,

        /// <summary>
        /// A root instance contains only pure geometry
        /// </summary>
        Root
    }
}

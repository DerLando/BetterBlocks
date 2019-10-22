using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;

namespace BetterBlocks.Core
{
    /// <summary>
    /// Helper class to simplify structure of nested blocks.
    /// </summary>
    public class NestedBlock : List<NestedBlock>
    {
        public InstanceDefinition Definition { get; private set; }

        /// <summary>
        /// Standard constructed for a nested block instance.
        /// This will recursively search the given rhinodoc and build
        /// a nested list of nested blocks...
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="def"></param>
        public NestedBlock(RhinoDoc doc, InstanceDefinition def)
        {
            // store head (original definition)
            Definition = def;

            // iterate over all definitions in the given rhinodoc
            foreach (var instanceDefinition in def.GetPartDefinitions())
            {
                // skip yourself, or face an overflow :/
                //if (instanceDefinition.Equals(Definition)) continue; ;

                // result of 1 means nested one level inside of the head definition
                if (true)//Definition.UsesDefinition(instanceDefinition.Index) == 1)
                {
                    // recursion (•_•) ( •_•)>⌐■-■ (⌐■_■)
                    Add(new NestedBlock(doc, instanceDefinition));
                }
            }
        }
    }
}

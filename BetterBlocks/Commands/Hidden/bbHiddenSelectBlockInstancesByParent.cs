using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;

namespace BetterBlocks.Commands.Hidden
{
    [Guid("15657BD2-CB89-4D43-832F-7A127AB7B830"), CommandStyle(Style.Hidden)]
    public class bbHiddenSelectBlockInstancesByParent : Command
    {
        private InstanceDefinition[] _definitions;

        static bbHiddenSelectBlockInstancesByParent _instance;
        public bbHiddenSelectBlockInstancesByParent()
        {
            _instance = this;
        }

        ///<summary>The only instance of the SelectBlockInstancesByParent command.</summary>
        public static bbHiddenSelectBlockInstancesByParent Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "bbHiddenSelectBlockInstancesByParent"; }
        }

        public void SetDefinition(IEnumerable<InstanceDefinition> definitons)
        {
            _definitions = definitons.ToArray();
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            foreach (var definition in _definitions)
            {
                // get all top-level references of the instance definition
                var references = definition.GetReferences(0);

                // empty array, show error and return
                if (references.Length == 0)
                {
                    RhinoApp.WriteLine($"No top-level references of {definition.Name}!");
                }
                else
                {
                    RhinoApp.WriteLine($"Found {references.Length} top-level references of {definition.Name}");
                    // select all instances
                    doc.Objects.Select(from iObj in references select iObj.Id);
                }

            }

            // redraw
            doc.Views.Redraw();

            return Result.Success;
        }
    }
}
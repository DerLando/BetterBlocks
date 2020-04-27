using RealBlocksDataManager.Library.DataAccess;
using RealBlocksDataManager.Library.Extensions;
using RealBlocksDataManager.Library.Internal.DataAccess;
using RealBlocksDataManager.Library.Models;
using Rhino;
using Rhino.DocObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksDataManager.Library.Factories
{
    public static class InstanceTreeFactory
    {
        public static InstanceTreeModel Create()
        {
            // Get active doc
            var doc = RhinoDoc.ActiveDoc;

            // get instance table
            var table = doc.InstanceDefinitions;

            // create empty list to store nested definitions
            var definitions = new List<NestedInstanceDefinitionModel>();

            // iterate over table, ignoring deleted definitions
            foreach (var item in table.GetList(true))
            {
                // create InstanceDefinitionModel
                var definition = InstanceDefinitionModelFactory.Create(item);

                // if the model is not an assembly, it has no children
                if (!definition.IsAssembly)
                {
                    var nestedModel = NestedInstanceDefinitionModelFactory.Create(definition);
                    definitions.Add(nestedModel);
                    continue;
                }

                // traverse children
                definitions.AddRange(CreateNested(definition));
            }

            return new InstanceTreeModel
            {
                Instances = definitions.ToArray()
            };
        }

        private static List<NestedInstanceDefinitionModel> CreateNested(InstanceDefinitionModel root)
        {
            var models = new List<NestedInstanceDefinitionModel>();
            var access = new InstanceTableDataAccess();
            var definition = access.GetDefinition(root.Id);

            int i = 0;
            foreach (var part in definition.GetPartInstances())
            {
                // create model for current part
                var partModel = InstanceDefinitionModelFactory.Create(part.InstanceDefinition);

                // test if not an assembly
                if (!partModel.IsAssembly)
                {
                    models.Add(NestedInstanceDefinitionModelFactory.Create(partModel, root, root, i));
                }

                // if it is an assembly we have to go deeper
                models.AddRange(
                    CreateNested(partModel));

                i += 1;
            }

            return models;
        }
    }
}

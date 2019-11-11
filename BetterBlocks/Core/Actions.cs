using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace BetterBlocks.Core
{
    public static class Actions
    {
        public static bool RenameInstanceDefinition(InstanceDefinition definition, RhinoDoc doc, string newName)
        {
            return doc.InstanceDefinitions.Modify(definition, newName, definition.Description, false);
        }

        public static bool ChangeInstanceDefinitionGeometryLayer(InstanceDefinition definition, RhinoDoc doc, Layer layer)
        {
            if (!doc.Layers.Contains(layer))
            {
                RhinoApp.WriteLine($"No Layer {layer} in document!");
                return false;
            }

            var rhObjects = definition.GetObjects();
            var geometries = new GeometryBase[rhObjects.Length];
            var attributes = new ObjectAttributes[rhObjects.Length];

            for (int i = 0; i < rhObjects.Length; i++)
            {
                geometries[i] = rhObjects[i].Geometry;
                attributes[i] = rhObjects[i].Attributes;
                attributes[i].LayerIndex = layer.Index;
            }

            return doc.InstanceDefinitions.ModifyGeometry(definition.Index, geometries, attributes);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace BetterBlocks.Core
{
    public static class Actions
    {
        public static bool RenameInstanceDefinition(InstanceDefinition definition, RhinoDoc doc, string newName)
        {
            var serial = doc.BeginUndoRecord("Rename Block definition");
            var success = doc.InstanceDefinitions.Modify(definition, newName, definition.Description, false);
            doc.EndUndoRecord(serial);
            return success;
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

            var serial = doc.BeginUndoRecord("Change Block geometry layer");
            var success = doc.InstanceDefinitions.ModifyGeometry(definition.Index, geometries, attributes);
            doc.EndUndoRecord(serial);
            return success;
        }

        public static bool DeleteInstanceDefinition(InstanceDefinition definition, RhinoDoc doc)
        {
            var serial = doc.BeginUndoRecord("Delete Block definition");
            var success = doc.InstanceDefinitions.Delete(definition);
            doc.EndUndoRecord(serial);
            return success;
        }
    }
}

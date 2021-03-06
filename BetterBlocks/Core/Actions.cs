﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.FileIO;
using Rhino.Geometry;

namespace BetterBlocks.Core
{
    public static class Actions
    {
        public static bool RenameInstanceDefinitions(IEnumerable<InstanceDefinition> definitions, RhinoDoc doc,
            string newName)
        {
            var definitionsArray = definitions.ToArray();
            if (definitionsArray.Length == 1)
            {
                var serial = doc.BeginUndoRecord("Changing Block name");
                var success = Actions._renameInstanceDefinition(definitionsArray[0], doc, newName);
                doc.EndUndoRecord(serial);
                return success;
            }
            else
            {
                var totalSuccess = true;
                var serial = doc.BeginUndoRecord("Changing Block names");
                for (int i = 0; i < definitionsArray.Length; i++)
                {
                    var formatted =
                        $"{newName}{Settings.CountDelimiter}{i.ToString().PadLeft(Settings.PadCount, '0')}";
                    if (!Actions._renameInstanceDefinition(definitionsArray[i], doc, formatted))
                    {
                        totalSuccess = false;
                    }
                }

                doc.EndUndoRecord(serial);
                return totalSuccess;
            }
        }

        private static bool _renameInstanceDefinition(InstanceDefinition definition, RhinoDoc doc, string newName)
        {
            var serial = doc.BeginUndoRecord("Rename Block definition");
            var success = doc.InstanceDefinitions.Modify(definition, newName, definition.Description, false);
            doc.EndUndoRecord(serial);
            return success;
        }

        public static bool ChangeInstanceDefinitionsGeometryLayer(IEnumerable<InstanceDefinition> definitions,
            RhinoDoc doc, Layer layer)
        {
            if (!doc.Layers.Contains(layer))
            {
                RhinoApp.WriteLine($"No Layer {layer} in document!");
                return false;
            }

            var serial = doc.BeginUndoRecord("Changing Block geometry layers");
            var totalSuccess = true;
            foreach (var definition in definitions)
            {
                if (Actions._changeInstanceDefinitionGeometryLayer(definition, doc, layer))
                {
                    RhinoApp.WriteLine($"Changed geometry of {definition} to layer {layer}!");
                }
                else
                {
                    RhinoApp.WriteLine($"Could not change geometry layers of {definition} to layer {layer}!");
                    totalSuccess = false;
                }
            }

            doc.EndUndoRecord(serial);
            return totalSuccess;
        }

        private static bool _changeInstanceDefinitionGeometryLayer(InstanceDefinition definition, RhinoDoc doc, Layer layer)
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

        public static bool DeleteInstanceDefinitions(IEnumerable<InstanceDefinition> definitions, RhinoDoc doc)
        {
            var serial = doc.BeginUndoRecord("Deleting Blocks");
            var totalSuccess = true;
            foreach (var definition in definitions)
            {
                if (Actions._deleteInstanceDefinition(definition, doc))
                {
                    RhinoApp.WriteLine($"Deleted {definition}!");
                }
                else
                {
                    RhinoApp.WriteLine($"Could not delete {definition}");
                    totalSuccess = false;
                }
            }

            doc.EndUndoRecord(serial);
            return totalSuccess;
        }

        private static bool _deleteInstanceDefinition(InstanceDefinition definition, RhinoDoc doc)
        {
            return doc.InstanceDefinitions.Delete(definition);
        }

        public static bool CountInstanceDefinitions(IEnumerable<InstanceDefinition> definitions, out IEnumerable<BlockCount> counts)
        {
            counts = from definition in definitions.Distinct() select new BlockCount(definition);
            return true;
        }

        public static bool ExportNestedBlock(NestedBlock nested, string filePath)
        {
            var doc = new File3dm();

            // Add all instance definitions
            foreach (var references in nested.GetRootTreeByRootDepth().Reverse())
            {
                var nameIndexDict = new Dictionary<string, int>();
                foreach (var referencedBlock in references)
                {
                    if (!nameIndexDict.ContainsKey(referencedBlock.Definition.Name))
                    {
                        var objs = referencedBlock.Definition.GetObjects();
                        var geos = new GeometryBase[objs.Length];
                        var attrs = new ObjectAttributes[objs.Length];
                        for (int i = 0; i < objs.Length; i++)
                        {
                            geos[i] = objs[i].Geometry;
                            attrs[i] = objs[i].Attributes;
                        }

                        var index = doc.AllInstanceDefinitions.Add(referencedBlock.Definition.Name, nested.Definition.Description,
                            Point3d.Origin, geos, attrs);
                        nameIndexDict.Add(referencedBlock.Definition.Name, index);
                    }

                    doc.Objects.AddInstanceObject(nameIndexDict[referencedBlock.Definition.Name],
                        referencedBlock.BlockInsertionParameters.InstanceXform);

                }
            }

            return doc.Write(filePath, RhinoApp.ExeVersion);
        }

    }
}

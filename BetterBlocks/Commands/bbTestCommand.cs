using System;
using BetterBlocks.Core;
using Rhino;
using Rhino.Commands;

namespace BetterBlocks.Commands
{
    public class bbTestCommand : Command
    {
        static bbTestCommand _instance;
        public bbTestCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the bbTestCommand command.</summary>
        public static bbTestCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "bbTestCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            //var preview = new BlockPreview(doc.InstanceDefinitions[0]);

            //var image = preview.Preview;

            int layerIndex = -1;
            bool _ = false;
            if (!Rhino.UI.Dialogs.ShowSelectLayerDialog(ref layerIndex, "Layer to change to", true, true, ref _))
            {
                RhinoApp.WriteLine($"No valid Layer selected!");
                return Result.Failure;
            }

            var layer = doc.Layers[layerIndex];
            if (layer is null)
            {
                RhinoApp.WriteLine($"No valid Layer selected!");
                return Result.Failure;
            }

            foreach (var docInstanceDefinition in doc.InstanceDefinitions)
            {
                if (Actions.ChangeInstanceDefinitionGeometryLayer(docInstanceDefinition, doc, layer))
                {
                    RhinoApp.WriteLine($"Changed geometry of {docInstanceDefinition} to layer {layer}!");
                }
            }
            return Result.Success;
        }
    }
}
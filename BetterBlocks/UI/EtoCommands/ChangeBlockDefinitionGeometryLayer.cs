using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using BetterBlocks.UI.Views;
using Eto.Drawing;
using Eto.Forms;
using Rhino;

namespace BetterBlocks.UI.EtoCommands
{
    public class ChangeBlockDefinitionGeometryLayer : InstanceDefinitionCommandBase
    {
        public ChangeBlockDefinitionGeometryLayer()
        {
            MenuText = "Change Layers";
            ToolTip = "Changes the layer of all geometry of this Block";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            var doc = RhinoDoc.ActiveDoc;

            int layerIndex = -1;
            bool _ = false;
            if (!Rhino.UI.Dialogs.ShowSelectLayerDialog(ref layerIndex, "Layer to change to", true, true, ref _))
            {
                RhinoApp.WriteLine($"No valid Layer selected!");
                return;
            }

            var layer = doc.Layers[layerIndex];
            if (layer is null)
            {
                RhinoApp.WriteLine($"No valid Layer selected!");
                return;
            }

            var modified = false;
            foreach (var definition in _definitions)
            {
                if (!Actions.ChangeInstanceDefinitionGeometryLayer(definition, doc, layer))
                {
                    RhinoApp.WriteLine($"Could not change geometry layer for {definition}");
                }
                else
                {
                    RhinoApp.WriteLine($"Changed all geometry of {definition} to layer {layer}");
                    modified = true;
                }
            }

            if(modified) doc.Modified = true;
            
        }
    }
}

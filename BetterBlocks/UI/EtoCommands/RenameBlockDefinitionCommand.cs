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
using Rhino.Input.Custom;

namespace BetterBlocks.UI.EtoCommands
{
    public class RenameBlockDefinitionCommand : InstanceDefinitionCommandBase
    {
        public RenameBlockDefinitionCommand()
        {
            MenuText = "Rename";
            ToolTip = "Renames the selected Block definition";
        }

        protected override void OnExecuted(EventArgs e)
        {
            base.OnExecuted(e);

            var renameDialog = new GetStringDialog("Rename Block");

            if (renameDialog.ShowModal() == DialogResult.Ok)
            {
                var doc = RhinoDoc.ActiveDoc;
                bool modified = false;

                if (_definitions.Length == 0)
                {
                    if (!Actions.RenameInstanceDefinition(_definitions[0], doc, renameDialog.StringResult))
                    {
                        RhinoApp.WriteLine($"Could not rename {_definitions[0].Name} to {renameDialog.StringResult}!");
                        return;
                    }

                    modified = true;
                }
                else
                {
                    for (int i = 0; i < _definitions.Length; i++)
                    {
                        var newName =
                            $"{renameDialog.StringResult}{Settings.CountDelimiter}{i.ToString().PadLeft(Settings.PadCount, '0')}";
                        if (!Actions.RenameInstanceDefinition(_definitions[i], doc, newName))
                        {
                            RhinoApp.WriteLine($"Could not rename {_definitions[i].Name} to {renameDialog.StringResult}!");
                        }
                        else
                        {
                            modified = true;
                        }
                    }
                    foreach (var definition in _definitions)
                    {
                    }
                }

                if (modified) doc.Modified = true;
            }
        }
    }
}

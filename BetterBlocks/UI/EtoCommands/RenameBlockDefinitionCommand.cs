﻿using System;
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

            var renameDialog = new RenameBlockDefinitionDialog
            {
                Location = new Point(Mouse.Position)
            };

            if (renameDialog.ShowModal() == DialogResult.Ok)
            {
                var doc = RhinoDoc.ActiveDoc;
                if (!Actions.RenameInstanceDefinition(_definition, doc, renameDialog.NewName))
                {
                    RhinoApp.WriteLine($"Could not rename {_definition.Name} to {renameDialog.NewName}!");
                    return;
                }

                doc.Modified = true;
            }
        }
    }
}

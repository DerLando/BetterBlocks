using System;
using System.Collections.Generic;
using BetterBlocks.Core;
using BetterBlocks.UI.Models;
using BetterBlocks.UI.Views;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.UI;

namespace BetterBlocks.Commands
{
    public class bbShowManager : Command
    {
        public bbShowManager()
        {
            // register block manager panel
            Panels.RegisterPanel(PlugIn, typeof(UI.Views.BlockManagerPanel), "bbManager", null);

            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static bbShowManager Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "bbShowManager"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            var panelId = UI.Views.BlockManagerPanel.PanelId;
            var visible = Panels.IsPanelVisible(panelId);

            var prompt = (visible)
                ? "Layer Panel is visible"
                : "Layer Panel is hidden";

            RhinoApp.WriteLine(prompt);

            // toggle visible
            if (!visible)
            {
                Panels.OpenPanel(panelId);
            }
            else Panels.ClosePanel(panelId);

            return Result.Success;
        }
    }
}

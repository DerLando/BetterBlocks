using System;
using RealBlocksUI.Views;
using Rhino;
using Rhino.Commands;
using Rhino.UI;

namespace RealBlocksUI.Commands
{
    public class rbEtoManager : Command
    {
        static rbEtoManager _instance;
        public rbEtoManager()
        {
            // register block manager panel
            Panels.RegisterPanel(PlugIn, typeof(BlockManagerPanel), "bbManager", null);

            _instance = this;
        }

        ///<summary>The only instance of the rbEtoManager command.</summary>
        public static rbEtoManager Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "rbEtoManager"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            var panelId = BlockManagerPanel.PanelId;
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
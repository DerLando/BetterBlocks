using System;
using System.Runtime.InteropServices;
using RealBlocksUI.Views;
using Rhino;
using Rhino.Commands;
using Rhino.UI;

namespace RealBlocksUI.Commands
{
    [Guid("D7FB9DEE-2AE0-42C8-A52E-D82A4D4F597A")]
    public class WpfManagerHost : RhinoWindows.Controls.WpfElementHost
    {
        public WpfManagerHost(uint docSn) : base(new BlockManager(docSn), null) { }
    }

    public class rbManager : Command
    {
        static rbManager _instance;
        public rbManager()
        {
            _instance = this;
            Panels.RegisterPanel(RealBlocksUIPlugIn.Instance, typeof(WpfManagerHost), "Block Manager", null);
        }

        ///<summary>The only instance of the rbManager command.</summary>
        public static rbManager Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "rbManager"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: complete command.

            var panelId = typeof(WpfManagerHost).GUID;
            var visible = Panels.IsPanelVisible(panelId);

            var prompt = (visible)
                ? "Sample panel is visible. New value"
                : "Sample Manager panel is hidden. New value";

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
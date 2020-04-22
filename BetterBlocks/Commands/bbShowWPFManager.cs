using System;
using System.Runtime.InteropServices;
using BetterBlocks.UI.Views;
using Rhino;
using Rhino.Commands;
using Rhino.UI;

namespace BetterBlocks.Commands
{
    [Guid("9D722409-FF9D-4133-B52D-78FD31B5BD48")]
    public class WpfManagerHost : RhinoWindows.Controls.WpfElementHost
    {
        public WpfManagerHost(uint docSn) : base(new BlockManager(docSn), null) { }
    }

    public class bbShowWPFManager : Command
    {
        static bbShowWPFManager _instance;
        public bbShowWPFManager()
        {
            _instance = this;
            Panels.RegisterPanel(BetterBlocksPlugIn.Instance, typeof(WpfManagerHost), "Block manager", null);
        }

        ///<summary>The only instance of the bbShowWPFManager command.</summary>
        public static bbShowWPFManager Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "bbShowWPFManager"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // TODO: complete command.

            var panel_id = typeof(BlockManager).GUID;
            var visible = Panels.IsPanelVisible(panel_id);

            var prompt = (visible)
                ? "Sample panel is visible. New value"
                : "Sample Manager panel is hidden. New value";

            RhinoApp.WriteLine(prompt);

            // toggle visible
            if (!visible)
            {
                Panels.OpenPanel(panel_id);
            }
            else Panels.ClosePanel(panel_id);

            return Result.Success;
        }
    }
}
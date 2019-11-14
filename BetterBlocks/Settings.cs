using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetterBlocks.Core;
using Rhino.Display;
using Rhino.DocObjects;

namespace BetterBlocks
{
    public static class Settings
    {

        public static DefinedViewportProjection BlockManagerPreviewProjection = DefinedViewportProjection.Perspective;
        public static BlockPreviewDisplayMode BlockManagerPreviewDisplayMode = BlockPreviewDisplayMode.Ghosted;

        public static DisplayModeDescription BlockManagerPreviewDisplayModeDescription =>
            DisplayModeParser.Parse(BlockManagerPreviewDisplayMode);
        public static int BlockManagerTableHeight = 200;
        public static int BlockManagerPreviewWidth = 200;
        public static double BlockManagerPreviewImageRatio = 4.0 / 3.0;

        public static int BlockManagerPreviewHeight =
            Convert.ToInt32(BlockManagerPreviewWidth * BlockManagerPreviewImageRatio);
        public static string CountDelimiter = "_";
        public static int PadCount = 3;
        public static Color BlockManagerPreviewInsertionPointColor = Color.Red;
    }
}

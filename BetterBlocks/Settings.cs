using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Display;
using Rhino.DocObjects;

namespace BetterBlocks
{
    public static class Settings
    {
        public static DefinedViewportProjection BlockManagerPreviewProjection = DefinedViewportProjection.Perspective;
        public static DisplayMode BlockManagerPreviewDisplayMode = DisplayMode.Shaded;
        public static int BlockManagerTableHeight = 200;
        public static int BlockManagerPreviewWidth = 200;
        public static double BlockManagerPreviewImageRatio = 4.0 / 3.0;

        public static int BlockManagerPreviewHeight =
            Convert.ToInt32(BlockManagerPreviewWidth * BlockManagerPreviewImageRatio);
        public static string CountDelimiter = "_";
        public static int PadCount = 3;
    }
}

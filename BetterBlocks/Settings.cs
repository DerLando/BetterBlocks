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
    }
}

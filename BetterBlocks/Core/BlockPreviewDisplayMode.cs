using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Display;

namespace BetterBlocks.Core
{
    public enum BlockPreviewDisplayMode
    {
        Wireframe,
        Shaded,
        Technical,
        AmbientOcclusion,
        Ghosted,
        Pen,
        Xray
    }

    public static class DisplayModeParser
    {
        public static DisplayModeDescription Parse(BlockPreviewDisplayMode displayMode)
        {
            DisplayModeDescription description = null;
            switch (displayMode)
            {
                case BlockPreviewDisplayMode.Wireframe:
                    description = DisplayModeDescription.GetDisplayMode(DisplayModeDescription.WireframeId);
                    break;
                case BlockPreviewDisplayMode.Shaded:
                    description = DisplayModeDescription.GetDisplayMode(DisplayModeDescription.ShadedId);
                    break;
                case BlockPreviewDisplayMode.Technical:
                    description = DisplayModeDescription.GetDisplayMode(DisplayModeDescription.TechId);
                    break;
                case BlockPreviewDisplayMode.AmbientOcclusion:
                    description = DisplayModeDescription.GetDisplayMode(DisplayModeDescription.AmbientOcclusionId);
                    break;
                case BlockPreviewDisplayMode.Ghosted:
                    description = DisplayModeDescription.GetDisplayMode(DisplayModeDescription.GhostedId);
                    break;
                case BlockPreviewDisplayMode.Pen:
                    description = DisplayModeDescription.GetDisplayMode(DisplayModeDescription.PenId);
                    break;
                case BlockPreviewDisplayMode.Xray:
                    description = DisplayModeDescription.GetDisplayMode(DisplayModeDescription.XRayId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(displayMode), displayMode, null);
            }

            return description;
        }
    }
}

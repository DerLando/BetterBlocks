using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace BetterBlocks.Core
{
    public static class BlockPreview
    {
        public static Bitmap GeneratePreview(NestedBlock nested, RhinoDoc doc)
        {
            var view = doc.Views.ActiveView;
            var viewCapture = new ViewCapture
            {
                Height = Settings.BlockManagerPreviewHeight,
                Width = Settings.BlockManagerPreviewWidth
            };

            // store old settings
            var cameraLocation = view.ActiveViewport.CameraLocation;
            var cameraTarget = view.ActiveViewport.CameraTarget;
            var cameraDirection = cameraTarget - cameraLocation;
            var cameraUp = view.ActiveViewport.CameraUp;
            var displayMode = view.ActiveViewport.DisplayMode;
            view.ActiveViewport.NextViewProjection();
            view.ActiveViewport.PushViewProjection();

            view.ActiveViewport.SetProjection(Settings.BlockManagerPreviewProjection, null, false);
            view.ActiveViewport.DisplayMode = Settings.BlockManagerPreviewDisplayModeDescription;
            var conduit = new BlockPreviewConduit(nested);
            conduit.Enabled = true;
            doc.Views.RedrawEnabled = false;

            var bb = conduit.GetReferenceBoundingBox();
            bb.Inflate(0.8, 0.8, 0.8);
            //view.ActiveViewport.SetCameraLocation(bb.Corner(true, true, true), false);
            //view.ActiveViewport.SetCameraTarget(bb.Center, false);
            view.ActiveViewport.ZoomBoundingBox(bb);
            view.Redraw();

            var image = viewCapture.CaptureToBitmap(view);

            conduit.Enabled = false;

            var prev = view.ActiveViewport.PreviousViewProjection();
            //var popped = view.ActiveViewport.PopViewProjection();
            view.ActiveViewport.CameraUp = cameraUp;
            view.ActiveViewport.SetCameraLocation(cameraLocation, false);
            view.ActiveViewport.SetCameraDirection(cameraDirection, false);
            view.ActiveViewport.SetCameraTarget(cameraTarget, false);
            view.ActiveViewport.DisplayMode = displayMode;
            doc.Views.RedrawEnabled = true;

            return image;
        }
    }
}

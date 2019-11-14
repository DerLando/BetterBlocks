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
    public class BlockPreview
    {
        private readonly InstanceDefinition _definition;

        public Image Preview { get; private set; }

        public BlockPreview(InstanceDefinition definition)
        {
            _definition = definition;

            Preview = GeneratePreview();
        }

        private Image GeneratePreview()
        {
            var viewCapture = new ViewCapture();
            viewCapture.Height = Settings.BlockManagerPreviewHeight;
            viewCapture.Width = Settings.BlockManagerPreviewWidth;

            var doc = RhinoDoc.ActiveDoc;
            var view = doc.Views.AddPageView("TEMP", viewCapture.Width, viewCapture.Height);

            var pipeline = view.DisplayPipeline;
            pipeline.DrawPoint(new Point3d(1, 1, 0));

            var image = viewCapture.CaptureToBitmap(view);

            return image;
        }
    }
}

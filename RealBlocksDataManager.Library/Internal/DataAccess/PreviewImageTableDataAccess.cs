using RealBlocksDataManager.Library.Internal.DataAccess.Base;
using RealBlocksDataManager.Library.Internal.Helpers;
using RealBlocksDataManager.Library.Internal.Models;
using Rhino;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;
using System;
using System.Drawing;

namespace RealBlocksDataManager.Library.Internal.DataAccess
{
    internal class PreviewImageTableDataAccess : TableDataAccessBase
    {
        private InstanceDefinitionConduit GetPreviewConduit(Guid id)
        {
            var instances = new InstanceTableDataAccess();
            var definition = instances.GetDefinition(id);

            return GetPreviewConduit(definition);
        }

        private InstanceDefinitionConduit GetPreviewConduit(InstanceDefinition definition)
        {
            return new InstanceDefinitionConduit(
                new InsertedInstanceModel(definition));
        }

        private NestedInstanceDefinitionConduit GetNestedPreviewConduit(InstanceDefinition main, InstanceDefinition nested)
        {
            var instanceAccess = new InstanceTableDataAccess();
            var relative = instanceAccess.GetNestedRelativeInserted(main.Id, nested.Id);
            return new NestedInstanceDefinitionConduit(
                new InsertedInstanceModel(main),
                relative);
        }

        private Image GetPreviewHelper(InstanceDefinitionConduit conduit, int width, int height)
        {
            var doc = RhinoDoc.ActiveDoc;
            var view = doc.Views.ActiveView;
            var viewCapture = new ViewCapture { Height = height, Width = width };

            // store old camera settings
            var settings = new CameraSettings(view);

            // set projection for viewcapture
            view.ActiveViewport.SetProjection(DefinedViewportProjection.Perspective, null, false);

            // get conduit and enable it
            conduit.Enabled = true;

            // disable redraw
            doc.Views.RedrawEnabled = false;

            // zoom to reference boundingbox
            var bb = conduit.GetReferenceBoundingBox();
            bb.Inflate(0.8, 0.8, 0.8);
            view.ActiveViewport.ZoomBoundingBox(bb);

            // redraw and capture
            view.Redraw();
            var image = viewCapture.CaptureToBitmap(view);

            // disable the conduit
            conduit.Enabled = false;

            // reset view changes
            settings.ApplySettings(view);

            // re-enable drawing
            doc.Views.RedrawEnabled = true;

            // return captured imaged
            return image;
        }

        public Image GetNestedPreview(InstanceDefinition main, InstanceDefinition nested, int width, int height)
        {
            var conduit = GetNestedPreviewConduit(main, nested);

            return GetPreviewHelper(conduit, width, height);
        }

        public Image GetPreview(InstanceDefinition definition, int width, int height)
        {
            var conduit = GetPreviewConduit(definition);

            return GetPreviewHelper(conduit, width, height);
        }
    }

    internal readonly struct CameraSettings
    {
        private readonly Point3d Location;
        private readonly Point3d Target;
        private readonly Vector3d Direction;
        private readonly Vector3d Up;

        public CameraSettings(RhinoView view)
        {
            Location = view.ActiveViewport.CameraLocation;
            Target = view.ActiveViewport.CameraTarget;
            Direction = Target - Location;
            Up = view.ActiveViewport.CameraUp;
        }

        public void ApplySettings(RhinoView view)
        {
            view.ActiveViewport.CameraUp = Up;
            view.ActiveViewport.SetCameraLocation(Location, false);
            view.ActiveViewport.SetCameraDirection(Direction, false);
            view.ActiveViewport.SetCameraTarget(Target, false);
        }
    }
}

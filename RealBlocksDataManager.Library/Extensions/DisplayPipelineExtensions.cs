using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealBlocksDataManager.Library.Extensions
{
    public static class DisplayPipelineExtensions
    {
        public static void DrawInstanceObject(this DisplayPipeline display, InstanceObject instanceObject, Transform xForm, bool highlight)
        {
            instanceObject.Explode(false, out var pieces, out _,
                out var pieceTransforms);
            var geo = instanceObject.Geometry;
            for (int i = 0; i < pieces.Length; i++)
            {
                if (highlight)
                {
                    display.DrawObjectHighlighted(pieces[i], xForm * pieceTransforms[i]);
                }
                else
                {
                    display.DrawObject(pieces[i], xForm * pieceTransforms[i]);
                }
            }
        }

        public static void DrawObjectHighlighted(this DisplayPipeline display, RhinoObject rhinoObject, Transform xForm)
        {
            // get old color and color source
            var objColor = rhinoObject.Attributes.ObjectColor;
            var objColorSource = rhinoObject.Attributes.ColorSource;

            // modify
            rhinoObject.Attributes.ObjectColor = Rhino.ApplicationSettings.AppearanceSettings.SelectedObjectColor;
            rhinoObject.Attributes.ColorSource = ObjectColorSource.ColorFromObject;
            rhinoObject.CommitChanges();

            // draw
            display.DrawObject(rhinoObject, xForm);

            // restore old settings
            rhinoObject.Attributes.ObjectColor = objColor;
            rhinoObject.Attributes.ColorSource = objColorSource;
            rhinoObject.CommitChanges();
        }

    }
}

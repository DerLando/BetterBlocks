using RealBlocksDataManager.Library.Extensions;
using RealBlocksDataManager.Library.Internal.Models;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace RealBlocksDataManager.Library.Internal.Helpers
{
    /// <summary>
    /// A conduit which can display an Instance definition
    /// </summary>
    internal class InstanceDefinitionConduit : DisplayConduit
    {
        private readonly InsertedInstanceModel MainReference;

        public InstanceDefinitionConduit(InsertedInstanceModel mainDefinition)
        {
            MainReference = mainDefinition;
        }

        #region Methods

        /// <summary>
        /// Calculates the combined <see cref="BoundingBox"/> of all
        /// objects contained inside the Main definition
        /// </summary>
        /// <returns></returns>
        public BoundingBox GetReferenceBoundingBox()
        {
            var bb = new BoundingBox();
            foreach (var rhinoObject in MainReference.Definition.GetObjects())
            {
                bb.Union(rhinoObject.Geometry.GetBoundingBox(false));
            }

            return bb;
        }

        #endregion

        #region overrides

        protected override void ObjectCulling(CullObjectEventArgs e)
        {
            e.CullObject = true;
            //base.ObjectCulling(e);
        }

        protected override void CalculateBoundingBox(CalculateBoundingBoxEventArgs e)
        {
            foreach (var rhinoObject in MainReference.Definition.GetObjects())
            {
                e.BoundingBox.Union(rhinoObject.Geometry.GetBoundingBox(false));
            }
        }

        protected override void PostDrawObjects(DrawEventArgs e)
        {
            foreach (var rhinoObject in MainReference.Definition.GetObjects())
            {
                if (rhinoObject.GetType() == typeof(InstanceObject))
                {
                    e.Display.DrawInstanceObject((InstanceObject)rhinoObject,
                        MainReference.Insertion.Transformation, false);
                }
                else
                {
                    e.Display.DrawObject(rhinoObject, MainReference.Insertion.Transformation);
                }
            }
        }

        protected override void DrawForeground(DrawEventArgs e)
        {
            e.Display.DrawPoint(MainReference.Insertion.InsertionPoint,
                Color.Red);
        }

        #endregion

    }
}

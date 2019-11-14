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
using Rhino.Render;

namespace BetterBlocks.Core
{
    public class BlockPreviewConduit : DisplayConduit
    {
        public ReferencedBlock MainReference { get; set; }
        public ReferencedBlock ActivePart { get; set; }

        public BlockPreviewConduit(NestedBlock nested)
        {
            if (nested.HasParent)
            {
                NestedBlock parent = nested.Parent;
                while (parent.HasParent)
                {
                    parent = parent.Parent;
                }
                var references = nested.Parent.GetRootTree();
                var activeIndex = nested.Parent.GetActiveIndex(nested);

                MainReference = new ReferencedBlock(parent);
                ActivePart = references.Last()[activeIndex];
            }
            else
            {
                MainReference = new ReferencedBlock(nested);
            }

        }

        public BoundingBox GetReferenceBoundingBox()
        {
            var bb = new BoundingBox();
            foreach (var rhinoObject in MainReference.Definition.GetObjects())
            {
                bb.Union(rhinoObject.Geometry.GetBoundingBox(false));
            }

            return bb;
        }

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
                    e.Display.DrawInstanceObject((InstanceObject) rhinoObject,
                        MainReference.BlockInsertionParameters.InstanceXform, false);
                }
                else
                {
                    e.Display.DrawObject(rhinoObject, MainReference.BlockInsertionParameters.InstanceXform);
                }
            }
        }

        protected override void DrawForeground(DrawEventArgs e)
        {
            if (ActivePart != null)
            {
                foreach (var rhinoObject in ActivePart.Definition.GetObjects())
                {
                    if (rhinoObject.GetType() == typeof(InstanceObject))
                    {
                        e.Display.DrawInstanceObject((InstanceObject) rhinoObject,
                            ActivePart.BlockInsertionParameters.InstanceXform, true);
                    }
                    else
                    {
                        e.Display.DrawObjectHighlighted(rhinoObject, ActivePart.BlockInsertionParameters.InstanceXform);
                    }
                }

                e.Display.DrawPoint(ActivePart.BlockInsertionParameters.InsertionPoint,
                    Settings.BlockManagerPreviewInsertionPointColor);

            }

            else
            {
                e.Display.DrawPoint(MainReference.BlockInsertionParameters.InsertionPoint,
                    Settings.BlockManagerPreviewInsertionPointColor);
            }

        }
    }
}

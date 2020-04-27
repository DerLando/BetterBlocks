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
    /// A conduit which can display a instance definition
    /// and also highlight a referenced inner part as active
    /// </summary>
    internal class NestedInstanceDefinitionConduit : InstanceDefinitionConduit
    {
        private readonly InsertedInstanceModel ActivePart;

        public NestedInstanceDefinitionConduit(InsertedInstanceModel mainDefinition, InsertedInstanceModel activePart) : base(mainDefinition)
        {
            ActivePart = activePart;
        }

        #region Overrides

        protected override void DrawForeground(DrawEventArgs e)
        {
            foreach (var rhinoObject in ActivePart.Definition.GetObjects())
            {
                if (rhinoObject.GetType() == typeof(InstanceObject))
                {
                    e.Display.DrawInstanceObject((InstanceObject)rhinoObject,
                        ActivePart.Insertion.Transformation, true);
                }
                else
                {
                    e.Display.DrawObjectHighlighted(rhinoObject, ActivePart.Insertion.Transformation);
                }
            }

            e.Display.DrawPoint(ActivePart.Insertion.InsertionPoint,
                Color.Red);
        }


        #endregion

    }
}

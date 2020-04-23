using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealBlocksDataManager.Library.Internal.Models
{
    /// <summary>
    /// A immutable struct giving information about the
    /// insertion parameters for a referenced block definition
    /// inside of a document
    /// </summary>
    internal readonly struct InstanceInsertionModel
    {
        /// <summary>
        /// The relative insertion Point,
        /// All instance definitions have their insertion Point at (0,0,0),
        /// The vector from the origin to the Insertion Point gives the translation
        /// of the inserted <see cref="InstanceReferenceGeometry"/>
        /// </summary>
        public readonly Point3d InsertionPoint;

        /// <summary>
        /// The Transformation applied to the geometry inside the instancedefinition
        /// as a whole
        /// </summary>
        public readonly Transform Transformation;

        public InstanceInsertionModel(Point3d insertionPoint, Transform transformation)
        {
            InsertionPoint = insertionPoint;
            Transformation = transformation;
        }

        public static InstanceInsertionModel Identity => new InstanceInsertionModel(Point3d.Origin, Transform.Identity);
    }
}

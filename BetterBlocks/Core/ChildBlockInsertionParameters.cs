using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.Geometry;

namespace BetterBlocks.Core
{
    public class ChildBlockInsertionParameters
    {
        public Point3d InsertionPoint { get; set; }
        public Transform InstanceXform { get; set; }

        public ChildBlockInsertionParameters(Point3d insertionPoint, Transform instanceXform)
        {
            InsertionPoint = insertionPoint;
            InstanceXform = instanceXform;
        }

        public static ChildBlockInsertionParameters Identity => new ChildBlockInsertionParameters(Point3d.Origin, Transform.Identity);
    }
}

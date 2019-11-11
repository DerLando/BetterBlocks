using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino;
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
            // create new rhino doc
            var newDoc = new Rhino.FileIO.File3dm();
            newDoc.Objects.Clear();

            //// get all blocks that reference the main definition
            //var parents = _definition.GetReferences(2);

            //foreach (var parent in parents)
            //{
            //    newDoc.AllInstanceDefinitions.Add(parent.InstanceDefinition.Name, parent.InstanceDefinition.Description,
            //        parent.InsertionPoint, from obj in parent.InstanceDefinition.GetObjects() select obj.Geometry);
            //}

            var index = newDoc.AllInstanceDefinitions.Add(_definition.Name, _definition.Description, new Point3d(0, 0, 0),
                from obj in _definition.GetObjects() select obj.Geometry);

            newDoc.Objects.AddInstanceObject(index, Transform.Identity);

            //newDoc.AllViews[0].Viewport.

            return newDoc.GetPreviewImage();
        }
    }
}

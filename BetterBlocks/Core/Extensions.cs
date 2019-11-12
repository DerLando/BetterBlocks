﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rhino.DocObjects;
using Rhino.FileIO;
using Rhino.Geometry;

namespace BetterBlocks.Core
{
    public static class Extensions
    {
        /// <summary>
        /// Tests a given instance definition, if it is at the root level
        /// meaning it only contains "pure" rhino geometry and no other nested instance definitions.
        /// </summary>
        /// <param name="definition"></param>
        /// <returns>A boolean telling if the given definition is root or not</returns>
        public static bool IsRoot(this InstanceDefinition definition)
        {
            return !definition.GetObjects().Any(o => o.ObjectType == ObjectType.InstanceReference);
        }

        public static bool IsInUse(this InstanceDefinition definition)
        {
            return definition.InUse(0) | definition.InUse(1) | definition.InUse(2);
        }

        public static IEnumerable<ChildBlockInsertionParameters> GetPartRelativeXforms(this InstanceDefinition definition)
        {
            return from obj in definition.GetObjects()
                where obj.ObjectType == ObjectType.InstanceReference
                let reference = obj as InstanceObject
                select new ChildBlockInsertionParameters(reference.InsertionPoint, reference.InstanceXform);
        }

        public static IEnumerable<InstanceObject> GetPartInstances(this InstanceDefinition definition)
        {
            return from obj in definition.GetObjects()
                where obj.ObjectType == ObjectType.InstanceReference
                select (InstanceObject)obj;
        }

        public static IEnumerable<InstanceDefinition> GetPartDefinitions(this InstanceDefinition definition)
        {
            return from obj in definition.GetPartInstances() select obj.InstanceDefinition;
        }

        public static int PartCount(this InstanceDefinition definition)
        {
            return definition.GetPartInstances().Count();
        }

        public static int GetNestedCount(this InstanceDefinition definition)
        {
            var containers = definition.GetContainers();
            int count = 0;

            foreach (var container in containers)
            {
                foreach (var instance in container.GetPartInstances())
                {
                    if (instance.InstanceDefinition.Equals(definition)) count += 1;
                }
            }

            return count;
        }

        //public static int[] AddNestedBlock(this File3dm file3dm, NestedBlock nested)
        //{
        //    List<int> indices = new List<int>();

        //    for (int i = 0; i < nested.Count; i++)
        //    {
                
        //    }
        //}

    }
}

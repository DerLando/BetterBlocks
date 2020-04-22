using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.DocObjects;

namespace RealBlocksDataManager.Library.Extensions
{
    public static class InstanceDefinitionExtensions
    {
        /// <summary>
        /// Tests a given instance definition, if it is at the root level
        /// meaning it only contains "pure" rhino geometry and no other nested instance definitions.
        /// </summary>
        /// <param name="definition"></param>
        /// <returns>A boolean telling if the given definition is root or not</returns>
        public static bool IsRoot(this InstanceDefinition definition)
        {
            return definition.GetObjects().All(o => o.ObjectType != ObjectType.InstanceReference);
        }

        /// <summary>
        /// Gets all <see cref="InstanceObject"/>s assembled in the given <see cref="InstanceDefinition"/>
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
        public static IEnumerable<InstanceObject> GetPartInstances(this InstanceDefinition definition)
        {
            return from obj in definition.GetObjects()
                where obj.ObjectType == ObjectType.InstanceReference
                select (InstanceObject)obj;
        }

        /// <summary>
        /// Gets all <see cref="InstanceDefinition"/>s assembled in the given <see cref="InstanceDefinition"/>
        /// </summary>
        /// <param name="definition"></param>
        /// <returns></returns>
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

    }
}

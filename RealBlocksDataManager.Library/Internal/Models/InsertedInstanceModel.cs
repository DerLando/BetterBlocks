using Rhino.DocObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealBlocksDataManager.Library.Internal.Models
{
    internal class InsertedInstanceModel
    {
        public readonly InstanceDefinition Definition;
        public readonly InstanceInsertionModel Insertion;

        public InsertedInstanceModel(InstanceDefinition definition)
        {
            Definition = definition;
            Insertion = InstanceInsertionModel.Identity;
        }

        public InsertedInstanceModel(InstanceDefinition definition, InstanceInsertionModel insertion) : this(definition)
        {
            Insertion = insertion;
        }
    }
}

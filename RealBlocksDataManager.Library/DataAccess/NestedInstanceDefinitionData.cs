using RealBlocksDataManager.Library.Factories;
using RealBlocksDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksDataManager.Library.DataAccess
{
    public static class NestedInstanceDefinitionData
    {
        public static IEnumerable<NestedInstanceDefinitionModel> GetAll()
        {
            return InstanceTreeFactory
                .Create()
                .Instances;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealBlocksDataManager.Library.DataAccess;
using RealBlocksUI.Library.Models;

namespace RealBlocksUI.Library.Api
{
    public class InstanceDefinitionEndpoint
    {
        public IEnumerable<InstanceDefinitionModel> GetAll()
        {
            return InstanceDefinitionData.GetInstanceDefinitions()
                .Select(d => new InstanceDefinitionModel
                {
                    Id = d.Id,
                    Index = d.Index,
                    IsInUse = d.IsInUse,
                    Name = d.Name,
                    ObjectCount = d.ObjectCount,
                    ObjectIds = d.ObjectIds,
                })
                ;
        }
    }
}

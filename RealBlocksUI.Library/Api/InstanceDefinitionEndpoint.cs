using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RealBlocksDataManager.Library.DataAccess;
using RealBlocksUI.Library.Models;

namespace RealBlocksUI.Library.Api
{
    public class InstanceDefinitionEndpoint : IInstanceDefinitionEndpoint
    {
        public IEnumerable<InstanceDefinitionModel> GetAll()
        {
            return InstanceDefinitionData.GetInstanceDefinitions()
                .Select(d => new InstanceDefinitionModel
                {
                    Description = d.Description,
                    Id = d.Id,
                    Index = d.Index,
                    IsAssembly = d.IsAssembly,
                    IsInUse = d.IsInUse,
                    Name = d.Name,
                    ObjectCount = d.ObjectCount,
                    ObjectIds = d.ObjectIds,
                })
                ;
        }

        public IEnumerable<InstanceDefinitionModel> GetChildren(Guid id)
        {
            return InstanceDefinitionData
                .GetChildrenById(id)
                .Select(d => new InstanceDefinitionModel
                {
                    Description = d.Description,
                    Id = d.Id,
                    Index = d.Index,
                    IsAssembly = d.IsAssembly,
                    IsInUse = d.IsInUse,
                    Name = d.Name,
                    ObjectCount = d.ObjectCount,
                    ObjectIds = d.ObjectIds,
                })
                ;
        }
    }
}

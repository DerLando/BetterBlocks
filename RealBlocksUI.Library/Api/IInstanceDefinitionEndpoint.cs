using System.Collections.Generic;
using RealBlocksUI.Library.Models;

namespace RealBlocksUI.Library.Api
{
    public interface IInstanceDefinitionEndpoint
    {
        IEnumerable<InstanceDefinitionModel> GetAll();
    }
}
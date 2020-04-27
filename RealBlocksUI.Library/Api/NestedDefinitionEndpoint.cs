using RealBlocksDataManager.Library.DataAccess;
using RealBlocksUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksUI.Library.Api
{
    public class NestedDefinitionEndpoint
    {
        public NestedInstanceDefinitionModel[] GetAll()
        {
            var test = NestedInstanceDefinitionData.GetAll();

            return null;
        }
    }
}

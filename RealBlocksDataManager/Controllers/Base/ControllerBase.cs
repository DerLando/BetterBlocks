using Rhino;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealBlocksDataManager.Controllers.Base
{
    public abstract class ControllerBase
    {
        public static RhinoDoc GetActiveDoc => RhinoDoc.ActiveDoc;
    }
}

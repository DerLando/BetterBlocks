using System;
using System.Collections.Generic;
using RealBlocksUI.Library.Api;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace RealBlocksUI
{
    public class RealBlocksUICommand : Command
    {
        public RealBlocksUICommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static RealBlocksUICommand Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName
        {
            get { return "RealBlocksUICommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            RhinoApp.WriteLine("The {0} command is under construction.", EnglishName);

            var endPoint = new InstanceDefinitionEndpoint();
            var test = endPoint.GetAll();

            return Result.Success;
        }
    }
}

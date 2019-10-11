using System;
using BetterBlocks.Core;
using Rhino;
using Rhino.Commands;

namespace BetterBlocks.Commands
{
    public class bbTestCommand : Command
    {
        static bbTestCommand _instance;
        public bbTestCommand()
        {
            _instance = this;
        }

        ///<summary>The only instance of the bbTestCommand command.</summary>
        public static bbTestCommand Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "bbTestCommand"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            NestedBlock[] nested = new NestedBlock[doc.InstanceDefinitions.Count];
            for (int i = 0; i < doc.InstanceDefinitions.Count; i++)
            {
                nested[i] = new NestedBlock(doc, doc.InstanceDefinitions[i]);
            }

            return Result.Success;
        }
    }
}
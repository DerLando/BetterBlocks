using System;
using System.IO;
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
            //var preview = new BlockPreview(doc.InstanceDefinitions[0]);

            //var image = preview.Preview;

            //image.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "bbTest.jpg"));

            //var conduit = new BlockPreviewConduit();
            //conduit.Enabled = true;

            return Result.Success;
        }
    }
}
using AutoMapper;
using RealBlocksUI.Library.Models;
using RealBlocksUI.ViewModels;
using Rhino.PlugIns;

namespace RealBlocksUI
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class RealBlocksUIPlugIn : Rhino.PlugIns.PlugIn

    {
        public RealBlocksUIPlugIn()
        {
            Instance = this;
            Mapper = ConfigureMapper();
        }

        ///<summary>Gets the only instance of the RealBlocksUIPlugIn plug-in.</summary>
        public static RealBlocksUIPlugIn Instance
        {
            get; private set;
        }

        /// <summary>
        /// Automapper singleton to map from data to display models
        /// </summary>
        public static IMapper Mapper { get; private set; }

        private IMapper ConfigureMapper()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<InstanceDefinitionModel, InstanceDefinitionDisplayModel>();
                });

            var mapper = config.CreateMapper();

            return mapper;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.
        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            return LoadReturnCode.Success;
        }
    }
}
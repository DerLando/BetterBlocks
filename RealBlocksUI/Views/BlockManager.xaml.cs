using System.Windows.Controls;
using RealBlocksUI.Library.Api;
using RealBlocksUI.ViewModels;

namespace RealBlocksUI.Views
{
    /// <summary>
    /// Interaktionslogik für BlockManager.xaml
    /// </summary>
    public partial class BlockManager : UserControl
    {
        public BlockManager(uint docSn)
        {
            InitializeComponent();

            this.DataContext = new BlockManagerViewModel(
                docSn, 
                new InstanceDefinitionEndpoint(), 
                RealBlocksUIPlugIn.Mapper
                );
        }
    }
}

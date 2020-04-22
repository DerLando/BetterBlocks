using System.Windows.Controls;
using BetterBlocks.UI.ViewModels;

namespace BetterBlocks.UI.Views
{
    /// <summary>
    /// Interaktionslogik für BlockManager.xaml
    /// </summary>
    public partial class BlockManager : UserControl
    {
        public BlockManager(uint docSn)
        {
            InitializeComponent();

            this.DataContext = new InstanceDefinitionStructureViewModel(docSn);
        }
    }
}

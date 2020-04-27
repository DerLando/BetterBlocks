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
                RealBlocksUIPlugIn.Mapper,
                new PreviewImageEndpoint()
                );
        }

        /// <summary>
        /// Code behind is bad bad, but there does not seem to be an easy way to do this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TvBlocks_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            ((BlockManagerViewModel)DataContext).SelectedItem = (InstanceDefinitionDisplayModel)e.NewValue;
        }
    }
}

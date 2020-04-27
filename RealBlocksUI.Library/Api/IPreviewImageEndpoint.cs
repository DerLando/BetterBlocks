using System;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace RealBlocksUI.Library.Api
{
    public interface IPreviewImageEndpoint
    {
        BitmapImage Get(Guid id, int width, int height);

        BitmapImage Get(Guid mainId, Guid activePartId, int width, int height);

    }
}
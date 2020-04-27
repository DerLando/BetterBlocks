using RealBlocksDataManager.Library.DataAccess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace RealBlocksUI.Library.Api
{
    public class PreviewImageEndpoint : IPreviewImageEndpoint
    {
        public BitmapImage Get(Guid id, int width, int height)
        {
            var image = ImageData.GetPreviewImage(id, width, height);
            var bitmap = new Bitmap(image);

            BitmapImage result;

            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                ms.Position = 0;
                result = new BitmapImage();
                result.BeginInit();
                result.StreamSource = ms;
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.EndInit();
            }

            return result;
        }
    }
}

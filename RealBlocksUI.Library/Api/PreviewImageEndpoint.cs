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
        /// <summary>
        /// Converts a <see cref="Image"/> to a <see cref="BitmapImage"/>
        /// </summary>
        /// <param name="image">Image to convert</param>
        /// <returns></returns>
        private BitmapImage ConvertFrom(Image image)
        {
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

        public BitmapImage Get(Guid id, int width, int height)
        {
            var image = ImageData.GetPreviewImage(id, width, height);

            return ConvertFrom(image);
        }

        public BitmapImage Get(Guid mainId, Guid activePartId, int width, int height)
        {
            var image = ImageData.GetPreviewImage(mainId, activePartId, width, height);

            return ConvertFrom(image);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using RealBlocksDataManager.Library.Internal.DataAccess;
using Rhino.DocObjects;

namespace RealBlocksDataManager.Library.DataAccess
{
    public static class ImageData
    {
        /// <summary>
        /// Gets a preview image for the given block definitions id
        /// </summary>
        /// <param name="id">Guid of the instance definition</param>
        /// <param name="width">width of the preview image in pixels</param>
        /// <param name="height">height of the preview image in pixels</param>
        /// <returns></returns>
        public static Image GetPreviewImage(Guid id, int width, int height)
        {
            var instanceAccess = new InstanceTableDataAccess();
            var definition = instanceAccess.GetDefinition(id);

            return GetPreviewImage(definition, width, height);
        }

        /// <summary>
        /// Gets a preview image for the given block definition
        /// </summary>
        /// <param name="definition">The definition to draw a preview image for</param>
        /// <param name="width">width of the preview image in pixels</param>
        /// <param name="height">height of the preview image in pixels</param>
        /// <returns></returns>
        public static Image GetPreviewImage(InstanceDefinition definition, int width, int height)
        {
            var imageAccess = new PreviewImageTableDataAccess();
            return imageAccess.GetPreview(definition, width, height);
        }
    }
}

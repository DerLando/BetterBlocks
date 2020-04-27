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
        private static Image GetPreviewImage(InstanceDefinition definition, int width, int height)
        {
            var imageAccess = new PreviewImageTableDataAccess();
            return imageAccess.GetPreview(definition, width, height);
        }

        /// <summary>
        /// Gets a preview image for the given block definitions id
        /// highlighting the supplied nested active part definitions id
        /// </summary>
        /// <param name="mainId">Guid of the main instance definition</param>
        /// <param name="activePartId">Guid of the nested part definition which should be highlighted</param>
        /// <param name="width">width of the preview image in pixels</param>
        /// <param name="height">height of the preview image in pixels</param>
        /// <returns></returns>
        public static Image GetPreviewImage(Guid mainId, Guid activePartId, int width, int height)
        {
            var instanceAccess = new InstanceTableDataAccess();
            var main = instanceAccess.GetDefinition(mainId);
            var active = instanceAccess.GetDefinition(activePartId);

            return GetPreviewImage(main, active, width, height);
        }

        private static Image GetPreviewImage(InstanceDefinition main, InstanceDefinition active, int width, int height)
        {
            var imageAccess = new PreviewImageTableDataAccess();
            return imageAccess.GetNestedPreview(main, active, width, height);
        }
    }
}

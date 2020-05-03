using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksUI.ViewModels.Base
{
    public interface IDisplayModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Raises the property changed event for the given property name
        /// </summary>
        /// <param name="propertyName"></param>
        void RaisePropertyChanged(string propertyName);

        /// <summary>
        /// Initializes inner display states
        /// can be called from Automapper after mapping to this model
        /// The default implementation does nothing
        /// </summary>
        void Initialize();

    }
}

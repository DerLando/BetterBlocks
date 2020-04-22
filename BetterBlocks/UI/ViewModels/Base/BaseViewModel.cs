using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterBlocks.UI.ViewModels.Base
{
    /// <summary>
    /// Abstract base class for all view models
    /// Implements <see cref="INotifyPropertyChanged"/>
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Basic <see cref="PropertyChangedEventHandler"/>
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The event firing method that derived classes can overwrite
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            var handler = PropertyChanged;
            handler?.Invoke(this, e);
        }
    }
}

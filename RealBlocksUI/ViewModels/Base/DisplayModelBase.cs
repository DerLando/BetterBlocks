using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksUI.ViewModels.Base
{
    public abstract class DisplayModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        /// <summary>
        /// Initializes inner display states
        /// can be called from Automapper after mapping to this model
        /// The default implementation does nothing
        /// </summary>
        public virtual void Initialize() { }
    }
}

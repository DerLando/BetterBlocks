using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBlocksUI.ViewModels.Base
{
    public abstract class ViewModelBase : Rhino.UI.ViewModel
    {
        #region private backing fields

        private readonly uint _documentSerialNumber;

        #endregion

        #region Constructor

        protected ViewModelBase(uint documentSerialNumber)
        {
            _documentSerialNumber = documentSerialNumber;

            Rhino.UI.Panels.Show += OnShowPanel;
        }

        #endregion

        #region Handlers

        internal virtual void OnShowPanel(object sender, Rhino.UI.ShowPanelEventArgs e) { }

        #endregion
    }
}

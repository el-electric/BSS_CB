using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.ControlManager
{
    public interface IControlManager_UserControl
    {
        UserControl getUserControl();
        void setView();
    }
}

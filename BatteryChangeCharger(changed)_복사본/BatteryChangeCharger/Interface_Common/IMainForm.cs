using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.Interface_Common
{
    public interface IMainForm
    {
        Panel getPanel_Main();
        Form getForm_Main();
        void initVariable();
    }
}

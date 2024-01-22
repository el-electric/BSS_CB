using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    public interface IUC_Include_Main
    {
        UserControl getUserControl();
        BCC_UC_ChargingMain_Unit_kiosk getUC_Unit_Kiosk();

    }
}

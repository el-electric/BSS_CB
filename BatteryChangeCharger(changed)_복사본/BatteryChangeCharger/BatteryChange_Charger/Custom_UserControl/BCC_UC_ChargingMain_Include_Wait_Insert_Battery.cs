using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    public partial class BCC_UC_ChargingMain_Include_Wait_Insert_Battery : UserControl
    {
        public BCC_UC_ChargingMain_Include_Wait_Insert_Battery()
        {
            InitializeComponent();



        }

        public void setContent_Insert_Battery_1st()
        {
            label_content.Text = "화면에 표시된 투입구로 배터리를 반납하여 주십시오. (1st)";
        }

        public void setContent_Insert_Battery_1st_Complete()
        {
            label_content.Text = "배터리가 반납되었습니다. (1st)";
        }

        public void setContent_Insert_Battery_2nd()
        {
            label_content.Text = "화면에 표시된 투입구로 배터리를 반납하여 주십시오. (2nd)";
        }

        public void setContent_Insert_Battery_2nd_Complete()
        {
            label_content.Text = "배터리가 반납되었습니다. (2nd)";
        }

        public BCC_UC_ChargingMain_Unit_kiosk UC_ChargingMain_Unit_Kiosk
        {
            get { return bcC_UC_ChargingMain_Unit_kiosk1; }
        }
    }
}

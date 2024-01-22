using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Controller;
using BatteryChangeCharger.ChargerVariable;
using BatteryChangeCharger.StaticVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    public partial class BCC_UC_ChargingMain_Include_Main : UserControl
    {
        public BCC_UC_ChargingMain_Include_Main()
        {
            InitializeComponent();

            //bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(5, CSocketState.CHARGING);
            //bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(7, CSocketState.CHARGING_COMPLETE);

        }

        public void setIsCanUse(bool setting)
        {
            if(setting)
            {
                label_content.Text = "시작 버튼을 눌러주세요.";
                button_start.Visible = true;
            }
            else
            {
                label_content.Text = "사용가능한 조건이 아닙니다. 관리자에게 문의해주세요.";
                button_start.Visible = false;
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            
        }

        private void button_start_Click_1(object sender, EventArgs e)
        {
            MyApplication.getInstance().Controller_Main.addEvent(CONST_EVENT.EVENT_CLICK_BUTTON_START);
        }

        
    }
}

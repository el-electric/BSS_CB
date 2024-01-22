using BatteryChangeCharger.BatteryChange_Charger.TestMode_Charging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Settings
{
    public partial class P788_BCC_UC_Setting_Main : UserControl
    {

        protected Form_Setting_Main mForm_Setting_Main = null;

        public P788_BCC_UC_Setting_Main()
        {
            InitializeComponent();
        }

        public P788_BCC_UC_Setting_Main(Form_Setting_Main form_SettingMain)
        {
            InitializeComponent();
            mForm_Setting_Main = form_SettingMain;
        }




        

        private void button_charging_testmode_Click(object sender, EventArgs e)
        {
            mForm_Setting_Main.setContent_TestMode_Charging();

        }

        private void P788_BCC_UC_Setting_Main_Load(object sender, EventArgs e)
        {

        }

        private void button_chargingcontrol_smartcardreader_Click(object sender, EventArgs e)
        {

        }

        private void button_setting_comm_Click(object sender, EventArgs e)
        {
            mForm_Setting_Main.setContent_CommDeviceSetting();
        }

        private void button_testmode_door_Click(object sender, EventArgs e)
        {
            mForm_Setting_Main.setContent_DoorSetting();
        }

        private void button_program_finish_Click(object sender, EventArgs e)
        {
            Form_Confirm_Program_Reset form = new Form_Confirm_Program_Reset();
            form.ShowDialog();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

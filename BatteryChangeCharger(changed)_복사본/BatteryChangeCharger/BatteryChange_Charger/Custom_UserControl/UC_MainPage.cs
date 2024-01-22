using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Settings;
using BatteryChangeCharger.CorrectionMode;
using BatteryChangeCharger.SmartCardReader_PCSC;
using BatteryChangeCharger.StaticVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.Custom_UsercControl
{
    public partial class UC_MainPage : UserControl
    {
        public UC_MainPage()
        {
            InitializeComponent();
        }

        Form_CorrectionMode mForm_CorrectionMode = null;
        
        Form_Setting_Main mForm_Setting_Main = null; 

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (mForm_Setting_Main == null)
            {
                mForm_Setting_Main = new Form_Setting_Main();
                //mForm_Setting_Main.setView();
            }

            mForm_Setting_Main.Show();



        }


        public void setVisible_Button_Back(bool visible)
        {
            button_back.Visible = visible;
        }

        public Panel getPanel_Main()
        {
            return panel_main;
        }

        private void button_back_Click(object sender, EventArgs e)
        {
            MyApplication.getInstance().Controller_Main.addEvent(CONST_EVENT.EVENT_CLICK_BUTTON_BACK);
        }
    }
}

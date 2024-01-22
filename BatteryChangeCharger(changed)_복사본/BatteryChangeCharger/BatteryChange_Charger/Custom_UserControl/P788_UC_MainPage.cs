using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.ChargerVariable;
using BatteryChangeCharger.BatteryChange_Charger.Settings;
using BatteryChangeCharger.CorrectionMode;
using BatteryChangeCharger.StaticVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    public partial class P788_UC_MainPage : UserControl
    {
        public P788_UC_MainPage()
        {
            InitializeComponent();

            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.UpdateStyles();

            button_back.Parent = tableLayoutPanel1;
        }

        Form_CorrectionMode mForm_CorrectionMode = null;

        protected Form_Setting_Main mForm_Setting_Main = null;



        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (mForm_Setting_Main == null)
            {
                mForm_Setting_Main = new Form_Setting_Main();
                //mForm_Setting_Main.setView();
            }

            mForm_Setting_Main.Show();
        }


        public void setBottombar_Weather()
        {
            panel_bottombar.Controls.Clear();
            mUC_Bottombar_Weather.Dock = DockStyle.Fill;
            panel_bottombar.Controls.Add(mUC_Bottombar_Weather);
        }

        public void setBottombar_ProcessStep()
        {
            panel_bottombar.Controls.Clear();
            mUC_Bottombar_ProcessStep.Dock = DockStyle.Fill;
            panel_bottombar.Controls.Add(mUC_Bottombar_ProcessStep);
        }

        protected P788_UC_Bottombar_Weather mUC_Bottombar_Weather = new P788_UC_Bottombar_Weather();
        public P788_UC_Bottombar_Weather getUC_Bottombar_Weather()
        {
            return mUC_Bottombar_Weather;
        }

        protected P788_UC_Bottombar_ProcessStep mUC_Bottombar_ProcessStep = new P788_UC_Bottombar_ProcessStep();
        public P788_UC_Bottombar_ProcessStep getUC_Bottombar_ProcessStep()
        {
            return mUC_Bottombar_ProcessStep;
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
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void button_back_Click_1(object sender, EventArgs e)
        {
            
        }


        private void button_back_Click_2(object sender, EventArgs e)
        {
            button_back.Image = BatteryChangeCharger.Properties.Resources.img_btn_back_white;
            MyApplication.getInstance().Controller_Main.addEvent(CONST_EVENT.EVENT_CLICK_BUTTON_BACK);
        }

        private void button_back_MouseUp(object sender, MouseEventArgs e)
        {
            button_back.Image = BatteryChangeCharger.Properties.Resources.img_btn_back_white;
        }

        private void button_back_MouseEnter_1(object sender, EventArgs e)
        {
            button_back.Image = BatteryChangeCharger.Properties.Resources.img_btn_back_white_clicked;
        }

        private void button_back_MouseLeave_1(object sender, EventArgs e)
        {
            button_back.Image = BatteryChangeCharger.Properties.Resources.img_btn_back_white;
        }

        private void pictureBox1_DoubleClick_1(object sender, EventArgs e)
        {
            if (MyApplication.getInstance().SystemMode == CSystemMode.NORMAL)
            {
                MyApplication.getInstance().setSystemMode(CSystemMode.SETTINGMODE_MAIN);

                mForm_Setting_Main = null;
                mForm_Setting_Main = new Form_Setting_Main();
                MyApplication.getInstance().Form_Setting_Main = mForm_Setting_Main;
                mForm_Setting_Main.Show();
            }
                

            
            
        }
    }
}

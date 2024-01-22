using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.ChargerVariable;
using BatteryChangeCharger.BatteryChange_Charger.TestMode_Charging;
using BatteryChangeCharger.SmartCardReader_PCSC;
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
    public partial class Form_Setting_Main : Form
    {
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;
        public Form_Setting_Main()
        {
            InitializeComponent();

            mUC_Content_Setting_Main = new P788_BCC_UC_Setting_Main(this);

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            //TopMost = true;
            
            if (MyApplication.bIsScreen_Width_1024)
            {
                this.Width = 1024;
                this.Height = 768;
            }
            else
            {
                this.Width = 768;
                this.Height = 1024;
            }
        //this.WindowState = FormWindowState.Maximized;



            setContent_Main();
        }




        P788_BCC_UC_Setting_CommDeviceSetting mUC_Content_CommDeviceSetting = new P788_BCC_UC_Setting_CommDeviceSetting();
        

        P788_BCC_UC_Setting_DoorSetting mUC_Content_Door_Setting = new P788_BCC_UC_Setting_DoorSetting();
        public P788_BCC_UC_TestMode_Charging mUC_Content_TestMode_Charging = null;
        //P1024_BCC_UC_Setting_CommDeviceSetting mUC_Content_CommDeviceSetting_1024 = new P1024_BCC_UC_Setting_CommDeviceSetting();
        //P1024_BCC_UC_TestMode_Charging mUC_Content_TestMode_Charging = new P1024_BCC_UC_TestMode_Charging();

        public void setTextButton(string title, string subtitle)
        {
            label_title.Text = title;
            label_subtitle.Text = subtitle;
        }

        public void setContent_Main()
        {

            panel_main.Controls.Clear();
            mUC_Content_Setting_Main.Dock = DockStyle.Fill;
            panel_main.Controls.Add(mUC_Content_Setting_Main);

            setTextButton("설정", "");
        }

        public void setContent_CommDeviceSetting()
        {
            MyApplication.getInstance().setSystemMode(CSystemMode.SETTINGMODE_SUB1_COMM_DEVICE_SETTING);

            panel_main.Controls.Clear();
            mUC_Content_CommDeviceSetting.Dock = DockStyle.Fill;
            panel_main.Controls.Add(mUC_Content_CommDeviceSetting);

            setTextButton("설정", "통신 및 장치설정");
        }

        public void setContent_DoorSetting()
        {
            MyApplication.getInstance().setSystemMode(CSystemMode.SETTINGMODE_SUB1_DOOR_SETTING);
            panel_main.Controls.Clear();
            mUC_Content_Door_Setting.Dock = DockStyle.Fill;
            panel_main.Controls.Add(mUC_Content_Door_Setting);

            setTextButton("설정", "도어 설정");
        }

        public void setContent_TestMode_Charging()
        {
            
            panel_main.Controls.Clear();

            mUC_Content_TestMode_Charging = new P788_BCC_UC_TestMode_Charging();
            mUC_Content_TestMode_Charging.Dock = DockStyle.Fill;
            panel_main.Controls.Add(mUC_Content_TestMode_Charging);

            setTextButton("설정", "테스트모드 (충전)");
            MyApplication.getInstance().setSystemMode(CSystemMode.SETTINGMODE_SUB1_TESTMODE_CHARGING);
        }



        public void setContent_TestMode_DoorOpen()
        {
            panel_main.Controls.Clear();
            //mUC_Content_TestMode_Charging.Dock = DockStyle.Fill;
            //panel_main.Controls.Add(mUC_Content_TestMode_Charging);

            setTextButton("설정", "테스트모드 (도어)");
        }

        Form_SmartCardReader_PCSC mForm_SmartCardReader_PCSC = null;
        private void button3_Click(object sender, EventArgs e)
        {

            if (mForm_SmartCardReader_PCSC == null)
            {
                mForm_SmartCardReader_PCSC = new Form_SmartCardReader_PCSC();
                //mForm_SmartCardReader_PCSC.setView();
            }

            mForm_SmartCardReader_PCSC.Show();
        }

        private void button_windowclose_Click(object sender, EventArgs e)
        {
            int width = Width;
            int height = Height;
            Size screenSize = Screen.PrimaryScreen.Bounds.Size;
            if (MyApplication.IsShow_Size)
            {
                MessageBox.Show("ScreenSize = " + screenSize.Width + "    " + screenSize.Height);
                MessageBox.Show("FormSize = " + this.Width + "    " + this.Height);
            }
            

            if (panel_main.Controls.Contains(mUC_Content_Setting_Main))
            {
                MyApplication.getInstance().Form_Setting_Main = null;
                MyApplication.getInstance().setSystemMode(CSystemMode.NORMAL);
                if (mForm_SmartCardReader_PCSC != null)
                {
                    mForm_SmartCardReader_PCSC.Close();
                    mForm_SmartCardReader_PCSC = null;
                }

                this.Close();
            }
            else
            {
                MyApplication.getInstance().setSystemMode(CSystemMode.SETTINGMODE_MAIN);
                setContent_Main();
            }
            
        }
        private void button_setupforengineer_Click(object sender, EventArgs e)
        {

        }

        private void button_charging_testmode_Click(object sender, EventArgs e)
        {

        }

        private void button_chargingcontrol_smartcardreader_Click(object sender, EventArgs e)
        {

        }


        protected P788_BCC_UC_Setting_Main mUC_Content_Setting_Main = null;

        private void Form_Setting_Main_Load(object sender, EventArgs e)
        {
            int width = Width;
            int height = Height;
            Size screenSize = Screen.PrimaryScreen.Bounds.Size;
            if (MyApplication.IsShow_Size)
            {
                MessageBox.Show("ScreenSize = " + screenSize.Width + "    " + screenSize.Height);
                MessageBox.Show("FormSize = " + this.Width + "    " + this.Height);
            }
        }
    }
}

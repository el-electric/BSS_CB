using BatteryChangeCharger.Applications;
using BatteryChangeCharger.Widgets;
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
    public partial class P1024_BCC_UC_ChargingMain_Include_Wait_Insert_Battery : UserControl
    {
        public P1024_BCC_UC_ChargingMain_Include_Wait_Insert_Battery()
        {
            InitializeComponent();


            for (int i = 0; i < 8; i++)
            {
                bcC_UC_ChargingMain_Unit_kiosk1.setSlot_Soc(
                        i + 1,
                        -1
                    );
            }
            //pictureBox1.Parent = this;
        }




        public void setContent_Insert_Battery_1st()
        {
            circularProgressBar1.Value = 0;
            pictureBox1.Visible = false;
            circularProgressBar1.Visible = false;
            label1.Visible = false;
            label_content.Text = "화면에 표시된 투입구로 첫번째 배터리를 반납하여 주십시오.";
        }

        public void setContent_Insert_Battery()
        {
            circularProgressBar1.Value = 0;
            pictureBox1.Visible = false;
            circularProgressBar1.Visible = false;
            label1.Visible = false;
            label_content.Text = "화면에 표시된 투입구로 배터리를 반납하여 주십시오.";
        }

        public void animationInsertBattery()
        {
            //pictureBox1.Visible = false;

            //bunifuTransition1.ShowSync(pictureBox1, false, Bunifu.UI.WinForms.BunifuAnimatorNS.Animation.HorizSlide);
        }


        public void setContent_Wait_Open_Door()
        {
            circularProgressBar1.Value = 0;
            pictureBox1.Visible = false;
            circularProgressBar1.Visible = false;
            label1.Visible = false;
            label_content.Text = "도어가 열리는 중입니다. 완전히 열릴때까지 잠시만 기다려 주세요.";
        }

        public void setContent_Wait_Open_Door_Toggle()
        {
            label_content.Visible = !label_content.Visible;
        }


        public void setContent_Insert_Battery_1st_Complete()
        {
            // bunifuRadialGauge1.TransitionValue(100, 1500); 
            circularProgressBar1.Value = 100;
            pictureBox1.Visible = true;
            circularProgressBar1.Visible = true;
            label1.Visible = true;
            label_content.Text = "첫번째 배터리가 반납되었습니다.";
        }

        public void setContent_Insert_Battery_Complete()
        {
            // bunifuRadialGauge1.TransitionValue(100, 1500); 
            circularProgressBar1.Value = 100;
            pictureBox1.Visible = true;
            circularProgressBar1.Visible = true;
            label1.Visible = true;
            label_content.Text = "배터리가 반납되었습니다.";
        }

        public void setContent_Insert_Battery_2nd()
        {
            circularProgressBar1.Value = 0; 
            pictureBox1.Visible = false;
            circularProgressBar1.Visible = false;
            label1.Visible = false;
            label_content.Text = "화면에 표시된 투입구로 두번째 배터리를 반납하여 주십시오.";
        }

        public void setContent_Insert_Battery_2nd_Complete()
        {
            pictureBox1.Visible = true;
            circularProgressBar1.Visible = true;
            label1.Visible = true;
            circularProgressBar1.Value = 100;
            // bunifuRadialGauge1.TransitionValue(100, 1500);
            label_content.Text = "두번째 배터리가 반납되었습니다.";
        }

        public BCC_UC_ChargingMain_Unit_kiosk UC_ChargingMain_Unit_Kiosk
        {
            get { return bcC_UC_ChargingMain_Unit_kiosk1; }
        }

        private void panel_kiosk_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


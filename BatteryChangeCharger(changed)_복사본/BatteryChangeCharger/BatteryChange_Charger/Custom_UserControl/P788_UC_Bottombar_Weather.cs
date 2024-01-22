using BatteryChangeCharger.Applications;
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
    public partial class P788_UC_Bottombar_Weather : UserControl
    {
        public P788_UC_Bottombar_Weather()
        {
            InitializeComponent();

            Morning_Temp.Text = MyApplication.getInstance().Morning_OneHour_Temp.ToString();
            Afternoon_Temp.Text = MyApplication.getInstance().Afternoon_OneHour_Temp.ToString();

            switch (MyApplication.getInstance().Morning_Weather_Info) // 오전
            {
                case 0:
                    pictureBox2.Image = BatteryChangeCharger.Properties.Resources.w1; // 해가 쩅쩅
                    break;
                case 1:
                    pictureBox2.Image = BatteryChangeCharger.Properties.Resources.w8; // 구름과 눈
                    break;
                case 2:
                    pictureBox2.Image = BatteryChangeCharger.Properties.Resources.w5; // 눈만 내림
                    break;
                case 3:
                    pictureBox2.Image = BatteryChangeCharger.Properties.Resources.w9; // 빗방울 모양
                    break;
            }

            switch (MyApplication.getInstance().Afternoon_Weather_Info) // 오후
            {
                case 0:
                    pictureBox3.Image = BatteryChangeCharger.Properties.Resources.w1; // 해가 쩅쩅
                    break;
                case 1:
                    pictureBox3.Image = BatteryChangeCharger.Properties.Resources.w8; // 구름과 눈
                    break;
                case 2:
                    pictureBox3.Image = BatteryChangeCharger.Properties.Resources.w5; // 눈만 내림
                    break;
                case 3:
                    pictureBox3.Image = BatteryChangeCharger.Properties.Resources.w9; // 빗방울 모양
                    break;
            }

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }
    }
}

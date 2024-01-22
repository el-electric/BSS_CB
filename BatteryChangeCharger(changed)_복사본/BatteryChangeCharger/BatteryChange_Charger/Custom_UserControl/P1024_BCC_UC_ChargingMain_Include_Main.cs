using BatteryChangeCharger.Applications;
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
    public partial class P1024_BCC_UC_ChargingMain_Include_Main : UserControl
    {
        public P1024_BCC_UC_ChargingMain_Include_Main()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();
            //bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(5, CSocketState.CHARGING);
            //bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(7, CSocketState.CHARGING_COMPLETE);


            updateBatteryInfor();

            //bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(0, CSocketState.NOTIFY_ALARM);
            //bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(3, CSocketState.NOTIFY_ALARM);
            //bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(7, CSocketState.NOTIFY_ALARM);
        }

        public BCC_UC_ChargingMain_Unit_kiosk getUC_Unit_Kiosk()
        {
            return bcC_UC_ChargingMain_Unit_kiosk1;
        }

        public void updateBatteryInfor()
        {
            //for(int i = 0; i < 8; i++)
            //{
            //    if(MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.isBatteryExist(i+1))
            //    {
            //        bcC_UC_ChargingMain_Unit_kiosk1.setSlot_Soc(
            //            i + 1,
            //            MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i].mPacket_c1_Receive.SOC_S1
            //        );
            //    }
            //    else
            //    {
            //        bcC_UC_ChargingMain_Unit_kiosk1.setSlot_Soc(
            //            i + 1,
            //            -1
            //        );
            //    }
            //}
            //bcC_UC_ChargingMain_Unit_kiosk1.updateSlot_By_Packet();
        }

        public void setIsCanUse(bool setting)
        {
            label_content.Text = "시작 버튼을 눌러주세요.";
            pictureBox_Start.Visible = true;
            //if (setting)
            //{
            //    label_content.Text = "시작 버튼을 눌러주세요.";
            //    pictureBox_Start.Visible = true;
            //}
            //else
            //{
            //    label_content.Text = "사용가능한 조건이 아닙니다. 관리자에게 문의해주세요.";
            //    pictureBox_Start.Visible = false;
            //}
        }


        private void button_start_Click_2(object sender, EventArgs e)
        {
            pictureBox_Start.Image = BatteryChangeCharger.Properties.Resources.Btn_s2;
            MyApplication.getInstance().Controller_Main.addEvent(CONST_EVENT.EVENT_CLICK_BUTTON_START);
        }

        private void panel_kiosk_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox_Start_MouseEnter(object sender, EventArgs e)
        {
            pictureBox_Start.Image = BatteryChangeCharger.Properties.Resources.Btn_s2;
        }

        private void pictureBox_Start_MouseLeave(object sender, EventArgs e)
        {
            pictureBox_Start.Image = BatteryChangeCharger.Properties.Resources.Btn_s1;
        }

        private void pictureBox_Start_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox_Start.Image = BatteryChangeCharger.Properties.Resources.Btn_s1;
        }
    }
}
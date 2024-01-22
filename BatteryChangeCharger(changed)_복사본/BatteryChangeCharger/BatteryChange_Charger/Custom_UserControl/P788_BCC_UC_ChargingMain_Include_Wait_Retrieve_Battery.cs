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
    public partial class P788_BCC_UC_ChargingMain_Include_Wait_Retrieve_Battery : UserControl
    {
        public P788_BCC_UC_ChargingMain_Include_Wait_Retrieve_Battery()
        {
            InitializeComponent();

            for (int i = 0; i < 8; i++)
            {
                bcC_UC_ChargingMain_Unit_kiosk1.setSlot_Soc(
                        i + 1,
                        -1
                    );
            }
        }

        public BCC_UC_ChargingMain_Unit_kiosk UC_ChargingMain_Unit_Kiosk
        {
            get { return bcC_UC_ChargingMain_Unit_kiosk1; }
        }

        public void setContent_Retrieve_Battery_1st(int socketNumber)
        {
            pictureBox1.Visible = true;
            circularProgressBar1.Visible = true;
            label1.Visible = true;
            circularProgressBar1.Value = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[socketNumber - 1].mPacket_c1_Receive.SOC;
            // bunifuRadialGauge1.ValueByTransition = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[socketNumber - 1].mPacket_c1_Receive.SOC_S1;
            label_content.Text = "화면에 표시된 첫번째 배터리팩을\n인출하여 주십시오.";
        }

        public void setContent_Retrieve_Battery_1st_Complete()
        {
            pictureBox1.Visible = false;
            circularProgressBar1.Visible = false;
            label1.Visible = false;
            label_content.Text = "첫번째 배터리가\n인출되었습니다.";
        }

        public void setContent_Retrieve_Battery_2nd(int socketNumber)
        {
            pictureBox1.Visible = true;
            circularProgressBar1.Visible = true;
            label1.Visible = true;
            // bunifuRadialGauge1.ValueByTransition = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[socketNumber - 1].mPacket_c1_Receive.SOC_S1;
            circularProgressBar1.Value = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[socketNumber - 1].mPacket_c1_Receive.SOC;
            label_content.Text = "화면에 표시된 두번째 배터리팩을\n인출하여 주십시오.";
        }

        public void setContent_Retrieve_Battery(int socketNumber)
        {
            pictureBox1.Visible = true;
            circularProgressBar1.Visible = true;
            label1.Visible = true;
            // bunifuRadialGauge1.ValueByTransition = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[socketNumber - 1].mPacket_c1_Receive.SOC_S1;
            circularProgressBar1.Value = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[socketNumber - 1].mPacket_c1_Receive.SOC;
            label_content.Text = "화면에 표시된 두번째 배터리팩을\n인출하여 주십시오.";
        }

        public void setContent_Retrieve_Battery_2nd_Complete()
        {
            pictureBox1.Visible = false;
            circularProgressBar1.Visible = false;
            label1.Visible = false;
            label_content.Text = "두번째 배터리가\n인출되었습니다.";
        }
    }
}

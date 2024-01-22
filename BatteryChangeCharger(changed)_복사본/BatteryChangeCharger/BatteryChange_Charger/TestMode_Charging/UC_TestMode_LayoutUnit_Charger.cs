using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Controller;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BatteryChangeCharger.BatteryChange_Charger.TestMode_Charging
{
    public partial class UC_TestMode_LayoutUnit_Charger : UserControl
    {


        public UC_TestMode_LayoutUnit_Charger()
        {
            InitializeComponent();
        }

        protected int mChargerNumber = 0;
        public UC_TestMode_LayoutUnit_Charger(int chargerNumber) : this()
        {
            mChargerNumber = chargerNumber;
            gb_main.Text = mChargerNumber + "번충전기";
        }
        public void updateView()
        {
            label_charger_Up_temp.Text = MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Up_Temp.ToString();
            label_charger_Down_temp.Text = MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Down_Temp.ToString();

            if (MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(mChargerNumber - 1))
            {
                Check_Battary_In.Text = "있음";
            }
            else
            {
                Check_Battary_In.Text = "없음";
            }

            btn_batterystate_soc.Text = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.SOC.ToString();
            btn_batterystate_soh.Text = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.SOH.ToString();
            btn_batterystate_serial.Text = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.RemainTime.ToString();
            btn_chargingstate_voltage.Text = ((double)MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.PowerPackVoltage /100).ToString();
            btn_chargingstate_current.Text = ((double)MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.PowerPackcurrent /100).ToString();

            /*MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.isBatteryExist(mChargerNumber);*/


            /*   btn_chargingstate_current.Text =
                   "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Output_Current_S10 / 100;

               NFCBoard_CommManager_Packet PACKET = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1];

               label_charger_temp.Text = "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Temperature_Battery_S1/10;
               label_sw_version.Text = "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.FW_Version;
               //label_sn.Text = "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.

               btn_batterystate_soc.Text = "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.SOC;
               btn_batterystate_soh.Text = "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.SOH;

               btn_batterystate_serial.Text = "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Serial;
               btn_batterystate_sw_ver.Text = "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.FW_Version;
               btn_batterystate_cc_cv.Text = (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.CV_S10 / 10)
                   + "/" +
                   (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.CC_S10 / 10);

               btn_batterystate_cc_cv.Text = (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Pack_In_S100/ 100)
                   + "/" +
                   (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Current_S100 / 100);


               if (MyApplication.bIsCert && MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Send.Command_ChargingControl == 1
                   && MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Temperature_Battery_S1 != 0
                   && !bIsStartCharging)
               {
                   tempValue = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Temperature_Battery_S1;
                   bIsStartCharging = true;
               }*/


            /* if (MyApplication.bIsCert)
             {
                 if(bIsStartCharging)
                 {
                     int temp = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Temperature_Battery_S1;
                     int diff = 0;
                     if (temp >= tempValue)
                     {
                         int tempValueAdjust = (tempValue % 1000)/100*100;
                         int battValueAdjust = (temp % 1000)/100*100;
                         diff = ((battValueAdjust - tempValueAdjust) % 1000) / 100 * 100;
                     }else
                     {
                         tempValue = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Temperature_Battery_S1;
                     }


                     commandValue  = (400 + diff + (temp % 100))/10.0f;
                     btn_batterystate_temp.Text = "" + commandValue;

                     if(commandValue > 52)
                     {
                         btn_chargingfinish_Click(null, EventArgs.Empty);
                     }
                 }
                 else
                 {
                     btn_batterystate_temp.Text = "0";
                 }
             }
             else
             {
                 btn_batterystate_temp.Text = "" + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Receive.Temperature_Battery_S1/10.0f;
             }*/

        }



      

        private void rb_chargingmode_normal_CheckedChanged(object sender, EventArgs e)
        {

        }


        int tempValue = 0;
        bool bIsStartCharging = false;
        float commandValue = 0.0f;
        private void btn_chargingstart_Click(object sender, EventArgs e)
        {
            MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Send.Command_Charging(false);
            MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Send.Output = false;
        }

        private void btn_chargingfinish_Click(object sender, EventArgs e)
        {
            MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Send.Command_Charging(true);
        }

        private void btn_init_Click(object sender, EventArgs e)
        {
            MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Send.Output = true;
        }

        private void btn_door_open_Click(object sender, EventArgs e)
        {
            MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(mChargerNumber);
            // CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_Test.ini", "Door", "Open" , true);
        }

        private void btn_door_close_Click(object sender, EventArgs e)
        {
            MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Close(mChargerNumber);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChargerNumber - 1].mPacket_c1_Send.BatteryWakeup = true;
        }
    }
}

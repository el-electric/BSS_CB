using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board.Packets
{
    public class NFCBoard_Packet_c1_Send : NFCBoard_Packet_Base_Send
    {
        
        public NFCBoard_Packet_c1_Send(MyApplication application, int channelIndex) : base(application, channelIndex, CONST_NFC_Board.LENGTH_VD_HMI2BOARD_c1, new byte[] { (byte)'z', (byte)'1' })
        {
            
        }


       

        public int mSetting_Voltage = 5800;
        public int mSetting_Current = 980;
        public bool Board_Reset = false;
        public bool IsFAN = false;
        public bool HMI_Manual_Mode = false;
        public bool Ignorez1Command;
        public bool dooropen;
         
        public bool LED_Blue;
        public bool LED_Green;
        public bool LED_Red;
        public bool BatteryWakeup;
        public bool BatteryOutput;
        public bool Output;
        public int request_Voltage;
        public int request_Wattage;
        public int Setting_Voltage
        {
            get { return mSetting_Voltage; }
        }

        public int Setting_Current
        {
            get { return mSetting_Current; }
        }

        public override void send_ApplyData()
        {
            send_mSendData[14] = 0;
            if (Board_Reset)
                send_mSendData[14] |= 0x80;
            if (IsFAN)
                send_mSendData[14] |= 0x08;
            if (HMI_Manual_Mode)
                send_mSendData[14] |= 0x04;

            send_mSendData[17] = 0;
            if (Ignorez1Command)
                send_mSendData[17] |= 0x04;
            if (dooropen)
                send_mSendData[17] |= 0x02;

            send_mSendData[19] = 0;
            if (LED_Blue)
                send_mSendData[19] |= 0x04;
            if (LED_Green)
                send_mSendData[19] |= 0x02;
            if (LED_Red)
                send_mSendData[19] |= 0x01;

            send_mSendData[20] = 0;
            if (BatteryWakeup)
                send_mSendData[20] |= 0x10;
            if (BatteryOutput)
            {
                if (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[mChannelIndex].mPacket_c1_Receive.SOC >= 95)
                {
                    Command_Charging(false);
                }
                else { send_mSendData[20] |= 0x04; }
            }
            if (Output)
                send_mSendData[20] |= 0x08;

            send_mSendData[21] = (byte)((mSetting_Voltage >> 8) & 0x000000ff);
            send_mSendData[22] = (byte)((mSetting_Voltage) & 0x000000ff);

            send_mSendData[23] = (byte)((mSetting_Current >> 8) & 0x000000ff);
            send_mSendData[24] = (byte)((mSetting_Current) & 0x000000ff);
        }


        public void Command_Charging(bool start)
        {
            if (start)
            {
                mSetting_Voltage = 5800;
                mSetting_Current = 980;
                BatteryOutput = true;
            }
            else if(!start)
            {
                mSetting_Voltage = 5800;
                mSetting_Current = 980;
                BatteryOutput = false;
            }
        }
    }
}

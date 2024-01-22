using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.CRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IO_Board.Packet
{
    abstract public class IOBoard_Packet_Base_Send : IOBoard_Packet_Base
    {
        protected byte[] mCmd = null;
        public IOBoard_Packet_Base_Send(MyApplication application, int channelIndex, int length_vd, byte[] cmd) : base(application, channelIndex, length_vd)
        {
            mCmd = cmd;

            send_mSendData = new byte[CONST_IO_Board.LENGTH_DEFUALT + 2 + CONST_IO_Board.LENGTH_RD_HMI2BOARD + length_vd];

            send_mSendData[CONST_IO_Board.INDEX_STX] = 0xfe;
            send_mSendData[1] = 0;
            send_mSendData[2] = 0;
            send_mSendData[3] = 0;

            send_mSendData[4] = 1;
            send_mSendData[5] = 0;
            send_mSendData[6] = (byte)'M';
            send_mSendData[7] = (byte)'M';
            send_mSendData[8] = mCmd[0];
            send_mSendData[9] = mCmd[1];

            

            send_mSendData[12] = 0;
            send_mSendData[13] = 1;

            send_mSendData[15] = 0;
            send_mSendData[16] = 1;

            send_mSendData[17] = 0;

            send_mSendData[send_mSendData.Length-1] = CONST_IO_Board.VALUE_ETX;
        }

        


        public byte[] SendData
        {
            get {

                if (mCmd[0] == 122 && mCmd[1] == 49)
                {
                    send_mSendData[10] = 0;
                    send_mSendData[11] = 7;
                }
                else if (mCmd[0] == 99 && mCmd[1] == 98)
                {
                    send_mSendData[10] = 0;
                    send_mSendData[11] = 35;

                    send_mSendData[20] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.Modem_Power_Reset))
                        send_mSendData[20] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.Pc_Power_Reset))
                        send_mSendData[20] |= 0x20;

                    send_mSendData[23] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT1_Door_Open))
                        send_mSendData[23] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT1_NFC_Power_Reset))
                        send_mSendData[23] |= 0x20;

                    send_mSendData[26] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT2_Door_Open))
                        send_mSendData[26] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT2_NFC_Power_Reset))
                        send_mSendData[26] |= 0x20;

                    send_mSendData[29] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT3_Door_Open))
                        send_mSendData[29] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT3_NFC_Power_Reset))
                        send_mSendData[29] |= 0x20;

                    send_mSendData[32] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT4_Door_Open))
                        send_mSendData[32] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT4_NFC_Power_Reset))
                        send_mSendData[32] |= 0x20;

                    send_mSendData[35] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT5_Door_Open))
                        send_mSendData[35] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT5_NFC_Power_Reset))
                        send_mSendData[35] |= 0x20;

                    send_mSendData[38] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT6_Door_Open))
                        send_mSendData[38] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT6_NFC_Power_Reset))
                        send_mSendData[38] |= 0x20;

                    send_mSendData[41] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT7_Door_Open))
                        send_mSendData[41] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT7_NFC_Power_Reset))
                        send_mSendData[41] |= 0x20;

                    send_mSendData[44] = 0;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT8_Door_Open))
                        send_mSendData[44] |= 0x80;
                    if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT8_NFC_Power_Reset))
                        send_mSendData[44] |= 0x20;
                }

                send_setCheckSum();
                return send_mSendData; }
        }
        protected byte[] send_mSendData = null;

        protected void send_setCheckSum()
        {
            byte[] temp = CRC16.getCRC16_CCITT(send_mSendData, 0, send_mSendData.Length);
            send_mSendData[send_mSendData.Length - 3] = temp[0];
            send_mSendData[send_mSendData.Length - 2] = temp[1];
        }

        virtual public void send_ApplyData()
        {
            /*byte valueCheck = 0x00000008;
            int index = 0;
            if (mApplication.SerialPort_IOBoard.getManager_Send().IsInitComplete_a1_Send)
                send_mSendData[CONST_IO_Board.INDEX_RD + index] = (byte)(send_mSendData[CONST_IO_Board.INDEX_RD + index] | valueCheck);
            else
                send_mSendData[CONST_IO_Board.INDEX_RD + index] = (byte)(send_mSendData[CONST_IO_Board.INDEX_RD + index] & ~valueCheck);*/
                

        }
    }
}
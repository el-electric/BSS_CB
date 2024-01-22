using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IO_Board.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard.Packet
{
    public class IOBoard_Packet_a1_Send : IOBoard_Packet_Base_Send
    {
        public IOBoard_Packet_a1_Send(MyApplication application, int channelIndex)
            : base(application, channelIndex, CONST_IO_Board.LENGTH_VD_HMI2BOARD_cb, new byte[] { (byte)'c', (byte)'b' })
        {
            send_mSendData[20] = 0;
            if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.Modem_Power_Reset))
                send_mSendData[20] |= 0x80;
            if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.Pc_Power_Reset))
                send_mSendData[20] |= 0x20;

            send_mSendData[23] = 0;
            if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT1_Door_Open))
                send_mSendData[23] |= 0x80;
            if(mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT1_NFC_Power_Reset))
                send_mSendData[23] |= 0x20;

            send_mSendData[26] = 0;
            if (mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT2_Door_Open))
                send_mSendData[26] |= 0x80;
            if(mApplication.Manager_SettingData_Main.getData_Bool_To_Bool(IOBoard_cb_Send.SLOT2_NFC_Power_Reset))
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

    }
}

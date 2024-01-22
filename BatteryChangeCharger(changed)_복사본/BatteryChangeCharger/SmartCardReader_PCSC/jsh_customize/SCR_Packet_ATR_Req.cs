using Acs.Readers.Pcsc;
using ACS.SmartCard.Reader.PCSC.Nfc;
using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    public class SCR_Packet_ATR_Req : SCR_Packet_Base_Send
    {
        
        public SCR_Packet_ATR_Req(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            mApdu.data = new byte[43];
        }

        public override void init()
        {
            
        }

        public override void send_ApplyData()
        {
            //byte[] uAtrInfoByte = {
            //    /*정승현*/0x01,0xFE,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
            //    /*원본*///0x01,0xFE,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x0A,
            //    /*정승현*/mSendData_DID,/*0x05*/
            //                        /*BS, BR, PP(변함없음)*/0x00,0x00,0x32,
            //                        /*Gi 길이 정승현*/20,
            //                        /*Gi 데이터 정승현*/0x46,0x66,0x6D,0x01,0x01,0x11,0x02,0x02,0x07,0x80,
            //                                            0x03,0x02,0x00,0x03,0x04,0x01,0x32,0x07,0x01,0x03};
            ///*Gi 데이터 원본(길이포함)*///0x0D,0x46,0x66,0x6D,0x01,0x01, 0x11,0x03,0x02,0x00,0x13,0x04,0x01,0x96}; // ATR information for ATR Request

            int iAtrInfoByte = 35;

            mApdu.data[0] = 0xE0;
            mApdu.data[1] = 0x00;
            mApdu.data[2] = 0x00;
            mApdu.data[3] = 0x42;
            mApdu.data[4] = (byte)(3 + iAtrInfoByte);
            mApdu.data[5] = 0x11;
            mApdu.data[6] = (byte)OPERATION_MODE.ACTIVE;
            mApdu.data[7] = (byte)CONNECTION_SPEED.KBPS_424;

            mApdu.data[8] = 0x01;
            mApdu.data[9] = 0xFE;
            mApdu.data[10] = 0x00;
            mApdu.data[11] = 0x00;
            mApdu.data[12] = 0x00;
            mApdu.data[13] = 0x00;
            mApdu.data[14] = 0x00;
            mApdu.data[15] = 0x00;
            mApdu.data[16] = 0x00;
            mApdu.data[17] = 0x00;
            mApdu.data[18] = mSendData_DID;
            mApdu.data[19] = 0x00;
            mApdu.data[20] = 0x00;
            mApdu.data[21] = 0x32;
            mApdu.data[22] = 20;
            mApdu.data[23] = 0x46;
            mApdu.data[24] = 0x66;
            mApdu.data[25] = 0x6D;
            mApdu.data[26] = 0x01;
            mApdu.data[27] = 0x01;
            mApdu.data[28] = 0x11;
            mApdu.data[29] = 0x02;
            mApdu.data[30] = 0x02;
            mApdu.data[31] = 0x07;
            mApdu.data[32] = 0x80;
            mApdu.data[33] = 0x03;
            mApdu.data[34] = 0x02;
            mApdu.data[35] = 0x00;
            mApdu.data[36] = 0x03;
            mApdu.data[37] = 0x04;
            mApdu.data[38] = 0x01;
            mApdu.data[39] = 0x32;
            mApdu.data[40] = 0x07;
            mApdu.data[41] = 0x01;
            mApdu.data[42] = 0x03;
    }
    }
}

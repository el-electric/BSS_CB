using BatteryChangeCharger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    abstract public class SCR_Packet_Base_Send_Dep : SCR_Packet_Base_Send
    {
        protected const int INDEX_START_DEP = 10;

        protected int mIndex_Length_Dep = 0;

        protected int mLength_Dep = 0;
        protected byte mCmd = 0;
        protected SCR_Packet_Base_Send_Dep(MyApplication application, int channelIndex, int length_Dep, byte cmd) : base(application, channelIndex)
        {
            mLength_Dep = length_Dep;
            mCmd = cmd;
            mApdu.data = new byte[8 + mLength_Dep];

            mApdu.data[0] = 0xE0;
            mApdu.data[1] = 0x00;
            mApdu.data[2] = 0x00;
            mApdu.data[3] = 0x43;

            mApdu.data[4] = (byte)(mLength_Dep + 3);//LC len//(byte)(sendDep);
            mApdu.data[5] = 0x11;
            mApdu.data[6] = mSendData_PFB;//PFB// (byte)(0x00 | (_pni & 0x03));  // PFB
            mApdu.data[7] = (byte)(mLength_Dep - 1);//DepLen //0x02;

            ////////////////////////////////////////////////
            //////////////////////DEP//////////////////////////
            ////////////////////////////////////////////////
            mApdu.data[8] = mSendData_DID;;//DID
            mApdu.data[9] = (byte)(mLength_Dep - 1);//length
            mIndex_Length_Dep = 9;
            mApdu.data[10] = mCmd;//cmd
        }


    }
}

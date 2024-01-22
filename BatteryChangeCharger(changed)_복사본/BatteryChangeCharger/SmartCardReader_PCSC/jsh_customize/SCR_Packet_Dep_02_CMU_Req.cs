using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    public class SCR_Packet_Dep_02_CMU_Req : SCR_Packet_Base_Send_Dep
    {
        protected const int LENGTH_DEP = 24;
        protected const byte CMD = 0x12;
        public SCR_Packet_Dep_02_CMU_Req(MyApplication application, int channelIndex) : base(application, channelIndex, LENGTH_DEP, CMD)
        {

        }

        public override void init()
        {

        }

        public override void send_ApplyData()
        {
            
            int sum = 0;

            for (int i = mIndex_Length_Dep; i < mApdu.data.Length - 2; i++)
            {
                sum += mApdu.data[i];
            }

            mApdu.data[mApdu.data.Length - 2] = (byte)(sum & 0x000000ff);
            mApdu.data[mApdu.data.Length - 1] = (byte)((sum >> 8) & 0x000000ff);
        }




    }
}

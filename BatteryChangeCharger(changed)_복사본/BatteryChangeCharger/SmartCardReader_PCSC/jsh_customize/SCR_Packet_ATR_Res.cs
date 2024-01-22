using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    public class SCR_Packet_ATR_Res : SCR_Packet_Base_Receive
    {
        
        public SCR_Packet_ATR_Res(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }

        public override void init()
        {

        }

        public override void receive_ApplyData(byte[] receiveData)
        {
            
        }
    }
}

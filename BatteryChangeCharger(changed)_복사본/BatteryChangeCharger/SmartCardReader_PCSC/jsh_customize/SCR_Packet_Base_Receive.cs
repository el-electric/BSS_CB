using BatteryChangeCharger.Applications;
using BatteryChangeCharger.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    abstract public class SCR_Packet_Base_Receive : SCR_Packet_Base
    {
        protected SCR_Packet_Base_Receive(MyApplication application, int channelIndex) : base(application, channelIndex)
        {

        }
        protected byte[] mReceiveData = null;
        protected JSH_Time mTime_Receive = new JSH_Time();
        virtual public void receive_ApplyData(byte[] receiveData)
        {
            mReceiveData = receiveData;
            mTime_Receive.setTime();
        }
    }
}

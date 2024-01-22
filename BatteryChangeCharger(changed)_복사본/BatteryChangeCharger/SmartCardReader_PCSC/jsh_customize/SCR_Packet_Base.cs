using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    abstract public class SCR_Packet_Base : CObject_Channel
    {
        protected SCR_Packet_Base(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }
    }
}

using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.ControlManager
{
    public class BCC_ControlManager_SocketCharging : CObject_Channel
    {
        public BCC_ControlManager_SocketCharging(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }

        public override void init()
        {
            throw new NotImplementedException();
        }
    }
}

using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using BatteryChangeCharger.Controller;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatteryChangeCharger.DataManager
{
    abstract public class DataManager_Channel_Base : CObject_Channel
    {
        public DataManager_Channel_Base(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }

        protected Controller_Base mController_State = null;
        public Controller_Base Controller_State
        {
            get { return mController_State; }
        }

    }
}

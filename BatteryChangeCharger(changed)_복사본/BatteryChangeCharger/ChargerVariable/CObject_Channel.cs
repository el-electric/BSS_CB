using BatteryChangeCharger.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatteryChangeCharger.ChargerVariable
{
    abstract public class CObject_Channel : CObject
    {
        protected int mChannelIndex = 0;


        protected CObject_Channel(MyApplication application, int channelIndex) : base(application)
        {
            mChannelIndex = channelIndex;
        }

        virtual public bool isConnected_Comm()
        {
            return true;
        }
    }
}

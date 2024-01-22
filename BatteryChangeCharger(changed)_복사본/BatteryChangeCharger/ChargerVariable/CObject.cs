using BatteryChangeCharger.Applications;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatteryChangeCharger.ChargerVariable
{
    abstract public class CObject
    {
        protected MyApplication mApplication = null;
        public CObject(MyApplication application)
        {
            mApplication = application;
        }

        abstract public void init();


        protected bool flagCheck(byte data, byte value)
        {
            bool temp = false;
            if ((data & 0x01) != 0)
                temp = true;
            else
                temp = false;
            return temp;
        }
    }
}

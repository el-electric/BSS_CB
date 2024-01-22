using BatteryChangeCharger.Applications;
using BatteryChangeCharger.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.Controller
{
    public class BCC_Socket_Controller : Controller_Base
    {

        protected bool bCommand_ChargingStart = false;
        public bool COMMAND_ChargingStart
        {
            get { return bCommand_ChargingStart; }
            set { bCommand_ChargingStart = value; }
        }


        public BCC_Socket_Controller(MyApplication application) : base(application, 0)
        {
            setTime_NextTime(60);
        }

        public override void process()
        {
            
        }
    }
}

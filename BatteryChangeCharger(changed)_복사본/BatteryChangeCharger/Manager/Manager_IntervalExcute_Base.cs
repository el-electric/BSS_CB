using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using ParkingControlCharger.Object;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ParkingControlCharger.baseClass
{
    abstract public class Manager_IntervalExcute_Base : CObject
    {
        protected int mInterval = 0;

        protected Thread mThread = null;

        public Manager_IntervalExcute_Base(MyApplication application, int interval) : base(application) {
            mInterval = interval;
            if (mInterval > 0) {
                bCommand_Stop = false;
                mThread = new Thread(run);
                mThread.IsBackground = true;
            }

            
        }

        private void run()
        {
            while(!bCommand_Stop)
            {
                timer_SendMessage_Tick();
                Thread.Sleep(mInterval);
            }
        }

        abstract protected void timer_SendMessage_Tick();

        public void start() {
            if (mInterval > 0)
            {
                bCommand_Stop = false; ;
                mThread.Start();
            }
            
        }
        public void stop()
        {
            bCommand_Stop = true;
            if(mThread != null)
            {
                mThread.Abort();
                mThread = null;
            }
        }

        protected bool bCommand_Stop = false;
    }
}

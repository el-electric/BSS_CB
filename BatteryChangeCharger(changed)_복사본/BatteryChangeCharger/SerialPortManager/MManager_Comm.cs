using BatteryChangeCharger.Applications;
using ParkingControlCharger.baseClass;
using System;

namespace ParkingControlCharger.Object
{
    abstract public class MManager_Comm : Manager_IntervalExcute_Base
    {
        protected int mInterval_Send = 0;
        protected int mCount_Failed_Attempt_Comm = 0;

        protected int mSecond_CommFault_SW = 60;
        public int Second_Fault
        {
            get { return mSecond_CommFault_SW; }
            set { mSecond_CommFault_SW = value; }
        }
        
        protected DateTime mDateTime_LastComm = DateTime.Now;

        protected string mPath_Commport = "";
        protected bool bIsConnected_HW = false;
        protected bool bIsConnected_SW = false;
        public bool IsConnected_SW
        {
            get { return bIsConnected_SW; }
        }

        public MManager_Comm(MyApplication application, int sendInterval)
            : base(application, sendInterval) {
            mInterval_Send = sendInterval;
        }

        public bool isExist_SerialPort(string path)
        {
            if (path == null || path.Length < 4)
                return false;

            string[] comlist = System.IO.Ports.SerialPort.GetPortNames();
            bool isExist_Port = false;
            for (int i = 0; i < comlist.Length; i++)
            {
                if (comlist[i].Equals(path))
                {
                    isExist_Port = true;
                    break;
                }
            }
            return isExist_Port;
        }


        protected void setLastComm() {
            mDateTime_LastComm = DateTime.Now;
            mCount_Failed_Attempt_Comm = 0;
            bIsConnected_SW = true;
        }

        virtual public bool isConnected_Comm() {
            TimeSpan span = DateTime.Now - mDateTime_LastComm;
            if (span.TotalMinutes >= 1) return false;
            return true;
        }

        public virtual bool write(byte[] data) {
            if (!bIsConnected) {
                commClose();
                commOpen();
                return false;
            }
            return bIsConnected;
        }

        public bool isPossible_SerialPort() {
            string[] devices = System.IO.Ports.SerialPort.GetPortNames();

            if (devices == null && devices.Length < 1) return false;

            for (int i = 0; i < devices.Length; i++)
                if (devices[i].Equals(getPath_SerialPort()))
                    return true;
            return false;
        }

        public void destroy() {
            stop();
            commClose();
        }

        abstract public void commOpen();
        abstract public void commClose();
        

        protected int[] mTemp_Data = null;

        abstract public string getPath_SerialPort();

        protected bool bIsReceiveData = false;
        public bool isReceiveData() => bIsReceiveData;

        protected bool bIsConnected = false;
        public bool isConnected() => bIsConnected;
    }
}

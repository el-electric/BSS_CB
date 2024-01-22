using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Controller;
using BatteryChangeCharger.ChargerVariable;
using BatteryChangeCharger.Manager;
using BatteryChangeCharger.Queue;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace BatteryChangeCharger.Controller
{
    abstract public class Controller_Base : CObject_Channel
    {
        protected bool bIsCommand_UsingStart = false;
        protected bool bIsCommand_Certification = false;



        //protected bool bIsCommand_Certification = false;





        protected int mMode = 0;


        protected int mTemp_Event = 0;
        protected EventQueue mQueue = new EventQueue(0);


        public void addEvent(int eventNum)
        {
            mQueue.insert(eventNum);
        }

        public Controller_Base(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            for (int i = 0; i < mTimes.Length; i++)
                mTimes[i] = new JSH_Time();
        }

        public void setMode(int mode)
        {
            mMode = mode;
            for (int i = 0; i < mTimes.Length; i++)
            {
                mTimes[i].setTime();
            }
        }

        public void setTime_NextTime(int sec)
        {
            mTime_NextStep_Sec = sec;
        }

        override public void init()
        {
            setMode(0);
        }


        abstract public void process();


        protected bool isTimer(int timerIndex)
        {
            switch (timerIndex)
            {
                case TIMER_NEXTSTEP:
                    if (mTimes[timerIndex].getSecond_WastedTime() >= mTime_NextStep_Sec)
                    {
                        mTimes[timerIndex].setTime();
                        return true;
                    }
                    break;
                default:
                    if (mTimes[timerIndex].getMiliSecond_WastedTime() >= TIME_SEC[timerIndex])
                    {
                        mTimes[timerIndex].setTime();
                        return true;
                    }
                    break;
            }
            return false;
        }

        protected int mTime_NextStep_Sec = 0;

        protected static int[] TIME_SEC = {0, 100, 200, 300, 500,
            1000, 3000, 5000, 10000, 7000,
            15000
        };

        protected JSH_Time[] mTimes = new JSH_Time[TIME_SEC.Length];

        protected const int TIMER_NEXTSTEP = 0;
        protected const int TIMER_100MS = 1;
        protected const int TIMER_200MS = 2;
        protected const int TIMER_300MS = 3;
        protected const int TIMER_500MS = 4;
        protected const int TIMER_1S = 5;
        protected const int TIMER_3S = 6;
        protected const int TIMER_5S = 7;
        protected const int TIMER_10S = 8;
        protected const int TIMER_7S = 9;

        protected const int TIMER_15S = 10;

        public Nullable<DateTime> Test_Time = null;

        public void test_timer(int time)
        {
            if (Test_Time != null && Test_Time.Value.AddMinutes(5) <= DateTime.Now)
            {
                CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Charger_Info", "Time", DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmm"));
                CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Charger", "Up_Temp", MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Up_Temp);
                CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Charger", "Down_Temp", MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Down_Temp);

                for (int i = 1; i < 9; i++)
                {
                    CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Slot_ " + i.ToString(), "Battery_Set", MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatterArrive);
                    CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Slot_ "+ i.ToString(), "PowerPackVoltage" ,(MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.PowerPackVoltage / 100));
                    CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Slot_ " + i.ToString(), "PowerPackCurrent", (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.PowerPackcurrent / 100));
                    CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Slot_ " + i.ToString(), "BatteryMaxTemp", MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatteryMaxTemper);
                    CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Slot_ " + i.ToString(), "BatteryMinTemp", MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatteryMinTemper);
                    CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Slot_ " + i.ToString(), "SOC", MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i-1].mPacket_c1_Receive.SOC);
                    CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Slot_" + i.ToString(), "SOH", MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.SOH);
                    CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\Log_For_File.ini", "Slot_" + i.ToString(), "Remain_Time", MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.RemainTime);
                }

            }
        }


    }
}

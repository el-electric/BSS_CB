using System;
using System.Collections.Generic;
using System.Text;

namespace BatteryChangeCharger.Manager
{
    public class JSH_Time
    {
        protected DateTime mTime;
        public JSH_Time() => mTime = DateTime.Now;
        public JSH_Time(DateTime time) => mTime = time;
        public DateTime getTime() => mTime;
        public void setTime(int year, int month, int day, int hour, int minute, int second) => mTime = new DateTime(year, month, day, hour, minute, second);

        public string getDate() => mTime.ToString("yyyyMMdd");
        public string getDateShort() => mTime.ToString("yyMMdd");
        public int getDate_Int() => Manager_Conversion.getInt(getDate());
        public string getDateTime() => mTime.ToString("yyyyMMddHHmmss");

        public string getDateTime_DB() => mTime.ToString("yyyy-MM-dd HH:mm:ss");

        public byte[] getTime_Bcd_7Bytes()
        {
            string n = mTime.ToString("yyyyMMddHHmmss");
            return Manager_Bcd.stringToBCD(n);
        }

        public void setTime(DateTime time) => mTime = time;
        public void setTime() => mTime = DateTime.Now;
        public double getSecond_WastedTime()
        {
            if (mTime == null) return 0; // 

            TimeSpan result = DateTime.Now - mTime;
            double second = result.TotalSeconds; // 총 지난 초의 시간
            if (second < 0)  
            {
                mTime = DateTime.Now; // mtime은 현제 시간
                return 0;
            }

            return second;
        }

        public string getSecond_WastedTime_hhMMss()
        {
            int second = (int)getSecond_WastedTime();
            int temp = second / 3600;
            string text = "";
            if (temp < 10)
                text = "0";

            text += temp;

            text += ":";

            temp = (second % 3600)/60;

            if (temp < 10)
                text += "0";

            text += temp;

            text += ":";

            temp = (second % 60);

            if (temp < 10)
                text += "0";

            text += temp;

            return text;
        }

        public double getSecond_WastedTime(JSH_Time time) => getSecond_WastedTime(time.getTime());

        public double getSecond_WastedTime(DateTime time)
        {
            if (time == null) return 0;

            if (mTime == null) return 0;

            TimeSpan result = time - mTime;
            double second = result.TotalSeconds;
            if (second < 0)
            {
                mTime = DateTime.Now;
                return 0;
            }

            return second;
        }

        public double getMiliSecond_WastedTime()
        {
            if (mTime == null) return 0;

            TimeSpan result = DateTime.Now - mTime;
            double second = result.TotalMilliseconds;
            if (second < 0)
            {
                mTime = DateTime.Now;
                return 0;
            }

            return second;
        }

        public double getMiliSecond_WastedTime(JSH_Time time) => getMiliSecond_WastedTime(time.getTime());

        public double getMiliSecond_WastedTime(DateTime time)
        {
            if (time == null) return 0;

            if (mTime == null) return 0;

            TimeSpan result = time - mTime;
            double second = result.TotalMilliseconds;
            if (second < 0)
            {
                mTime = DateTime.Now;
                return 0;
            }

            return second;
        }
    }
}

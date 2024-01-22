using System;
using System.Collections.Generic;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.Controller
{
    public class BCC_MODE_MAIN
    {
        public const int MODE_BOOT_ON = 0;
        public const int MODE_FIRST_SETTING = 10000;
        public const int MODE_SELF_DIAGNOSIS_MAIN = 20000;

        public const int MODE_SELF_DIAGNOSIS_MAIN_TEST = 20010;

        public const int MODE_SELF_DIAGNOSIS_AUTO = 21000;
        public const int MODE_SELF_DIAGNOSIS_MANUAL = 22000;

        public const int MODE_PREPARE = 30000;

        public const int MODE_MAIN = 40000;

        public const int MODE_MAIN_QRCODE = 50000;

        public const int MODE_MAIN_WAIT_INSERT_BATTERY = 60000;

        public const int MODE_MAIN_WAIT_INSERT_BATTERY_FIRST = 60010;
        public const int MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE = 60020;
        public const int MODE_MAIN_WAIT_INSERT_BATTERY_SECOND = 60030;
        public const int MODE_MAIN_WAIT_INSERT_BATTERY_SECOND_COMPLETE = 60040;

        public const int MODE_MAIN_PROCESS_CERTIFICATION_BATTERY = 70000;

        public const int MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY = 80000;
        public const int MODE_MAIN_WAIT_FAILED_CERTIFICATION_BATTERY = 82000;
        public const int MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY = 82100;

        public const int MODE_MAIN_WAIT_RETRIEVE_BATTERY = 90000;

        public const int MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST = 90010;
        public const int MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE = 90020;
        public const int MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND = 90030;
        public const int MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND_COMPLETE = 90040;

        public const int MODE_MAIN_USE_COMPLETE = 100000;



        public const int MODE_RESTART_PROGRAM = 900000;

        public const int MODE_RESTART_SYSTEM = 910000;

    }
}

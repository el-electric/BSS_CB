using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCTVPlayer.Database
{
    public class Manager_Table_MainSetting : Manager_Table_Setting
    {

        readonly string[][] SETTING_DATA = new string[][]
        {
            //////////////////////////////////////////////////////////
            new string[]{"PATH_SERIAL_IOBOARD", "" },
            new string[]{"PATH_SERIAL_NFC_MAIN", "COM5" },
            new string[]{"PATH_SERIAL_NFC_LEFT", "" },
            new string[]{"PATH_SERIAL_NFC_RIGHT", "" },
            new string[]{"IS_COMPLETE_FIRSTSETTING", "0" },

            //////////////////////////////////////////////////////////
            new string[]{"LIMIT_HIGH_CONTROL_TEMPERATURE", "40" },
            new string[]{"LIMIT_HIGH_OUTPUT_VOLTAGE", "59" },
            new string[]{"LIMIT_LOW_OUTPUT_VOLTAGE", "30" },
            new string[]{"LIMIT_HIGH_OUTPUT_CURRENT", "20" },
            new string[]{"LIMIT_LOW_OUTPUT_CURRENT", "1" },

            //////////////////////////////////////////////////////////
            new string[]{"BATTERYCONTROL_SOC_CHARGINGSTART", "20" },
            new string[]{"BATTERYCONTROL_SOC_CHARGINGSTOP", "80" },
            new string[]{"BATTERYCONTROL_TEMPERATURE_STOP_OUTPUT", "45" },
            new string[]{"BATTERYCONTROL_TEMPERATURE_START_CONTROL", "40" },
            new string[]{"BATTERYCONTROL_CONTROLSTEP_CURRENT_PERCENT", "20" },

            //////////////////////////////////////////////////////////
            new string[]{"BATTERYCONTROL_CONTROLSTEP_TEMPERATURE", "2" },
        };
    
        public Manager_Table_MainSetting(Manager_SQLite manager_SQLite, string tableName, bool maketable) : base(manager_SQLite, tableName, maketable)
        {

        }

        public override string[][] getData_FirstSetting()
        {
            return SETTING_DATA;
        }

    }
}

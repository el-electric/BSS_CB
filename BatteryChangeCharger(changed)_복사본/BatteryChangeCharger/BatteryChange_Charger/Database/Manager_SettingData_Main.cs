using BatteryChangeCharger.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Database
{
    public class Manager_SettingData_Main : Database_Setting_Base
    {
        private const string FILEPATH = @"..\BCC_Setting_Main.db";
        private const string TABLENAME = "Table_BCC_Setting_Main";

        private readonly static string[][] CULUMN = new string[][]
        {
            new string[]{"PATH_SERIAL_IOBOARD", "" },
            new string[]{"PATH_SERIAL_NFC_MAIN", "COM5" },
            new string[]{"PATH_SERIAL_NFC_LEFT", "" },
            new string[]{"PATH_SERIAL_NFC_RIGHT", "" },
            new string[]{"IS_COMPLETE_FIRSTSETTING", "0" },

            new string[]{"LIMIT_HIGH_CONTROL_TEMPERATURE", "40" },
            new string[]{"LIMIT_HIGH_OUTPUT_VOLTAGE", "59" },
            new string[]{"LIMIT_LOW_OUTPUT_VOLTAGE", "30" },
            new string[]{"LIMIT_HIGH_OUTPUT_CURRENT", "20" },
            new string[]{"LIMIT_LOW_OUTPUT_CURRENT", "1" },

            new string[]{"BATTERYCONTROL_SOC_CHARGINGSTART", "20" },
            new string[]{"BATTERYCONTROL_SOC_CHARGINGSTOP", "80" },
            new string[]{"BATTERYCONTROL_TEMPERATURE_STOP_OUTPUT", "45" },
            new string[]{"BATTERYCONTROL_TEMPERATURE_START_CONTROL", "40" },
            new string[]{"BATTERYCONTROL_CONTROLSTEP_CURRENT_PERCENT", "20" },

            new string[]{"BATTERYCONTROL_CONTROLSTEP_TEMPERATURE", "2" },

            new string[]{ "IS_DOOR_CONTROL_OPEN_STOP_SWITCH", "0" },
            new string[]{ "IS_DOOR_CONTROL_CLOSE_STOP_SWITCH", "0" },
            new string[]{ "IS_DOOR_CONTROL_AUTO_OPEN_STOP", "0" },
            new string[]{ "IS_DOOR_CONTROL_AUTO_CLOSE_STOP", "0" },

            new string[]{ "IS_DOOR_CONTROL_SENSING_OBJECT", "0" },
            new string[]{ "TIME_DOOR_AUTO_OPEN", "100" },
            new string[]{ "TIME_DOOR_AUTO_CLOSE", "100" },

            
            
        };


    

        public Manager_SettingData_Main() : base(FILEPATH, TABLENAME)
        {
        }

        public override string[][] getCulumnContents()
        {
            return CULUMN;
        }
    }


    public static class EINDEX_SETTING_MAIN
    {
        public const int PATH_SERIAL_IOBOARD = 0;
        public const int PATH_SERIAL_NFC_MAIN = 1;
        public const int PATH_SERIAL_NFC_LEFT = 2;
        public const int PATH_SERIAL_NFC_RIGHT = 3;
        public const int IS_COMPLETE_FIRSTSETTING = 4;
    }

    public static class IOBoard_cb_Send
    {
        public static bool Modem_Power_Reset = false;
        public static bool Pc_Power_Reset = false;

        public static bool SLOT1_Door_Open = false;
        public static bool SLOT1_NFC_Power_Reset = false;

        public static bool SLOT2_Door_Open = false;
        public static bool SLOT2_NFC_Power_Reset = false;

        public static bool SLOT3_Door_Open = false;
        public static bool SLOT3_NFC_Power_Reset = false;

        public static bool SLOT4_Door_Open = false;
        public static bool SLOT4_NFC_Power_Reset = false;

        public static bool SLOT5_Door_Open = false;
        public static bool SLOT5_NFC_Power_Reset = false;

        public static bool SLOT6_Door_Open = false;
        public static bool SLOT6_NFC_Power_Reset = false;

        public static bool SLOT7_Door_Open = false;
        public static bool SLOT7_NFC_Power_Reset = false;

        public static bool SLOT8_Door_Open = false;
        public static bool SLOT8_NFC_Power_Reset = false;
    }

    

    public static class IOBoard_1z_Receive
    {
        public static bool VibrationWarning = false;

        public static bool floodingWarning = false;
        public static bool floodingDanger = false;

        public static int DIP_Switch_Check;
        public static int Charger_Up_Temp;
        public static int Charger_Down_Temp;
        public static int Charger_humidity;
        public static int VibrationSenor_Value;
        public static int LightSensor_Value;
        public static int FW_Version_Major;
        public static int FW_Version_Minor;
        public static int FW_Version_Patch;
        public static int PT_Version_Major;
        public static int PT_Version_Minor;
        public static int PT_Version_Patch;
    }

    

    
}

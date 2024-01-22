using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IO_Board.Packet;
using BatteryChangeCharger.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard.Packet
{
    public class IOBoard_Packet_z1_Receive : IOBoard_Packet_Base_Receive
    {

        public static bool VibrationWarning = false;

        public static bool floodingWarning = false;
        public static bool floodingDanger = false;

        public int DIP_Switch_Check;
        public int Charger_Up_Temp;
        public int Charger_Down_Temp;
        public int Charger_humidity;
        public int VibrationSenor_Value;
        public int LightSensor_Value;
        public int FW_Version_Major;
        public int FW_Version_Minor;
        public int FW_Version_Patch;
        public int PT_Version_Major;
        public int PT_Version_Minor;
        public int PT_Version_Patch;

        private int index;


        public IOBoard_Packet_z1_Receive(MyApplication application, int channelIndex) : base(application, channelIndex)
        {

        }

        public override void receive_ApplyData(byte[] receiveData)
        {
            base.receive_ApplyData(receiveData);

            mApplication.Manager_SettingData_Main.SetData_Bool_To_Bool(IOBoard_1z_Receive.VibrationWarning, Manager_Conversion.getFlagByByteArray(receiveData[20], 0));
            mApplication.Manager_SettingData_Main.SetData_Bool_To_Bool(IOBoard_1z_Receive.floodingDanger, Manager_Conversion.getFlagByByteArray(receiveData[20], 6));
            mApplication.Manager_SettingData_Main.SetData_Bool_To_Bool(IOBoard_1z_Receive.floodingWarning, Manager_Conversion.getFlagByByteArray(receiveData[20], 7));

            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.DIP_Switch_Check, Manager_Conversion.getInt(receiveData[22]));

            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.Charger_Up_Temp, Manager_Conversion.getInt_2Byte(receiveData[23], receiveData[24]));
            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.Charger_Down_Temp, Manager_Conversion.getInt_2Byte(receiveData[25], receiveData[26]));

            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.Charger_humidity, Manager_Conversion.getInt(receiveData[27]));

            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.VibrationSenor_Value, Manager_Conversion.getInt_2Byte(receiveData[30], receiveData[31]));
            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.LightSensor_Value, Manager_Conversion.getInt_2Byte(receiveData[32], receiveData[33]));

            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.FW_Version_Major, Manager_Conversion.getInt(receiveData[32]));
            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.FW_Version_Minor, Manager_Conversion.getInt(receiveData[33]));
            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.FW_Version_Patch, Manager_Conversion.getInt(receiveData[34]));
            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.PT_Version_Major, Manager_Conversion.getInt(receiveData[35]));
            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.PT_Version_Minor, Manager_Conversion.getInt(receiveData[36]));
            mApplication.Manager_SettingData_Main.setData_Int_To_Int(IOBoard_1z_Receive.PT_Version_Patch, Manager_Conversion.getInt(receiveData[37]));


            index = 20;
            VibrationWarning = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            floodingWarning = Manager_Conversion.getFlagByByteArray(receiveData[index], 6);
            floodingDanger = Manager_Conversion.getFlagByByteArray(receiveData[index], 7);

            index = 22;
            DIP_Switch_Check = Manager_Conversion.getInt(receiveData[index]);

            Charger_Up_Temp = Manager_Conversion.getInt_2Byte(receiveData[23], receiveData[24]);
            Charger_Down_Temp = Manager_Conversion.getInt_2Byte(receiveData[25], receiveData[26]);

            Charger_humidity = Manager_Conversion.getInt(receiveData[27]);

            VibrationSenor_Value = Manager_Conversion.getInt_2Byte(receiveData[28], receiveData[29]);

            LightSensor_Value = Manager_Conversion.getInt_2Byte(receiveData[30], receiveData[31]);





        }

        
    }
}

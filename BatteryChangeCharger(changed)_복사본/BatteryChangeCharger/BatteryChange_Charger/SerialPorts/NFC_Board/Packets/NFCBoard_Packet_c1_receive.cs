using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board.Packets
{
    public class NFCBoard_Packet_c1_receive : NFCBoard_Packet_Base_Receive
    {
        protected byte[] temp = new byte[2];
        public NFCBoard_Packet_c1_receive(MyApplication application, int channelIndex) : base(application, channelIndex)
        {

        }

        public bool PowerPackStatus;
        public int PowerPackVoltage;
        public int PowerPackVoltage1;
        public int PowerPackcurrent;
        public int PowerPackWattage1;
        public int BatteryCurrentVoltage;
        public int BatteryCurrentWattage;
        public int BatteryRequestVoltage;
        public int BatteryRequestWattage;
        public int BatteryMaxTemper;
        public int BatteryMinTemper;
        public int ChargingStatus;
        public int ErrorCode;
        public int ProcessStatus;
        public int SOC;
        public int SOH;
        public int RemainTime;
        public int firmWareVersion_Major;
        public int firmWareVersion_Minor;
        public int firmWareVersion_Patch;
        public int protocolVersion_Major;
        public int protocolVersion_Minor;
        public int protocolVersion_Patch;
        public string BatteryType = "";

        public bool overCharging;
        public bool overDischarge;
        public bool packOverCharging;
        public bool cellOverCharging;
        public bool packHighVoltage;
        public bool packLowVoltage;
        public bool highVoltage;
        public bool rowVoltage;

        public bool FET_HighTemp;
        public bool FET_LowTemp;
        public bool cell_HighTemp;
        public bool cell_LowTemp;
        public bool reCycleOverCharging;

        public bool overChargingProtection;
        public bool overDischargeProtection;
        public bool packRecycleOverChargingProtection;
        public bool cellRecycleOverChargingProtection;
        public bool packHighVoltageProtection;
        public bool packLowVoltageProtection;
        public bool highVoltageProtection;
        public bool lowVoltageProtection;

        public override void receive_ApplyData(byte[] receiveData)
        {
            base.receive_ApplyData(receiveData);

            PowerPackStatus = Manager_Conversion.getFlagByByteArray(receiveData[20], 7);
            PowerPackVoltage = Manager_Conversion.getInt_2Byte(receiveData[22], receiveData[21]);
            PowerPackcurrent = Manager_Conversion.getInt_2Byte(receiveData[24], receiveData[23]);
            /*PowerPackVoltage = receiveData[21];
            PowerPackVoltage1 = receiveData[22];
            PowerPackWattage = receiveData[23];
            PowerPackWattage1 = receiveData[24];
            Console.Write(receiveData[23]);
            Console.Write(receiveData[24]);*/
            BatteryCurrentVoltage = Manager_Conversion.getInt_2Byte(receiveData[25], receiveData[26]);
            BatteryCurrentWattage = Manager_Conversion.getInt_2Byte(receiveData[27], receiveData[28]);
            BatteryRequestVoltage = Manager_Conversion.getInt_2Byte(receiveData[29], receiveData[30]);
            BatteryRequestWattage = Manager_Conversion.getInt_2Byte(receiveData[31], receiveData[32]);
            BatteryMaxTemper = Manager_Conversion.getInt_2Byte(receiveData[33], receiveData[34]);
            BatteryMinTemper = Manager_Conversion.getInt_2Byte(receiveData[35], receiveData[36]);


            if (!MyApplication.getInstance().Manual_ProcessStep)
                ChargingStatus = Manager_Conversion.getInt(receiveData[37]);
            else
                ChargingStatus = 100;

            ErrorCode = Manager_Conversion.getInt_2Byte(receiveData[38], receiveData[39]);
            SOC = Manager_Conversion.getInt(receiveData[40]);
            SOH = Manager_Conversion.getInt(receiveData[41]);
            RemainTime = (Manager_Conversion.getInt_2Byte(receiveData[43], receiveData[42]) / 60);
            firmWareVersion_Major = Manager_Conversion.getInt(receiveData[44]);
            firmWareVersion_Minor = Manager_Conversion.getInt(receiveData[45]);
            firmWareVersion_Patch = Manager_Conversion.getInt(receiveData[46]);
            protocolVersion_Major = Manager_Conversion.getInt(receiveData[47]);
            protocolVersion_Minor = Manager_Conversion.getInt(receiveData[48]);
            protocolVersion_Patch = Manager_Conversion.getInt(receiveData[49]);
            temp[0] = receiveData[50];
            temp[1] = receiveData[51];
            BatteryType = Manager_Conversion.ByteArrayToString(temp);
        }




        //////////fDet1


    }
}
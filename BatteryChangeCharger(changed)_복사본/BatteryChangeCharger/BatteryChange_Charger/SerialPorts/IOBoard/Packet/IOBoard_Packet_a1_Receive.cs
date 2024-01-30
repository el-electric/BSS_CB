using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IO_Board.Packet;
using BatteryChangeCharger.Manager;
using EL_DC_Charger.ocpp.ver16.datatype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard.Packet
{
    public class IOBoard_Packet_a1_Receive : IOBoard_Packet_Base_Receive
    {
        private int index;
        public IOBoard_Packet_a1_Receive(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }

        public override void receive_ApplyData(byte[] receiveData)
        {
            base.receive_ApplyData(receiveData);

            Check_MCCB_Trip = Manager_Conversion.getFlagByByteArray(receiveData[20], 0);
            Charger_Temp = receiveData[23];

            index = 28;
            Check_SLOT1_Door_Open = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            Check_SLOT1_Door_Close = Manager_Conversion.getFlagByByteArray(receiveData[index], 1);
            Check_SLOT1_Battery_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 2);
            Check_SLOT1_Other_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 3);

            index = 31;
            Check_SLOT2_Door_Open = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            Check_SLOT2_Door_Close = Manager_Conversion.getFlagByByteArray(receiveData[index], 1);
            Check_SLOT2_Battery_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 2);
            Check_SLOT2_Other_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 3);

            index = 34;
            Check_SLOT3_Door_Open = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            Check_SLOT3_Door_Close = Manager_Conversion.getFlagByByteArray(receiveData[index], 1);
            Check_SLOT3_Battery_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 2);
            Check_SLOT3_Other_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 3);

            index = 37;
            Check_SLOT4_Door_Open = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            Check_SLOT4_Door_Close = Manager_Conversion.getFlagByByteArray(receiveData[index], 1);

            if (!MyApplication.getInstance().Manual_BatterArrive)
                Check_SLOT4_Battery_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 2);
            else
                Check_SLOT4_Battery_In = true;


            if (Check_SLOT4_Battery_In && MyApplication.getInstance().bIsStep_Main)
            { MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Preparing); }
            else if (!Check_SLOT4_Battery_In && MyApplication.getInstance().bIsStep_Main)
            { MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Available); }
            else
            {}

            Check_SLOT4_Other_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 3);

            index = 40;
            Check_SLOT5_Door_Open = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            Check_SLOT5_Door_Close = Manager_Conversion.getFlagByByteArray(receiveData[index], 1);
            Check_SLOT5_Battery_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 2);
            Check_SLOT5_Other_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 3);

            index = 43;
            Check_SLOT6_Door_Open = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            Check_SLOT6_Door_Close = Manager_Conversion.getFlagByByteArray(receiveData[index], 1);
            Check_SLOT6_Battery_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 2);
            Check_SLOT6_Other_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 3);

            index = 46;
            Check_SLOT7_Door_Open = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            Check_SLOT7_Door_Close = Manager_Conversion.getFlagByByteArray(receiveData[index], 1);
            Check_SLOT7_Battery_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 2);
            Check_SLOT5_Other_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 3);

            index = 49;
            Check_SLOT8_Door_Open = Manager_Conversion.getFlagByByteArray(receiveData[index], 0);
            Check_SLOT8_Door_Close = Manager_Conversion.getFlagByByteArray(receiveData[index], 1);
            Check_SLOT8_Battery_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 2);
            Check_SLOT8_Other_In = Manager_Conversion.getFlagByByteArray(receiveData[index], 3);
        }


        public bool Check_MCCB_Trip = false;

        public int Charger_Temp;

        public bool Check_SLOT1_Door_Open = false;
        public bool Check_SLOT1_Door_Close = false;
        public bool Check_SLOT1_Battery_In = false;
        public bool Check_SLOT1_Other_In = false;

        public bool Check_SLOT2_Door_Open = false;
        public bool Check_SLOT2_Door_Close = false;
        public bool Check_SLOT2_Battery_In = false;
        public bool Check_SLOT2_Other_In = false;

        public bool Check_SLOT3_Door_Open = false;
        public bool Check_SLOT3_Door_Close = false;
        public bool Check_SLOT3_Battery_In = false;
        public bool Check_SLOT3_Other_In = false;

        public bool Check_SLOT4_Door_Open = false;
        public bool Check_SLOT4_Door_Close = false;
        public bool Check_SLOT4_Battery_In = false;
        public bool Check_SLOT4_Other_In = false;

        public bool Check_SLOT5_Door_Open = false;
        public bool Check_SLOT5_Door_Close = false;
        public bool Check_SLOT5_Battery_In = false;
        public bool Check_SLOT5_Other_In = false;

        public bool Check_SLOT6_Door_Open = false;
        public bool Check_SLOT6_Door_Close = false;
        public bool Check_SLOT6_Battery_In = false;
        public bool Check_SLOT6_Other_In = false;

        public bool Check_SLOT7_Door_Open = false;
        public bool Check_SLOT7_Door_Close = false;
        public bool Check_SLOT7_Battery_In = false;
        public bool Check_SLOT7_Other_In = false;

        public bool Check_SLOT8_Door_Open = false;
        public bool Check_SLOT8_Door_Close = false;
        public bool Check_SLOT8_Battery_In = false;
        public bool Check_SLOT8_Other_In = false;


        public int get_Can_Use_Battery_Slot()
        {
            if (!Check_SLOT1_Battery_In)
            { return 0; }
            else if (!Check_SLOT2_Battery_In)
            { return 1; }
            else if (!Check_SLOT3_Battery_In)
            { return 2; }
            else if (!Check_SLOT4_Battery_In)
            { return 3; }
            else if (!Check_SLOT5_Battery_In)
            { return 4; }
            else if (!Check_SLOT6_Battery_In)
            { return 5; }
            else if (!Check_SLOT7_Battery_In)
            { return 6; }
            else if (!Check_SLOT8_Battery_In)
            { return 7; }
            else
            { return 8; }
        }



        public bool Check_Slot_battery_In(int slotnum)
        {
            switch (slotnum)
            {
                case 0:
                    if (Check_SLOT1_Battery_In)
                    { return true; }
                    else
                    { return false; }
                    break;
                case 1:
                    if (Check_SLOT2_Battery_In)
                    { return true; }
                    else
                    { return false; }
                    break;
                case 2:
                    if (Check_SLOT3_Battery_In)
                    { return true; }
                    else
                    { return false; }
                    break;
                case 3:
                    if (Check_SLOT4_Battery_In)
                    { return true; }
                    else
                    { return false; }
                    break;
                case 4:
                    if (Check_SLOT5_Battery_In)
                    { return true; }
                    else
                    { return false; }
                    break;
                case 5:
                    if (Check_SLOT6_Battery_In)
                    { return true; }
                    else
                    { return false; }
                    break;
                case 6:
                    if (Check_SLOT7_Battery_In)
                    { return true; }
                    else
                    { return false; }
                    break;
                case 7:
                    if (Check_SLOT8_Battery_In)
                    { return true; }
                    else
                    { return false; }
                    break;
                default:
                    return false;
                    break;
            }

        }

    }
}

using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    public class SCR_Packet_Dep_01_BMU_Res : SCR_Packet_Base_Receive
    {

        public SCR_Packet_Dep_01_BMU_Res(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }

        public override void init()
        {

        }

        public bool isCanCharging()
        {
            if (Data_30_fsts_1bit_Charge_FET_ON
                || Data_30_fsts_2bit_Enable_Charge)
                return true;

            return false;
        }


        public override void receive_ApplyData(byte[] receiveData)
        {
            base.receive_ApplyData(receiveData);
            int indexArray = 14;//MF_Year
            Data_10_Mf_Year_Scale_1 = (receiveData[indexArray] & 0x000000ff) + 2000;

            indexArray++;//
            Data_11_Mf_mon_Scale_1 = (receiveData[indexArray] & 0x000000ff);

            indexArray++;//
            Data_12_Mf_day_Scale_1 = (receiveData[indexArray] & 0x000000ff);

            indexArray++;//dep 13 serial
            Data_13_Serial_Scale_1 = ((receiveData[indexArray+1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            indexArray++;//14
            indexArray++;//15
            indexArray++;//16
            indexArray++;//17
            indexArray++;//18
            indexArray++;//19
            indexArray++;//20
            indexArray++;//21

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//22
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_22_fDet1_0bit_Over_Voltage_Protection = true;
            else
                Data_22_fDet1_0bit_Over_Voltage_Protection = false;
                       
            if ((receiveData[indexArray] & 0x02) != 0)
                Data_22_fDet1_1bit_Under_Voltage_Protection = true;
            else
                Data_22_fDet1_1bit_Under_Voltage_Protection = false;

            if ((receiveData[indexArray] & 0x04) != 0)
                Data_22_fDet1_2bit_Charge_Over_Current = true;
            else
                Data_22_fDet1_2bit_Charge_Over_Current = false;

            if ((receiveData[indexArray] & 0x08) != 0)
                Data_22_fDet1_3bit_Discharge_Overcurrent = true;
            else
                Data_22_fDet1_3bit_Discharge_Overcurrent = false;

            if ((receiveData[indexArray] & 0x10) != 0)
                Data_22_fDet1_4bit_Charge_Over_Temp_Protection = true;
            else
                Data_22_fDet1_4bit_Charge_Over_Temp_Protection = false;

            if ((receiveData[indexArray] & 0x20) != 0)
                Data_22_fDet1_5bit_Discharge_Over_Temp_Protection = true;
            else
                Data_22_fDet1_5bit_Discharge_Over_Temp_Protection = false;

            if ((receiveData[indexArray] & 0x40) != 0)
                Data_22_fDet1_6bit_Short_Protection = true;
            else
                Data_22_fDet1_6bit_Short_Protection = false;

            if ((receiveData[indexArray] & 0x80) != 0)
                Data_22_fDet1_7bit_FET_Over_Temp_Protection = true;
            else
                Data_22_fDet1_7bit_FET_Over_Temp_Protection = false;

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//23
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_23_fDet2_0bit_Charge_Under_Temp_Protection = true;
            else
                Data_23_fDet2_0bit_Charge_Under_Temp_Protection = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_23_fDet2_1bit_Discharge_Under_Temp_Protection = true;
            else
                Data_23_fDet2_1bit_Discharge_Under_Temp_Protection = false;

            if ((receiveData[indexArray] & 0x10) != 0)
                Data_23_fDet2_4bit_Cell_Unbancing_Protection = true;
            else
                Data_23_fDet2_4bit_Cell_Unbancing_Protection = false;


            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//24
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_24_fAlm1_0bit_Over_Votage = true;
            else
                Data_24_fAlm1_0bit_Over_Votage = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_24_fAlm1_1bit_Under_Voltage = true;
            else
                Data_24_fAlm1_1bit_Under_Voltage = false;

            if ((receiveData[indexArray] & 0x04) != 0)
                Data_24_fAlm1_2bit_Charge_Over_Current = true;
            else
                Data_24_fAlm1_2bit_Charge_Over_Current = false;

            if ((receiveData[indexArray] & 0x08) != 0)
                Data_24_fAlm1_3bit_Discharge_overcurrent = true;
            else
                Data_24_fAlm1_3bit_Discharge_overcurrent = false;

            if ((receiveData[indexArray] & 0x10) != 0)
                Data_24_fAlm1_4bit_Charge_Over_Temp = true;
            else
                Data_24_fAlm1_4bit_Charge_Over_Temp = false;

            if ((receiveData[indexArray] & 0x20) != 0)
                Data_24_fAlm1_5bit_Discharge_Over_Temp = true;
            else
                Data_24_fAlm1_5bit_Discharge_Over_Temp = false;

            if ((receiveData[indexArray] & 0x8) != 0)
                Data_24_fAlm1_7bit_FET_Over_Temp = true;
            else
                Data_24_fAlm1_7bit_FET_Over_Temp = false;



            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//25
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_25_fAlm2_0bit_Charge_Under_Temp = true;
            else
                Data_25_fAlm2_0bit_Charge_Under_Temp = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_25_fAlm2_1bit_Discharge_Under_Temp = true;
            else
                Data_25_fAlm2_1bit_Discharge_Under_Temp = false;

            if ((receiveData[indexArray] & 0x10) != 0)
                Data_25_fAlm2_4bit_Cell_Unbalancing = true;
            else
                Data_25_fAlm2_4bit_Cell_Unbalancing = false;




            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//26
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_26_fBal1_0bit_1_Cel_Balancing = true;
            else
                Data_26_fBal1_0bit_1_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_26_fBal1_1bit_2_Cel_Balancing = true;
            else
                Data_26_fBal1_1bit_2_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x04) != 0)
                Data_26_fBal1_2bit_3_Cel_Balancing = true;
            else
                Data_26_fBal1_2bit_3_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x08) != 0)
                Data_26_fBal1_3bit_4_Cel_Balancing = true;
            else
                Data_26_fBal1_3bit_4_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x10) != 0)
                Data_26_fBal1_4bit_5_Cel_Balancing= true;
            else
                Data_26_fBal1_4bit_5_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x20) != 0)
                Data_26_fBal1_5bit_6_Cel_Balancing= true;
            else
                Data_26_fBal1_5bit_6_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x40) != 0)
                Data_26_fBal1_6bit_7_Cel_Balancing = true;
            else
                Data_26_fBal1_6bit_7_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x80) != 0)
                Data_26_fBal1_7bit_8_Cel_Balancing= true;
            else
                Data_26_fBal1_7bit_8_Cel_Balancing = false;


            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//27
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_27_fBal2_0bit_9_Cel_Balancing = true;
            else
                Data_27_fBal2_0bit_9_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_27_fBal2_1bit_10_Cel_Balancing= true;
            else
                Data_27_fBal2_1bit_10_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x04) != 0)
                Data_27_fBal2_2bit_11_Cel_Balancing = true;
            else
                Data_27_fBal2_2bit_11_Cel_Balancing = false;


            if ((receiveData[indexArray] & 0x08) != 0)
                Data_27_fBal2_3bit_12_Cel_Balancing = true;
            else
                Data_27_fBal2_3bit_12_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x10) != 0)
                Data_27_fBal2_4bit_13_Cel_Balancing = true;
            else
                Data_27_fBal2_4bit_13_Cel_Balancing = false;

            if ((receiveData[indexArray] & 0x20) != 0)
                Data_27_fBal2_5bit_14_Cel_Balancing = true;
            else
                Data_27_fBal2_5bit_14_Cel_Balancing = false;



            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//28
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_28_fbmssts1_0bit_Sleepmode_Normal = true;
            else
                Data_28_fbmssts1_0bit_Sleepmode_Normal = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_28_fbmssts1_1bit_Power_Down_By_Cell_Low_Voltage = true;
            else
                Data_28_fbmssts1_1bit_Power_Down_By_Cell_Low_Voltage = false;

            if ((receiveData[indexArray] & 0x04) != 0)
                Data_28_fbmssts1_2bit_Power_Down_By_Command = true;
            else
                Data_28_fbmssts1_2bit_Power_Down_By_Command = false;

            if ((receiveData[indexArray] & 0x08) != 0)
                Data_28_fbmssts1_3bit_Power_Down_By_Long_Time_Storage = true;
            else
                Data_28_fbmssts1_3bit_Power_Down_By_Long_Time_Storage = false;

            if ((receiveData[indexArray] & 0x10) != 0)
                Data_28_fbmssts1_4bit_Communication_With_Charger = true;
            else
                Data_28_fbmssts1_4bit_Communication_With_Charger = false;

            if ((receiveData[indexArray] & 0x20) != 0)
                Data_28_fbmssts1_5bit_Communication_With_Scooter_A = true;
            else
                Data_28_fbmssts1_5bit_Communication_With_Scooter_A = false;

            if ((receiveData[indexArray] & 0x40) != 0)
                Data_28_fbmssts1_6bit_Communication_With_Scooter_B = true;
            else
                Data_28_fbmssts1_6bit_Communication_With_Scooter_B = false;

            if ((receiveData[indexArray] & 0x80) != 0)
                Data_28_fbmssts1_7bit_Communication_With_SW = true;
            else
                Data_28_fbmssts1_7bit_Communication_With_SW = false;


            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//29
            if ((receiveData[indexArray] & 0x08) != 0)
                Data_29_fbmssts2_3bit_Discharge_State = true;
            else
                Data_29_fbmssts2_3bit_Discharge_State = false;

            if ((receiveData[indexArray] & 0x10) != 0)
                Data_29_fbmssts2_4bit_Charging = true;
            else
                Data_29_fbmssts2_4bit_Charging = false;

            if ((receiveData[indexArray] & 0x20) != 0)
                Data_29_fbmssts2_5bit_Precharge_FET_ON = true;
            else
                Data_29_fbmssts2_5bit_Precharge_FET_ON = false;

            if ((receiveData[indexArray] & 0x40) != 0)
                Data_29_fbmssts2_6bit_Discharge_FET_ON = true;
            else
                Data_29_fbmssts2_6bit_Discharge_FET_ON = false;

            if ((receiveData[indexArray] & 0x80) != 0)
                Data_29_fbmssts2_7bit_Charge_FET_ON = true;
            else
                Data_29_fbmssts2_7bit_Charge_FET_ON = false;




            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//30
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_30_fsts_0bit_Discharger_FET_ON = true;
            else
                Data_30_fsts_0bit_Discharger_FET_ON = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_30_fsts_1bit_Charge_FET_ON = true;
            else
                Data_30_fsts_1bit_Charge_FET_ON = false;

            if ((receiveData[indexArray] & 0x04) != 0)
                Data_30_fsts_2bit_Enable_Charge = true;
            else
                Data_30_fsts_2bit_Enable_Charge = false;

            if ((receiveData[indexArray] & 0x08) != 0)
                Data_30_fsts_3bit_Enable_Discharge = true;
            else
                Data_30_fsts_3bit_Enable_Discharge = false;

            Data_30_fsts_4bit_4state_Fault_Level = (receiveData[indexArray] >> 4) & 0x03;

            if ((receiveData[indexArray] & 0x40) != 0)
                Data_30_fsts_6bit_Enable_Regenerate = true;
            else
                Data_30_fsts_6bit_Enable_Regenerate = false;



            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//31
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_31_fnfcerr_0bit_NFC_Length_Error = true;
            else
                Data_31_fnfcerr_0bit_NFC_Length_Error = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_31_fnfcerr_1bit_Disallowed_Device_Id = true;
            else
                Data_31_fnfcerr_1bit_Disallowed_Device_Id = false;

            if ((receiveData[indexArray] & 0x04) != 0)
                Data_31_fnfcerr_2bit_Chksum_Error = true;
            else
                Data_31_fnfcerr_2bit_Chksum_Error = false;


            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//32
            if ((receiveData[indexArray] & 0x01) != 0)
                Data_32_fbattype_0bit_Individual_Type = true;
            else
                Data_32_fbattype_0bit_Individual_Type = false;

            if ((receiveData[indexArray] & 0x02) != 0)
                Data_32_fbattype_1bit_Sharing_Type = true;
            else
                Data_32_fbattype_1bit_Sharing_Type = false;

            indexArray++;//33
            indexArray++;//34
            indexArray++;//35
            indexArray++;//36
            indexArray++;//37


            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++;//dep 38
            Data_38_Pack_In_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++;//dep 40
            Data_40_Pack_Out_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++;//dep 42
            Data_42_Current_In_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++;//dep 44

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 46
            Data_46_SOC_Scale_1 = (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; //dep 47
            Data_47_SOH_Scale_1 = (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; //dep 48
            Data_48_Thmin_Scale_10 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 50
            Data_50_Thmax_Scale_10 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 52
            Data_52_Cvmin_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 54
            Data_54_Cvmax_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 56
            Data_56_CC_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 58
            Data_58_CV_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 60
            Data_60_Max_Curr_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 62
            Data_62_Min_Volt_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 64
            Data_64_FW_Scale_1 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 66
            Data_66_HW_Scale_1 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 68
            Data_68_Remiain_Capacity = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 70
            Data_70_Cut_Off_Current_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            ////////////////////////////////////////////////////////////////////////////////////
            indexArray++; indexArray++; //dep 72
            Data_72_Remain_Charge_T_Scale_100 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

        }

        public bool isProtection()
        {
            if (mData_22_fDet1_0bit_Over_Voltage_Protection
                || mData_22_fDet1_1bit_Under_Voltage_Protection
                || mData_22_fDet1_2bit_Charge_Over_Current
                || mData_22_fDet1_3bit_Discharge_Overcurrent
                || mData_22_fDet1_4bit_Charge_Over_Temp_Protection
                || mData_22_fDet1_5bit_Discharge_Over_Temp_Protection
                || mData_22_fDet1_6bit_Short_Protection
                || mData_22_fDet1_7bit_FET_Over_Temp_Protection
                || mData_23_fDet2_0bit_Charge_Under_Temp_Protection
                || mData_23_fDet2_1bit_Discharge_Under_Temp_Protection
                || mData_23_fDet2_4bit_Cell_Unbancing_Protection)
                return true;

            return false;
        }

        public bool isAlarm()
        {
            if (mData_24_fAlm1_0bit_Over_Votage
                || mData_24_fAlm1_1bit_Under_Voltage
                || mData_24_fAlm1_2bit_Charge_Over_Current
                || mData_24_fAlm1_3bit_Discharge_overcurrent
                || mData_24_fAlm1_4bit_Charge_Over_Temp
                || mData_24_fAlm1_5bit_Discharge_Over_Temp
                || mData_24_fAlm1_7bit_FET_Over_Temp
                || mData_25_fAlm2_0bit_Charge_Under_Temp
                || mData_25_fAlm2_1bit_Discharge_Under_Temp
                || mData_25_fAlm2_4bit_Cell_Unbalancing)
                return true;

            return false;
        }


        protected int mData_10_Mf_Year_Scale_1 = 0;
        public int Data_10_Mf_Year_Scale_1
        {
            get { return mData_10_Mf_Year_Scale_1; }
            set { mData_10_Mf_Year_Scale_1 = value; }
        }

        protected int mData_11_Mf_mon_Scale_1 = 0;
        public int Data_11_Mf_mon_Scale_1
        {
            get { return mData_11_Mf_mon_Scale_1; }
            set { mData_11_Mf_mon_Scale_1 = value; }
        }

        protected int mData_12_Mf_day_Scale_1 = 0;
        public int Data_12_Mf_day_Scale_1
        {
            get { return mData_12_Mf_day_Scale_1; }
            set { mData_12_Mf_day_Scale_1 = value; }
        }

        protected int mData_13_Serial_Scale_1 = 0;
        public int Data_13_Serial_Scale_1
        {
            get { return mData_13_Serial_Scale_1; }
            set { mData_13_Serial_Scale_1 = value; }
        }



        protected bool mData_22_fDet1_0bit_Over_Voltage_Protection = false;
        public bool Data_22_fDet1_0bit_Over_Voltage_Protection
        {
            get { return mData_22_fDet1_0bit_Over_Voltage_Protection; }
            set { mData_22_fDet1_0bit_Over_Voltage_Protection = value; }
        }

        protected bool mData_22_fDet1_1bit_Under_Voltage_Protection = false;
        public bool Data_22_fDet1_1bit_Under_Voltage_Protection
        {
            get { return mData_22_fDet1_1bit_Under_Voltage_Protection; }
            set { mData_22_fDet1_1bit_Under_Voltage_Protection = value; }
        }

        protected bool mData_22_fDet1_2bit_Charge_Over_Current = false;
        public bool Data_22_fDet1_2bit_Charge_Over_Current
        {
            get { return mData_22_fDet1_2bit_Charge_Over_Current; }
            set { mData_22_fDet1_2bit_Charge_Over_Current = value; }
        }

        protected bool mData_22_fDet1_3bit_Discharge_Overcurrent = false;
        public bool Data_22_fDet1_3bit_Discharge_Overcurrent
        {
            get { return mData_22_fDet1_3bit_Discharge_Overcurrent; }
            set { mData_22_fDet1_3bit_Discharge_Overcurrent = value; }
        }

        protected bool mData_22_fDet1_4bit_Charge_Over_Temp_Protection = false;
        public bool Data_22_fDet1_4bit_Charge_Over_Temp_Protection
        {
            get { return mData_22_fDet1_4bit_Charge_Over_Temp_Protection; }
            set { mData_22_fDet1_4bit_Charge_Over_Temp_Protection = value; }
        }

        protected bool mData_22_fDet1_5bit_Discharge_Over_Temp_Protection = false;
        public bool Data_22_fDet1_5bit_Discharge_Over_Temp_Protection
        {
            get { return mData_22_fDet1_5bit_Discharge_Over_Temp_Protection; }
            set { mData_22_fDet1_5bit_Discharge_Over_Temp_Protection = value; }
        }

        protected bool mData_22_fDet1_6bit_Short_Protection = false;
        public bool Data_22_fDet1_6bit_Short_Protection
        {
            get { return mData_22_fDet1_6bit_Short_Protection; }
            set { mData_22_fDet1_6bit_Short_Protection = value; }
        }

        protected bool mData_22_fDet1_7bit_FET_Over_Temp_Protection = false;
        public bool Data_22_fDet1_7bit_FET_Over_Temp_Protection
        {
            get { return mData_22_fDet1_7bit_FET_Over_Temp_Protection; }
            set { mData_22_fDet1_7bit_FET_Over_Temp_Protection = value; }
        }
        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_23_fDet2_0bit_Charge_Under_Temp_Protection = false;
        public bool Data_23_fDet2_0bit_Charge_Under_Temp_Protection
        {
            get { return mData_23_fDet2_0bit_Charge_Under_Temp_Protection; }
            set { mData_23_fDet2_0bit_Charge_Under_Temp_Protection = value; }
        }

        protected bool mData_23_fDet2_1bit_Discharge_Under_Temp_Protection = false;
        public bool Data_23_fDet2_1bit_Discharge_Under_Temp_Protection
        {
            get { return mData_23_fDet2_1bit_Discharge_Under_Temp_Protection; }
            set { mData_23_fDet2_1bit_Discharge_Under_Temp_Protection = value; }
        }

        protected bool mData_23_fDet2_4bit_Cell_Unbancing_Protection = false;
        public bool Data_23_fDet2_4bit_Cell_Unbancing_Protection
        {
            get { return mData_23_fDet2_4bit_Cell_Unbancing_Protection; }
            set { mData_23_fDet2_4bit_Cell_Unbancing_Protection = value; }
        }
        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_24_fAlm1_0bit_Over_Votage = false;
        public bool Data_24_fAlm1_0bit_Over_Votage
        {
            get { return mData_24_fAlm1_0bit_Over_Votage; }
            set { mData_24_fAlm1_0bit_Over_Votage = value; }
        }

        protected bool mData_24_fAlm1_1bit_Under_Voltage = false;
        public bool Data_24_fAlm1_1bit_Under_Voltage
        {
            get { return mData_24_fAlm1_1bit_Under_Voltage; }
            set { mData_24_fAlm1_1bit_Under_Voltage = value; }
        }

        protected bool mData_24_fAlm1_2bit_Charge_Over_Current = false;
        public bool Data_24_fAlm1_2bit_Charge_Over_Current
        {
            get { return mData_24_fAlm1_2bit_Charge_Over_Current; }
            set { mData_24_fAlm1_2bit_Charge_Over_Current = value; }
        }

        protected bool mData_24_fAlm1_3bit_Discharge_overcurrent = false;
        public bool Data_24_fAlm1_3bit_Discharge_overcurrent
        {
            get { return mData_24_fAlm1_3bit_Discharge_overcurrent; }
            set { mData_24_fAlm1_3bit_Discharge_overcurrent = value; }
        }

        protected bool mData_24_fAlm1_4bit_Charge_Over_Temp = false;
        public bool Data_24_fAlm1_4bit_Charge_Over_Temp
        {
            get { return mData_24_fAlm1_4bit_Charge_Over_Temp; }
            set { mData_24_fAlm1_4bit_Charge_Over_Temp = value; }
        }

        protected bool mData_24_fAlm1_5bit_Discharge_Over_Temp = false;
        public bool Data_24_fAlm1_5bit_Discharge_Over_Temp
        {
            get { return mData_24_fAlm1_5bit_Discharge_Over_Temp; }
            set { mData_24_fAlm1_5bit_Discharge_Over_Temp = value; }
        }

        protected bool mData_24_fAlm1_7bit_FET_Over_Temp = false;
        public bool Data_24_fAlm1_7bit_FET_Over_Temp
        {
            get { return mData_24_fAlm1_7bit_FET_Over_Temp; }
            set { mData_24_fAlm1_7bit_FET_Over_Temp = value; }
        }
        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_25_fAlm2_0bit_Charge_Under_Temp = false;
        public bool Data_25_fAlm2_0bit_Charge_Under_Temp
        {
            get { return mData_25_fAlm2_0bit_Charge_Under_Temp; }
            set { mData_25_fAlm2_0bit_Charge_Under_Temp = value; }
        }

        protected bool mData_25_fAlm2_1bit_Discharge_Under_Temp = false;
        public bool Data_25_fAlm2_1bit_Discharge_Under_Temp
        {
            get { return mData_25_fAlm2_1bit_Discharge_Under_Temp; }
            set { mData_25_fAlm2_1bit_Discharge_Under_Temp = value; }
        }

        protected bool mData_25_fAlm2_4bit_Cell_Unbalancing = false;
        public bool Data_25_fAlm2_4bit_Cell_Unbalancing
        {
            get { return mData_25_fAlm2_4bit_Cell_Unbalancing; }
            set { mData_25_fAlm2_4bit_Cell_Unbalancing = value; }
        }

        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_26_fBal1_0bit_1_Cel_Balancing = false;
        public bool Data_26_fBal1_0bit_1_Cel_Balancing
        {
            get { return mData_26_fBal1_0bit_1_Cel_Balancing; }
            set { mData_26_fBal1_0bit_1_Cel_Balancing = value; }
        }

        protected bool mData_26_fBal1_1bit_2_Cel_Balancing = false;
        public bool Data_26_fBal1_1bit_2_Cel_Balancing
        {
            get { return mData_26_fBal1_1bit_2_Cel_Balancing; }
            set { mData_26_fBal1_1bit_2_Cel_Balancing = value; }
        }

        protected bool mData_26_fBal1_2bit_3_Cel_Balancing = false;
        public bool Data_26_fBal1_2bit_3_Cel_Balancing
        {
            get { return mData_26_fBal1_2bit_3_Cel_Balancing; }
            set { mData_26_fBal1_2bit_3_Cel_Balancing = value; }
        }

        protected bool mData_26_fBal1_3bit_4_Cel_Balancing = false;
        public bool Data_26_fBal1_3bit_4_Cel_Balancing
        {
            get { return mData_26_fBal1_3bit_4_Cel_Balancing; }
            set { mData_26_fBal1_3bit_4_Cel_Balancing = value; }
        }

        protected bool mData_26_fBal1_4bit_5_Cel_Balancing = false;
        public bool Data_26_fBal1_4bit_5_Cel_Balancing
        {
            get { return mData_26_fBal1_4bit_5_Cel_Balancing; }
            set { mData_26_fBal1_4bit_5_Cel_Balancing = value; }
        }

        protected bool mData_26_fBal1_5bit_6_Cel_Balancing = false;
        public bool Data_26_fBal1_5bit_6_Cel_Balancing
        {
            get { return mData_26_fBal1_5bit_6_Cel_Balancing; }
            set { mData_26_fBal1_5bit_6_Cel_Balancing = value; }
        }

        protected bool mData_26_fBal1_6bit_7_Cel_Balancing = false;
        public bool Data_26_fBal1_6bit_7_Cel_Balancing
        {
            get { return mData_26_fBal1_6bit_7_Cel_Balancing; }
            set { mData_26_fBal1_6bit_7_Cel_Balancing = value; }
        }

        protected bool mData_26_fBal1_7bit_8_Cel_Balancing = false;
        public bool Data_26_fBal1_7bit_8_Cel_Balancing
        {
            get { return mData_26_fBal1_7bit_8_Cel_Balancing; }
            set { mData_26_fBal1_7bit_8_Cel_Balancing = value; }
        }

        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_27_fBal2_0bit_9_Cel_Balancing = false;
        public bool Data_27_fBal2_0bit_9_Cel_Balancing
        {
            get { return mData_27_fBal2_0bit_9_Cel_Balancing; }
            set { mData_27_fBal2_0bit_9_Cel_Balancing = value; }
        }

        protected bool mData_27_fBal2_1bit_10_Cel_Balancing = false;
        public bool Data_27_fBal2_1bit_10_Cel_Balancing
        {
            get { return mData_27_fBal2_1bit_10_Cel_Balancing; }
            set { mData_27_fBal2_1bit_10_Cel_Balancing = value; }
        }

        protected bool mData_27_fBal2_2bit_11_Cel_Balancing = false;
        public bool Data_27_fBal2_2bit_11_Cel_Balancing
        {
            get { return mData_27_fBal2_2bit_11_Cel_Balancing; }
            set { mData_27_fBal2_2bit_11_Cel_Balancing = value; }
        }

        protected bool mData_27_fBal2_3bit_12_Cel_Balancing = false;
        public bool Data_27_fBal2_3bit_12_Cel_Balancing
        {
            get { return mData_27_fBal2_3bit_12_Cel_Balancing; }
            set { mData_27_fBal2_3bit_12_Cel_Balancing = value; }
        }

        protected bool mData_27_fBal2_4bit_13_Cel_Balancing = false;
        public bool Data_27_fBal2_4bit_13_Cel_Balancing
        {
            get { return mData_27_fBal2_4bit_13_Cel_Balancing; }
            set { mData_27_fBal2_4bit_13_Cel_Balancing = value; }
        }

        protected bool mData_27_fBal2_5bit_14_Cel_Balancing = false;
        public bool Data_27_fBal2_5bit_14_Cel_Balancing
        {
            get { return mData_27_fBal2_5bit_14_Cel_Balancing; }
            set { mData_27_fBal2_5bit_14_Cel_Balancing = value; }
        }

        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_28_fbmssts1_0bit_Sleepmode_Normal = false;
        public bool Data_28_fbmssts1_0bit_Sleepmode_Normal
        {
            get { return mData_28_fbmssts1_0bit_Sleepmode_Normal; }
            set { mData_28_fbmssts1_0bit_Sleepmode_Normal = value; }
        }

        protected bool mData_28_fbmssts1_1bit_Power_Down_By_Cell_Low_Voltage = false;
        public bool Data_28_fbmssts1_1bit_Power_Down_By_Cell_Low_Voltage
        {
            get { return mData_28_fbmssts1_1bit_Power_Down_By_Cell_Low_Voltage; }
            set { mData_28_fbmssts1_1bit_Power_Down_By_Cell_Low_Voltage = value; }
        }

        protected bool mData_28_fbmssts1_2bit_Power_Down_By_Command = false;
        public bool Data_28_fbmssts1_2bit_Power_Down_By_Command
        {
            get { return mData_28_fbmssts1_2bit_Power_Down_By_Command; }
            set { mData_28_fbmssts1_2bit_Power_Down_By_Command = value; }
        }

        protected bool mData_28_fbmssts1_3bit_Power_Down_By_Long_Time_Storage = false;
        public bool Data_28_fbmssts1_3bit_Power_Down_By_Long_Time_Storage
        {
            get { return mData_28_fbmssts1_3bit_Power_Down_By_Long_Time_Storage; }
            set { mData_28_fbmssts1_3bit_Power_Down_By_Long_Time_Storage = value; }
        }

        protected bool mData_28_fbmssts1_4bit_Communication_With_Charger = false;
        public bool Data_28_fbmssts1_4bit_Communication_With_Charger
        {
            get { return mData_28_fbmssts1_4bit_Communication_With_Charger; }
            set { mData_28_fbmssts1_4bit_Communication_With_Charger = value; }
        }

        protected bool mData_28_fbmssts1_5bit_Communication_With_Scooter_A = false;
        public bool Data_28_fbmssts1_5bit_Communication_With_Scooter_A
        {
            get { return mData_28_fbmssts1_5bit_Communication_With_Scooter_A; }
            set { mData_28_fbmssts1_5bit_Communication_With_Scooter_A = value; }
        }

        protected bool mData_28_fbmssts1_6bit_Communication_With_Scooter_B = false;
        public bool Data_28_fbmssts1_6bit_Communication_With_Scooter_B
        {
            get { return mData_28_fbmssts1_6bit_Communication_With_Scooter_B; }
            set { mData_28_fbmssts1_6bit_Communication_With_Scooter_B = value; }
        }

        protected bool mData_28_fbmssts1_7bit_Communication_With_SW = false;
        public bool Data_28_fbmssts1_7bit_Communication_With_SW
        {
            get { return mData_28_fbmssts1_7bit_Communication_With_SW; }
            set { mData_28_fbmssts1_7bit_Communication_With_SW = value; }
        }

        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_29_fbmssts2_3bit_Discharge_State = false;
        public bool Data_29_fbmssts2_3bit_Discharge_State
        {
            get { return mData_29_fbmssts2_3bit_Discharge_State; }
            set { mData_29_fbmssts2_3bit_Discharge_State = value; }
        }

        protected bool mData_29_fbmssts2_4bit_Charging = false;
        public bool Data_29_fbmssts2_4bit_Charging
        {
            get { return mData_29_fbmssts2_4bit_Charging; }
            set { mData_29_fbmssts2_4bit_Charging = value; }
        }

        protected bool mData_29_fbmssts2_5bit_Precharge_FET_ON = false;
        public bool Data_29_fbmssts2_5bit_Precharge_FET_ON
        {
            get { return mData_29_fbmssts2_5bit_Precharge_FET_ON; }
            set { mData_29_fbmssts2_5bit_Precharge_FET_ON = value; }
        }

        protected bool mData_29_fbmssts2_6bit_Discharge_FET_ON = false;
        public bool Data_29_fbmssts2_6bit_Discharge_FET_ON
        {
            get { return mData_29_fbmssts2_6bit_Discharge_FET_ON; }
            set { mData_29_fbmssts2_6bit_Discharge_FET_ON = value; }
        }

        protected bool mData_29_fbmssts2_7bit_Charge_FET_ON = false;
        public bool Data_29_fbmssts2_7bit_Charge_FET_ON
        {
            get { return mData_29_fbmssts2_7bit_Charge_FET_ON; }
            set { mData_29_fbmssts2_7bit_Charge_FET_ON = value; }
        }

        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_30_fsts_0bit_Discharger_FET_ON = false;
        public bool Data_30_fsts_0bit_Discharger_FET_ON
        {
            get { return mData_30_fsts_0bit_Discharger_FET_ON; }
            set { mData_30_fsts_0bit_Discharger_FET_ON = value; }
        }

        protected bool mData_30_fsts_1bit_Charge_FET_ON = false;
        public bool Data_30_fsts_1bit_Charge_FET_ON
        {
            get { return mData_30_fsts_1bit_Charge_FET_ON; }
            set { mData_30_fsts_1bit_Charge_FET_ON = value; }
        }

        protected bool mData_30_fsts_2bit_Enable_Charge = false;
        public bool Data_30_fsts_2bit_Enable_Charge
        {
            get { return mData_30_fsts_2bit_Enable_Charge; }
            set { mData_30_fsts_2bit_Enable_Charge = value; }
        }

        protected bool mData_30_fsts_3bit_Enable_Discharge = false;
        public bool Data_30_fsts_3bit_Enable_Discharge
        {
            get { return mData_30_fsts_3bit_Enable_Discharge; }
            set { mData_30_fsts_3bit_Enable_Discharge = value; }
        }

        protected int mData_30_fsts_4bit_4state_Fault_Level = 0;
        public int Data_30_fsts_4bit_4state_Fault_Level
        {
            get { return mData_30_fsts_4bit_4state_Fault_Level; }
            set { mData_30_fsts_4bit_4state_Fault_Level = value; }
        }

        protected bool mData_30_fsts_6bit_Enable_Regenerate = false;
        public bool Data_30_fsts_6bit_Enable_Regenerate
        {
            get { return mData_30_fsts_6bit_Enable_Regenerate; }
            set { mData_30_fsts_6bit_Enable_Regenerate = value; }
        }


        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_31_fnfcerr_0bit_NFC_Length_Error = false;
        public bool Data_31_fnfcerr_0bit_NFC_Length_Error
        {
            get { return mData_31_fnfcerr_0bit_NFC_Length_Error; }
            set { mData_31_fnfcerr_0bit_NFC_Length_Error = value; }
        }

        protected bool mData_31_fnfcerr_1bit_Disallowed_Device_Id = false;
        public bool Data_31_fnfcerr_1bit_Disallowed_Device_Id
        {
            get { return mData_31_fnfcerr_1bit_Disallowed_Device_Id; }
            set { mData_31_fnfcerr_1bit_Disallowed_Device_Id = value; }
        }

        protected bool mData_31_fnfcerr_2bit_Chksum_Error = false;
        public bool Data_31_fnfcerr_2bit_Chksum_Error
        {
            get { return mData_31_fnfcerr_2bit_Chksum_Error; }
            set { mData_31_fnfcerr_2bit_Chksum_Error = value; }
        }




        //////////////////////////////////////////////////////////////////////////////
        protected bool mData_32_fbattype_0bit_Individual_Type = false;
        public bool Data_32_fbattype_0bit_Individual_Type
        {
            get { return mData_32_fbattype_0bit_Individual_Type; }
            set { mData_32_fbattype_0bit_Individual_Type = value; }
        }

        protected bool mData_32_fbattype_1bit_Sharing_Type = false;
        public bool Data_32_fbattype_1bit_Sharing_Type
        {
            get { return mData_32_fbattype_1bit_Sharing_Type; }
            set { mData_32_fbattype_1bit_Sharing_Type = value; }
        }


        //////////////////////////////////////////////////////////////////////////////
        protected int mData_38_Pack_In_Scale_100 = 0;
        public int Data_38_Pack_In_Scale_100
        {
            get { return mData_38_Pack_In_Scale_100; }
            set { mData_38_Pack_In_Scale_100 = value; }
        }

        protected int mData_40_Pack_Out_Scale_100 = 0;
        public int Data_40_Pack_Out_Scale_100
        {
            get { return mData_40_Pack_Out_Scale_100; }
            set { mData_40_Pack_Out_Scale_100 = value; }
        }

        protected int mData_42_Current_In_Scale_100 = 0;
        public int Data_42_Current_In_Scale_100
        {
            get { return mData_42_Current_In_Scale_100; }
            set { mData_42_Current_In_Scale_100 = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        protected int mData_46_SOC_Scale_1 = 0;
        public int Data_46_SOC_Scale_1
        {
            get { return mData_46_SOC_Scale_1; }
            set { mData_46_SOC_Scale_1 = value; }
        }

        protected int mData_47_SOH_Scale_1 = 0;
        public int Data_47_SOH_Scale_1
        {
            get { return mData_47_SOH_Scale_1; }
            set { mData_47_SOH_Scale_1 = value; }
        }

        protected int mData_48_Thmin_Scale_10 = 0;
        public int Data_48_Thmin_Scale_10
        {
            get { return mData_48_Thmin_Scale_10; }
            set { mData_48_Thmin_Scale_10 = value; }
        }

        protected int mData_50_Thmax_Scale_10 = 0;
        public int Data_50_Thmax_Scale_10
        {
            get { return mData_50_Thmax_Scale_10; }
            set { mData_50_Thmax_Scale_10 = value; }
        }

        protected int mData_52_Cvmin_Scale_1000 = 0;
        public int Data_52_Cvmin_Scale_1000
        {
            get { return mData_52_Cvmin_Scale_1000; }
            set { mData_52_Cvmin_Scale_1000 = value; }
        }

        protected int mData_54_Cvmax_Scale_1000 = 0;
        public int Data_54_Cvmax_Scale_1000
        {
            get { return mData_54_Cvmax_Scale_1000; }
            set { mData_54_Cvmax_Scale_1000 = value; }
        }

        protected int mData_56_CC_Scale_100 = 0;
        public int Data_56_CC_Scale_100
        {
            get { return mData_56_CC_Scale_100; }
            set { mData_56_CC_Scale_100 = value; }
        }

        protected int mData_58_CV_Scale_100 = 0;
        public int Data_58_CV_Scale_100
        {
            get { return mData_58_CV_Scale_100; }
            set { mData_58_CV_Scale_100 = value; }
        }

        protected int mData_60_Max_Curr_Scale_100 = 0;
        public int Data_60_Max_Curr_Scale_100
        {
            get { return mData_60_Max_Curr_Scale_100; }
            set { mData_60_Max_Curr_Scale_100 = value; }
        }

        protected int mData_62_Min_Volt_Scale_100 = 0;
        public int Data_62_Min_Volt_Scale_100
        {
            get { return mData_62_Min_Volt_Scale_100; }
            set { mData_62_Min_Volt_Scale_100 = value; }
        }

        protected int mData_64_FW_Scale_1 = 0;
        public int Data_64_FW_Scale_1
        {
            get { return mData_64_FW_Scale_1; }
            set { mData_64_FW_Scale_1 = value; }
        }

        protected int mData_66_HW_Scale_1 = 0;
        public int Data_66_HW_Scale_1
        {
            get { return mData_66_HW_Scale_1; }
            set { mData_66_HW_Scale_1 = value; }
        }

        protected int mData_68_Remiain_Capacity = 0;
        public int Data_68_Remiain_Capacity
        {
            get { return mData_68_Remiain_Capacity; }
            set { mData_68_Remiain_Capacity = value; }
        }

        protected int mData_70_Cut_Off_Current_Scale_100 = 0;
        public int Data_70_Cut_Off_Current_Scale_100
        {
            get { return mData_70_Cut_Off_Current_Scale_100; }
            set { mData_70_Cut_Off_Current_Scale_100 = value; }
        }

        protected int mData_72_Remain_Charge_T_Scale_100 = 0;
        public int Data_72_Remain_Charge_T_Scale_100
        {
            get { return mData_72_Remain_Charge_T_Scale_100; }
            set { mData_72_Remain_Charge_T_Scale_100 = value; }
        }


    }
}

using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    public class SCR_Packet_Dep_01_BMU_Req : SCR_Packet_Base_Send_Dep
    {
        protected const int LENGTH_DEP = 80;
        protected const byte CMD = 0x11;
        public SCR_Packet_Dep_01_BMU_Req(MyApplication application, int channelIndex) : base(application, channelIndex, LENGTH_DEP, CMD)
        {
        }



        public override void init()
        {

        }

        public override void send_ApplyData()
        {
            
            int sum = 0;
            int index = 14;


            ////////////////// dep 10 SCT req1
            mApdu.data[index] = 0;
            if (mData_10_SCT_Reg1_0bit_Request_FET_ON)
                mApdu.data[index] |= 0x01;

            if (mData_10_SCT_Reg1_1bit_Ready)
                mApdu.data[index] |= 0x02;

            mApdu.data[index] |= (byte)((Data_10_SCT_Reg1_2bit_4State_Fault_Level & 0x03) << 2) ;

            ////////////////// dep 11
            index++;
            mApdu.data[index] = 0;
            if (mData_11_SCT_Reg2_0bit_Controller_Over_Temp_Fault)
                mApdu.data[index] |= 0x01;
            if (mData_11_SCT_Reg2_1bit_Controller_Over_Voltage_Fault)
                mApdu.data[index] |= 0x02;
            if (mData_11_SCT_Reg2_2bit_Controller_Under_Voltage_Fault)
                mApdu.data[index] |= 0x04;
            if (mData_11_SCT_Reg2_3bit_Motor_Stall_Protection_Failure)
                mApdu.data[index] |= 0x08;
            if (mData_11_SCT_Reg2_4bit_HALL_Failure)
                mApdu.data[index] |= 0x10;
            if (mData_11_SCT_Reg2_5bit_Motor_Controller_SelfCheck_Failure)
                mApdu.data[index] |= 0x20;
            if (mData_11_SCT_Reg2_6bit_Motor_Anti_Theft_Status)
                mApdu.data[index] |= 0x40;
            if (mData_11_SCT_Reg2_7bit_Controller_Over_Current_Fault)
                mApdu.data[index] |= 0x80;

            ////////////////// dep 12
            index++;
            mApdu.data[index] = 0;
            if (mData_12_SCT_A_Reg1_0bit_Discharge_FET_ON)
                mApdu.data[index] |= 0x01;
            if (mData_12_SCT_A_Reg1_1bit_Charge_FET_ON)
                mApdu.data[index] |= 0x02;
            if (mData_12_SCT_A_Reg1_2bit_Enable_Charge)
                mApdu.data[index] |= 0x04;
            if (mData_12_SCT_A_Reg1_3bit_Enable_Discharge)
                mApdu.data[index] |= 0x08;
            mApdu.data[index] |= (byte)((Data_10_SCT_Reg1_2bit_4State_Fault_Level & 0x03) << 4);
            if (mData_12_SCT_A_Reg1_6bit_Enable_Regenerate)
                mApdu.data[index] |= 0x40;

            ////////////////// dep 13
            index++;
            ////////////////// dep 14
            index++;
            mApdu.data[index] = 0;
            if (mData_14_SCT_B_Reg1_0bit_Discharge_FET_ON)
                mApdu.data[index] |= 0x01;
            if (mData_14_SCT_B_Reg1_1bit_Charge_FET_ON)
                mApdu.data[index] |= 0x02;
            if (mData_14_SCT_B_Reg1_2bit_Enable_Charge)
                mApdu.data[index] |= 0x04;
            if (mData_14_SCT_B_Reg1_3bit_Enable_Discharge)
                mApdu.data[index] |= 0x08;
            mApdu.data[index] |= (byte)((Data_14_SCT_B_Reg1_4bit_4State_Fault_Level & 0x03) << 4);
            if (mData_14_SCT_B_Reg1_6bit_Enable_Regenerate)
                mApdu.data[index] |= 0x40;
            ////////////////// dep 15
            index++;
            ////////////////// dep 16
            index++;
            mApdu.data[index] = (byte)(mData_16_A_Soc_Scale_1 & 0x000000ff);
            ////////////////// dep 17
            index++;
            mApdu.data[index] = (byte)(mData_17_B_Soc_Scale_1 & 0x000000ff);
            ////////////////// dep 18
            index++;
            mApdu.data[index] = (byte)(mData_18_A_Soh_Scale_1 & 0x000000ff);
            ////////////////// dep 19
            index++;
            mApdu.data[index] = (byte)(mData_19_B_Soh_Scale_1 & 0x000000ff);

            ////////////////// dep 20 21 A Voltage
            index++;
            mApdu.data[index] = (byte)((mData_20_A_Voltage_Scale_100) & 0x000000ff);
            index++;
            mApdu.data[index] = (byte)((mData_20_A_Voltage_Scale_100 >> 8) & 0x000000ff);

            ////////////////// dep 22 23 B Voltage
            index++;
            mApdu.data[index] = (byte)((mData_22_B_Voltage_Scale_100) & 0x000000ff);
            index++;
            mApdu.data[index] = (byte)((mData_22_B_Voltage_Scale_100 >> 8) & 0x000000ff);

            ////////////////// dep 24 25 A Current
            index++;
            mApdu.data[index] = (byte)((mData_24_A_Current_Scale_100) & 0x000000ff);
            index++;
            mApdu.data[index] = (byte)((mData_24_A_Current_Scale_100 >> 8) & 0x000000ff);

            ////////////////// dep 26 27 B Current
            index++;
            mApdu.data[index] = (byte)((mData_26_B_Current_Scale_100) & 0x000000ff);
            index++;
            mApdu.data[index] = (byte)((mData_26_B_Current_Scale_100 >> 8) & 0x000000ff);

            ////////////////// 28
            index++;
            ////////////////// 29
            index++;
            ////////////////// 30
            index++;
            ////////////////// 31
            index++;
            ////////////////// 32
            index++;
            ////////////////// 33
            index++;
            ////////////////// 34
            index++;

            ////////////////// dep 35 36 CC
            index++;
            mApdu.data[index] = (byte)((mData_35_CC_Scale_100) & 0x000000ff);
            index++;
            mApdu.data[index] = (byte)((mData_35_CC_Scale_100 >> 8) & 0x000000ff);

            ////////////////// dep 37 38
            index++;
            mApdu.data[index] = (byte)((mData_37_CV_Scale_100) & 0x000000ff);
            index++;
            mApdu.data[index] = (byte)((mData_37_CV_Scale_100 >> 8) & 0x000000ff);

            ////////////////// dep 39 40
            index++;
            mApdu.data[index] = (byte)((mData_39_A_Cvmin_Scale_100) & 0x000000ff);
            index++;
            mApdu.data[index] = (byte)((mData_39_A_Cvmin_Scale_100 >> 8) & 0x000000ff);

            ////////////////// dep 41 42
            index++;
            mApdu.data[index] = (byte)((mData_41_A_Cvmin_Scale_100) & 0x000000ff);
            index++;
            mApdu.data[index] = (byte)((mData_41_A_Cvmin_Scale_100 >> 8) & 0x000000ff);


            ////////////////// dep 43
            index++;
            //mApdu.data[index] = (byte)((mData_43_Counter_Scale_1) & 0x000000ff);
            //mData_43_Counter_Scale_1++;
            //if (mData_43_Counter_Scale_1 > 255) mData_43_Counter_Scale_1 = 0;




            for (int i = mIndex_Length_Dep; i < mApdu.data.Length-2; i++)
            {
                sum += mApdu.data[i];
            }

            mApdu.data[mApdu.data.Length - 2] = (byte)(sum & 0x000000ff);
            mApdu.data[mApdu.data.Length - 1] = (byte)((sum >> 8) & 0x000000ff);
        }





        protected bool mData_10_SCT_Reg1_0bit_Request_FET_ON = false;
        public bool Data_10_SCT_Reg1_0bit_Request_FET_ON
        {
            get{ return mData_10_SCT_Reg1_0bit_Request_FET_ON; }
            set { mData_10_SCT_Reg1_0bit_Request_FET_ON = value; }
        }
        protected bool mData_10_SCT_Reg1_1bit_Ready = false;
        public bool Data_10_SCT_Reg1_1bit_Ready
        {
            get { return mData_10_SCT_Reg1_1bit_Ready; }
            set { mData_10_SCT_Reg1_1bit_Ready = value; }
        }
        protected int mData_10_SCT_Reg1_2bit_4State_Fault_Level = 0;
        public int Data_10_SCT_Reg1_2bit_4State_Fault_Level
        {
            get { return mData_10_SCT_Reg1_2bit_4State_Fault_Level; }
            set { mData_10_SCT_Reg1_2bit_4State_Fault_Level = value; }
        }

        protected bool mData_11_SCT_Reg2_0bit_Controller_Over_Temp_Fault = false;
        public bool Data_11_SCT_Reg2_0bit_Controller_Over_Temp_Fault
        {
            get { return mData_11_SCT_Reg2_0bit_Controller_Over_Temp_Fault; }
            set { mData_11_SCT_Reg2_0bit_Controller_Over_Temp_Fault = value; }
        }
        protected bool mData_11_SCT_Reg2_1bit_Controller_Over_Voltage_Fault = false;
        public bool Data_11_SCT_Reg2_1bit_Controller_Over_Voltage_Fault
        {
            get { return mData_11_SCT_Reg2_1bit_Controller_Over_Voltage_Fault; }
            set { mData_11_SCT_Reg2_1bit_Controller_Over_Voltage_Fault = value; }
        }
        protected bool mData_11_SCT_Reg2_2bit_Controller_Under_Voltage_Fault = false;
        public bool Data_11_SCT_Reg2_2bit_Controller_Under_Voltage_Fault
        {
            get { return mData_11_SCT_Reg2_2bit_Controller_Under_Voltage_Fault; }
            set { mData_11_SCT_Reg2_2bit_Controller_Under_Voltage_Fault = value; }
        }
        protected bool mData_11_SCT_Reg2_3bit_Motor_Stall_Protection_Failure = false;
        public bool Data_11_SCT_Reg2_3bit_Motor_Stall_Protection_Failure
        {
            get { return mData_11_SCT_Reg2_3bit_Motor_Stall_Protection_Failure; }
            set { mData_11_SCT_Reg2_3bit_Motor_Stall_Protection_Failure = value; }
        }
        protected bool mData_11_SCT_Reg2_4bit_HALL_Failure = false;
        public bool Data_11_SCT_Reg2_4bit_HALL_Failure
        {
            get { return mData_11_SCT_Reg2_4bit_HALL_Failure; }
            set { mData_11_SCT_Reg2_4bit_HALL_Failure = value; }
        }
        protected bool mData_11_SCT_Reg2_5bit_Motor_Controller_SelfCheck_Failure = false;
        public bool Data_11_SCT_Reg2_5bit_Motor_Controller_SelfCheck_Failure
        {
            get { return mData_11_SCT_Reg2_5bit_Motor_Controller_SelfCheck_Failure; }
            set { mData_11_SCT_Reg2_5bit_Motor_Controller_SelfCheck_Failure = value; }
        }
        protected bool mData_11_SCT_Reg2_6bit_Motor_Anti_Theft_Status = false;
        public bool Data_11_SCT_Reg2_6bit_Motor_Anti_Theft_Status
        {
            get { return mData_11_SCT_Reg2_6bit_Motor_Anti_Theft_Status; }
            set { mData_11_SCT_Reg2_6bit_Motor_Anti_Theft_Status = value; }
        }
        protected bool mData_11_SCT_Reg2_7bit_Controller_Over_Current_Fault = false;
        public bool Data_11_SCT_Reg2_7bit_Controller_Over_Current_Fault
        {
            get { return mData_11_SCT_Reg2_7bit_Controller_Over_Current_Fault; }
            set { mData_11_SCT_Reg2_7bit_Controller_Over_Current_Fault = value; }
        }






        protected bool mData_12_SCT_A_Reg1_0bit_Discharge_FET_ON = false;
        public bool Data_12_SCT_A_Reg1_0bit_Discharge_FET_ON
        {
            get { return mData_12_SCT_A_Reg1_0bit_Discharge_FET_ON; }
            set { mData_12_SCT_A_Reg1_0bit_Discharge_FET_ON = value; }
        }
        protected bool mData_12_SCT_A_Reg1_1bit_Charge_FET_ON = false;
        public bool Data_12_SCT_A_Reg1_1bit_Charge_FET_ON
        {
            get { return mData_12_SCT_A_Reg1_1bit_Charge_FET_ON; }
            set { mData_12_SCT_A_Reg1_1bit_Charge_FET_ON = value; }
        }
        protected bool mData_12_SCT_A_Reg1_2bit_Enable_Charge = false;
        public bool Data_12_SCT_A_Reg1_2bit_Enable_Charge
        {
            get { return mData_12_SCT_A_Reg1_2bit_Enable_Charge; }
            set { mData_12_SCT_A_Reg1_2bit_Enable_Charge = value; }
        }
        protected bool mData_12_SCT_A_Reg1_3bit_Enable_Discharge = false;
        public bool Data_12_SCT_A_Reg1_3bit_Enable_Discharge
        {
            get { return mData_12_SCT_A_Reg1_3bit_Enable_Discharge; }
            set { mData_12_SCT_A_Reg1_3bit_Enable_Discharge = value; }
        }
        protected int mData_12_SCT_A_Reg1_4bit_4State_Fault_Level = 0;
        public int Data_12_SCT_A_Reg1_4bit_4State_Fault_Level
        {
            get { return mData_12_SCT_A_Reg1_4bit_4State_Fault_Level; }
            set { mData_12_SCT_A_Reg1_4bit_4State_Fault_Level = value; }
        }
        protected bool mData_12_SCT_A_Reg1_6bit_Enable_Regenerate = false;
        public bool Data_12_SCT_A_Reg1_6bit_Enable_Regenerate
        {
            get { return mData_12_SCT_A_Reg1_6bit_Enable_Regenerate; }
            set { mData_12_SCT_A_Reg1_6bit_Enable_Regenerate = value; }
        }



        protected bool mData_14_SCT_B_Reg1_0bit_Discharge_FET_ON = false;
        public bool Data_14_SCT_B_Reg1_0bit_Discharge_FET_ON
        {
            get { return mData_14_SCT_B_Reg1_0bit_Discharge_FET_ON; }
            set { mData_14_SCT_B_Reg1_0bit_Discharge_FET_ON = value; }
        }
        protected bool mData_14_SCT_B_Reg1_1bit_Charge_FET_ON = false;
        public bool Data_14_SCT_B_Reg1_1bit_Charge_FET_ON
        {
            get { return mData_14_SCT_B_Reg1_1bit_Charge_FET_ON; }
            set { mData_14_SCT_B_Reg1_1bit_Charge_FET_ON = value; }
        }
        protected bool mData_14_SCT_B_Reg1_2bit_Enable_Charge = false;
        public bool Data_14_SCT_B_Reg1_2bit_Enable_Charge
        {
            get { return mData_14_SCT_B_Reg1_2bit_Enable_Charge; }
            set { mData_14_SCT_B_Reg1_2bit_Enable_Charge = value; }
        }
        protected bool mData_14_SCT_B_Reg1_3bit_Enable_Discharge = false;
        public bool Data_14_SCT_B_Reg1_3bit_Enable_Discharge
        {
            get { return mData_14_SCT_B_Reg1_3bit_Enable_Discharge; }
            set { mData_14_SCT_B_Reg1_3bit_Enable_Discharge = value; }
        }
        protected int mData_14_SCT_B_Reg1_4bit_4State_Fault_Level = 0;
        public int Data_14_SCT_B_Reg1_4bit_4State_Fault_Level
        {
            get { return mData_14_SCT_B_Reg1_4bit_4State_Fault_Level; }
            set { mData_14_SCT_B_Reg1_4bit_4State_Fault_Level = value; }
        }
        protected bool mData_14_SCT_B_Reg1_6bit_Enable_Regenerate = false;
        public bool Data_14_SCT_B_Reg1_6bit_Enable_Regenerate
        {
            get { return mData_14_SCT_B_Reg1_6bit_Enable_Regenerate; }
            set { mData_14_SCT_B_Reg1_6bit_Enable_Regenerate = value; }
        }



        protected int mData_16_A_Soc_Scale_1 = 0;
        public int Data_16_A_Soc_Scale_1
        {
            get { return mData_16_A_Soc_Scale_1; }
            set { mData_16_A_Soc_Scale_1 = value; }
        }

        protected int mData_17_B_Soc_Scale_1 = 0;
        public int Data_17_B_Soc_Scale_1
        {
            get { return mData_17_B_Soc_Scale_1; }
            set { mData_17_B_Soc_Scale_1 = value; }
        }




        protected int mData_18_A_Soh_Scale_1 = 0;
        public int Data_18_A_Soh_Scale_1
        {
            get { return mData_18_A_Soh_Scale_1; }
            set { mData_18_A_Soh_Scale_1 = value; }
        }

        protected int mData_19_B_Soh_Scale_1 = 0;
        public int Data_19_B_Soh_Scale_1
        {
            get { return mData_19_B_Soh_Scale_1; }
            set { mData_19_B_Soh_Scale_1 = value; }
        }




        protected int mData_20_A_Voltage_Scale_100 = 0;
        public int Data_20_A_Voltage_Scale_100
        {
            get { return mData_20_A_Voltage_Scale_100; }
            set { mData_20_A_Voltage_Scale_100 = value; }
        }

        protected int mData_22_B_Voltage_Scale_100 = 0;
        public int Data_22_B_Voltage_Scale_100
        {
            get { return mData_22_B_Voltage_Scale_100; }
            set { mData_22_B_Voltage_Scale_100 = value; }
        }

        protected int mData_24_A_Current_Scale_100 = 0;
        public int Data_24_A_Current_Scale_100
        {
            get { return mData_24_A_Current_Scale_100; }
            set { mData_24_A_Current_Scale_100 = value; }
        }

        protected int mData_26_B_Current_Scale_100 = 0;
        public int Data_26_B_Current_Scale_100
        {
            get { return mData_26_B_Current_Scale_100; }
            set { mData_26_B_Current_Scale_100 = value; }
        }

        protected int mData_35_CC_Scale_100 = 0;
        public int Data_35_CC_Scale_100
        {
            get { return mData_35_CC_Scale_100; }
            set { mData_35_CC_Scale_100 = value; }
        }

        protected int mData_37_CV_Scale_100 = 0;
        public int Data_37_CV_Scale_100
        {
            get { return mData_37_CV_Scale_100; }
            set { mData_37_CV_Scale_100 = value; }
        }

        protected int mData_39_A_Cvmin_Scale_100 = 0;
        public int Data_39_A_Cvmin_Scale_100
        {
            get { return mData_39_A_Cvmin_Scale_100; }
            set { mData_39_A_Cvmin_Scale_100 = value; }
        }


        protected int mData_41_A_Cvmin_Scale_100 = 0;
        public int Data_41_A_Cvmin_Scale_100
        {
            get { return mData_39_A_Cvmin_Scale_100; }
            set { mData_39_A_Cvmin_Scale_100 = value; }
        }

        protected int mData_43_Counter_Scale_1 = 0;
        public int Data_43_Counter_Scale_1
        {
            get { return mData_43_Counter_Scale_1; }
            set { mData_43_Counter_Scale_1 = value; }
        }


    }
}

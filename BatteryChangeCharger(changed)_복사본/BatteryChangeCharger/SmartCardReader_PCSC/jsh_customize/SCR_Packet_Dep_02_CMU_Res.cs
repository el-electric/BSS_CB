using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    public class SCR_Packet_Dep_02_CMU_Res : SCR_Packet_Base_Receive
    {
        public SCR_Packet_Dep_02_CMU_Res(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }

        public override void init()
        {

        }

        public override void receive_ApplyData(byte[] receiveData)
        {
            base.receive_ApplyData(receiveData);

            int indexArray = 11;//id 7
            //8
            indexArray++;
            //9
            indexArray++;
            //10
            indexArray++;
            mData_10_temp_00_Scale_10 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            //12
            indexArray++; indexArray++;
            mData_12_temp_01_Scale_10 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            //14
            indexArray++; indexArray++;
            mData_14_temp_02_Scale_10 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            //16
            indexArray++; indexArray++;
            mData_16_temp_03_Scale_10 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            //18
            indexArray++; indexArray++;
            mData_18_temp_04_Scale_10 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

            //
            indexArray += 18;
            //38
            indexArray ++; indexArray++;
            mData_38_cellv_00_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //40
            indexArray++; indexArray++;
            mData_40_cellv_01_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //42
            indexArray++; indexArray++;
            mData_42_cellv_02_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //44
            indexArray++; indexArray++;
            mData_44_cellv_03_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //46
            indexArray++; indexArray++;
            mData_46_cellv_04_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //48
            indexArray++; indexArray++;
            mData_48_cellv_05_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //50
            indexArray++; indexArray++;
            mData_50_cellv_06_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //52
            indexArray++; indexArray++;
            mData_52_cellv_07_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //54
            indexArray++; indexArray++;
            mData_54_cellv_08_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //56
            indexArray++; indexArray++;
            mData_56_cellv_09_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //58
            indexArray++; indexArray++;
            mData_58_cellv_10_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //60
            indexArray++; indexArray++;
            mData_60_cellv_11_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //62
            indexArray++; indexArray++;
            mData_62_cellv_12_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //64
            indexArray++; indexArray++;
            mData_64_cellv_13_Scale_1000 = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);
            //66
            indexArray++; indexArray++;
            //68
            indexArray++; indexArray++;
            //70
            indexArray++; indexArray++;
            //72
            indexArray++; indexArray++;
            //74
            indexArray++; indexArray++;
            //76
            indexArray++; indexArray++;
            //78
            indexArray++; indexArray++;
            mData_78_FW_Version = ((receiveData[indexArray + 1] << 8) & 0x0000ff00) + (receiveData[indexArray] & 0x000000ff);

        }


        protected int mData_10_temp_00_Scale_10 = 0;
        public int Data_10_temp_00_Scale_10
        {
            get { return mData_10_temp_00_Scale_10; }
            set { mData_10_temp_00_Scale_10 = value; }
        }

        protected int mData_12_temp_01_Scale_10 = 0;
        public int Data_12_temp_01_Scale_10
        {
            get { return mData_12_temp_01_Scale_10; }
            set { mData_12_temp_01_Scale_10 = value; }
        }

        protected int mData_14_temp_02_Scale_10 = 0;
        public int Data_14_temp_02_Scale_10
        {
            get { return mData_14_temp_02_Scale_10; }
            set { mData_14_temp_02_Scale_10 = value; }
        }

        protected int mData_16_temp_03_Scale_10 = 0;
        public int Data_16_temp_03_Scale_10
        {
            get { return mData_16_temp_03_Scale_10; }
            set { mData_16_temp_03_Scale_10 = value; }
        }

        protected int mData_18_temp_04_Scale_10 = 0;
        public int Data_18_temp_04_Scale_10
        {
            get { return mData_18_temp_04_Scale_10; }
            set { mData_18_temp_04_Scale_10 = value; }
        }




        /////////////////////////////////////
        protected int mData_38_cellv_00_Scale_1000 = 0;
        public int Data_38_cellv_00_Scale_1000
        {
            get { return mData_38_cellv_00_Scale_1000; }
            set { mData_38_cellv_00_Scale_1000 = value; }
        }

        protected int mData_40_cellv_01_Scale_1000 = 0;
        public int Data_40_cellv_01_Scale_1000
        {
            get { return mData_40_cellv_01_Scale_1000; }
            set { mData_40_cellv_01_Scale_1000 = value; }
        }

        protected int mData_42_cellv_02_Scale_1000 = 0;
        public int Data_42_cellv_02_Scale_1000
        {
            get { return mData_42_cellv_02_Scale_1000; }
            set { mData_42_cellv_02_Scale_1000 = value; }
        }

        protected int mData_44_cellv_03_Scale_1000 = 0;
        public int Data_44_cellv_03_Scale_1000
        {
            get { return mData_44_cellv_03_Scale_1000; }
            set { mData_44_cellv_03_Scale_1000 = value; }
        }

        protected int mData_46_cellv_04_Scale_1000 = 0;
        public int Data_46_cellv_04_Scale_1000
        {
            get { return mData_46_cellv_04_Scale_1000; }
            set { mData_46_cellv_04_Scale_1000 = value; }
        }

        protected int mData_48_cellv_05_Scale_1000 = 0;
        public int Data_48_cellv_05_Scale_1000
        {
            get { return mData_48_cellv_05_Scale_1000; }
            set { mData_48_cellv_05_Scale_1000 = value; }
        }

        protected int mData_50_cellv_06_Scale_1000 = 0;
        public int Data_50_cellv_06_Scale_1000
        {
            get { return mData_50_cellv_06_Scale_1000; }
            set { mData_50_cellv_06_Scale_1000 = value; }
        }

        protected int mData_52_cellv_07_Scale_1000 = 0;
        public int Data_52_cellv_07_Scale_1000
        {
            get { return mData_52_cellv_07_Scale_1000; }
            set { mData_52_cellv_07_Scale_1000 = value; }
        }

        protected int mData_54_cellv_08_Scale_1000 = 0;
        public int Data_54_cellv_08_Scale_1000
        {
            get { return mData_54_cellv_08_Scale_1000; }
            set { mData_54_cellv_08_Scale_1000 = value; }
        }

        protected int mData_56_cellv_09_Scale_1000 = 0;
        public int Data_56_cellv_09_Scale_1000
        {
            get { return mData_56_cellv_09_Scale_1000; }
            set { mData_56_cellv_09_Scale_1000 = value; }
        }

        protected int mData_58_cellv_10_Scale_1000 = 0;
        public int Data_58_cellv_10_Scale_1000
        {
            get { return mData_58_cellv_10_Scale_1000; }
            set { mData_58_cellv_10_Scale_1000 = value; }
        }

        protected int mData_60_cellv_11_Scale_1000 = 0;
        public int Data_60_cellv_11_Scale_1000
        {
            get { return mData_60_cellv_11_Scale_1000; }
            set { mData_60_cellv_11_Scale_1000 = value; }
        }

        protected int mData_62_cellv_12_Scale_1000 = 0;
        public int Data_62_cellv_12_Scale_1000
        {
            get { return mData_62_cellv_12_Scale_1000; }
            set { mData_62_cellv_12_Scale_1000 = value; }
        }

        protected int mData_64_cellv_13_Scale_1000 = 0;
        public int Data_64_cellv_13_Scale_1000
        {
            get { return mData_64_cellv_13_Scale_1000; }
            set { mData_64_cellv_13_Scale_1000 = value; }
        }


        protected int mData_78_FW_Version = 0;
        public int Data_78_FW_Version
        {
            get { return mData_78_FW_Version; }
            set { mData_78_FW_Version = value; }
        }
        
    }
}

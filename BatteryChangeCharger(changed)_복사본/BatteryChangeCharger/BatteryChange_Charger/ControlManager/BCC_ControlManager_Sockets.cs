using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.ControlManager
{
    public class BCC_ControlManager_Sockets : CObject
    {
        
        public BCC_ControlManager_Sockets(MyApplication application) : base(application)
        {

        }

        public bool isCanUsing()
        {

            /*MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_SLOT1_Battery_In

               int count = mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.getCount_BatteryNotExist();
               if (count < 2)
                   return false;

               count = mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.getCount_BatteryExist();
               if (count < 2)
                   return false;

               return true;*/

            return true;
        }

        

        public int[] getBatteryIndex_Can_Retrieve_Battery()
        {
            int[] data = new int[2];
            int count = 0;
            for(int i = 0; i < 8; i++)
            {
                //if(mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[i].isConnected_Comm())
                //{
                  /*  bool setting = mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.isBatteryExist(i + 1);
                    if (setting
                    && i != 3)
                    {
                        data[count] = i + 1;
                        count++;
                        if (count >= 2)
                            break;
                    }*/
                //}
            }

            if (count == 2)
                return data;

            return null;
        }

        int mTempIndex_Insert_Battery = -1;

        int mTempIndex_Test = -1;
        public int getBatteryIndex_Random()
        {
            //Random random = new Random();
            //for(int i = 0; i < 1000; i++)
            //{
            //    int index = random.Next(1, 9);
            //    if (mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[index-1].isConnected_Comm())
            //    {
            //        return index;
            //    }
            //}

            //return random.Next(1, 9); ;

            if (mTempIndex_Test >= 7)
                mTempIndex_Test = -1;

            mTempIndex_Test++;

            return mTempIndex_Test;
        }

        public int[] getBatteryIndex_Can_Insert_Battery()
        {
            int[] data = new int[2];
            int count = 0;  
            for (int i = 0; i < 16; i++) 
            {
                if (mTempIndex_Insert_Battery >= 16)
                    mTempIndex_Insert_Battery = -1;

                mTempIndex_Insert_Battery++; 

                int battIndex = mTempIndex_Insert_Battery % 8;
                if (mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[battIndex].isConnected_Comm()
                    && i != 3)
                {
                   /* bool setting = mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.isBatteryExist(battIndex + 1);
                    if (!setting)
                    {
                        data[count] = battIndex + 1;
                        count++;
                        if (count >= 2)
                            break;
                    }*/
                }
                
            }

            if (count == 2)
                return data;

            return null;
        }

        public override void init()
        {
            throw new NotImplementedException();
        }
    }
}

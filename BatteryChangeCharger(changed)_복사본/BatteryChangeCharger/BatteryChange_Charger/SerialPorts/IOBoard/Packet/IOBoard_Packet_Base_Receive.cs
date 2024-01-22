using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board.Packets;
using BatteryChangeCharger.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IO_Board.Packet
{
    abstract public class IOBoard_Packet_Base_Receive : IOBoard_Packet_Base
    {
        public IOBoard_Packet_Base_Receive(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }

        protected byte[] mReceiveData = null;
        protected JSH_Time mTime_Receive = new JSH_Time();

        
        virtual public void receive_ApplyData(byte[] receiveData)
        {
            mReceiveData = receiveData;
            mTime_Receive.setTime();

            int index = 1;

            if (mReceiveData[CONST_IO_Board.INDEX_CMD] == '1' && mReceiveData[CONST_IO_Board.INDEX_CMD +1] == 'z') return;


            /*if ((mReceiveData[CONST_IO_Board.INDEX_RD + index] & 0x00000006) != 0)
            {
                mApplication.SerialPort_IOBoard.getManager_Send().IsInitComplete_a1_Send = true;
                mApplication.SerialPort_IOBoard.getManager_Send().IsInitComplete_a1_Receive = true;

            }
            else
            {
                mApplication.SerialPort_IOBoard.getManager_Send().IsInitComplete_a1_Send = false;
                mApplication.SerialPort_IOBoard.getManager_Send().IsInitComplete_a1_Receive = false;
            }*/
                
        }

        
    }
}

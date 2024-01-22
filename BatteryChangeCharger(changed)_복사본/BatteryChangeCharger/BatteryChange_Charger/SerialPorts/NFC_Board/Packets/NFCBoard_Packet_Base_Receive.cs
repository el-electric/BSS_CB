using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board.Packets
{
    abstract public class NFCBoard_Packet_Base_Receive : NFCBoard_Packet_Base
    {
        public NFCBoard_Packet_Base_Receive(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
        }

        protected byte[] mReceiveData = null;
        protected JSH_Time mTime_Receive = new JSH_Time();

        public bool BatterArrive;
        public bool isDoor;
        public int SeqNum;
        public int NFC_Slave_Id;
        virtual public void receive_ApplyData(byte[] receiveData)
        {
            mReceiveData = receiveData;

            NFC_Slave_Id = Manager_Conversion.getInt(mReceiveData[17]);
            /*mTime_Receive.setTime();*/

            /*int index = 0;*/

            /*if ((mReceiveData[CONST_NFC_Board.INDEX_RD + index] & 0x00000008) != 0)
                IsInitComplete_a1 = true;
            else
                IsInitComplete_a1 = false;

            if ((mReceiveData[CONST_NFC_Board.INDEX_RD + index] & 0x00000080) != 0)
                bEmg_Push = true;
            else
                bEmg_Push = false;*/



            BatterArrive = Manager_Conversion.getFlagByByteArray(mReceiveData[17], 7);
            isDoor = Manager_Conversion.getFlagByByteArray(mReceiveData[18], 6);
            SeqNum = mReceiveData[18];
        }

        
    }
}

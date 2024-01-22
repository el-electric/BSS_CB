using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board
{
    public class NFCBoard_CommManager_Send : CObject
    {


        public NFCBoard_CommManager_Packet[] mPackets = new NFCBoard_CommManager_Packet[8];

        protected NFCBoard_CommManager_SerialPort mManager_SerialPort = null;
        public NFCBoard_CommManager_Send(MyApplication application, NFCBoard_CommManager_SerialPort manager_SerialPort) : base(application)
        {
            mManager_SerialPort = manager_SerialPort;
            for(int i = 0; i < mPackets.Length; i++)
            {
                mPackets[i] = new NFCBoard_CommManager_Packet(application, i + 1);
            }
        }

        public override void init()
        {

        }

        protected int mSendIndex = 0;

        public void sendData_Interval()
        {
            if (mSendIndex >= mPackets.Length)
                mSendIndex = 0;

            mPackets[mSendIndex].mPacket_c1_Send.send_ApplyData();
            byte[] data = mPackets[mSendIndex].mPacket_c1_Send.SendData;
            mManager_SerialPort.write(data);  // data에 보낼 정보를 담는다.
            mSendIndex++;
        }

        protected bool bIsInitComplete_a1_Receive = false;

        public bool IsInitComplete_a1_Receive
        {
            get { return bIsInitComplete_a1_Receive; }
            set
            {


                bIsInitComplete_a1_Receive = value;
            }
        }

        protected bool bIsInitComplete_a1_Send = false;

        public bool IsInitComplete_a1_Send
        {
            get { return bIsInitComplete_a1_Send; }
            set
            { bIsInitComplete_a1_Send = value; }
        }
    }
}

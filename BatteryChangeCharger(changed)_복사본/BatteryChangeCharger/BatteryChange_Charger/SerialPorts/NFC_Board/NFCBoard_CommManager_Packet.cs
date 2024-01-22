using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board.Packets;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board
{
    public class NFCBoard_CommManager_Packet : CObject_Channel
    {
        public NFCBoard_Packet_c1_Send mPacket_c1_Send = null;
        public NFCBoard_Packet_c1_receive mPacket_c1_Receive = null;

        public NFCBoard_CommManager_Packet(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            mPacket_c1_Send = new NFCBoard_Packet_c1_Send(application, channelIndex);
            mPacket_c1_Receive = new NFCBoard_Packet_c1_receive(application, channelIndex);
        }

        public override void init()
        {
            throw new NotImplementedException();
        }

        protected int mCount_Failed_Attempt_Comm = 0;
        protected DateTime mDateTime_LastComm = DateTime.Now;

        protected bool bIsConnected = false;
        public bool IsConnected
        {
            get { return bIsConnected; }
        }
        public void setLastComm()
        {
            mDateTime_LastComm = DateTime.Now;
            mCount_Failed_Attempt_Comm = 0;
            bIsConnected = true;
        }

        override public bool isConnected_Comm()
        {
            if (bIsConnected)
            {
                TimeSpan span = DateTime.Now - mDateTime_LastComm; //지금 시간을 가지고 이전에 연결된 시간을 뺀다
                if (span.TotalMinutes >= 1) return false;
                return true;
            }

            return false;
        }
    }
}

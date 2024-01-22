using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard.Packet;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard
{
    public class IO_Board_CommManager_Packet : CObject_Channel
    {
        public IOBoard_Packet_a1_Send mPacket_a1_Send = null;
        public IOBoard_Packet_a1_Receive mPacket_a1_Receive = null;

        public IOBoard_Packet_z1_Send mPacket_z1_Send = null;
        public IOBoard_Packet_z1_Receive mPacket_z1_Receive = null;

        public IO_Board_CommManager_Packet(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            mPacket_a1_Send = new IOBoard_Packet_a1_Send(application, channelIndex);
            mPacket_a1_Receive = new IOBoard_Packet_a1_Receive(application, channelIndex);

            mPacket_z1_Send = new IOBoard_Packet_z1_Send(application, channelIndex);
            mPacket_z1_Receive = new IOBoard_Packet_z1_Receive(application, channelIndex);
        }

        


        public override void init()
        {
            
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
                TimeSpan span = DateTime.Now - mDateTime_LastComm;
                if (span.TotalMinutes >= 1) return false;
                return true;
            }

            return false;
        }
    }
}

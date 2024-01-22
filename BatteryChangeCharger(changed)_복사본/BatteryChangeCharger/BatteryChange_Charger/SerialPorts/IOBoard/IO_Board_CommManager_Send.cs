using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard
{
    public class IO_Board_CommManager_Send : CObject
    {

        public IO_Board_CommManager_Packet mManager_Packet = null;
        IO_Board_CommManager_SerialPort mManager_SerialPort = null;
        public IO_Board_CommManager_Send(MyApplication application, IO_Board_CommManager_SerialPort manager_SerialPort) : base(application)
        {
            mManager_SerialPort = manager_SerialPort;
            mManager_Packet = new IO_Board_CommManager_Packet(application, 1);
        }

        public override void init()
        {

        }

        public bool Send_z1_Packet_Data = false;
        public void sendData_Interval()
        {

            byte[] data = null;

            //mManager_Packet.mPacket_a1_Send.send_ApplyData();
            //data = mManager_Packet.mPacket_a1_Send.SendData;

            /*if (!bIsInitComplete_a1_Receive || !bIsInitComplete_a1_Send)
            {
                mManager_Packet.mPacket_a1_Send.send_ApplyData();
                data = mManager_Packet.mPacket_a1_Send.SendData;
            }
            else
            {
                mManager_Packet.mPacket_z1_Send.send_ApplyData();
                data = mManager_Packet.mPacket_z1_Send.SendData;
            }*/

            if (!Send_z1_Packet_Data)
            {
                mManager_Packet.mPacket_z1_Send.send_ApplyData();
                data = mManager_Packet.mPacket_z1_Send.SendData;
                Send_z1_Packet_Data = true;
            }
            else
            {
                mManager_Packet.mPacket_a1_Send.send_ApplyData();
                data = mManager_Packet.mPacket_a1_Send.SendData;
                Send_z1_Packet_Data = false;
            }
                if (data != null)
                mManager_SerialPort.write(data);
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

        //protected bool bIs_InitComplete_a1_Send = false;
        //public bool Is_InitComplete_a1_Send
        //{
        //    get { return bIs_InitComplete_a1_Send; }
        //    set
        //    {
        //        if(!bIs_InitComplete_a1_Send)
        //        {
        //            mManager_Packet.mPacket_a1_Send.
        //        }
        //        bIs_InitComplete_a1_Send = value;
        //    }
        //}

        //protected bool bIs_InitComplete_a1_Receive = false;
        //public bool Is_InitComplete_a1_Receive
        //{
        //    get { return bIs_InitComplete_a1_Receive; }
        //    set
        //    {
        //        bIs_InitComplete_a1_Receive = value;
        //    }
        //}
    }
}


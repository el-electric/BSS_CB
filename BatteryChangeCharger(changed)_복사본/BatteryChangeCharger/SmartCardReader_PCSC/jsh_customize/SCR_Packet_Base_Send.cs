using Acs.Readers.Pcsc;
using BatteryChangeCharger.Applications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    abstract public class SCR_Packet_Base_Send : SCR_Packet_Base
    {
        
        protected SCR_Packet_Base_Send(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            mApdu = new Apdu();
            mApdu.lengthExpected = 510;
            
        }

        protected byte mSendData_DID = 5;
        protected byte mSendData_PFB = 0;

        protected Apdu mApdu = null;
        abstract public void send_ApplyData();


        public Apdu PacketApdu
        {
            get {
                send_ApplyData();
                return mApdu; }
            set { mApdu = value ; }
        }
    }
}

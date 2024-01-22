using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board.Packets
{
    public class NFCBoard_Packet_Base : CObject_Channel
    {
        protected int mLength_VD = 0;

        public NFCBoard_Packet_Base(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            
        }

        public NFCBoard_Packet_Base(MyApplication application, int channelIndex, int length_VD) : this(application, channelIndex)
        {
            mLength_VD = length_VD;
        }

        public override void init()
        {
            
        }
    }
}

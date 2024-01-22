using BatteryChangeCharger.Applications;
using BatteryChangeCharger.ChargerVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize
{
    public class SCR_PacketManager : CObject_Channel
    {

        protected SCR_Packet_ATR_Req mPacket_ATR_Req= null;
        public SCR_Packet_ATR_Req Packet_ATR_Req
        {
            get { return mPacket_ATR_Req; }
            set { mPacket_ATR_Req = value; }
        }
        protected SCR_Packet_ATR_Res mPacket_ATR_Res = null;
        public SCR_Packet_ATR_Res Packet_ATR_Res
        {
            get { return mPacket_ATR_Res; }
        }
        protected SCR_Packet_Dep_01_BMU_Req mPacket_Dep_BMU_Req = null;
        public SCR_Packet_Dep_01_BMU_Req Packet_Dep_BMU_Req
        {
            get { return mPacket_Dep_BMU_Req; }
        }
        protected SCR_Packet_Dep_01_BMU_Res mPacket_Dep_BMU_Res = null;
        public SCR_Packet_Dep_01_BMU_Res Packet_Dep_BMU_Res
        {
            get { return mPacket_Dep_BMU_Res; }
        }
        protected SCR_Packet_Dep_02_CMU_Req mPacket_Dep_CMU_Req = null;
        public SCR_Packet_Dep_02_CMU_Req Packet_Dep_CMU_Req
        {
            get { return mPacket_Dep_CMU_Req; }
        }
        protected SCR_Packet_Dep_02_CMU_Res mPacket_Dep_CMU_Res = null;
        public SCR_Packet_Dep_02_CMU_Res Packet_Dep_CMU_Res
        {
            get { return mPacket_Dep_CMU_Res; }
        }






        public SCR_PacketManager(MyApplication application, int channelIndex) : base(application, channelIndex)
        {
            mPacket_ATR_Req = new SCR_Packet_ATR_Req(application, channelIndex);
            mPacket_ATR_Res = new SCR_Packet_ATR_Res(application, channelIndex);
            mPacket_Dep_BMU_Req = new SCR_Packet_Dep_01_BMU_Req(application, channelIndex);
            mPacket_Dep_BMU_Res = new SCR_Packet_Dep_01_BMU_Res(application, channelIndex);
            mPacket_Dep_CMU_Req = new SCR_Packet_Dep_02_CMU_Req(application, channelIndex);
            mPacket_Dep_CMU_Res = new SCR_Packet_Dep_02_CMU_Res(application, channelIndex);
        }

        public override void init()
        {
            
        }
    }
}

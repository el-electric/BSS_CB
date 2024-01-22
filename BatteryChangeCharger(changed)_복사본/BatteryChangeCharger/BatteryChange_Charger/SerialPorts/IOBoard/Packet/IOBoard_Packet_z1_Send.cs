using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IO_Board.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard.Packet
{
    public class IOBoard_Packet_z1_Send : IOBoard_Packet_Base_Send
    {
        public IOBoard_Packet_z1_Send(MyApplication application, int channelIndex) 
            : base(application, channelIndex, CONST_IO_Board.LENGTH_VD_HMI2BOARD_z1, new byte[] { (byte)'z',(byte)'1'})
        {
            
        }



        
        

    }
}

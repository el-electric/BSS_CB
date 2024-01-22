using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts
{
    public class CONST_IO_Board
    {
        public const int LENGTH_DEFUALT = 14;
        public const int LENGTH_RD_HMI2BOARD = 1;
        public const int LENGTH_RD_BOARD2HMI = 1;

        public const int LENGTH_VD_HMI2BOARD_z1 = 5;
        public const int LENGTH_VD_HMI2BOARD_cb = 33; 


        public const byte VALUE_STX = 0xfe;
        public const byte VALUE_ETX = 0xff;

        public readonly static byte[] VALUE_PROTOCOL_ID = { (byte)'M', (byte)'N' };

        public const int INDEX_STX = 0;
        public const int INDEX_SEQ = 1;
        public const int INDEX_CHARGER_TYPE = 3;
        public const int INDEX_CHARGER_ID = 4;
        public const int INDEX_PROTOCOL_ID = 5;
        public const int INDEX_CMD = 8;
        public const int INDEX_LENGTH_DATA = 9;
        public const int INDEX_LENGTH_RD = 11;
        public const int INDEX_RD = 13;
        public const int INDEX_VD_HMI2BOARD = INDEX_RD + LENGTH_RD_HMI2BOARD;
        public const int INDEX_VD_BOARD2HMI = INDEX_RD + LENGTH_RD_BOARD2HMI;
    }
}

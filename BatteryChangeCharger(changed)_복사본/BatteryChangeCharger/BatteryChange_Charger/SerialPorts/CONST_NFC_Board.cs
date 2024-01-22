using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts
{
    public class CONST_NFC_Board
    {
        public const int LENGTH_DEFUALT = 14;
        public const int LENGTH_RD_HMI2BOARD = 1;
        public const int LENGTH_RD_BOARD2HMI = 0;

        public const int LENGTH_VD_HMI2BOARD_c1 = 27;

        public const byte VALUE_STX = 0xfe;
        public const byte VALUE_ETX = 0xff;

        public readonly static byte[] VALUE_PROTOCOL_ID = { (byte)'M', (byte)'N' };

        public const int INDEX_STX = 0;
        public const int INDEX_SEQ = 1;
        public const int INDEX_CHARGER_TYPE = 3;
        public const int INDEX_CHARGER_ID = 5;
        public const int INDEX_PROTOCOL_ID = 6;
        public const int INDEX_CMD = 7;
        public const int INDEX_LENGTH_DATA = 9;
        public const int INDEX_LENGTH_RD = 11;
        public const int INDEX_RD = 13;
        public const int INDEX_VD_HMI2BOARD = INDEX_RD + LENGTH_RD_HMI2BOARD;
        public const int INDEX_VD_BOARD2HMI = INDEX_RD + LENGTH_RD_BOARD2HMI;

    }
}

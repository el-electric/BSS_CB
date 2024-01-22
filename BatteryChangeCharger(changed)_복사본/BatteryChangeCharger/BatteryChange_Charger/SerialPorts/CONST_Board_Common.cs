using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts
{
    public class CONST_Board_Common
    {
        public const byte DOOR_CONTROL_STOP = 0;
        public const byte DOOR_CONTROL_OPEN = 1;
        public const byte DOOR_CONTROL_CLOSE = 2;

        public const byte DOOR_STATE_NOT_DEFINE = 0x00;

        public const byte DOOR_STATE_DOOR_OPEN_ING = 0x01;
        public const byte DOOR_STATE_DOOR_OPEN_ING_DETECTING_OBJECT = 0x02;
        public const byte DOOR_STATE_DOOR_OPEN_COMPLETE = 0x03;

        public const byte DOOR_STATE_DOOR_CLOSE_ING = 0x11;
        public const byte DOOR_STATE_DOOR_CLOSE_ING_DETECTING_OBJECT = 0x12;
        public const byte DOOR_STATE_DOOR_CLOSE_COMPLETE = 0x13;

        public const byte DOOR_STATE_DOOR_STOP = 0x21;

        public const byte ACK = 0x06;
        public const byte NAK = 0x15;

    }
}

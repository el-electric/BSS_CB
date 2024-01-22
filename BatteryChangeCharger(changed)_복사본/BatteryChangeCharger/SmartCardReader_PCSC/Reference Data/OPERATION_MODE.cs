using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public enum OPERATION_MODE : byte
    {
        ACTIVE = 0x01,
        PASSIVE = 0x02,
        UNKNOWN = 0x00
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public enum CONNECTION_SPEED : byte
    {
        KBPS_106 = 0x01,
        KBPS_212 = 0x02,
        KBPS_424 = 0x03,/// <summary>
        UNKNOWN = 0x00
    }
}

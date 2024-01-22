using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public enum ACTION_RECORD
    {
        DO_THE_ACTION = 0x00,
        SAVE_FOR_LATER = 0x01,
        OPEN_FOR_EDITING = 0x02
    }
}

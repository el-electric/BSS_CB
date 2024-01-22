using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public enum TYPE_NAME_FORMAT
    {
        EMPTY = 0x00,
        NFC_FORM_WELL_KNOWN_TYPE = 0x01,
        MEDIA_TYPE = 0x02,
        ABSOLUTE_URI = 0x03,
        NFC_FORM_EXTERNAL_TYPE = 0x04,
        UNKNOWN = 0x05
    }
}

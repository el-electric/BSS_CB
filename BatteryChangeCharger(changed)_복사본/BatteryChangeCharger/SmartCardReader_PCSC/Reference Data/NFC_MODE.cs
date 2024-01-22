using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public enum NFC_MODE : byte
    {
        PEER_TO_PEER_INITIATOR = 0x08,
        INITIATOR_DEMO = 0x06,
        PEER_TO_PEER_TARGET = 0x04,
        CARD_READER_WRITER = 0x00,
        MIFARE_ULTRALIGHT_CARD_EMULATION = 0x01,
        FELICA_CARD_EMULATION = 0x03
    }
}

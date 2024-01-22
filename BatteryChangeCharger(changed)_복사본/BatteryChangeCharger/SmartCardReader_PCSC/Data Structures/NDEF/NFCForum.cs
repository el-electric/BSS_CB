/*===========================================================================================
 * 
 *  Author          : Arturo Salvamante
 * 
 *  File            : NFCForum.cs
 * 
 *  Copyright (C)   : Advanced Card System Ltd
 * 
 *  Description     : Contains Methods and Properties related to NFC Forum
 * 
 *  Date            : June 03, 2011
 * 
 *  Revision Traile : [Author] / [Date if modification] / [Details of Modifications done]
 * 
 * 
 * =========================================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public class NFCForum
    {
        public enum TAGs
        {
            NULL_TLV = 0x00,
            NDEF_MESSAGE_TLV = 0x03,
            PROPRIETARY_TLV = 0xFD,
            TERMINATOR_TLV = 0xFE
        }

    }
}

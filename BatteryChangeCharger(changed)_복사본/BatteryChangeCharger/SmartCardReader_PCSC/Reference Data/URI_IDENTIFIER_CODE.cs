using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public enum URI_IDENTIFIER_CODE
    {
        NONE = 0x00,
        HTTP_WWW = 0x01,
        HTTPS_WWW = 0x02,
        HTTP = 0x03,
        HTTPS = 0x04,
        TEL = 0x05,
        MAIL_TO = 0x06,
        FTP_ANONYMOUS = 0x07,
        FTP_FTP = 0x08,
        FTPS = 0x09,
        SFTP = 0x0A,
        SMB = 0x0B,
        NFS = 0x0C,
        FTP = 0x0D,
        DAV = 0x0E,
        NEWS = 0x0F,
        TELNET = 0x10,
        IMAP = 0x11,
        RTSP = 0x12,
        URN = 0x13,
        POP = 0x14,
        SIP = 0x15,
        SIPS = 0x16,
        TFTP = 0x17,
        BTSPP = 0x18,
        BT12CAP = 0x19,
        BTGOEP = 0x1A,
        TCPOBEX = 0x1B,
        IRDAOBEX = 0x1C,
        FILE = 0x1D,
        URN_EPC_ID = 0x1E,
        URN_EPC_TAG = 0x1F,
        URN_EPC_PAT = 0x20,
        URN_EPC_RAW = 0x21,
        URN_EPC = 0x22,
        URN_NFC = 0x23
    }
}

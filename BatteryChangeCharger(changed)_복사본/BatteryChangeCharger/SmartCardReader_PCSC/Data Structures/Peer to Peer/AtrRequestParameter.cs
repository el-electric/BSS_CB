using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public class AtrRequestParameter
    {
        private NFC_MODE _nfcMode;
        private CONNECTION_SPEED _connectionSpeed;
        private byte[] _nfcId;
        private byte[] _generalBytes;
        private byte _deviceId;
        private byte _sendBitRate;
        private byte _bitRate;
        private byte _optionalParameter;

        public NFC_MODE nfcMode
        {
            get { return _nfcMode; }
            set { _nfcMode = value; }
        }

        public CONNECTION_SPEED connectionSpeed
        {
            get { return _connectionSpeed; }
            set { _connectionSpeed = value; }
        }

        public byte[] nfcId
        {
            get { return _nfcId; }
            set { _nfcId = value; }
        }

        public byte[] generalBytes
        {
            get { return _generalBytes; }
            set { _generalBytes = value; }
        }

        public byte deviceId
        {
            get { return _deviceId; }
            set { _deviceId = value; }
        }

        public byte sendBitRate
        {
            get { return _sendBitRate; }
            set { _sendBitRate = value; }
        }

        public byte bitRate
        {
            get { return _bitRate; }
            set { _bitRate = value; }
        }

        public byte optionalParameter
        {
            get { return _optionalParameter; }
            set { _optionalParameter = value; }
        }
        
    }
}

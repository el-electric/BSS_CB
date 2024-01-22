using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acs.Readers.Pcsc;
using BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
    public class PeerToPeer : PcscReader
    {
        private byte[]           _timeout;
        private NFC_MODE         _nfcMode;
        private OPERATION_MODE   _operationMode;
        private CONNECTION_SPEED _connectionSpeed;
        private NdefMessage      _snepMessage;
   
        public PeerToPeer()
        {
            operationControlCode = (uint)PcscProvider.FILE_DEVICE_SMARTCARD + 3500 * 4;
        }

        public PeerToPeer(string selectedReader)
        {
            readerName = selectedReader;
            operationControlCode = (uint)PcscProvider.FILE_DEVICE_SMARTCARD + 3500 * 4;

            this._timeout         = new byte[2];
            this._nfcMode         = NFC_MODE.CARD_READER_WRITER;
            this._operationMode   = OPERATION_MODE.UNKNOWN;
            this._connectionSpeed = CONNECTION_SPEED.UNKNOWN;
            this._snepMessage     = new NdefMessage();
        }

        public byte[] timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public NFC_MODE nfcMode
        {
            get { return _nfcMode; }
            set { _nfcMode = value; }
        }

        public OPERATION_MODE operationMode
        {
            get { return _operationMode; }
            set { _operationMode = value; }
        }

        public CONNECTION_SPEED connectionSpeed
        {
            get { return _connectionSpeed; }
            set { _connectionSpeed = value; }
        }

        public NdefMessage snepMessage
        {
            get { return _snepMessage; }
        }

        public byte[] setInitiatorModeTimeout(byte[] timeout)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 20;
            apdu.data = new byte[7];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x41;
            apdu.data[4] = 0x02;
            apdu.data[5] = timeout[0];
            apdu.data[6] = timeout[1];

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] enterInitiatorMode(NFC_MODE nfcMode, OPERATION_MODE operationMode, CONNECTION_SPEED speed)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 10;
            apdu.data = new byte[8];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x40;
            apdu.data[4] = 0x03;
            apdu.data[5] = (byte)nfcMode;
            apdu.data[6] = (byte)operationMode;
            apdu.data[7] = (byte)speed;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;

        }

        public byte[] atrRequest(OPERATION_MODE operationMode, CONNECTION_SPEED speed)
        {
            // ATR REQUEST
	        // | 0xE0 0x00 0x00 0x42 Len | 0x11 Mode Speed NFCID DID BS BR PP GiLen Gi |
	        // <------- Command --------> <---------------- Data In ------------------>
	        //                             1    1    1     10    1   1  1  1  1     1   (byte(s))
	        //                                                       <-LLCP Parameter->
	        //
	        // NFCID = 0x01 0xFE 0x03 0x04 0x05 0x06 0x07 0x08 0x09 0x0A
	        // DID   = 0x00
	        // BS    = 0x00
	        // BR    = 0x00
	        // PP    = 0x32
	        // GiLen = 0x0D
	        // Gi    = 0x46 0x66 0x6D 0x01 0x01 0x11 0x03 0x02 0x00 0x13 0x04 0x01 0x96

            byte[] uAtrInfoByte = {
                /*정승현*/0x01,0xFE,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,
                /*원본*///0x01,0xFE,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x0A,
                /*정승현*/JSH_DID,/*0x05*/
                                    /*BS, BR, PP(변함없음)*/0x00,0x00,0x32,
                                    /*Gi 길이 정승현*/20,
                                    /*Gi 데이터 정승현*/0x46,0x66,0x6D,0x01,0x01,0x11,0x02,0x02,0x07,0x80,
                                                        0x03,0x02,0x00,0x03,0x04,0x01,0x32,0x07,0x01,0x03};
            /*Gi 데이터 원본(길이포함)*///0x0D,0x46,0x66,0x6D,0x01,0x01, 0x11,0x03,0x02,0x00,0x13,0x04,0x01,0x96}; // ATR information for ATR Request

            int iAtrInfoByte = uAtrInfoByte.Length;

            Apdu apdu = new Apdu();
            apdu.lengthExpected = 510;
            apdu.data = new byte[8];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x42;
            apdu.data[4] = (byte)(3 + iAtrInfoByte);
            apdu.data[5] = 0x11;
            apdu.data[6] = (byte)operationMode;
            apdu.data[7] = (byte)speed;

            apdu.data   = Helper.appendArrays(apdu.data, uAtrInfoByte);

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] atrRequest(SCR_PacketManager packetManager)
        {
            Apdu pdu = packetManager.Packet_ATR_Req.PacketApdu;
            apduCommand = packetManager.Packet_ATR_Req.PacketApdu;
            sendCardControl(ref pdu, operationControlCode);

            return apduCommand.response;
        }


        public byte[] depExchange_1_BMU(SCR_PacketManager packetManager)
        {
            Apdu pdu = packetManager.Packet_Dep_BMU_Req.PacketApdu;
            apduCommand = packetManager.Packet_Dep_BMU_Req.PacketApdu;
            sendCardControl(ref pdu, operationControlCode);
            return apduCommand.response;
        }

        public byte[] depExchange_2_CMU(SCR_PacketManager packetManager)
        {
            Apdu pdu = packetManager.Packet_Dep_CMU_Req.PacketApdu;
            apduCommand = packetManager.Packet_Dep_CMU_Req.PacketApdu;
            sendCardControl(ref pdu, operationControlCode);
            return apduCommand.response;
        }


        static byte JSH_DID = 0x05;
        static byte JSH_PFB = 0x00;
        public byte[] depExchange(byte[] dep, int sendDep)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 510;



            //////////////////////////////////////////////////////////////////////////
            ///
            //      DEP
            //
            //////////////////////////////////////////////////////////////////////////

            //원래포맷 맞춤
            apdu.data = new byte[8];
            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x43;
            dep = new byte[83-3];
            apdu.data[4] = (byte)(dep.Length + 3);//LC len//(byte)(sendDep);
            apdu.data[5] = 0x11;
            apdu.data[6] = JSH_PFB;//PFB// (byte)(0x00 | (_pni & 0x03));  // PFB
            apdu.data[7] = (byte)(dep.Length-1);//DepLen //0x02;

            int index = 0;

            int depMinusCount = 0;
            int index_Length = 0;
            //dep[index++] = 0xd4; depMinusCount++;
            //dep[index++] = 0x06; depMinusCount++;
            //dep[index++] = JSH_PFB; depMinusCount++;
            dep[index++] = JSH_DID; depMinusCount++;//DID
            dep[index++] = (byte)(dep.Length - depMinusCount);//length
            index_Length = index - 1;
            dep[index++] = 0x11;//CMD
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            dep[index++] = 0x00;
            int sum = 0;
            if (index_Length < 0)
            {
                index_Length = 0;
                sum += apdu.data[7];
            }

            for (int i = index_Length; i < dep.Length; i++)
            {
                sum += dep[i];
            }

            dep[index++] = (byte)(sum & 0x000000ff);
            dep[index++] = (byte)((sum >> 8) & 0x000000ff);



            apdu.data = Helper.appendArrays(apdu.data, dep);

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte deselectRequest()
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[7];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x44;
            apdu.data[4] = 0x02;
            apdu.data[5] = 0x11;
            apdu.data[6] = JSH_DID;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response[7];
        }

        public byte[] releaseRequest()
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[7];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x45;
            apdu.data[4] = 0x02;
            apdu.data[5] = 0x11;
            apdu.data[6] = JSH_DID;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] enterTargetMode()
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[5];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x51;
            apdu.data[4] = 0x00;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] enterTargetMode(OPERATION_MODE operationMode, CONNECTION_SPEED speed)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[7];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x51;
            apdu.data[4] = 0x02;
            apdu.data[5] = (byte)speed;
            apdu.data[6] = (byte)operationMode;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] getInitiatorMessage()
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 512;
            apdu.data = new byte[5];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x58;
            apdu.data[4] = 0x00;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);         

            return apduCommand.response;
        }

        public byte[] setTargetModeTimeout(byte[] timeout)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[7];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x59;
            apdu.data[4] = 0x02;
            apdu.data[5] = timeout[0];
            apdu.data[6] = timeout[1];

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;

        }

        public byte[] atrResponse(byte[] llcpParameter)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[5];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x52;
            apdu.data[4] = (byte)llcpParameter.Length;

            apdu.data = Helper.appendArrays(apdu.data, llcpParameter);

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] atrResponse(int giLength, byte[] llcpParameter)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[6];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x52;
            apdu.data[4] = (byte)giLength;
            apdu.data[5] = (byte)llcpParameter.Length;

            apdu.data = Helper.appendArrays(apdu.data, llcpParameter);

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] depResponse(byte[] depMessage, int depLength)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[5];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x53;
            apdu.data[4] = (byte)depLength;

            apdu.data = Helper.appendArrays(apdu.data, depMessage);

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] deselectResponse()
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[5];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x54;
            apdu.data[4] = 0x00;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] releaseResponse()
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[5];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x55;
            apdu.data[4] = 0x00;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] parameterSelectionResponse(byte brs, byte fsl)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[7];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x56;
            apdu.data[4] = 0x02;
            apdu.data[5] = brs;
            apdu.data[6] = fsl;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] wakeUpResponse()
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 7;
            apdu.data = new byte[5];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x57;
            apdu.data[4] = 0x00;

            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public byte[] cardEmulationSendCommand(string sendComand)
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 255;

            apdu.data = Helper.getBytes(sendComand);
            apduCommand = apdu;
            sendCardControl(ref apdu, operationControlCode);

            return apduCommand.response;
        }

        public override byte[] getFirmwareVersion()
        {
            Apdu apdu = new Apdu();
            apdu.lengthExpected = 256;
            apdu.data = new byte[5];

            apdu.data[0] = 0xE0;
            apdu.data[1] = 0x00;
            apdu.data[2] = 0x00;
            apdu.data[3] = 0x18;
            apdu.data[4] = 0x00;

            apduCommand = apdu;
            sendCardControl();

            return apduCommand.response;
        }
    }
}

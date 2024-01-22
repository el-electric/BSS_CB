/*===========================================================================================
 * 
 *  Author          : Arturo Salvamante
 * 
 *  File            : NDEF.cs
 * 
 *  Copyright (C)   : Advanced Card System Ltd
 * 
 *  Description     : Contains Methods and Properties for NDEF
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
using ACS;

namespace ACS.SmartCard.Reader.PCSC.Nfc
{
     public class NdefMessage
    {
        List<NdefRecord> _ndefRecords = new List<NdefRecord>();

        public void appendRecord(NdefRecord record)
        {
            _ndefRecords.Add(record);
        }

        public void insertRecord(int index, NdefRecord record)
        {
            _ndefRecords.Insert(index, record);
        }

        public List<NdefRecord> getRecords()
        {
            return _ndefRecords;
        }

        public NdefRecord getRecord(NdefRecordType recordType)
        {
            foreach (NdefRecord r in _ndefRecords)
            {
                if (r.recordType.typeName == recordType.typeName &&
                   r.recordType.typeNameFormat == recordType.typeNameFormat)
                {
                    return r;
                }
            }

            return null;
        }

        public int getNumberOfRecords()
        {
            return _ndefRecords.Count;
        }

        public byte[] toByteArray()
        {
            byte[] buffer = new byte[0];
            byte[] tmpArray;
            int indx = 0;

            for (int i = 0; i < _ndefRecords.Count; i++)
            {
                indx = buffer.Length;

                if (i == 0)
                    _ndefRecords[i].messageBegin = true;

                if ((i + 1) == _ndefRecords.Count)
                    _ndefRecords[i].messageEnd = true;
                    

                tmpArray = _ndefRecords[i].encodeToNDEF();

                //Resize destination array to acoomodate new record
                Array.Resize(ref buffer, buffer.Length + tmpArray.Length);
                
                //Copy new  ndef record to byte array
                Array.Copy(tmpArray, 0, buffer, indx, tmpArray.Length);
            }

            return buffer;
        }        
    }

    public class NdefRecordType
    {
        public NdefRecordType(TYPE_NAME_FORMAT format, string name)
        {
            _typeNameFormat = format;
            _typeName = name;
        }

        TYPE_NAME_FORMAT _typeNameFormat;
        public TYPE_NAME_FORMAT typeNameFormat
        {
            get { return _typeNameFormat; }
            set { _typeNameFormat = value; }
        }

        string _typeName;
        public string typeName
        {
            get { return _typeName; }
            set 
            {
                if (value.Trim().Length > 255)
                    throw new Exception("Pay load type is too long");

                _typeName = value.Trim();
            }
        }
    }

    public class NdefRecord
    {
        public NdefRecord(NdefRecordType recordType)
        {
            _messageBegin = false;
            _messageEnd = false;
            _isTerminatingRecordChunk = true;
            _recordType = recordType;
        }

        private bool _messageBegin;
        public bool messageBegin
        {
            get { return _messageBegin; }
            set { _messageBegin = value; }
        }

        private bool _messageEnd;
        public bool messageEnd
        {
            get { return _messageEnd; }
            set { _messageEnd = value; }
        }

        private bool _isTerminatingRecordChunk;
        public bool isTerminatingRecordChunk
        {
            get { return _isTerminatingRecordChunk; }
            set { _isTerminatingRecordChunk = value; }
        }
        
        public bool isShortRecord
        {
            get
            {
                if (payLoad.Length < 256) return true;
                else
                    return false;
            }
        }

        private NdefRecordType _recordType;
        public NdefRecordType recordType
        {
            get { return _recordType; }
            set { _recordType = value; }
        }

        private byte[] _payLoad;
        public byte[] payLoad
        {
            get { return _payLoad; }
            set { _payLoad = value; }
        }

        public string payLoadStr
        {
            get { return ASCIIEncoding.ASCII.GetString(_payLoad); }
            set { _payLoad = ASCIIEncoding.ASCII.GetBytes(value);
            }
        }

        private byte[] _messageID = new byte[0];
        public byte[] messageID
        {
            get { return _messageID; }
            set { _messageID = value; }
        }
        
        public byte[] encodeToNDEF()
        {
            byte ndefHeader = 0;
            byte[] ndef = new byte[0];

            //Set NDEF Header
            if (messageBegin)
                ndefHeader |= 0x80;

            if (messageEnd)
                ndefHeader |= 0x40;

            if (!isTerminatingRecordChunk)
                ndefHeader |= 0x20;

            if (payLoad.Length < 256)
                ndefHeader |= 0x10;

            if (messageID.Length > 0)
                ndefHeader |= 0x08;

            ndefHeader |= (byte)recordType.typeNameFormat;


            ndef = Helper.appendArrays(ndef, ndefHeader);

            //Set Payload Type Length
            ndef = Helper.appendArrays(ndef, (byte)recordType.typeName.Length);

            //Set Payload Length
            if (payLoad.Length < 256)
                ndef = Helper.appendArrays(ndef, (byte)payLoad.Length);
            else
                ndef = Helper.appendArrays(ndef, Helper.intToByte(payLoad.Length));

            //Set Message ID Length
            if (messageID != null && messageID.Length > 0)
                ndef = Helper.appendArrays(ndef, (byte)messageID.Length);

            //Set Payload Type
            ndef = Helper.appendArrays(ndef, ASCIIEncoding.ASCII.GetBytes(recordType.typeName.Trim()));

            //Set Message ID
            if (messageID != null && messageID.Length > 0)
                ndef = Helper.appendArrays(ndef, messageID);

            //Set Payload
            ndef =  Helper.appendArrays(ndef, payLoad);
            
            return ndef;
        }
        
        public void appendPayload(byte[] payLoad)
        {
            int indx = 0;

            if (_payLoad == null)
                _payLoad = new byte[0];

            indx = _payLoad.Length;

            Array.Resize(ref _payLoad, _payLoad.Length + payLoad.Length);

            Array.Copy(payLoad, 0, _payLoad, indx, payLoad.Length);
        }

        public void appendPayload(string payLoad)
        {
            int indx = 0;

            if (payLoad == "")
                return;

            if (_payLoad == null)
                _payLoad = new byte[0];

            indx = _payLoad.Length;

            byte[] buffer = ASCIIEncoding.ASCII.GetBytes(payLoad);

            Array.Resize(ref _payLoad, _payLoad.Length + buffer.Length);

            Array.Copy(buffer, 0, _payLoad, indx, buffer.Length);
        }

        public void appendPayload(byte payLoad)
        {
            appendPayload(new byte[] { payLoad });
        }

        public NdefMessage getNestedNdefMessage()
        {
            if (payLoad == null)
                throw new Exception("Payload is not yet been set");

            return getNestedNdefMessage(0, payLoad);
        }

        public static NdefMessage getNestedNdefMessage(int index, byte[] data)
        {
            NdefMessage ndefMessage = new NdefMessage();
            NdefRecord ndefRecord;
            byte typeNameFormat = 0x00;
            byte typeNameLength = 0x00;
            byte idLength = 0x00;

            int payloadLength = 0x00;
            int currentIndex = 0;

            string typeName = "";

            bool isMessageBeginSet = false;
            bool isMessageEndSet = false;
            bool isIdLengthPresent = false;
            bool isChunkFlagPresent = false;
            bool isShortRecordSet = false;


            if (data.Length <= index)
                throw new Exception("Invalid index");


            for (; index < data.Length;)
            {
                currentIndex = index;

                //Get Type Name Format
                //e.g. NFC Well Know Type = 0x01
                typeNameFormat = (byte)(data[currentIndex] & 0x07);

                if (typeNameFormat != 0x01)
                    throw new Exception("Type Name Format " + ((TYPE_NAME_FORMAT)(typeNameFormat)).ToString() + " is not supported.");
                
                //Check if Message Begin (Bit 7) is set 
                if ((data[currentIndex] & 0x80) != 0x00)
                    isMessageBeginSet = true;
                else
                    isMessageBeginSet = false;

                //Check if Message End (Bit 6) is set
                if ((data[currentIndex] & 0x40) != 0x00)
                    isMessageEndSet = true;
                else
                    isMessageEndSet = false;

                //Check if Chunk Flag (Bit 5) is set
                if ((data[currentIndex] & 0x20) != 0x00)
                    isChunkFlagPresent = true;
                else
                    isChunkFlagPresent = false;

                //Check if Short Record (bit 4) is set
                if ((data[currentIndex] & 0x10) != 0x00)
                    isShortRecordSet = true;
                else
                    isShortRecordSet = false;

                //Check if ID length is set
                if ((data[currentIndex] & 0x08) != 0x00)
                    isIdLengthPresent = true;
                else
                    isIdLengthPresent = false;


                currentIndex += 1;


                //get the type length
                //Refer for Short Record section of NFC Data Exchange Format
                //Technical Specification for more information
                typeNameLength = data[currentIndex];
                currentIndex += 1;

                if (isShortRecordSet)
                {
                    //For Short Record payload, length is 1 byte
                    //Get Payload Length    
                    payloadLength = data[currentIndex];
                    currentIndex += 1;
                }
                else
                {
                    //For Non Short Record payload, length is 4 bytes
                    payloadLength = Helper.byteToInt(data.Skip(currentIndex).Take(4).ToArray());
                    currentIndex += 4;
                }

                if (isIdLengthPresent)
                {
                    //+1 to get ID Length
                    idLength = data[currentIndex];
                    currentIndex += 1;
                }

                //+1 to get Payload Type Name
                //Payload Type Name offset = currentIndex to currentIndex  + Type Name Length
                typeName = ASCIIEncoding.ASCII.GetString(data.Skip(currentIndex).Take(typeNameLength).ToArray());
                currentIndex += typeNameLength;
                
                //Initialize new ndef record object
                ndefRecord = new NdefRecord(new NdefRecordType((TYPE_NAME_FORMAT)typeNameFormat, typeName));
                ndefRecord.messageBegin = isMessageBeginSet;
                ndefRecord.messageEnd = isMessageEndSet;
                ndefRecord.isTerminatingRecordChunk = !isChunkFlagPresent;

                //If ID Length is present get record ID
                if (isIdLengthPresent)
                {
                    ndefRecord.messageID = data.Skip(currentIndex).Take(idLength).ToArray();
                    currentIndex += idLength;
                }

                ndefRecord.payLoad = data.Skip(currentIndex).Take(payloadLength).ToArray();
                currentIndex += payloadLength;
                index = currentIndex;


                ndefMessage.appendRecord(ndefRecord);
            }

            return ndefMessage;
        }
    }

    public class Ndef
    {
        /// <summary>
        /// Encode the following paramter for NDEF SmartPoster
        /// </summary>
        /// <param name="titleLanguage"></param>
        /// <param name="title"></param>
        /// <param name="uriPrefix"></param>
        /// <param name="uri"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static byte[] encodeSmartPoster(string titleLanguage, string title, URI_IDENTIFIER_CODE uriPrefix, string uri, ACTION_RECORD? action)
        {
            byte[] buffer = new byte[0];
            bool useUTF8 = true;
            byte statusByte = 0;

            NdefRecord tmpRecord;
            NdefMessage ndefMessage = new NdefMessage();

            //Encode Title
            if (title.Trim() != "")
            {
                //If UTF 8 set bit 7 to 0
                //If UTF 16 set bit 7 to 1
                if(!useUTF8)
                    statusByte = 0x80;
                   
                //Length of the Language (ISO/IANA language code)
                //ex. "en-US"
                statusByte |= (byte)titleLanguage.Length;

                tmpRecord = new NdefRecord(new NdefRecordType(TYPE_NAME_FORMAT.NFC_FORM_WELL_KNOWN_TYPE, "T"));
                tmpRecord.messageBegin = true;
                tmpRecord.appendPayload(statusByte);
                tmpRecord.appendPayload(titleLanguage);
                tmpRecord.appendPayload(title);

                ndefMessage.appendRecord(tmpRecord);
            }

            
            //Encode Action            
            if (action != null && action.HasValue)
            {
                tmpRecord = new NdefRecord(new NdefRecordType(TYPE_NAME_FORMAT.NFC_FORM_WELL_KNOWN_TYPE, "act"));
                tmpRecord.appendPayload((byte)action);
                ndefMessage.appendRecord(tmpRecord);
            }


            //Encode URI
            tmpRecord = new NdefRecord(new NdefRecordType(TYPE_NAME_FORMAT.NFC_FORM_WELL_KNOWN_TYPE, "U"));

            
            //URI Prefix
            //See URI Record Type Definition 
            //http://www.nfc-forum.org
            tmpRecord.appendPayload((byte)uriPrefix);
            tmpRecord.appendPayload(uri);

            ndefMessage.appendRecord(tmpRecord);


            buffer = ndefMessage.toByteArray();

            //Smart Poster Header
            tmpRecord = new NdefRecord(new NdefRecordType(TYPE_NAME_FORMAT.NFC_FORM_WELL_KNOWN_TYPE, "Sp"));
            tmpRecord.messageBegin = true;
            tmpRecord.messageEnd = true;
            tmpRecord.appendPayload(buffer);

            buffer = tmpRecord.encodeToNDEF();

            //Create NDEF Message for SmartPoster
            ndefMessage = new NdefMessage();
            ndefMessage.appendRecord(tmpRecord);

            //Encode NDEF Message
            buffer = ndefMessage.toByteArray();

            return buffer;
            
        }

        public static byte[] encodeNfcForumExternalType(string type, string payload)
        {
            byte[] buffer = new byte[0];

            NdefRecord tmpRecord;
            NdefMessage ndefMessage = new NdefMessage();


            tmpRecord = new NdefRecord(new NdefRecordType(TYPE_NAME_FORMAT.NFC_FORM_EXTERNAL_TYPE, type));
            tmpRecord.messageBegin = true;
            tmpRecord.messageEnd = true;
            tmpRecord.appendPayload(payload);

            buffer = tmpRecord.encodeToNDEF();

            //Create NDEF Message for SmartPoster
            ndefMessage = new NdefMessage();
            ndefMessage.appendRecord(tmpRecord);

            //Encode NDEF Message
            buffer = ndefMessage.toByteArray();

            return buffer;

        }


        public static string getURIIdentifierCode(URI_IDENTIFIER_CODE code)
        {
            //For more information please refer to
            // URI Record Type Definition Technical Specification
            switch (code)
            {
                case URI_IDENTIFIER_CODE.HTTP: return "http://";
                case URI_IDENTIFIER_CODE.HTTPS: return "https://";
                case URI_IDENTIFIER_CODE.HTTP_WWW: return "http://www.";
                case URI_IDENTIFIER_CODE.HTTPS_WWW: return "https://www.";
                case URI_IDENTIFIER_CODE.TEL: return "tel:";
                case URI_IDENTIFIER_CODE.MAIL_TO: return "mailto:";
                case URI_IDENTIFIER_CODE.FTP: return "ftp://";
                case URI_IDENTIFIER_CODE.NONE: return "";
                default: return "Unknown";
            }
        }

        public static string decodeNdefMessage(byte[] data)
        {
            int i = 0;
            int j = 0;
            int index = 0;
            int recordLen = 0;
            int payloadLen = 0;
            int payloadSize = 0;

            int[] payloadLength;
            byte[] displayText;

            byte typeNameFormat = 0x00;

            string displayMessage = "";
            string recordName = "";

            bool isMessageBeginSet = false;
            bool isMessageEndSet = false;
            bool isIdLengthPresent = false;
            bool isChunkFlagPresent = false;
            bool isShortRecordSet = false;

            if (data.Length <= index)
                throw new Exception("Invalid index");

            payloadSize = data[2];

            while (index < payloadSize)
            {
                // Get type name format
                //e.g. NFC Well know type = 0x01
                typeNameFormat = (byte)(data[index] & 0x07);

                if (typeNameFormat != 0x01 && typeNameFormat != 0x02)
                {
                    string message = Convert.ToString((TYPE_NAME_FORMAT)(typeNameFormat)) + " is not supported";
                    throw new Exception(message);
                }

                // Check if Message begin (Bit 7) is set
                if ((data[index] & 0x80) != 0x00)
                    isMessageBeginSet = true;
                else
                    isMessageBeginSet = false;

                // Check if message End (Bit 6) is set
                if ((data[index] & 0x40) != 0x00)
                    isMessageEndSet = true;
                else
                    isMessageEndSet = false;

                // Check if Chunk Flag (Bit 5) is set
                if ((data[index] & 0x20) != 0x00)
                    isChunkFlagPresent = true;
                else
                    isChunkFlagPresent = false;

                // Check if Short Record (bit 4) is set
                if ((data[index] & 0x10) != 0x00)
                    isShortRecordSet = true;
                else
                    isShortRecordSet = false;

                index++;

                recordLen = data[index];
                index++;

                if (isShortRecordSet)
                    payloadLen = 1;
                else
                    payloadLen = 4;

                payloadLength = new int[payloadLen];
                for (i = 0; i < payloadLen; i++)
                {
                    payloadLength[i] = data[index];
                    index++;
                }

                displayMessage += "Record Name: ";
                recordName = "";
                for (i = 0; i < recordLen; i++)
                {
                    recordName += (char)data[index];
                    displayMessage += (char)data[index];
                    index++;
                }

                displayMessage += "\r\n";

                if (recordName == "Sp")
                {

                }
                else if (recordName == "T")
                {
                    for (j = 0; j < payloadLen; j++)
                    {
                        int iStatusByteLen = data[index] & 0x3F;
                        index++;

                        displayMessage += "ISO Language Code: ";

                        for (i = 0; i < iStatusByteLen; i++)
                        {
                            displayMessage += (char)data[index];
                            index++;
                        }

                        displayMessage += "\r\nText: ";

                        displayText = new byte[payloadLength[j] - iStatusByteLen - 1];
                        Array.Copy(data, index, displayText, 0, payloadLength[j] - iStatusByteLen - 1);
                        displayMessage += ASCIIEncoding.ASCII.GetString(displayText);
                        displayMessage += "\r\n";

                        index += (payloadLength[j] - iStatusByteLen - 1);
                    }
                }
                else if (recordName == "U")
                {
                    for (j = 0; j < payloadLen; j++)
                    {
                        displayMessage += "Abbreviation: ";
                        displayMessage += getURIIdentifierCode((URI_IDENTIFIER_CODE)data[index]);
                        index++;

                        displayMessage += "\r\nURI: ";

                        displayText = new byte[payloadLength[j] - 1];
                        Array.Copy(data, index, displayText, 0, payloadLength[j] - 1);
                        displayMessage += ASCIIEncoding.ASCII.GetString(displayText);
                        displayMessage += "\r\n";

                        index += (payloadLength[j] - 1);
                    }
                }
                else if (recordName == "act")
                {
                    displayMessage += "Action: ";
                    displayMessage += getActionCode((ACTION_RECORD)data[index]);
                    displayMessage += "\r\n";

                    for (j = 0; j < payloadLen; j++)
                    {
                        index += payloadLength[j];
                    }
                }
                //VD 10/20/2015
                else if (recordName.Equals("text/x-vcard", StringComparison.InvariantCultureIgnoreCase))
                //else if (sRecordName == "text/x-vCard")
                {
                    displayMessage += "vCard: ";
                    for (j = 0; j < payloadLen; j++)
                    {
                        int iCurrIdx = 0;
                        int iEndIdx = 0;
                        int iIdx = 0;
                        char sSeparator = ';';
                        string sDisplayTextTemp = "";

                        for (i = 0; i < payloadLen; i++)
                        {

                            displayText = new byte[payloadLength[j] - 1];
                            Array.Copy(data, index, displayText, 0, payloadLength[j] - 1);
                            sDisplayTextTemp += ASCIIEncoding.ASCII.GetString(displayText);
                            iCurrIdx = sDisplayTextTemp.IndexOf("\r\nN:", 0);
                            //VD 10/20/15
                            iEndIdx = sDisplayTextTemp.IndexOf("END", 0);
                            if (iEndIdx == -1)
                                iEndIdx = payloadLength[j] - 1;
                            //iEndIdx = sDisplayTextTemp.IndexOf("END:VCARD", 0);
                            for (iCurrIdx += 4; iCurrIdx < iEndIdx; iCurrIdx++)
                            {
                                if (sDisplayTextTemp[iCurrIdx] == sSeparator)
                                    displayMessage += " ";
                                else
                                    displayMessage += sDisplayTextTemp[iCurrIdx];
                             }
                        }

                        index += payloadLength[j];
                    }
                }

            }

            return displayMessage;
        }

        public static string getActionCode(ACTION_RECORD code)
        {
            switch (code)
            {
                case ACTION_RECORD.DO_THE_ACTION: return "Do the action";
                case ACTION_RECORD.SAVE_FOR_LATER: return "Save for later";
                case ACTION_RECORD.OPEN_FOR_EDITING: return "Open for editing";
                default: return "";
            }
        }
    }
}

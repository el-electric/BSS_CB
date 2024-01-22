using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.CRC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board.Packets
{
    abstract public class NFCBoard_Packet_Base_Send : NFCBoard_Packet_Base
    {

        protected byte[] mCmd = null;
        public NFCBoard_Packet_Base_Send(MyApplication application, int channelIndex, int length_vd, byte[] cmd) : base(application, channelIndex, length_vd)
        {
            mCmd = cmd;

            send_mSendData = new byte[28];

            send_mSendData[CONST_NFC_Board.INDEX_STX] = CONST_NFC_Board.VALUE_STX;
            send_mSendData[4] = 1;
            send_mSendData[5] = (byte)mChannelIndex;

            send_mSendData[6] = (byte)'M';
            send_mSendData[7] = (byte)'S';

            send_mSendData[8] = (byte)'z';
            send_mSendData[9] = (byte)'1';

            send_mSendData[10] = 0;
            send_mSendData[11] = 13;

            send_mSendData[12] = 0;
            send_mSendData[13] = 1;

            send_mSendData[14] = 0;

            /*int length_Data = mLength_VD + 2 + CONST_NFC_Board.LENGTH_RD_HMI2BOARD;

            send_mSendData[CONST_NFC_Board.INDEX_LENGTH_DATA] = (byte)(length_Data >> 8);
            send_mSendData[CONST_NFC_Board.INDEX_LENGTH_DATA + 1] = (byte)(length_Data);*/

            /*send_mSendData[CONST_NFC_Board.INDEX_LENGTH_RD] = (byte)(CONST_NFC_Board.LENGTH_RD_HMI2BOARD >> 8);
            send_mSendData[CONST_NFC_Board.INDEX_LENGTH_RD + 1] = (byte)(CONST_NFC_Board.LENGTH_RD_HMI2BOARD);*/




            send_mSendData[send_mSendData.Length - 1] = CONST_NFC_Board.VALUE_ETX;
        }
        public byte[] SendData
        {
            get {

                send_setCheckSum();
                return send_mSendData; }
        }
        protected byte[] send_mSendData = null;

        protected void send_setCheckSum()
        {
            byte[] temp = CRC16.getCRC16_CCITT(send_mSendData, 0, send_mSendData.Length);
            send_mSendData[send_mSendData.Length - 3] = temp[0];
            send_mSendData[send_mSendData.Length - 2] = temp[1];
        }

        virtual public void send_ApplyData()
        {
            /*byte valueCheck = 0x00000008;
            int index = 0;

            if (mApplication.SerialPort_NFCBoard.getManager_Send().IsInitComplete_a1_Send)
                send_mSendData[CONST_NFC_Board.INDEX_RD + index] = (byte)(send_mSendData[CONST_NFC_Board.INDEX_RD + index] | valueCheck);
            else
                send_mSendData[CONST_NFC_Board.INDEX_RD + index] = (byte)(send_mSendData[CONST_NFC_Board.INDEX_RD + index] & ~valueCheck);*/
        }

    }
}

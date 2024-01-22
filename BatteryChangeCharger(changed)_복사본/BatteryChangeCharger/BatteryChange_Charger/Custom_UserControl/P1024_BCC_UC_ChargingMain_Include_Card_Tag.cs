using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using BatteryChangeCharger.Applications;
using BatteryChangeCharger.Manager;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    public partial class P1024_BCC_UC_ChargingMain_Include_Card_Tag : UserControl
    {
        byte[] data = new byte[37];  // 보내는 총 데이터 패킷 길이
        public string Card_Number = null;
        public P1024_BCC_UC_ChargingMain_Include_Card_Tag()
        {
            InitializeComponent();

            serialPort1.PortName = "COM7";
            serialPort1.BaudRate = 115200;

            serialPort1.Open();

            send_RF_Reader();

        }
        protected void send_setCheckSum(byte[] arrays)
        {
            byte crc = getCheckSum(arrays);
            arrays[arrays.Length - 1] = crc;
        }

        public static byte getCheckSum(byte[] packet)
        {
            byte checksum = packet[0];
            for (int i = 1; i < packet.Length - 1; i++)
            {
                checksum ^= packet[i];
            }
            return checksum;
        }

        public void Write(byte[] bytes)
        {
            if (serialPort1.IsOpen)
                serialPort1.Write(bytes, 0, bytes.Length);
            else
            {
                Console.WriteLine("포트가 닫혀 있습니다.");
                //serial.Close();

                //await Task.Run(() => Open(Model.Master_PortName));
            }
        }

        protected byte[] mData_Temp = new byte[4096];
        int readSize = 0;
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            readSize = serialPort1.Read(mData_Temp, 0, mData_Temp.Length);

            if (readSize < 1)
                return;

            for (int i = 0; i < readSize; i++)
                processData(mData_Temp[i]);
        }

        public static int getInt_2Byte(byte data1, byte data2) => (data1 << 8 | data2) & 0x0000ffff;

        protected byte[] compare_Data(byte[] receive_Data, int startIndexArray, int finishIndex)
        {
            if (IsCorrectData(receive_Data, startIndexArray, finishIndex))
            {
                byte[] returnData = new byte[finishIndex - startIndexArray];
                Array.Copy(receive_Data, startIndexArray, returnData, 0, returnData.Length);
                return returnData;
            }
            return null;
        }

        public static bool isCheckSum(byte[] packet, int startIndexArray, int finishIndex)
        {
            byte checksum = packet[startIndexArray];
            for (int i = startIndexArray + 1; i < finishIndex - 1; i++)
            {
                checksum ^= packet[i];
            }

            if (packet[finishIndex - 1] == checksum)
                return true;

            return false;
        }

        public static bool IsCorrectData(byte[] data, int startIndexArray, int finishIndex)
        {
            if (data[startIndexArray] != (byte)0x02)
                return false;

            if (data[finishIndex - 2] != (byte)0x03)
                return false;

            int packetLength = getInt_2Byte(data[startIndexArray + 34], data[startIndexArray + 33]) + 37;

            if (finishIndex - startIndexArray != packetLength)
                return false;

            bool result = isCheckSum(data, startIndexArray, startIndexArray + packetLength);

            return result;
        }

        protected int mCountData = -1;
        protected byte[] mReceive_Data = new byte[2048];
        protected byte[] mReceiveData_Data = new byte[53];
        protected void processData(byte data)
        {
            if (mCountData >= (mReceive_Data.Length - 1))
            {
                mCountData = -1;
            }

            mCountData++;
            mReceive_Data[mCountData] = data;

            if (mCountData >= 37 - 1 && mReceive_Data[mCountData - 1] == (byte)0x03)
            {

                for (int i = mCountData - 38; i > -1; i--)
                {
                    if ((byte)0x02 == mReceive_Data[i])
                    {
                        byte[] data_copyed = compare_Data(mReceive_Data, i, mCountData + 1);

                        if (data_copyed == null)
                        {
                            continue;
                        }

                        if (data_copyed[31] == (byte)'d')
                        {

                            Array.Copy(data_copyed, 35, mReceiveData_Data, 0, 53);

                            if (mReceiveData_Data == null) return;

                            byte[] temp = new byte[16];
                            int indexArray = 6;
                            for (i = 0; i < temp.Length; i++)
                            {
                                temp[i] = mReceiveData_Data[indexArray++];
                            }
                            Card_Number = ASCIIEncoding.ASCII.GetString(temp);

                            /*if (Card_Number.Length > 16)
                            {
                                Card_Number = Card_Number.Substring(Card_Number.Length - 16);
                            }*/
                            mCountData = -1;
                            this.Invoke(new MethodInvoker(delegate ()
                            {
                                MyApplication.getInstance().Card_Number = Card_Number;
                                MyApplication.getInstance().manager_time.
                            }));

                            return;
                        }
                    }

                }

            }
        }


        public void send_RF_Reader()
        {
            data[0] = 0x02;
            data[1] = (byte)'K';  //75
            data[2] = (byte)'I';  //73
            data[3] = (byte)'O';  //79
            data[4] = (byte)'S';  //82 
            data[5] = (byte)'K';  //75
            data[6] = (byte)'1';  //49
            data[7] = (byte)'1';  //49
            data[8] = (byte)'1';  //49 
            data[9] = (byte)'4';  //52
            data[10] = (byte)'9'; //57
            data[11] = (byte)'1'; //49
            data[12] = (byte)'5'; //53
            data[13] = (byte)'5'; //53
            data[14] = (byte)'4'; //52
            data[15] = (byte)'5'; //53
            data[16] = 0;

            byte[] temp = Manager_Time.getTime_ASCii_14Byte();

            for (int i = 1; i < temp.Length; i++)
            {
                data[16 + i] = temp[i];
            }

            data[31] = 0x44;

            /*data[32] = (byte)(37 & 0x000000ff);
            data[33] = (byte)((37 >> 8) & 0x000000ff);*/

            data[35] = 0x03;
            send_setCheckSum(data);

            Write(data);
        }

    }
}

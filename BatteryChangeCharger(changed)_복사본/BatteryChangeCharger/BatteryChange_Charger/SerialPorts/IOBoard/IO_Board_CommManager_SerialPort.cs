using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.CRC;
using BatteryChangeCharger.Manager;
using ParkingControlCharger.Object;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard
{
    public class IO_Board_CommManager_SerialPort : MManager_Comm
    {
        private static List<byte> mReceive_Data = new List<byte>();

        public string PATH = "COM6"; //path
        public static string sPATH = "COM5";
        protected const int BAUDRATE = 38400; //보레이트

        protected SerialPort mSerialPort = null;

        //protected static HS_ControlBd_BoardComm_Manager_SerialPort mPort = null;
        //public static HS_ControlBd_BoardComm_Manager_SerialPort getInstance()
        //{
        //    if (mPort != null)
        //        return mPort;

        //    sPATH = "COM" + MyApplication.getInstance().getSettingData_System().getSettingData(EINDEX_SETTING_MAIN.path);

        //    if (!Manager_SerialPort.isConnected_SerialPort(sPATH)) //sPath에 연결됐는지
        //        return null;

        //    mPort = new HS_ControlBd_BoardComm_Manager_SerialPort(MyApplication.getInstance());
        //    return mPort;
        //}

        public IO_Board_CommManager_SerialPort(MyApplication application) : base(application, 500)
        {
            mPath_Commport = application.Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.PATH_SERIAL_IOBOARD);
            //mPath_Commport = "COM8";//PATH;
            if (isExist_SerialPort(mPath_Commport) && isPossible_SerialPort())
            {
                mSerialPort = new SerialPort(mPath_Commport, BAUDRATE);
                try
                {
                    //if (!mSerialPort.IsOpen)
                    //    mSerialPort.Open();
                    mSerialPort.DataReceived += serialPort1_DataReceived; //시리얼포트 데이터를 받음
                    bIsConnected_HW = true;
                }
                catch (Exception e)
                {

                }

            }
        }

        public override void init()
        {
            mManager_Send = new IO_Board_CommManager_Send(mApplication, this);
        }

        public override void commClose()
        { // 닫힘
            if (mSerialPort == null)
            {
                bIsConnected = false;
                return;
            }

            mSerialPort.Close();
            mSerialPort.Dispose();
            bIsConnected = false;
        }

        public override void commOpen()
        {

            if ((!isPossible_SerialPort() && bIsConnected) || mSerialPort == null)
            {
                bIsConnected = false;
                commClose();
                return;
            }

            try
            {
                mSerialPort.Open();
                bIsConnected = true;
            }
            catch (Exception ex)
            {
                bIsConnected = false;
            }
        }

        public override string getPath_SerialPort() => mPath_Commport;
        protected override void timer_SendMessage_Tick() => mManager_Send.sendData_Interval(); //타이머
        protected IO_Board_CommManager_Send mManager_Send = null;
        public IO_Board_CommManager_Send getManager_Send() => mManager_Send;

        public override bool write(byte[] data)
        {
            if (mSerialPort == null)
                return false;

            try
            {
                mSerialPort.Write(data, 0, data.Length);
                bIs_FaultSerial = false;
            }
            catch (Exception ex)
            {
                if (!bIs_FaultSerial)
                {
                    bIs_FaultSerial = true;
                    mTime_FaultSerial.setTime();
                    commClose();
                }
            }
            finally
            {
                if (bIs_FaultSerial && mTime_FaultSerial.getSecond_WastedTime() > 15)
                {
                    try
                    {
                        commOpen();
                    }
                    catch (Exception ex)
                    {
                        bIs_FaultSerial = true;
                        mTime_FaultSerial.setTime();
                        commClose();
                    }

                }
            }
            return true;
        }

        protected JSH_Time mTime_FaultSerial = new JSH_Time();
        bool bIs_FaultSerial = false;

        protected byte[] mData_Temp = new byte[4096];

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            /*int readSize = mSerialPort.Read(mData_Temp, 0, mData_Temp.Length);

            if (readSize < 1)
                return;

            for (int i = 0; i < readSize; i++)
                processData(mData_Temp[i]);*/

            int bytesToRead = mSerialPort.BytesToRead;
            byte[] receivedData = new byte[bytesToRead];
            mSerialPort.Read(receivedData, 0, bytesToRead);

            // processData 함수에 전체 바이트 배열을 한 번에 전달
            //await Task.Run(() => processData(receivedData));
            processData(receivedData);
        }

        /*protected int mCountData = -1;
        protected byte[] mReceive_Data = new byte[2048];

        public void processData(byte data)
        {
            if (mCountData >= (mReceive_Data.Length - 1))
                mCountData = -1;

            mCountData++;
            mReceive_Data[mCountData] = data;

            if (mReceive_Data[mCountData] == CONST_IO_Board.VALUE_ETX
                && mCountData >= CONST_IO_Board.LENGTH_DEFUALT - 1)
            {
                for (int i = mCountData - CONST_IO_Board.LENGTH_DEFUALT + 1; i > -1; i--)
                    if (CONST_IO_Board.VALUE_STX == mReceive_Data[i])
                    {
                        byte[] data_copyed = compare_Data(mReceive_Data, i, mCountData + 1);

                        if (data_copyed == null)
                            continue;
                        setLastComm();

                        onReceiveCorrectPacket(data_copyed);
                        mCountData = -1;
                        return;
                    }
            }
        }*/

        public void processData(byte[] data)
        {
            mReceive_Data.AddRange(data);

            while (true)
            {
                int startIndex = mReceive_Data.IndexOf(0xfe); // STX
                int endIndex = mReceive_Data.IndexOf(0xff); // ETX

                /* if (mReceive_Data.Count < 41 && mReceive_Data.Count > 33)
                {


                    if (mReceive_Data[startIndex + 23] == 255)
                        mReceive_Data[startIndex + 23] = 0;
                    if (mReceive_Data[startIndex + 25] == 255)
                        mReceive_Data[startIndex + 25] = 0;
                    if (mReceive_Data[startIndex + 29] == 255)
                        mReceive_Data[startIndex + 29] = 0;
                    if (mReceive_Data[startIndex + 31] == 255)
                        mReceive_Data[startIndex + 31] = 0;
                } */
                // startIndex = mReceive_Data.IndexOf(0xfe); // STX
                // endIndex = mReceive_Data.IndexOf(0xff); // ETX
                // 완전한 패킷이 수신될 때까지 기다림
                if (startIndex != -1 && endIndex != -1)
                {
                    if (endIndex > startIndex && endIndex - startIndex == 40) // z1 받을때
                    {
                        byte[] packet = mReceive_Data.GetRange(startIndex, 41).ToArray();
                        mManager_Send.mManager_Packet.mPacket_z1_Receive.receive_ApplyData(packet);
                        mReceive_Data.RemoveRange(0, endIndex + 1);
                    }
                    else if (endIndex > startIndex && endIndex - startIndex == 54) // cb 받을때
                    {
                        byte[] packet = mReceive_Data.GetRange(startIndex, 55).ToArray();
                        mManager_Send.mManager_Packet.mPacket_a1_Receive.receive_ApplyData(packet);
                        mReceive_Data.RemoveRange(0, endIndex + 1);
                    }
                    else
                    {
                        // 유효하지 않은 데이터 제거 (ETX가 STX보다 먼저 나오거나 패킷 길이가 71이 아닌 경우)
                        mReceive_Data.RemoveRange(0, endIndex + 1);
                    }

                    // 버퍼에 더 이상 데이터가 없으면 반복 중단
                    if (mReceive_Data.Count == 0)
                    {
                        break;
                    }
                }
                else if (startIndex == -1 && endIndex != -1)
                {
                    // STX 없이 ETX만 있는 경우, ETX 이전의 데이터 제거
                    mReceive_Data.RemoveRange(0, endIndex + 1);
                }
                else
                {
                    // 유효한 패킷이 없거나 더 이상의 데이터가 없으면 반복 중단
                    break;
                }
            }
        }





            public void onReceiveCorrectPacket(byte[] packet)
        {
            if (packet == null)
                return;

            if (packet[CONST_NFC_Board.INDEX_PROTOCOL_ID] != CONST_NFC_Board.VALUE_PROTOCOL_ID[0]
               && packet[CONST_NFC_Board.INDEX_PROTOCOL_ID + 1] != CONST_NFC_Board.VALUE_PROTOCOL_ID[1])
                return;

            int index_Cmd = CONST_IO_Board.INDEX_CMD;
            int channelIndex = (packet[CONST_NFC_Board.INDEX_CHARGER_ID]) & 0x000000ff;
            byte[] ins = new byte[] { packet[index_Cmd], packet[index_Cmd + 1] };

            setLastComm();

            bIsReceiveData = true;
            switch (ins[1])
            {
                case (byte)'c':
                    switch (ins[0])
                    {
                        case (byte)'b':
                            mManager_Send.mManager_Packet.mPacket_a1_Receive.receive_ApplyData(packet);
                            break;
                    }
                    break;
                case (byte)'z':
                    switch (ins[0])
                    {
                        case (byte)'1':
                            mManager_Send.mManager_Packet.mPacket_z1_Receive.receive_ApplyData(packet);
                            break;
                    }
                    break;
            }
        }


        protected byte[] compare_Data(byte[] receive_Data, int startIndexArray, int finishIndex)
        {
            if (receive_Data == null)
                return null;

            if (finishIndex - startIndexArray < CONST_IO_Board.LENGTH_DEFUALT)
                return null;

            if (receive_Data[startIndexArray] != CONST_IO_Board.VALUE_STX)
                return null; ;

            if (receive_Data[finishIndex - 1] != CONST_IO_Board.VALUE_ETX)
                return null;

            int Length = (0x0000ff00 & (receive_Data[startIndexArray + CONST_IO_Board.INDEX_LENGTH_DATA] << 8))
                    | (0x000000ff & receive_Data[startIndexArray + CONST_IO_Board.INDEX_LENGTH_DATA + 1]);

            if ((finishIndex - startIndexArray)!= Length + CONST_IO_Board.LENGTH_DEFUALT)
                return null;

            byte[] crc = CRC16.getCRC16_CCITT(receive_Data, startIndexArray, finishIndex);

            if (crc[0] != receive_Data[finishIndex - 3]
                    || crc[1] != receive_Data[finishIndex - 2])
            {
                return null;
            }

            byte[] returnData = new byte[finishIndex - startIndexArray];
            Array.Copy(receive_Data, startIndexArray, returnData, 0, returnData.Length);
            return returnData;
        }





        public override bool isConnected_Comm()
        {


            return base.isConnected_Comm();
        }
    }

}
using Acs;
using Acs.Readers.Pcsc;
using ACS.SmartCard.Reader.PCSC.Nfc;
using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board;
using BatteryChangeCharger.Manager;
using BatteryChangeCharger.SmartCardReader_PCSC.jsh_customize;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BatteryChangeCharger.SmartCardReader_PCSC
{
    public partial class Form_SmartCardReader_PCSC : Form
    {
        protected SCR_PacketManager mSCR_PacketManager = null;

        /*protected Test_NFCBoard_CommManager_SerialPort mSerialPort_NFCBoard = null;*/
        //protected IOBoard_CommManager_SerialPort mSerialPort_IOBoard = null;

        protected System.Windows.Forms.Timer mTimer = new System.Windows.Forms.Timer();
        public Form_SmartCardReader_PCSC()
        {
            InitializeComponent();


            /*tb_batterycontrol_soc_chargingstart.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.BATTERYCONTROL_SOC_CHARGINGSTART);
            tb_batterycontrol_soc_chargingstop.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.BATTERYCONTROL_SOC_CHARGINGSTOP);
            tb_batterycontrol_temp_stop_output.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.BATTERYCONTROL_TEMPERATURE_STOP_OUTPUT);

            tb_batterycontrol_temp_start_control.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.BATTERYCONTROL_TEMPERATURE_START_CONTROL);
            tb_batterycontrol_controlstep_current_percent.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.BATTERYCONTROL_CONTROLSTEP_CURRENT_PERCENT);
            tb_batterycontrol_controlstep_temp.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.BATTERYCONTROL_CONTROLSTEP_TEMPERATURE);*/

            if (mSCR_PacketManager == null)
                mSCR_PacketManager = new SCR_PacketManager(MyApplication.getInstance(), 1);

            /*mSerialPort_NFCBoard = new Test_NFCBoard_CommManager_SerialPort(MyApplication.getInstance());
            mSerialPort_NFCBoard.init();
            mSerialPort_NFCBoard.commOpen();
            mSerialPort_NFCBoard.start();*/

            //mSerialPort_IOBoard = new IOBoard_CommManager_SerialPort(MyApplication.getInstance());
            //mSerialPort_IOBoard.init();
            //mSerialPort_IOBoard.commOpen();

            resetApplication();
            mTimer.Interval = 1000;
            mTimer.Tick += timer_process_Tick;
            mTimer.Enabled = true;
            mTimer.Start();

        }

        JSH_Time mTime_Update = new JSH_Time();

        protected bool IS_TEST = false;
        protected JSH_Time mTime_LogClear = new JSH_Time();
        protected JSH_Time mTime_DataSave = new JSH_Time();
        protected JSH_Time mTime_DataSave_Start = new JSH_Time();
        private void timer_process_Tick(object sender, EventArgs e)
        {
            if(mTime_Update.getSecond_WastedTime() > 60)
            {
                if (cb_autoupdate.Checked
                    && mSelect_DivideCode_Chart != null
                    && mSelect_DivideCode_Chart.Length >= 6)
                {
                    DataTable table = DataManager_ChargingInfor_List.getInstance().queryColumn(
                    new int[]
                    {
                        CINDEX_CHARGINGINFOR_LIST.bmu_thmax,
                        CINDEX_CHARGINGINFOR_LIST.bmu_thmin,
                        CINDEX_CHARGINGINFOR_LIST.bmu_pack_out,
                        CINDEX_CHARGINGINFOR_LIST.bmu_current,
                        CINDEX_CHARGINGINFOR_LIST.bmu_soc,
                        CINDEX_CHARGINGINFOR_LIST.wasted_second
                    },
                    0,
                    0,
                    CINDEX_CHARGINGINFOR_LIST.wasted_second,
                    " where " + DataManager_ChargingInfor_List.getInstance().getColumnName(CINDEX_CHARGINGINFOR_LIST.dividecode) + " = '" + mSelect_DivideCode_Chart + "'",
                    "ASC");

                        chart1.Titles.Clear();
                        chart1.Titles.Add(mSelect_DivideCode_Chart + " 온도변화");

                        chart1.Series[0].LegendText = "최고온도";
                        chart1.Series[1].LegendText = "최저온도";
                        chart1.Series[2].LegendText = "출력전압";
                        chart1.Series[3].LegendText = "전류";
                        chart1.Series[4].LegendText = "SOC";

                        for (int i = 0; i < chart1.Series.Count; i++)
                        {
                            chart1.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                            chart1.Series[i].MarkerSize = 5;
                            chart1.Series[i].Points.Clear();
                        }

                        int count = chart1.Series.Count;
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            for (int j = 0; j < chart1.Series.Count; j++)
                            {
                                if (j == 4)
                                    chart1.Series[j].Points.AddXY((Int64)table.Rows[i][count] / 60, ((Int64)table.Rows[i][j]));
                                else
                                    chart1.Series[j].Points.AddXY((Int64)table.Rows[i][count] / 60, ((Double)table.Rows[i][j]));
                            }

                        }
                }
                mTime_Update.setTime();
            }

            if(cb_clearlog.Checked && mTime_LogClear.getSecond_WastedTime() > 120)
            {
                RichTextBoxLogs.Clear();
                mTime_LogClear.setTime();
            }

            if (bIsChargingStart)
                label_time_chargingstart.Text = mTime_ChargingStart.getSecond_WastedTime_hhMMss();

            if (bIsDataSave)
                label_time_savedata.Text = mTime_DataSave_Start.getSecond_WastedTime_hhMMss();

            process_SaveBatteryData();
            processChargingControl();

            /*label_send_nfcboard_output_voltage.Text = "" + mSerialPort_NFCBoard.getManager_Send().Packet_Receive.Bd_1_Voltage_Scale_100 / 100.0f;
            label_send_nfcboard_output_current.Text = "" + mSerialPort_NFCBoard.getManager_Send().Packet_Receive.Bd_1_Current_Scale_100 / 100.0f;*/


            bmu_label_pack_in.Text = ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_38_Pack_In_Scale_100 / 100.0f;
            bmu_label_pack_out.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_40_Pack_Out_Scale_100/ 100.0f;
            bmu_label_current.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_42_Current_In_Scale_100 / 100.0f;
            bmu_label_soc.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1;
            bmu_label_soh.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_47_SOH_Scale_1;
            bmu_label_th_min.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_48_Thmin_Scale_10 / 10.0f;
            bmu_label_th_max.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10 / 10.0f;
            bmu_label_cv_min.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_52_Cvmin_Scale_1000 / 1000.0f;
            bmu_label_cv_max.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_54_Cvmax_Scale_1000 / 1000.0f;

            bmu_label_cc.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_56_CC_Scale_100 / 100.0f;
            bmu_label_cv.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_58_CV_Scale_100 / 100.0f;

            bmu_label_max_current.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_60_Max_Curr_Scale_100 / 100.0f;
            bmu_label_min_voltage.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_62_Min_Volt_Scale_100 / 100.0f;
            bmu_label_fw_version.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_64_FW_Scale_1;
            bmu_label_hw_version.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_66_HW_Scale_1;
            bmu_label_remain_capacity.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_68_Remiain_Capacity;
            bmu_label_cut_off_current.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_70_Cut_Off_Current_Scale_100 / 100.0f;
            bmu_label_remain_chargetime.Text = "" + mSCR_PacketManager.Packet_Dep_BMU_Res.Data_72_Remain_Charge_T_Scale_100/100.0f;

            bmu_label_discharge_fet_on.Text = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_30_fsts_0bit_Discharger_FET_ON ? "On" : "Off";
            bmu_label_charge_fet_on.Text = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_30_fsts_1bit_Charge_FET_ON ? "On" : "Off";
            bmu_label_enable_charge.Text = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_30_fsts_2bit_Enable_Charge ? "On" : "Off";
            bmu_label_enable_discharge.Text = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_30_fsts_3bit_Enable_Discharge ? "On" : "Off";


            cmu_label_temperature_01.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_10_temp_00_Scale_10 / 10.0f;
            cmu_label_temperature_02.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_12_temp_01_Scale_10 / 10.0f;
            cmu_label_temperature_03.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_14_temp_02_Scale_10 / 10.0f;
            cmu_label_temperature_04.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_16_temp_03_Scale_10 / 10.0f;
            cmu_label_temperature_05.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_18_temp_04_Scale_10 / 10.0f;

            cmu_label_temperature_average.Text = "" + (mSCR_PacketManager.Packet_Dep_CMU_Res.Data_10_temp_00_Scale_10
                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_12_temp_01_Scale_10
                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_14_temp_02_Scale_10
                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_16_temp_03_Scale_10
                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_18_temp_04_Scale_10) / 5 / 10.0f;

            cmu_label_fw_ver.Text = ""+mSCR_PacketManager.Packet_Dep_CMU_Res.Data_78_FW_Version;

            cmu_label_cell_voltage_01.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_38_cellv_00_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_02.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_40_cellv_01_Scale_1000/ 1000.0f;
            cmu_label_cell_voltage_03.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_42_cellv_02_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_04.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_44_cellv_03_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_05.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_46_cellv_04_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_06.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_48_cellv_05_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_07.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_50_cellv_06_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_08.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_52_cellv_07_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_09.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_54_cellv_08_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_10.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_56_cellv_09_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_11.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_58_cellv_10_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_12.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_60_cellv_11_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_13.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_62_cellv_12_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_14.Text = "" + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_64_cellv_13_Scale_1000 / 1000.0f;
            cmu_label_cell_voltage_average.Text = "" + (mSCR_PacketManager.Packet_Dep_CMU_Res.Data_38_cellv_00_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_40_cellv_01_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_42_cellv_02_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_44_cellv_03_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_46_cellv_04_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_48_cellv_05_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_50_cellv_06_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_52_cellv_07_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_54_cellv_08_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_56_cellv_09_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_58_cellv_10_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_60_cellv_11_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_62_cellv_12_Scale_1000
                                                + mSCR_PacketManager.Packet_Dep_CMU_Res.Data_64_cellv_13_Scale_1000) / 14 / 1000.0f;

        }























        PeerToPeer _peerToPeer;
        Thread _receiveThread;

        string _firmwareVersion = "";

        private void initalize()
        {
            bIsAttempt_Connect_NFCReader = true;

            string[] readers = PcscHelper.getAllReaders();
            if(readers.Length < 1)
            {
                addErrMsgToLog("연결안됨");
                return;
            }

            addMsgToLog("Initialize Success!\r\n");
            byte[] firmware;
            try
            {
                _peerToPeer.readerName = readers[0];
                _peerToPeer.connectDirect();
                addMsgToLog("Successfully connected to : " + readers[0]);


                _peerToPeer.setInitiatorModeTimeout(Helper.getBytes(string.Format("{0:X4}", (Int32)100))); // Default: 1000ms


                firmware = _peerToPeer.getFirmwareVersion();
                _firmwareVersion = ASCIIEncoding.ASCII.GetString(firmware, 5, firmware.Length - 5);

                addTitleToLog(false, "Get Firmware Version => " + _firmwareVersion );



                //P2P통신 셋팅
                _peerToPeer.operationMode = OPERATION_MODE.ACTIVE;
                _peerToPeer.connectionSpeed = CONNECTION_SPEED.KBPS_424;
                
                _receiveThread = new Thread(new ThreadStart(initiatorMode));
                _receiveThread.IsBackground = true;
                _receiveThread.Start();
                //_isSendMsg = false;
            }
            catch (PcscException ex)
            {
                showPcscException(ex.Message);
            }
            catch (Exception ex)
            {
                showSystemException(ex.Message);
            }
        }

        protected int mMode = 0;


        #region Initiator Mode
        public void initiatorMode()
        {
            

            //OperationModeAndConnectingSpeed(); edit:05-08-2014
            byte[] SendBuf = new byte[500];
            byte[] bAPDUResponse;

            int SendCnt = 0x00;

            byte[] RecData = new byte[500];

            string sReceive = "";
            try
            {
                //Enter Initiator Mode
                addMsgToLog("Initiator Mode");
                _peerToPeer.nfcMode = NFC_MODE.PEER_TO_PEER_INITIATOR;
                _peerToPeer.enterInitiatorMode(_peerToPeer.nfcMode, OPERATION_MODE.ACTIVE, CONNECTION_SPEED.KBPS_424);
            //peerToPeer_.operationMode = OPERATION_MODE.PASSIVE; edit: 11/17/2014
            //peerToPeer_.connectionSpeed = CONNECTION_SPEED.KBPS_212; edit: 11/17/2014

            //for ATR Request
            ATRreq:

                addMsgToLog("ATR Request");
                //E0 00 00 42 1F 11 01 01 01 FE 03 04 05 06 07 08 09 0A 00 00 00 32 0D 46 66 6D 01 01 11 03 02 00 13 04 01 96 
                bAPDUResponse = _peerToPeer.atrRequest(mSCR_PacketManager);
                sReceive = Helper.byteAsString(bAPDUResponse, false);
                
                if (sReceive == "E1000000026300")
                {
                    goto ATRreq;
                }
                mSCR_PacketManager.Packet_ATR_Res.receive_ApplyData(bAPDUResponse);
            DEP1_BMU_REQ:

                addMsgToLog("DEP1 (BMU) Request");
                bAPDUResponse = new byte[5];
                bAPDUResponse = _peerToPeer.depExchange_1_BMU(mSCR_PacketManager);
                
                sReceive = "";
                sReceive = Helper.byteAsString(bAPDUResponse, false);
                if (sReceive == "E1000000026300")
                {
                    goto ATRreq;
                }
                
                mSCR_PacketManager.Packet_Dep_BMU_Res.receive_ApplyData(bAPDUResponse);
                Thread.Sleep(700);

            DEP2_CMU_REQ:
                addMsgToLog("DEP2 (CMU) Request");
                bAPDUResponse = new byte[5];
                bAPDUResponse = _peerToPeer.depExchange_2_CMU(mSCR_PacketManager);
                
                sReceive = "";
                sReceive = Helper.byteAsString(bAPDUResponse, false);
                if (sReceive == "E1000000026300")
                {
                    goto ATRreq;
                }
                mSCR_PacketManager.Packet_Dep_CMU_Res.receive_ApplyData(bAPDUResponse);
                bIsConnected_NFCReader = true;
                Thread.Sleep(500);
                goto DEP1_BMU_REQ;

            //trigger to stop the thread
            //EndAppLoop:
            //    setConnectText("Start");
            }
            catch (PcscException ex)
            {
                showPcscException(ex.Message);
            }
            catch (Exception ex)
            {
                showSystemException(ex.Message);
            }
        }
        #endregion


        void resetApplication()
        {
            cb_data_save.Checked = false;
            bIsConnected_NFCReader = false;
            bIsAttempt_Connect_NFCReader = false;
            if (_receiveThread != null)
            {
                if (_receiveThread.IsAlive)
                    _receiveThread.Abort();
            }

            if (_peerToPeer != null)
            {
                try
                {
                    _peerToPeer.disconnect();
                }
                catch (Exception ex)
                {
                    showSystemException(ex.Message);
                }
            }

            _peerToPeer = new PeerToPeer();
            _peerToPeer.OnReceivedCommand += new TransmitApduDelegate(PeerToPeerOnReceivedCommand);
            _peerToPeer.OnSendCommand += new TransmitApduDelegate(PeerToPeerOnSendCommand);

            addMsgToLog("########################Reset And Ready\r\n");
        }


        #region MESSAGES
        void PeerToPeerOnSendCommand(object sender, TransmitApduEventArg e)
        {
            addMsgToLog("<< ", e.data, "", e.data.Length, false);
        }

        void PeerToPeerOnReceivedCommand(object sender, TransmitApduEventArg e)
        {
            addMsgToLog(">> ", e.data, "", e.data.Length, false);
        }

        void showPcscException(string msg)
        {
            addErrMsgToLog(msg + "\r\n");
            MessageBox.Show(msg, "PCSC Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void showSystemException(string msg)
        {
            addErrMsgToLog(msg + "\r\n");
            MessageBox.Show(msg, "System Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void showWarningMessage(string msg)
        {
            addErrMsgToLog(msg + "\r\n");
            MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        void addMsgToLog(string msg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(delegate () { addMsgToLog(msg); }));
                return;
            }
            else
            {
                RichTextBoxLogs.Select(RichTextBoxLogs.Text.Length, 0);
                RichTextBoxLogs.SelectionColor = Color.Black;
                RichTextBoxLogs.SelectionFont = new Font(RichTextBoxLogs.Font.Name, RichTextBoxLogs.Font.Size, FontStyle.Regular);
                RichTextBoxLogs.SelectedText = msg + "\r\n";
                RichTextBoxLogs.ScrollToCaret();
            }
        }

        void addErrMsgToLog(string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate () { addErrMsgToLog(msg); }));
            }
            else
            {
                RichTextBoxLogs.Select(RichTextBoxLogs.Text.Length, 0);
                RichTextBoxLogs.SelectionColor = Color.Red;
                RichTextBoxLogs.SelectionFont = new Font(RichTextBoxLogs.Font.Name, RichTextBoxLogs.Font.Size, FontStyle.Regular);
                RichTextBoxLogs.SelectedText = msg + "\r\n";
                RichTextBoxLogs.ScrollToCaret();
            }
        }

        void addTitleToLog(bool isBold, string msg)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate () { addTitleToLog(isBold, msg); }));
            }
            else
            {
                if (isBold)
                    RichTextBoxLogs.SelectionFont = new Font(RichTextBoxLogs.Font.Name, RichTextBoxLogs.Font.Size, FontStyle.Bold);
                else
                    RichTextBoxLogs.SelectionFont = new Font(RichTextBoxLogs.Font.Name, RichTextBoxLogs.Font.Size, FontStyle.Regular);

                RichTextBoxLogs.Select(RichTextBoxLogs.Text.Length, 0);
                RichTextBoxLogs.SelectionColor = Color.Black;
                RichTextBoxLogs.SelectedText = "\r\n" + msg + "\r\n";
                RichTextBoxLogs.ScrollToCaret();
            }
        }

        void addMsgToLog(string prefixStr, byte[] buff, string postfixStr, int buffLen, bool isIndented)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(delegate () { addMsgToLog(prefixStr, buff, postfixStr, buffLen, isIndented); }));
            }
            else
            {

                if (buff.Length < buffLen)
                    return;

                RichTextBoxLogs.Select(RichTextBoxLogs.Text.Length, 0);
                if (isIndented)
                    RichTextBoxLogs.SelectionIndent = 10;
                else
                    RichTextBoxLogs.SelectionIndent = 0;

                string tmpStr = string.Empty;

                // Convert each byte from buff to its string representation.
                for (int i = 0; i < buffLen; i++)
                    tmpStr += string.Format("{0:X2}", buff[i]) + " ";

                // Add item to log (listBox)                   
                RichTextBoxLogs.SelectionColor = Color.Green;
                RichTextBoxLogs.SelectionFont = new Font(RichTextBoxLogs.Font.Name, RichTextBoxLogs.Font.Size, FontStyle.Regular);
                RichTextBoxLogs.SelectedText = prefixStr + tmpStr + postfixStr + "\r\n";

                // Select the last item from the listbox.
                RichTextBoxLogs.ScrollToCaret();
            }
        }
        #endregion

        #region SetThread
        // Function for Display Recieved Message
        //private void setShowDataText(string text)
        //{
        //    if (this.RichTextBoxReceivedMessage.InvokeRequired)
        //    {
        //        this.Invoke((MethodInvoker)delegate
        //        {
        //            RichTextBoxReceivedMessage.Text = text; // runs on UI thread
        //        });

        //        //SetTextCallback d = new SetTextCallback(Set_tshowdata_Text);
        //        //this.Invoke(d, new object[] { Text });
        //    }
        //    else
        //    {
        //        RichTextBoxReceivedMessage.Select(RichTextBoxReceivedMessage.Text.Length, 0);
        //        RichTextBoxReceivedMessage.SelectionColor = Color.Black;
        //        RichTextBoxReceivedMessage.SelectionFont = new Font(RichTextBoxReceivedMessage.Font.Name, RichTextBoxReceivedMessage.Font.Size, FontStyle.Regular);
        //        RichTextBoxReceivedMessage.SelectedText = text + "\r\n";
        //        RichTextBoxReceivedMessage.ScrollToCaret();
        //    }
        //}
        #endregion

        protected bool bIsConnected_NFCReader = true;
        protected bool bIsAttempt_Connect_NFCReader = true;


        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 1)
            {
                
            }
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            initalize();
        }

        private void button_disconnect_Click(object sender, EventArgs e)
        {
            resetApplication();
        }

        private void Form_SmartCardReader_PCSC_Load(object sender, EventArgs e)
        {

        }

        private void Form_SmartCardReader_PCSC_FormClosing(object sender, FormClosingEventArgs e)
        {
            bIsChargingStart = false;
            bIsDataSave = false;
            

            resetApplication();

            /*if (mSerialPort_NFCBoard != null)
                mSerialPort_NFCBoard.commClose();*/

            //if (mSerialPort_IOBoard != null)
            //    mSerialPort_IOBoard.commClose();

            mTimer.Stop();
            mTimer.Enabled = false;
            mTimer.Dispose();
        }

        protected bool bIsChargingControl = false;

        protected bool bIsChargingStart = false;

        //protected bool bIsChargingStart_SOC = false;
        protected bool bIsChargingStop_SOC = false;
        protected JSH_Time mTime_ChargingStart = new JSH_Time();
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            bIsChargingStart = ((CheckBox)sender).Checked;

            if (((CheckBox)sender).Checked)
            {
                initChargingVariable();

                mSCR_PacketManager.Packet_Dep_BMU_Req.Data_10_SCT_Reg1_1bit_Ready = true;
                mSCR_PacketManager.Packet_Dep_BMU_Req.Data_10_SCT_Reg1_0bit_Request_FET_ON = true;


                mBatterycontrol_soc_chargingstart = int.Parse(tb_batterycontrol_soc_chargingstart.Text);
                mBatterycontrol_soc_chargingstop = int.Parse(tb_batterycontrol_soc_chargingstop.Text);
                mBatterycontrol_temp_stop_output = int.Parse(tb_batterycontrol_temp_stop_output.Text);

                mTime_ChargingStart.setTime();
            }
            else
            {
                /*mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Voltage_Scale_100 = 0;
                mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Current_Scale_100 = 0;*/
                mSCR_PacketManager.Packet_Dep_BMU_Req.Data_10_SCT_Reg1_1bit_Ready = false;
                mSCR_PacketManager.Packet_Dep_BMU_Req.Data_10_SCT_Reg1_0bit_Request_FET_ON = false;

                mTime_Sampling_Temperature = null;
                bIsChargingStop_SOC = false;
            }
        }

        private void button_log_clear_Click(object sender, EventArgs e)
        {
            RichTextBoxLogs.Clear();
        }

        private void cb_led_red_on_CheckedChanged(object sender, EventArgs e)
        {
            /*mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Red_On = ((CheckBox)sender).Checked;*/
        }

        private void cb_led_blue_on_CheckedChanged(object sender, EventArgs e)
        {
            /*mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Blue_On = ((CheckBox)sender).Checked;*/
        }

        private void cb_led_green_on_CheckedChanged(object sender, EventArgs e)
        {
            /*mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Green_On = ((CheckBox)sender).Checked;*/
        }

        private void cb_door_open_CheckedChanged(object sender, EventArgs e)
        {
            //mSerialPort_IOBoard.getManager_Send().Packet_Send.Bd_1_Door_Open = ((CheckBox)sender).Checked;
            //mSerialPort_IOBoard.getManager_Send().Packet_Send.Bd_1_Door_Close = false;
            cb_door_close.Checked = false;
        }

        private void cb_door_close_CheckedChanged(object sender, EventArgs e)
        {
            //mSerialPort_IOBoard.getManager_Send().Packet_Send.Bd_1_Door_Close = ((CheckBox)sender).Checked;
            //mSerialPort_IOBoard.getManager_Send().Packet_Send.Bd_1_Door_Open = false;
            cb_door_open.Checked = false;
        }

        protected string mSelect_DivideCode = "";
        protected string mSelect_DivideCode_Chart = "";
        protected bool bIsDataSave = false;
        private void cb_data_save_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                bIsDataSave = false;
                saveBatteryData();
                return;
            }

            if (textBox_dividecode.Text.Length < 6)
            {
                MessageBox.Show("6글자 이상 입력해주세요.");
                ((CheckBox)sender).Checked = false;
                return;
            }

            if (DataManager_ChargingInfor_List.getInstance().queryExistColumn(CINDEX_CHARGINGINFOR_LIST.dividecode, textBox_dividecode.Text))
            {
                MessageBox.Show("구분코드가 이미 존재합니다. 변경해 주세요.");
                ((CheckBox)sender).Checked = false;
                return;
            }

            mSelect_DivideCode = textBox_dividecode.Text;

            bIsDataSave = true;
            mTime_DataSave_Start.setTime();

            saveBatteryData();


        }

        private void button_search_dividecode_Click(object sender, EventArgs e)
        {
            if(comboBox_code.SelectedItem == null)
            {
                MessageBox.Show("구분코드를 선택해 주세요.");
                return;
            }

            if (((string)comboBox_code.SelectedItem).Length < 6)
            {
                MessageBox.Show("구분코드를 선택해 주세요.");
                return;
            }
            mSelect_DivideCode = (string)comboBox_code.SelectedItem;
            DataTable table = DataManager_ChargingInfor_List.getInstance().queryColumn(
                null, 
                0, 
                0,
                0,
                " where " +DataManager_ChargingInfor_List.getInstance().getColumnName(CINDEX_CHARGINGINFOR_LIST.dividecode) + " = '" + mSelect_DivideCode + "'",
                "DESC");
            dataGridView_charginglist.DataSource = table;
        }

        private void comboBox_code_Click(object sender, EventArgs e)
        {
            comboBox_code.Items.Clear();
            List<string> list = DataManager_ChargingInfor_List.getInstance().queryColumn_Uniq(CINDEX_CHARGINGINFOR_LIST.dividecode);

            for(int i = 0; i < list.Count; i++)
            {
                comboBox_code.Items.Add(list[i]);
            }
            

        }

        private void button_charginglist_chart_Click(object sender, EventArgs e)
        {
            if (comboBox_charginglist_chart.SelectedItem == null)
            {
                MessageBox.Show("구분코드를 선택해 주세요.");
                return;
            }

            if (((string)comboBox_charginglist_chart.SelectedItem).Length < 6)
            {
                MessageBox.Show("구분코드를 선택해 주세요.");
                return;
            }
            mSelect_DivideCode_Chart = (string)comboBox_charginglist_chart.SelectedItem;
            DataTable table = DataManager_ChargingInfor_List.getInstance().queryColumn(
                new int[] 
                {
                    CINDEX_CHARGINGINFOR_LIST.bmu_thmax,
                    CINDEX_CHARGINGINFOR_LIST.bmu_thmin,
                    CINDEX_CHARGINGINFOR_LIST.bmu_pack_out,
                    CINDEX_CHARGINGINFOR_LIST.bmu_current,
                    CINDEX_CHARGINGINFOR_LIST.bmu_soc,
                    CINDEX_CHARGINGINFOR_LIST.wasted_second
                },
                0,
                0,
                CINDEX_CHARGINGINFOR_LIST.wasted_second,
                " where " + DataManager_ChargingInfor_List.getInstance().getColumnName(CINDEX_CHARGINGINFOR_LIST.dividecode) + " = '" + mSelect_DivideCode_Chart + "'",
                "ASC");
            
            chart1.Titles.Clear();
            chart1.Titles.Add(mSelect_DivideCode_Chart + " 온도변화");
            
            chart1.Series[0].LegendText = "최고온도";
            chart1.Series[1].LegendText = "최저온도";
            chart1.Series[2].LegendText = "출력전압";
            chart1.Series[3].LegendText = "전류";
            chart1.Series[4].LegendText = "SOC";

            for (int i = 0; i < chart1.Series.Count; i++)
            {
                chart1.Series[i].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                chart1.Series[i].MarkerSize = 5;
                chart1.Series[i].Points.Clear();
            }

            int count = chart1.Series.Count;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                for(int j = 0; j < chart1.Series.Count; j++)
                {
                    if(j == 4)
                        chart1.Series[j].Points.AddXY((Int64)table.Rows[i][count] / 60, ((Int64)table.Rows[i][j]));
                    else
                        chart1.Series[j].Points.AddXY((Int64)table.Rows[i][count] / 60, ((Double)table.Rows[i][j]));
                }
                
            }
            
        }

        private void comboBox_charginglist_chart_Click(object sender, EventArgs e)
        {
            comboBox_charginglist_chart.Items.Clear();
            List<string> list = DataManager_ChargingInfor_List.getInstance().queryColumn_Uniq(CINDEX_CHARGINGINFOR_LIST.dividecode);

            for (int i = 0; i < list.Count; i++)
            {
                comboBox_charginglist_chart.Items.Add(list[i]);
            }
        }
        protected bool bIsUse_Temperature_Control = false;
        private void cb_use_temperature_control_CheckedChanged(object sender, EventArgs e)
        {
            bIsUse_Temperature_Control = ((CheckBox)sender).Checked;
            initChargingVariable_Control();
            if (bIsUse_Temperature_Control)
            {
                mBatterycontrol_temp_start_control = int.Parse(tb_batterycontrol_temp_start_control.Text);
                mBatterycontrol_controlstep_current_percent = int.Parse(tb_batterycontrol_controlstep_current_percent.Text);
                mBatterycontrol_controlstep_temp = int.Parse(tb_batterycontrol_controlstep_temp.Text);
            }
        }


        protected int mMode_Charging = 0;
        protected JSH_Time mTime_Mode_Charging = null;
        protected JSH_Time mTime_Sampling_Temperature = null;
        protected int mSamplingTemp_Max = 0;
        protected int mSamplingTemp_Min = 800;

        protected bool bIsTempHigher_Sampling = false;

        protected int mTempHigher = 0;

        private void initChargingVariable()
        {
            mTime_Mode_Charging = null;


            mSamplingTemp_Max = 0;
            mSamplingTemp_Min = 800;
            mMode_Charging = 0;
            
            mTime_Sampling_Temperature = null;
            bIsTempHigher_Sampling = false;

            mTempHigher = 0;
        }

        private void initChargingVariable_Control()
        {
            mSamplingTemp_Max = 0;
            mSamplingTemp_Min = 800;

            mTime_Sampling_Temperature = null;
            bIsTempHigher_Sampling = false;

            mTempHigher = 0;
        }


        private void processChargingControl()
        {
            if (bIsChargingStart)
            {
                

                if (mTarget_SOC_ChargingComplete < mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1
                    || mSCR_PacketManager.Packet_Dep_BMU_Res.Data_68_Remiain_Capacity < 10
                    )
                {
                    cb_chargingstart.Checked = false;
                }else
                {

                    if (!cb_use_temperature_control.Checked)
                    {
                        if (mSCR_PacketManager.Packet_Dep_BMU_Res.isCanCharging()
                                    //&& !mSCR_PacketManager.Packet_Dep_BMU_Res.isAlarm()
                                    && !mSCR_PacketManager.Packet_Dep_BMU_Res.isProtection())
                        {
                            command_Powerpack_Output(mOutputCommand_Voltage, mOutputCommand_Current);
                        }
                        else
                        {
                            command_Powerpack_Output(0, 0);
                        }
                    }
                    else
                    {
                        if (mTime_Sampling_Temperature == null)
                        {
                            bIsTempHigher_Sampling = false;
                            mSamplingTemp_Max = 0;
                            mTime_Sampling_Temperature = new JSH_Time();
                            mSamplingTemp_Max = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10;
                        }

                        if (mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10 < mSamplingTemp_Min)
                            mSamplingTemp_Min = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10;


                        //if (mTime_Sampling_Temperature.getSecond_WastedTime() >= 600)
                        //{
                        //    mTime_Sampling_Temperature.setTime();

                        //    if(mSamplingTemp_Max > 0 && bIsConnected_NFCReader)
                        //    {
                        //        if (!bIsTempHigher_Sampling)
                        //        {
                        //            if (mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10 >= mSamplingTemp_Max + 20
                        //            && mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10 > 43)
                        //            {
                        //                bIsTempHigher_Sampling = true;
                        //                mTempHigher = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10;
                        //            }
                        //        }
                        //        else
                        //        {
                        //            if (mTempHigher >= mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10 + 20
                        //            && mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10 <= 45)
                        //            {
                        //                bIsTempHigher_Sampling = false;
                        //            }
                        //        }
                        //    }
                        //    mSamplingTemp_Max = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10;
                        //}

                        switch (mMode_Charging)
                        {
                            case 0:
                                if (mSCR_PacketManager.Packet_Dep_BMU_Res.isCanCharging()
                                    //&& !mSCR_PacketManager.Packet_Dep_BMU_Res.isAlarm()
                                    && !mSCR_PacketManager.Packet_Dep_BMU_Res.isProtection())
                                {
                                    command_Powerpack_Output(mOutputCommand_Voltage, mOutputCommand_Current);
                                }
                                else
                                {
                                    command_Powerpack_Output(0, 0);
                                }

                                if (bIsUse_Temperature_Control)
                                {
                                    if (mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10 > mSamplingTemp_Min + 10
                                        && mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1 < 95)
                                        mMode_Charging = 2;

                                }
                                break;
                            case 1:

                                if (mSCR_PacketManager.Packet_Dep_BMU_Res.isCanCharging()
                                    //&& !mSCR_PacketManager.Packet_Dep_BMU_Res.isAlarm()
                                    && !mSCR_PacketManager.Packet_Dep_BMU_Res.isProtection())
                                {
                                    command_Powerpack_Output(mOutputCommand_Voltage, mOutputCommand_Current);
                                }
                                else
                                {
                                    command_Powerpack_Output(0, 0);
                                }

                                if (mTime_Mode_Charging == null)
                                    mTime_Mode_Charging = new JSH_Time();

                                if (mTime_Mode_Charging.getSecond_WastedTime() >= 600)
                                {
                                    mTime_Mode_Charging = null;
                                    mMode_Charging = 2;
                                }
                                //else if (mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1 >= 85)
                                //{
                                //    mTime_Mode_Charging = null;
                                //    mMode_Charging = 0;
                                //}
                                else if (!bIsUse_Temperature_Control
                                    || mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1 >= 95)
                                {
                                    mTime_Mode_Charging = null;
                                    mMode_Charging = 0;
                                }
                                break;
                            case 2:
                                command_Powerpack_Output(0, 0);

                                if (mTime_Mode_Charging == null)
                                    mTime_Mode_Charging = new JSH_Time();

                                if (mTime_Mode_Charging.getSecond_WastedTime() >= 300)
                                {
                                    mTime_Mode_Charging = null;
                                    mMode_Charging = 1;
                                }
                                //else if (mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1 >= 85)
                                //{
                                //    mTime_Mode_Charging = null;
                                //    mMode_Charging = 0;
                                //}
                                else if (!bIsUse_Temperature_Control
                                    || mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1 >= 95)
                                {
                                    mTime_Mode_Charging = null;
                                    mMode_Charging = 0;
                                }
                                break;
                        }
                    }




                }

                

                //if (mSCR_PacketManager.Packet_Dep_BMU_Res.isCanCharging()
                //    //&& !mSCR_PacketManager.Packet_Dep_BMU_Res.isAlarm()
                //    && !mSCR_PacketManager.Packet_Dep_BMU_Res.isProtection())
                //{
                //    command_Powerpack_Output(mOutputCommand_Voltage, mOutputCommand_Current);
                //}else
                //{
                //    command_Powerpack_Output(0, 0);
                //}
                //if (mBatterycontrol_soc_chargingstop <= mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1 )
                //{
                //    cb_chargingstart.Checked = false;
                //}else
                //{
                //    if(cb_use_temperature_control.Checked)
                //    {

                //    }

                //    if(bIsChargingControl)

                //    if((mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10 * 10) >= mBatterycontrol_temp_stop_output
                //        ||
                //        mSCR_PacketManager.Packet_Dep_BMU_Res.isProtection()
                //        ||
                //        mSCR_PacketManager.Packet_Dep_BMU_Res.isAlarm())
                //    {
                //        command_Powerpack_OutputStop();
                //    }else if()
                //    {

                //    }


                //}

                //if (bIsUse_Temperature_Control)
                //{

                //} else
                //{

                //}
            }else
            {
                command_Powerpack_OutputStop();
            }

            //if (mSCR_PacketManager.Packet_Dep_BMU_Res.Data_30_fsts_1bit_Charge_FET_ON &&
            //   mSCR_PacketManager.Packet_Dep_BMU_Res.Data_30_fsts_2bit_Enable_Charge
            //   && mSCR_PacketManager.Packet_Dep_BMU_Req.Data_10_SCT_Reg1_0bit_Request_FET_ON
            //   && mSCR_PacketManager.Packet_Dep_BMU_Req.Data_10_SCT_Reg1_1bit_Ready)
            //{
                
            //}
            //else
            //{
            //    mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Voltage_Scale_100 = 0;
            //    mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Current_Scale_100 = 0;
            //}
        }

        private void command_Powerpack_Output(int voltage_scale_10, int current_scale_10)
        {
            /*mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Voltage_Scale_100 = voltage_scale_10;
            mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Current_Scale_100 = current_scale_10;*/
        }


        private void command_Powerpack_OutputStop()
        {
            /*mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Voltage_Scale_100 = 0;
            mSerialPort_NFCBoard.getManager_Send().Packet_Send.Bd_1_Current_Scale_100 = 0;*/
        }


        private int mBatterycontrol_soc_chargingstart = 0;
        private int mBatterycontrol_soc_chargingstop = 0;
        private int mBatterycontrol_temp_stop_output = 0;

        private int mBatterycontrol_temp_start_control = 0;
        private int mBatterycontrol_controlstep_current_percent = 0;
        private int mBatterycontrol_controlstep_temp = 0;

        private void process_SaveBatteryData()
        {
            if (mTime_DataSave.getSecond_WastedTime() >= 60)
            {
                if (IS_TEST)
                {
                    Random ran = new Random();
                    DataManager_ChargingInfor_List.getInstance().InsertColumn(
                        new string[] {
                                mSelect_DivideCode, ""+1, ""+mTime_DataSave_Start.getSecond_WastedTime(),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100),
                                ""+ran.Next(100)
                        }
                    );
                    
                }else
                {
                    saveBatteryData();
                }

                mTime_DataSave.setTime();
            }
        }

        private void saveBatteryData()
        {
            float value = mSCR_PacketManager.Packet_Dep_BMU_Res.Data_42_Current_In_Scale_100 / 100.0f;
            if (value >= 100)
                value = 0;
            DataManager_ChargingInfor_List.getInstance().InsertColumn(
                new string[] {
                                mSelect_DivideCode, ""+1, ""+mTime_DataSave_Start.getSecond_WastedTime(),
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_38_Pack_In_Scale_100/100.0f,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_40_Pack_Out_Scale_100/100.0f,
                                ""+value,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_46_SOC_Scale_1,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_47_SOH_Scale_1,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_48_Thmin_Scale_10/10.0f,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_50_Thmax_Scale_10/10.0f,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_56_CC_Scale_100/100.0f,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_58_CV_Scale_100/100.0f,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_72_Remain_Charge_T_Scale_100/100.0f,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_70_Cut_Off_Current_Scale_100/100.0f,
                                ""+mSCR_PacketManager.Packet_Dep_BMU_Res.Data_30_fsts_2bit_Enable_Charge,
                                /*""+mSerialPort_NFCBoard.getManager_Send().Packet_Receive.Bd_1_Voltage_Scale_100/100.0f,
                                ""+mSerialPort_NFCBoard.getManager_Send().Packet_Receive.Bd_1_Current_Scale_100/100.0f*/
                }
            );
        }
        
        private void button_apply_Click(object sender, EventArgs e)
        {
            mOutputCommand_Voltage = int.Parse(tb_setting_output_voltage.Text);
            mOutputCommand_Current = int.Parse(tb_setting_output_current.Text);

            mTarget_SOC_ChargingComplete = int.Parse(tb_batterycontrol_soc_chargingstop.Text);
            /*MyApplication.getInstance().Manager_SettingData_Main.setSettingData(EINDEX_SETTING_MAIN.BATTERYCONTROL_SOC_CHARGINGSTOP, ""+mTarget_SOC_ChargingComplete);*/


            command_Powerpack_Output(mOutputCommand_Voltage, mOutputCommand_Current);
        }

        private void button_default_Click(object sender, EventArgs e)
        {
            tb_setting_output_voltage.Text = "" + DEFAULT_OUTPUT_VOLTAGE;
            tb_setting_output_current.Text = "" + DEFAULT_OUTPUT_CURRENT;
            mOutputCommand_Voltage = DEFAULT_OUTPUT_VOLTAGE;
            mOutputCommand_Current = DEFAULT_OUTPUT_CURRENT;
            command_Powerpack_Output(mOutputCommand_Voltage, mOutputCommand_Current);
        }

        private int mTarget_SOC_ChargingComplete = 80;

        private int mOutputCommand_Voltage = DEFAULT_OUTPUT_VOLTAGE;
        private int mOutputCommand_Current = DEFAULT_OUTPUT_CURRENT;

        //private const int DEFAULT_OUTPUT_VOLTAGE = 5740;
        private const int DEFAULT_OUTPUT_VOLTAGE = 5000;
        private const int DEFAULT_OUTPUT_CURRENT = 980;

        private void button_windowclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_out_low_ep_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                command_Powerpack_Output(5740, 100);
            }
            else
            {
                command_Powerpack_Output(0, 0);
            }
        }
    }
}

using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.ChargerVariable;
using BatteryChangeCharger.BatteryChange_Charger.Controller;
using BatteryChangeCharger.Interface_Common;
using BatteryChangeCharger.Manager;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using EL_DC_Charger.ocpp.ver16.packet;
using BatteryChangeCharger.OCPP;
using System.Security.Cryptography.X509Certificates;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.comm;
using System.Threading;
using EL_DC_Charger.ocpp.ver16.datatype;
using Microsoft.Office.Interop.Excel;
using System.Net.PeerToPeer;
using BatteryChangeCharger.Sequence;
using System.IO.Ports;
using DataTable = System.Data.DataTable;
using BatteryChangeCharger.OCPP.database;
using EL_DC_Charger.ocpp.ver16.packet.csms2cp;
using Application = System.Windows.Forms.Application;

namespace BatteryChangeCharger
{
    public partial class Form1 : Form, IMainForm
    {
        public MyApplication myApplication;
        public Form1()
        {


            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.UpdateStyles();

            Size screenSize = Screen.PrimaryScreen.Bounds.Size;
            if (screenSize.Width > 768)
            {
                this.Width = 1024;
                this.Height = 768;
                MyApplication.bIsScreen_Width_1024 = true;
            }
            else
            {
                MyApplication.bIsScreen_Width_1024 = false;
                this.Width = 768;
                this.Height = 1024;
            }
            //MyApplication.bIsScreen_Width_1024 = false;
            //this.Width = 768;
            //this.Height = 1024;


            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            //TopMost = true;

            //this.WindowState = FormWindowState.Maximized;



            //WindowState = FormWindowState.Maximized;

            int width = Width;
            int height = Height;

            timer_init.Start();

            if (MyApplication.IsShow_Size)
            {
                MessageBox.Show("ScreenSize = " + screenSize.Width + "    " + screenSize.Height);
                MessageBox.Show("FormSize = " + this.Width + "    " + this.Height);
            }

            /*Manager_TTS.Speak("바보");*/

            //byte[] data = new byte[1];
            //data[2] = 0;

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(e.X + ", " + e.Y);
        }




        public Panel getPanel_Main()
        {
            return panel_main;
        }

        public Form getForm_Main()
        {
            return this;
        }


        private void timer_init_Tick(object sender, EventArgs e)
        {

            initVariable();
            timer_init.Stop();
            timer_process.Start();
        }

        protected JSH_Time mTime_Timer_1Sec = new JSH_Time();

        private void timer_process_Tick(object sender, EventArgs e)
        {
            if (MyApplication.getInstance().Controller_Main != null)
                MyApplication.getInstance().Controller_Main.process();

            if (mTime_Timer_1Sec.getSecond_WastedTime() > 1
                &&
                MyApplication.getInstance().SystemMode != CSystemMode.NORMAL)
            {
                mTime_Timer_1Sec.setTime();
                if (MyApplication.getInstance().Form_Setting_Main != null)
                {
                    if (MyApplication.getInstance().Form_Setting_Main.mUC_Content_TestMode_Charging != null)
                    {

                        MyApplication.getInstance().Form_Setting_Main.mUC_Content_TestMode_Charging.updateView();
                    }
                }
            }

            test_timer();



            /*for (int i = 0; i < 8; i++)
            {
                if (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i].mPacket_c1_Receive.SOC >= 95)
                {
                    MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i].mPacket_c1_Send.Command_Charging(false);
                }
            }*/


        }

        public Nullable<DateTime> Test_Time = null;
        public string logmessage;

        public void test_timer()
        {

            if (Test_Time == null)
            {
                Test_Time = DateTime.Now;
            }
            else if (Test_Time != null && Test_Time.Value.AddMinutes(5) <= DateTime.Now)
            {

                /*logmessage = "\n" + "charger" + "_" + "Up_Temp = " + MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Up_Temp.ToString() + "\n";
                logmessage += "charger" + "_" + "Down_Temp = " +  MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Down_Temp.ToString() + "\n" + "\n";

                for (int i = 1; i < 9; i++)
                {
                    if (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatterArrive)
                    {
                        logmessage += "Slot_" + i.ToString() + "_" + "Battery_Set = " + "true" + "\n";
                    }
                    else
                    {
                        logmessage += "Slot_" + i.ToString() + "_" + "Battery_Set = " + "false" + "\n";
                    }

                    logmessage += "Slot_" + i.ToString() + "_"+ "PowerPackVoltage = " + (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.PowerPackVoltage / 100) + "\n";
                    logmessage += "Slot_" + i.ToString() + "_"+ "PowerPackCurrent = " + (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.PowerPackcurrent / 100) + "\n";
                    logmessage += "Slot_" + i.ToString() + "_"+ "BatteryMaxTemp = " + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatteryMaxTemper + "\n";
                    logmessage += "Slot_" + i.ToString() + "_"+ "BatteryMinTemp = " + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatteryMinTemper + "\n";
                    logmessage += "Slot_" + i.ToString() + "_"+ "SOC = " + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.SOC + "\n";
                    logmessage += "Slot_" + i.ToString() + "_"+ "SOH = " + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.SOH + "\n" + "\n";
                    
                }*/


                logmessage = "날짜," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ",," + "\n";
                logmessage += "\n";
                logmessage += ",상부온도,하부온도" + "\n";
                logmessage += "충전기," + MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Up_Temp.ToString() + "," + MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive.Charger_Down_Temp.ToString() + "\n" + "\n";
                logmessage += ",안착여부,파워팩 전압,파워팩 전류,배터리 최고 온도,배터리 최저 온도,배터리 SOC,배터리 SOH" + "\n";
                for (int i = 1; i < 9; i++)
                {
                    logmessage += "슬롯" + i + ",";
                    if (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatterArrive)
                    {
                        logmessage += "true";
                    }
                    else
                    {
                        logmessage += "false";
                    }

                    logmessage += "," + (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.PowerPackVoltage / 100).ToString();
                    logmessage += "," + (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.PowerPackcurrent / 100).ToString();
                    logmessage += "," + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatteryMaxTemper.ToString();
                    logmessage += "," + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.BatteryMinTemper.ToString();
                    logmessage += "," + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.SOC.ToString();
                    logmessage += "," + MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i - 1].mPacket_c1_Receive.SOH.ToString();
                    logmessage += "\n";

                }
                logmessage += "\n";
                CsUtil.WriteLog(logmessage, "Log_For_Test");
                Test_Time = null;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_init.Stop();
            timer_process.Stop();
        }

        public void initVariable()
        {
            MyApplication.getInstance().MainForm = this;
            MyApplication.getInstance().initVariable();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(SerialPort.GetPortNames());



            try
            {
                comboBox1.Text = CsUtil.IniReadValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "COMPORT", "CARD");
                serialPort1.PortName = CsUtil.IniReadValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "COMPORT", "CARD");
                serialPort1.Open();
            }
            catch
            {
                MessageBox.Show("컴포트 오픈 실패");
            }


            int width = Width;
            int height = Height;
            Size screenSize = Screen.PrimaryScreen.Bounds.Size;

            if (MyApplication.IsShow_Size)
            {
                MessageBox.Show("ScreenSize = " + screenSize.Width + "    " + screenSize.Height);
                MessageBox.Show("FormSize = " + this.Width + "    " + this.Height);
            }

            myApplication = MyApplication.getInstance();
            //디비
            MyApplication.getInstance().oCPP_Manager_Table_Setting = new OCPP_Manager_Table_Setting();
            //웹소켓
            MyApplication.getInstance().oCPP_Comm_Manager = new OCPP_Comm_Manager();
            MyApplication.getInstance().oCPP_Comm_SendMgr = new OCPP_Comm_SendMgr();
            MyApplication.getInstance().oCPP_AuthCache = new OCPP_AuthCache();
            MyApplication.getInstance().oCPP_TransactionInfo = new OCPP_TransactionInfo();


            if (!bck_sequnce.IsBusy)
            {
                bck_sequnce.RunWorkerAsync();
            }

            timer1.Enabled = true;
        }

        public void setSendPacket_Call_CP(JArray packet)
        {
            if (packet == null)
            {
                mPacket_SendPacket_Call_CP = null;
                return;
            }
            mPacket_SendPacket_Call_CP = new EL_OCPP_Packet_Wrapper(packet[2].ToString(), packet[1].ToString(), packet);

        }

        EL_OCPP_Packet_Wrapper mPacket_SendPacket_Call_CP;
        public static JsonSerializerSettings mJsonSerializerSettings = new JsonSerializerSettings();



        private void button1_Click(object sender, EventArgs e)
        {

        }



        private void SequenceCycle()
        {
            Seq_Boot.MainCycle();
            Seq_Preparing.MainCycle();
            Seq_Main.MainCycle();
            Seq_Authrize.MainCycle();
            Seq_Charging.MainCycle();
        }
        int meterValueSampleInterval = 10;
        int step = 0;

        const int Boot = 0;
        const int Main = 10;
        const int Authrize = 20;
        const int BattertyIn = 30;
        const int ProcessChk = 40;
        const int ChargingPrepare = 50;
        const int Charging = 60;
        const int Finish = 70;

        const int RemoteStartTransaction = 80;
        const int Reset = 90;

        DateTime dt = new DateTime();
        private async void bck_sequnce_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {

                /////////////////////
                Thread.Sleep(1);
            }
        }

        private void bck_Counting_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                for (int i = 0; i < 25; i++)
                {
                    if (CsDefine.Delayed[i] == 0)
                    {
                        CsDefine.dt_beforeDealy[i] = DateTime.Now;
                        CsDefine.Delayed[i] = 1;
                    }
                    else
                    {
                        if (CsDefine.Delayed[i] < 999999999)
                        {
                            CsDefine.Delayed[i] = (int)(DateTime.Now - CsDefine.dt_beforeDealy[i]).TotalMilliseconds;
                        }
                    }

                    if (CsDefine.Counted[i] == 0)
                    {
                        CsDefine.dt_beforeCount[i] = DateTime.Now;
                        CsDefine.Counted[i] = 1;
                    }
                    else
                    {
                        if (CsDefine.Counted[i] < 999999999)
                        {
                            CsDefine.Counted[i] = (int)(DateTime.Now - CsDefine.dt_beforeCount[i]).TotalMilliseconds;
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            step = Authrize;
        }
        DataTable dt_lastInfo;
        private async void timer1_TickAsync(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            lbl_status.Text = step.ToString();
            switch (step)
            {
                case Boot:
                    lbl_text.Text = "서버 접속 중";

                    myApplication.conf_BootNotification = new Conf_BootNotification();
                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_BootNotification();

                    dt = DateTime.Now;
                    step = Boot + 1;

                    break;
                case Boot + 1:
                    if (myApplication.conf_BootNotification.status == RegistrationStatus.Accepted)
                    {
                        dt_lastInfo = myApplication.oCPP_TransactionInfo.selectLastInfo();
                        if (dt_lastInfo.Rows.Count > 0)
                        {
                            dt = DateTime.Now;
                            step = Boot + 2;
                        }
                        else
                        {
                            dt = DateTime.Now;
                            step = Main;
                        }
                    }
                    else if (myApplication.conf_BootNotification.status == RegistrationStatus.Rejected || myApplication.conf_BootNotification.status == RegistrationStatus.Pending)
                    {
                        //재시도
                        if (dt.AddSeconds((double)myApplication.conf_BootNotification.interval) <= DateTime.Now)
                        {
                            step = 0;
                        }
                    }
                    break;


                case Boot + 2:
                    long tid = long.Parse(dt_lastInfo.Rows[0]["TransactionID"].ToString());
                     
                    myApplication.oCPP_Comm_SendMgr.conf_StartTransaction.transactionId = tid;

                    if (dt_lastInfo.Rows[0]["RESET_REASON"].ToString().Equals("HARD"))
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StopTransAction(dt_lastInfo.Rows[0]["idTag"].ToString(), Reason.HardReset);
                    else if (dt_lastInfo.Rows[0]["RESET_REASON"].ToString().Equals("SOFT"))
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StopTransAction(dt_lastInfo.Rows[0]["idTag"].ToString(), Reason.SoftReset);
                    else if (dt_lastInfo.Rows[0]["RESET_REASON"].ToString().Equals(""))
                    {
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StopTransAction(dt_lastInfo.Rows[0]["idTag"].ToString(), Reason.PowerLoss);
                    }


                    dt = DateTime.Now;
                    step = Boot + 3;
                    break;
                case Boot + 3:
                    await Task.Delay(1000);
                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Finishing);
                    await Task.Delay(1000);

                    dt = DateTime.Now;
                    step = Main;
                    break;

                case Main:

                    btn_start.Enabled = false;
                    await Task.Delay(3000);

                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_HearthBeat();
                    dt = DateTime.Now;
                    step = Main + 1;
                    break;
                case Main + 1:


                    if (CsUtil.IniReadValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "RESET", "CON_ID_0").Equals("Y"))
                    {
                        CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "RESET", "CON_ID_0", "N");
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(0, ChargePointErrorCode.NoError, ChargePointStatus.Unavailable);
                    }
                    else
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(0, ChargePointErrorCode.NoError, ChargePointStatus.Available);

                    //거래없는 리셋
                    if (CsUtil.IniReadValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "RESET", "YN").Equals("Y"))
                    {
                        CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "RESET", "YN", "N");
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Unavailable);
                    }
                    else
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Available);

                    //거래 있는 리셋 충전중 리셋
                    if (dt_lastInfo.Rows.Count > 0 && dt_lastInfo.Rows[0]["RESET_TRIGGER"].ToString().Equals("Y"))
                    {
                        myApplication.oCPP_TransactionInfo.updateResetTrigger(dt_lastInfo.Rows[0]["TransactionID"].ToString(), "N");
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Unavailable);
                    }
                    else
                    {
                        if (dt_lastInfo.Rows.Count > 0)
                            myApplication.oCPP_TransactionInfo.updateResetTrigger(dt_lastInfo.Rows[0]["TransactionID"].ToString(), "N");
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Available);
                    }

                    dt = DateTime.Now;
                    step = Main + 2;
                    break;
                case Main + 2:

                    if (tabControl1.SelectedTab != tabPage2)
                    {
                        tabControl1.SelectedTab = tabPage2;
                        btn_start.Enabled = true;
                    }

                    if (myApplication.conf_RemoteStartTransaction)
                    {
                        myApplication.conf_RemoteStartTransaction = false;

                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Preparing);

                        if (myApplication.oCPP_Manager_Table_Setting.selectData(CONST_INDEX_OCPP_Setting.AuthorizeRemoteTxRequests.ToString()).Rows[0][1].ToString().ToUpper() == "TRUE")
                        {

                        }
                        else
                        {
                            dt = DateTime.Now;
                            step = BattertyIn;
                        }
                    }


                    if (myApplication.conf_ChangeAvailability)
                    {
                        myApplication.conf_ChangeAvailability = false;

                        if (myApplication.conf_InOperative)
                        {
                            myApplication.conf_InOperative = false;
                            MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Unavailable);
                        }
                        else if (myApplication.conf_Operative)
                        {
                            CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "RESET", "YN", "N");
                            myApplication.conf_Operative = false;
                            MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Available);
                        }
                    }

                    //거래없는 리셋
                    if (myApplication.conf_HardReset)
                    {
                        CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "RESET", "YN", "Y");
                        myApplication.conf_HardReset = false;
                        lbl_text.Text = "잠시후 재부팅 됩니다.(하드리셋)";
                        await Task.Delay(5000);
                        restartSystem();
                    }
                    else if (myApplication.conf_SoftReset)
                    {

                        CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "RESET", "YN", "Y");
                        myApplication.conf_SoftReset = false;
                        lbl_text.Text = "잠시후 재부팅 됩니다.(소프트리셋)";
                        await Task.Delay(5000);

                        restartApplication();
                    }
                    break;

                case RemoteStartTransaction:

                    dt = DateTime.Now;
                    step = RemoteStartTransaction + 1;
                    break;
                case RemoteStartTransaction + 1:
                    break;


                case Authrize:
                    myApplication.bIsTagDone = false;
                    tabControl1.SelectedTab = tabPage1;
                    lbl_text.Text = "카드 태깅";
                    send_RF_Reader();

                    dt = DateTime.Now;
                    step = Authrize + 1;
                    break;
                case Authrize + 1:

                    if (myApplication.bIsTagDone)
                    {
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_Authorize(Card_Number);
                        dt = DateTime.Now;
                        step = Authrize + 2;
                    }

                    break;
                case Authrize + 2:

                    if (myApplication.bIsCertificationSuccess)
                    {
                        dt = DateTime.Now;
                        step = BattertyIn;
                    }
                    else if (myApplication.bIsCertificationFailed)
                    {
                        lbl_text.Text = "회원 카드가 아닙니다.";
                    }
                    break;

                case BattertyIn:

                    myApplication.ConnectionTimeOut = int.Parse(myApplication.oCPP_Manager_Table_Setting.selectData(CONST_INDEX_OCPP_Setting.ConnectionTimeOut.ToString()).Rows[0][1].ToString());
                    tabControl1.SelectedTab = tabPage1;
                    lbl_text.Text = "배터리를 장착해 주세요";

                    dt = DateTime.Now;
                    step = BattertyIn + 1;
                    break;
                case BattertyIn + 1:
                    if (myApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(3) || myApplication.Manual_BatterArrive)
                    {
                        dt = DateTime.Now;
                        step = ProcessChk;
                    }
                    else if (DateTime.Now >= dt.AddSeconds(myApplication.ConnectionTimeOut))
                    {
                        step = Main + 1;
                    }
                    break;

                case ProcessChk:
                    tabControl1.SelectedTab = tabPage1;
                    lbl_text.Text = "배터리 프로세스 확인 중";


                    dt = DateTime.Now;
                    step = ProcessChk + 1;
                    break;

                case ProcessChk + 1:
                    if (myApplication.SerialPort_NFCBoard.getManager_Send().mPackets[3].mPacket_c1_Receive.ChargingStatus == 100)
                    {
                        dt = DateTime.Now;
                        step = ChargingPrepare;
                    }
                    break;


                case ChargingPrepare:
                    myApplication.bIsConfStartTransAction = false;
                    myApplication.bIsConfStopTransAction = false;

                    tabControl1.SelectedTab = tabPage1;
                    lbl_text.Text = "STARTTRANSACTION 전송 ";

                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StartTransAction();

                    dt = DateTime.Now;
                    step = ChargingPrepare + 1;
                    break;

                case ChargingPrepare + 1:
                    if (myApplication.bIsConfStartTransAction)
                    {
                        dt = DateTime.Now;
                        step = Charging;
                    }
                    break;



                case Charging:

                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Charging);
                    tabControl1.SelectedTab = tabPage3;
                    DataTable dt_metervalue = myApplication.oCPP_Manager_Table_Setting.selectData("MeterValueSampleInterval");
                    meterValueSampleInterval = int.Parse(dt_metervalue.Rows[0][1].ToString());

                    dt = DateTime.Now;
                    step = Charging + 1;
                    break;
                case Charging + 1:

                    if (DateTime.Now >= dt.AddSeconds(meterValueSampleInterval))
                    {
                        dt = DateTime.Now;
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_MeterValue();
                    }
                    else if (myApplication.bIsConfStopTransAction)
                    {
                        dt = DateTime.Now;
                        step = Finish;
                    }
                    else if (myApplication.conf_RemoteStopTransaction)
                    {
                        //딜레이 준 이유 -> OCTT 툴에서 인식을 못함
                        await Task.Delay(1000);
                        myApplication.conf_RemoteStopTransaction = false;
                        myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StopTransAction(myApplication.Card_Number, Reason.Remote);

                        await Task.Delay(3000);

                        dt = DateTime.Now;
                        step = Finish;
                    }

                    if (myApplication.conf_HardReset)
                    {
                        tabControl1.SelectedTab = tabPage1;
                        myApplication.conf_HardReset = false;
                        myApplication.oCPP_TransactionInfo.updateResetTrigger(myApplication.oCPP_Comm_SendMgr.conf_StartTransaction.transactionId.ToString(), "Y");
                        myApplication.oCPP_TransactionInfo.updateResetReason(myApplication.oCPP_Comm_SendMgr.conf_StartTransaction.transactionId.ToString(), "HARD");

                        lbl_text.Text = "잠시후 재부팅 됩니다.(하드리셋)";
                        await Task.Delay(5000);
                        restartSystem();
                    }
                    else if (myApplication.conf_SoftReset)
                    {
                        tabControl1.SelectedTab = tabPage1;
                        myApplication.conf_SoftReset = false;
                        myApplication.oCPP_TransactionInfo.updateResetTrigger(myApplication.oCPP_Comm_SendMgr.conf_StartTransaction.transactionId.ToString(), "Y");
                        myApplication.oCPP_TransactionInfo.updateResetReason(myApplication.oCPP_Comm_SendMgr.conf_StartTransaction.transactionId.ToString(), "SOFT");

                        lbl_text.Text = "잠시후 재부팅 됩니다.(소프트리셋)";
                        await Task.Delay(5000);

                        restartApplication();
                    }

                    break;
                case Finish:
                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Finishing);

                    lbl_text.Text = "충전이 종료되었습니다.";
                    tabControl1.SelectedTab = tabPage1;
                    dt = DateTime.Now;
                    step = Finish + 1;
                    break;
                case Finish + 1:
                    TimeSpan timeElapsed = DateTime.Now - dt;
                    double secondsElapsed = timeElapsed.TotalSeconds;
                    int roundedSeconds = (int)Math.Round(secondsElapsed);
                    lbl_text.Text = "충전이 종료되었습니다 " + roundedSeconds + "/5";

                    if (secondsElapsed >= 5)
                    {
                        step = Main + 1;
                    }
                    break;
            }


            /////////////////하트비트
            if (myApplication.HeartBeatInterval > 0 && myApplication.HeartBeatLastSendTime.AddSeconds(myApplication.HeartBeatInterval) <= DateTime.Now)
            {
                myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_HearthBeat();
            }

            timer1.Enabled = true;
        }



        byte[] data = new byte[37];  // 보내는 총 데이터 패킷 길이


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
        string Card_Number = "";
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

                                byte[] bytes = new byte[1];
                                bytes[0] = 0x06;
                                serialPort1.Write(bytes, 0, bytes.Length);
                                myApplication.bIsTagDone = true;
                                //MyApplication.getInstance().manager_time.
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\config.ini", "COMPORT", "CARD", comboBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            step = Main + 1;
        }

        private void btn_chargingStop_Click(object sender, EventArgs e)
        {
            myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StopTransAction();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            myApplication.Manual_BatterArrive = !myApplication.Manual_BatterArrive;

            if (myApplication.Manual_BatterArrive)
            {
                button3.BackColor = Color.Lime;
                MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Preparing);
            }
            else
            {
                MyApplication.getInstance().oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Available);
                button3.BackColor = Color.Transparent;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myApplication.Manual_ProcessStep = !myApplication.Manual_ProcessStep;

            if (myApplication.Manual_ProcessStep)
            {
                myApplication.SerialPort_NFCBoard.getManager_Send().mPackets[3].mPacket_c1_Receive.ChargingStatus = 100;
                button4.BackColor = Color.Lime;
            }
            else
            {
                myApplication.SerialPort_NFCBoard.getManager_Send().mPackets[3].mPacket_c1_Receive.ChargingStatus = 0;
                button4.BackColor = Color.Transparent;
            }
        }

        public static void restartApplication()
        {
            try
            {
                Process.Start(Application.StartupPath + "\\BatteryChangeCharger.exe");
                Process.GetCurrentProcess().Kill();
            }
            catch { }
        }
        public static void restartSystem() => Process.Start("ShutDown", "-r -t 10");

        private void button5_Click(object sender, EventArgs e)
        {
            myApplication.oCPP_Comm_Manager.CloseAsync();
        }
    }
}

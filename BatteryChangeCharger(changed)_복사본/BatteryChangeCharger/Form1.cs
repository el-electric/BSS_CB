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

            if (!bck_sequnce.IsBusy)
            {
                bck_sequnce.RunWorkerAsync();
            }


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

        int step = 0;

        const int Boot = 0;
        const int Main = 10;

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

        }

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
                        dt = DateTime.Now;
                        step = Main;
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

                case Main:
                    tabControl1.SelectedTab = tabPage2;
                    await Task.Delay(3000);
                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(0, ChargePointErrorCode.NoError, ChargePointStatus.Available);
                    await Task.Delay(1000);
                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_StatusNotification(1, ChargePointErrorCode.NoError, ChargePointStatus.Available);
                    await Task.Delay(1000);
                    myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_HearthBeat();
                    dt = DateTime.Now;
                    step = Main + 1;
                    break;
                case Main + 1:
                    break;
                case Main + 2:
                    break;

            }


            /////////////////하트비트
            if (myApplication.HeartBeatInterval > 0 && myApplication.HeartBeatLastSendTime.AddSeconds(myApplication.HeartBeatInterval) <= DateTime.Now)
            {
                myApplication.oCPP_Comm_SendMgr.sendOCPP_CP_Req_HearthBeat();
            }

            timer1.Enabled = true;
        }
    }
}

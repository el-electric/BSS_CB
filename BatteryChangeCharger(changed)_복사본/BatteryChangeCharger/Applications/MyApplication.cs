using BatteryChangeCharger.BatteryChange_Charger.ChargerVariable;
using BatteryChangeCharger.BatteryChange_Charger.Controller;
using BatteryChangeCharger.BatteryChange_Charger.ControlManager;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using BatteryChangeCharger.BatteryChange_Charger.DataManager;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board;
using BatteryChangeCharger.BatteryChange_Charger.Settings;
using BatteryChangeCharger.Controller;
using BatteryChangeCharger.Interface_Common;
using BatteryChangeCharger.Manager;
using BatteryChangeCharger.OCPP;
using BatteryChangeCharger.OCPP.database;
using EL_DC_Charger.ocpp.ver16.comm;
using EL_DC_Charger.ocpp.ver16.database;
using EL_DC_Charger.ocpp.ver16.packet;
using EL_DC_Charger.ocpp.ver16.packet.cp2csms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.Applications
{
    public class MyApplication
    {
        public MyApplication()
        {
            mJsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            mJsonSerializerSettings.Formatting = Formatting.None;
            mJsonSerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeNonAscii;
        }

        public const bool bIsCert = true;

        public const bool IsShow_Size = false;

        public const int COUNT_SOCKET = 8;

        public static bool bIsScreen_Width_1024 = false;

        protected int mSystemMode = CSystemMode.NORMAL;

        protected Form_Setting_Main mForm_Setting_Main = null;
        public Form_Setting_Main Form_Setting_Main
        {
            get { return mForm_Setting_Main; }
            set { mForm_Setting_Main = value; }
        }

        public void setSystemMode(int systemMode)
        {
            switch (systemMode)
            {

            }
            mSystemMode = systemMode;
        }

        public int SystemMode
        {
            get { return mSystemMode; }
        }


        public bool IS_TEST
        {
            get { return false; }
        }

        public Manager_Time manager_time;


        protected bool OCPP_Test = false;

        public bool getOCPP_Test()
        {
            return OCPP_Test;
        }

        public void setOCPP_Test(bool setting)
        {
            OCPP_Test = setting;
        }


        protected static MyApplication myApplication = null;

        public static MyApplication getInstance()
        {
            if (myApplication == null)
                myApplication = new MyApplication();
            return myApplication;
        }

        protected IMainForm mMainForm = null;
        public IMainForm MainForm
        {
            get { return mMainForm; }
            set
            {
                mMainForm = value;
            }
        }

        protected Controller_Base mController_Main = null;
        public Controller_Base Controller_Main
        {
            get { return mController_Main; }
        }

        public BCC_DataManager_CustomUserControl DataManager_UserControl
        {
            get { return mDataManager_UserControl; }
        }

        protected BCC_DataManager_CustomUserControl mDataManager_UserControl = null;
        protected Manager_SettingData_Main mManager_SettingData_Main = null;

        public int Morning_Weather_Info;
        public int Morning_OneHour_Temp;

        public int Afternoon_Weather_Info;
        public int Afternoon_OneHour_Temp;
        public bool bOCPP_IsReceivePacket_CallResult_BootNotification;


        /// ////////////////////////////////////////////////////////////////////
        public SQLiteConnection connection;
        public OCPP_Manager_Table_Setting oCPP_Manager_Table_Setting;
        public OCPP_Comm_Manager oCPP_Comm_Manager;
        public OCPP_Comm_SendMgr oCPP_Comm_SendMgr;
        public OCPP_AuthCache oCPP_AuthCache;
        public OCPP_TransactionInfo oCPP_TransactionInfo;

        public bool conf_RemoteStartTransaction = false;
        public bool conf_RemoteStopTransaction = false;
        public bool conf_HardReset = false;
        public bool conf_SoftReset = false;
        public bool conf_ChangeAvailability = false;
        public bool conf_Operative = false;
        public bool conf_InOperative = false;

        //public List<EL_OCPP_Packet_Wrapper> list_packet = new List<EL_OCPP_Packet_Wrapper>();
        public static JsonSerializerSettings mJsonSerializerSettings = new JsonSerializerSettings();

        public Conf_BootNotification conf_BootNotification;

        //public const int Cyc_Maint = 10;

        public int HeartBeatInterval = 0;
        public DateTime HeartBeatLastSendTime = new DateTime();


        public bool Manual_BatterArrive = false;


        public bool Manual_ProcessStep = false;


        public bool bIsTagDone = false;
        public bool bIsCertificationSuccess = false;
        public bool bIsCertificationFailed = false;
        public string Card_Number = null;

        public bool bIsConfStartTransAction = false;
        public bool bIsConfStopTransAction = false;        
        public int ConnectionTimeOut = 0;



        ////////////////////////////////////////////////////////////////////////

        /*public void setOCPP_CSMS_Conf_BootNotification(Conf_BootNotification conf) 이후 수정
        {
            mOCPP_CSMS_Conf_BootNotification = conf;
            bOCPP_IsReceivePacket_CallResult_BootNotification = true;
        }*/
        public Manager_SettingData_Main Manager_SettingData_Main
        {
            get { return mManager_SettingData_Main; }
        }
        public void initVariable()
        {
            mManager_SettingData_Main = new Manager_SettingData_Main();
            mManager_SettingData_Main.insertFirstColumn();
            mManager_SettingData_Main.setTempData();

            mControlManager_Sockets = new BCC_ControlManager_Sockets(this);


            mSerialPort_NFCBoard = new NFCBoard_CommManager_SerialPort(MyApplication.getInstance());
            mSerialPort_NFCBoard.init();
            mSerialPort_NFCBoard.commOpen();
            mSerialPort_NFCBoard.start();


            mSerialPort_IOBoard = new IO_Board_CommManager_SerialPort(MyApplication.getInstance());
            mSerialPort_IOBoard.init();
            mSerialPort_IOBoard.commOpen();
            mSerialPort_IOBoard.start();


            //mController_Main = new BCC_Main_Controller_Ver02(this);  //BCC_Main_Controller_Ver02
            //mController_Main.init();

            //mDataManager_UserControl = new BCC_DataManager_CustomUserControl(this);
            //mDataManager_UserControl.init();

            //mContolManager_SelfDiagnosis = new BCC_ControlManager_SelfDiagnosis(this);
            //mContolManager_SelfDiagnosis.setView();
        }

        public void close()
        {
            mSerialPort_NFCBoard.destroy();
            mSerialPort_IOBoard.destroy();
        }

        protected BCC_ControlManager_Sockets mControlManager_Sockets = null;
        public BCC_ControlManager_Sockets ControlManager_Sockets
        {
            get { return mControlManager_Sockets; }
        }


        /// <summary>
        /// 
        /// </summary>
        protected NFCBoard_CommManager_SerialPort mSerialPort_NFCBoard = null;
        protected IO_Board_CommManager_SerialPort mSerialPort_IOBoard = null;

        public NFCBoard_CommManager_SerialPort SerialPort_NFCBoard
        {
            get { return mSerialPort_NFCBoard; }
        }

        public IO_Board_CommManager_SerialPort SerialPort_IOBoard
        {
            get { return mSerialPort_IOBoard; }
        }

        /*
         * 
         */
        public BCC_ControlManager_SelfDiagnosis ControlManager_SelfDiagnosis
        {
            get { return mContolManager_SelfDiagnosis; }
        }
        protected BCC_ControlManager_SelfDiagnosis mContolManager_SelfDiagnosis = null;

    }
}

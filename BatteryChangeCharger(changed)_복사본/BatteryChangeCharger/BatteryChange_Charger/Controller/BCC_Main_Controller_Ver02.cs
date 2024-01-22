using BatteryChangeCharger.Applications;
using BatteryChangeCharger.Controller;
using BatteryChangeCharger.StaticVariable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BatteryChangeCharger.BatteryChange_Charger.Controller
{
    public class BCC_Main_Controller_Ver02 : Controller_Base
    {
        protected int[] mSocketNumber_RetreiveBattery = null;
        protected int[] mSocketNumber_InsertBattery = null;

        protected bool bIsCanUse = false;

        protected int Insert_Battery_First = 0;
        protected int Insert_Battery_Second = 0;
        protected int Retreive_Battery_First = 0;
        protected int Retreive_Battery_Second = 0;

        int Highest_Battery_SOC_Slot = 0;
        int Second_Highest_Battery_SOC_Slot = 0;

        public BCC_Main_Controller_Ver02(MyApplication application) : base(application, 0)
        {
            setTime_NextTime(60); // mTime_NextStep_Sec = 60
        }

        protected bool bIsFirst = true;

        protected int mSocketNumber_Selected = 0;
        protected bool bSocketNumber_Selected = false;

        public override void process()
        {
            if(isTimer(TIMER_5S))
            /*CsUtil.IniWriteValue(System.Windows.Forms.Application.StartupPath + @"\web_socet_url.ini", "web_socet_url", "url", "wss://dev.wev-charger.com:12200/ws/NYJ-TEST0001");*/
            if (mApplication.getOCPP_Test() == false)
            {
                switch (mMode)
                {
                    case BCC_MODE_MAIN.MODE_BOOT_ON: // 처음 시작하면
                                                     //mApplication.DataManager_UserControl.setView_Main_NotifyBootOn();
                                                     //setMode(BCC_MODE_MAIN.MODE_BOOT_ON + 1);
                                                     //mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Send.operation_DoorOpen(false);
                        setMode(BCC_MODE_MAIN.MODE_MAIN);  // 매인으로 이동
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN: // 시작한 이후

                        mApplication.DataManager_UserControl.setView_ChargingMain_Main(true);

                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(0);

                        setMode(BCC_MODE_MAIN.MODE_MAIN + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN + 1:

                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                                case CONST_EVENT.EVENT_CLICK_BUTTON_START:
                                    if (MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.get_Can_Use_Battery_Slot() == 8)
                                    {

                                    }
                                    else
                                    { setMode(BCC_MODE_MAIN.MODE_MAIN_QRCODE); }
                                    break;
                            }
                        }

                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_QRCODE:  // main 이후
                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(1);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Qrcode("http://el-electric.co.kr/");
                        setMode(BCC_MODE_MAIN.MODE_MAIN_QRCODE + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_QRCODE + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                                case CONST_EVENT.EVENT_CLICK_BUTTON_BACK:  // 뒤로가기 버튼이 눌렸을떄
                                    setMode(BCC_MODE_MAIN.MODE_MAIN);
                                    break;

                                case CONST_EVENT.EVENT_CERTIFICATION_COMPLETE:  // 인증이 끝났을때
                                    setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST);
                                    break;
                            }
                        }
                        else if (isTimer(TIMER_5S))  // 그냥 5초가 지났을때
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST);
                        }
                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST: // 첫번째 배터리를 반납해주세요
                        Insert_Battery_First = 0;

                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(2);

                        Insert_Battery_First = MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.get_Can_Use_Battery_Slot();

                        MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Insert_Battery_First);

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery("", 1, Insert_Battery_First);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_AnimcationInsertBattery();

                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {

                            }
                        }
                        else if (mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Insert_Battery_First))
                        {
                            if (isTimer(TIMER_5S))
                            {
                                MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Close(Insert_Battery_First);
                                setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE);
                            }
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE:

                        if (!mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Insert_Battery_First))
                        {
                            MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Insert_Battery_First);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery("", 1, Insert_Battery_First);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_AnimcationInsertBattery();
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST + 1);
                        }

                        mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[Insert_Battery_First].mPacket_c1_Send.Command_Charging(true);

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_Complete(1, Insert_Battery_First);
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE + 1);
                        break;

                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_5S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND);
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND:

                        Insert_Battery_Second = MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.get_Can_Use_Battery_Slot();
                        MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Insert_Battery_Second);

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery("", 2, Insert_Battery_Second);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_AnimcationInsertBattery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Insert_Battery_Second))
                        {
                            if (isTimer(TIMER_5S))
                            {
                                MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Close(Insert_Battery_Second);
                                setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND_COMPLETE);
                            }
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND_COMPLETE:
                        if (!mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Insert_Battery_Second))
                        {
                            MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Insert_Battery_Second);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery("", 2, Insert_Battery_Second);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_AnimcationInsertBattery();
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND + 1);
                        }
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_Complete(2, Insert_Battery_Second);
                        mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[Insert_Battery_Second].mPacket_c1_Send.Command_Charging(true);

                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND_COMPLETE + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_SECOND_COMPLETE + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else
                        {

                        }

                        if (isTimer(TIMER_5S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_PROCESS_CERTIFICATION_BATTERY);
                        }
                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_PROCESS_CERTIFICATION_BATTERY:
                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(3);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Process_Certification_Battery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_PROCESS_CERTIFICATION_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_PROCESS_CERTIFICATION_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY);
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY:
                        mApplication.DataManager_UserControl.setView_ChargingMain_Complete_Certification_Battery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST);
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_FAILED_CERTIFICATION_BATTERY:
                        mApplication.DataManager_UserControl.setView_ChargingMain_Failed_Certification_Battery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_FAILED_CERTIFICATION_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_FAILED_CERTIFICATION_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_3S))
                        {
                            if (mApplication.IS_TEST)
                                setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY);
                        }
                        else
                        {

                        }
                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY:

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery_When_Failed_Certification_Battery();

                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else
                        {

                        }
                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY:

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST:

                        Retreive_Battery_First = find_Highest_Soc();
                        mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[Retreive_Battery_First].mPacket_c1_Send.Command_Charging(false);
                        MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Retreive_Battery_First);

                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(4);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery("", 1, Retreive_Battery_First);

                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {

                            }
                        }
                        else if (!mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Retreive_Battery_First))
                        {
                            if (isTimer(TIMER_5S))
                            {
                                MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Close(Retreive_Battery_First);
                                setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE);
                            }
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE:
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery_Complete(1, Retreive_Battery_First);
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE + 1:
                        if (!mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Retreive_Battery_First))
                        {
                            MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Retreive_Battery_First);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery("", 1, Retreive_Battery_First);
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST + 1);
                        }
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }

                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND);
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND:

                        Retreive_Battery_Second = find_Highest_Soc();
                        mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[Retreive_Battery_Second].mPacket_c1_Send.Command_Charging(false);
                        MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Retreive_Battery_Second);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery("", 2, Retreive_Battery_Second);

                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (!mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Retreive_Battery_Second))
                        {
                            if (isTimer(TIMER_5S))
                            {
                                MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Close(Retreive_Battery_Second);
                                setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND_COMPLETE);
                            }
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND_COMPLETE:
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery_Complete(2, Retreive_Battery_Second);
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND_COMPLETE + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND_COMPLETE + 1:
                        if (mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Retreive_Battery_Second))
                        {
                            MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Retreive_Battery_Second);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery("", 2, Retreive_Battery_Second);
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_SECOND + 1);
                        }
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }

                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_USE_COMPLETE);
                        }
                        else
                        {

                        }
                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_USE_COMPLETE:
                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(5);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Use_Complete();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_USE_COMPLETE + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_USE_COMPLETE + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN);
                        }
                        else
                        {

                        }
                        break;


                }
            }

            else if (mApplication.getOCPP_Test() == true)    // ocpp 테스트용 로직
            {
                switch (mMode)
                {
                    case BCC_MODE_MAIN.MODE_BOOT_ON: // 처음 시작하면
                                                     //mApplication.DataManager_UserControl.setView_Main_NotifyBootOn();
                                                     //setMode(BCC_MODE_MAIN.MODE_BOOT_ON + 1);
                                                     //mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Send.operation_DoorOpen(false);
                        setMode(BCC_MODE_MAIN.MODE_MAIN);  // 매인으로 이동
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN: // 시작한 이후

                        mApplication.DataManager_UserControl.setView_ChargingMain_Main(true);

                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(0);

                        setMode(BCC_MODE_MAIN.MODE_MAIN + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN + 1:

                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                                case CONST_EVENT.EVENT_CLICK_BUTTON_START:
                                    if (MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.get_Can_Use_Battery_Slot() == 8)
                                    {

                                    }
                                    else
                                    { setMode(BCC_MODE_MAIN.MODE_MAIN_QRCODE); }
                                    break;
                            }
                        }

                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_QRCODE:  // main 이후
                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(1);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Qrcode("http://el-electric.co.kr/");
                        setMode(BCC_MODE_MAIN.MODE_MAIN_QRCODE + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_QRCODE + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                                case CONST_EVENT.EVENT_CLICK_BUTTON_BACK:  // 뒤로가기 버튼이 눌렸을떄
                                    setMode(BCC_MODE_MAIN.MODE_MAIN);
                                    break;

                                case CONST_EVENT.EVENT_CERTIFICATION_COMPLETE:  // 인증이 끝났을때
                                    setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST);
                                    break;
                            }
                        }
                        else if (isTimer(TIMER_5S))  // 그냥 5초가 지났을때
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST);
                        }
                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST: // 첫번째 배터리를 반납해주세요
                        Insert_Battery_First = 0;

                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(2);

                        Insert_Battery_First = MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.get_Can_Use_Battery_Slot();

                        MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Insert_Battery_First);

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery("", 3, Insert_Battery_First);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_AnimcationInsertBattery();

                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {

                            }
                        }
                        else if (mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Insert_Battery_First))
                        {
                            if (isTimer(TIMER_5S))
                            {
                                MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Close(Insert_Battery_First);
                                setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE);
                            }
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE:

                        if (!mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Insert_Battery_First))
                        {
                            MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Insert_Battery_First);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery("", 1, Insert_Battery_First);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_AnimcationInsertBattery();
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST + 1);
                        }

                        mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[Insert_Battery_First].mPacket_c1_Send.Command_Charging(true);

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Insert_Battery_Complete(3, Insert_Battery_First);
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE + 1);
                        break;

                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_INSERT_BATTERY_FIRST_COMPLETE + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_5S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_PROCESS_CERTIFICATION_BATTERY);
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                   
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_PROCESS_CERTIFICATION_BATTERY:
                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(3);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Process_Certification_Battery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_PROCESS_CERTIFICATION_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_PROCESS_CERTIFICATION_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY);
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY:
                        mApplication.DataManager_UserControl.setView_ChargingMain_Complete_Certification_Battery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_COMPLETE_CERTIFICATION_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST);
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_FAILED_CERTIFICATION_BATTERY:
                        mApplication.DataManager_UserControl.setView_ChargingMain_Failed_Certification_Battery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_FAILED_CERTIFICATION_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_FAILED_CERTIFICATION_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_3S))
                        {
                            if (mApplication.IS_TEST)
                                setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY);
                        }
                        else
                        {

                        }
                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY:

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery_When_Failed_Certification_Battery();

                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_WHEN_FAILED_CERTIFICATION_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else
                        {

                        }
                        break;
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY:

                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }

                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST:

                        Retreive_Battery_First = find_Highest_Soc();
                        mApplication.SerialPort_NFCBoard.getManager_Send().mPackets[Retreive_Battery_First].mPacket_c1_Send.Command_Charging(false);
                        MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Retreive_Battery_First);

                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(4);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery("", 1, Retreive_Battery_First);

                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {

                            }
                        }
                        else if (!mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Retreive_Battery_First))
                        {
                            if (isTimer(TIMER_5S))
                            {
                                MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Close(Retreive_Battery_First);
                                setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE);
                            }
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE:
                        mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery_Complete(1, Retreive_Battery_First);
                        setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST_COMPLETE + 1:
                        if (!mApplication.SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_a1_Receive.Check_Slot_battery_In(Retreive_Battery_First))
                        {
                            MyApplication.getInstance().Manager_SettingData_Main.SetDoor_Open(Retreive_Battery_First);
                            mApplication.DataManager_UserControl.setView_ChargingMain_Wait_Retrieve_Battery("", 1, Retreive_Battery_First);
                            setMode(BCC_MODE_MAIN.MODE_MAIN_WAIT_RETRIEVE_BATTERY_FIRST + 1);
                        }
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }

                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN_USE_COMPLETE);
                        }
                        else
                        {

                        }
                        break;

                    /**************************************************************************************************************/
                    
                    /**************************************************************************************************************/
                    case BCC_MODE_MAIN.MODE_MAIN_USE_COMPLETE:
                        mApplication.DataManager_UserControl.mUC_MainPage.getUC_Bottombar_ProcessStep().setBottomBar_Step(5);
                        mApplication.DataManager_UserControl.setView_ChargingMain_Use_Complete();
                        setMode(BCC_MODE_MAIN.MODE_MAIN_USE_COMPLETE + 1);
                        break;
                    case BCC_MODE_MAIN.MODE_MAIN_USE_COMPLETE + 1:
                        mTemp_Event = mQueue.remove();
                        if (mTemp_Event > 0)
                        {
                            switch (mTemp_Event)
                            {
                            }
                        }
                        else if (isTimer(TIMER_3S))
                        {
                            setMode(BCC_MODE_MAIN.MODE_MAIN);
                        }
                        else
                        {

                        }
                        break;


                }
            }
        }

        public int find_Highest_Soc()
        {
            int max_soc = 0;
            for (int i = 0; i < 8; i++)
            {
                if (MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i].mPacket_c1_Receive.SOC > max_soc)
                {
                    max_soc = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[i].mPacket_c1_Receive.SOC;
                    Highest_Battery_SOC_Slot = i;
                }
            }

            return Highest_Battery_SOC_Slot;
        }
    }
}
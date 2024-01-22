using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.ControlManager;
using BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl;
using BatteryChangeCharger.ChargerVariable;
using BatteryChangeCharger.Custom_UsercControl;
using BatteryChangeCharger.Interface_Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.DataManager
{
    public class BCC_DataManager_CustomUserControl : CObject_Channel
    {
        protected IMainForm mMainForm = null;

        public BCC_DataManager_CustomUserControl(MyApplication application) : base(application, 0)
        {
            mMainForm = application.MainForm;
        }

        override public void init()
        {
            mUC_Main_Notify_1Button = new UC_MainPage_Notify_1Button();
            mUC_Main_Notify_1Button.Dock = System.Windows.Forms.DockStyle.Fill;
            mUC_Main_Notify_2Button = new UC_MainPage_Notify_2Button();
            mUC_Main_Notify_2Button.Dock = System.Windows.Forms.DockStyle.Fill;
            mUC_MainPage = new P788_UC_MainPage();
            mUC_MainPage.Dock = System.Windows.Forms.DockStyle.Fill;


            if(MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Main_1024 = new P1024_BCC_UC_ChargingMain_Include_Main();
                mUC_ChargingMain_Include_Main_1024.Dock = System.Windows.Forms.DockStyle.Fill;
                mUC_ChargingMain_Include_Qrcode_1024 = new P1024_BCC_UC_ChargingMain_Include_Qrcode();
                mUC_ChargingMain_Include_Qrcode_1024.Dock = System.Windows.Forms.DockStyle.Fill;


                mUC_ChargingMain_Include_Use_Complete_1024 = new P1024_BCC_UC_ChargingMain_Include_Use_Complete();
                mUC_ChargingMain_Include_Use_Complete_1024.Dock = System.Windows.Forms.DockStyle.Fill;
                mUC_ChargingMain_Include_Wait_Insert_Battery_1024 = new P1024_BCC_UC_ChargingMain_Include_Wait_Insert_Battery();
                mUC_ChargingMain_Include_Wait_Insert_Battery_1024.Dock = System.Windows.Forms.DockStyle.Fill;
                mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024 = new P1024_BCC_UC_ChargingMain_Include_Wait_Retrieve_Battery();
                mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.Dock = System.Windows.Forms.DockStyle.Fill;
            }
            else
            {
                mUC_ChargingMain_Include_Main = new P788_BCC_UC_ChargingMain_Include_Main();
                mUC_ChargingMain_Include_Main.Dock = System.Windows.Forms.DockStyle.Fill;
                mUC_ChargingMain_Include_Qrcode = new P788_BCC_UC_ChargingMain_Include_Qrcode();
                mUC_ChargingMain_Include_Qrcode.Dock = System.Windows.Forms.DockStyle.Fill;


                mUC_ChargingMain_Include_Use_Complete = new P788_BCC_UC_ChargingMain_Include_Use_Complete();
                mUC_ChargingMain_Include_Use_Complete.Dock = System.Windows.Forms.DockStyle.Fill;
                mUC_ChargingMain_Include_Wait_Insert_Battery = new P788_BCC_UC_ChargingMain_Include_Wait_Insert_Battery();
                mUC_ChargingMain_Include_Wait_Insert_Battery.Dock = System.Windows.Forms.DockStyle.Fill;
                mUC_ChargingMain_Include_Wait_Retrieve_Battery = new P788_BCC_UC_ChargingMain_Include_Wait_Retrieve_Battery();
                mUC_ChargingMain_Include_Wait_Retrieve_Battery.Dock = System.Windows.Forms.DockStyle.Fill;
            }


            mUC_ChargingMain_Include_Process_Certification_Battery = new P788_BCC_UC_ChargingMain_Include_Process_Certification_Battery();
            mUC_ChargingMain_Include_Process_Certification_Battery.Dock = System.Windows.Forms.DockStyle.Fill;

        }

        public void setView_Main_NotifyBootOn()
        {
            mMainForm.getPanel_Main().Controls.Clear();
            mUC_Main_Notify_1Button.Text_Title = "프로그램을 시작중입니다..";
            mUC_Main_Notify_1Button.Text_Subtitle = "잠시만 기다려주세요.";
            mUC_Main_Notify_1Button.Button_Confirm.Visible = false;
            mMainForm.getPanel_Main().Controls.Add(mUC_Main_Notify_1Button);
        }

        public void setView_Main_Notify_Booton_First()
        {
            mMainForm.getPanel_Main().Controls.Clear();
            mUC_Main_Notify_1Button.Text_Title = "프로그램을 처음 실행하셨습니다.\n점검모드로 이동합니다.";
            mUC_Main_Notify_1Button.Text_Subtitle = "확인버튼을 눌러주세요.";
            mUC_Main_Notify_1Button.Button_Confirm.Visible = true;
            mMainForm.getPanel_Main().Controls.Add(mUC_Main_Notify_1Button);
        }

        public void setView_Main_SelfDiagnosis()
        {
            mMainForm.getPanel_Main().Controls.Clear();
            mMainForm.getPanel_Main().Controls.Add(mApplication.ControlManager_SelfDiagnosis.getUserControl());
        }

        public void setView_Main_NotifyBootOn2()
        {
            mMainForm.getPanel_Main().Controls.Clear();
            mMainForm.getPanel_Main().Controls.Add(mUC_Main_Notify_2Button);
        }

        public void clearView_Main()
        {
            mMainForm.getPanel_Main().Controls.Clear();
        }

        /*
         *
         *
         */
        public void setView_ChargingMain()
        {
            mMainForm.getPanel_Main().Controls.Clear();
            mMainForm.getPanel_Main().Controls.Add(mUC_MainPage);
        }

        public void setView_ChargingMain_Main()
        {
            setView_ChargingMain();
            mUC_ChargingMain_Include_Qrcode_1024.disposeQRcode();

            setView_ChargingMain_Front(mUC_ChargingMain_Include_Main);

            mUC_MainPage.setVisible_Button_Back(false);
        }

        public void updateView_ChargingMain_Main_Kiosk()
        {
            //mApplication.serial
        }

        public void setView_ChargingMain_Main(bool isCanUse)  // 시작한 이후 화면전환함
        {
            setView_ChargingMain();
            if (MyApplication.bIsScreen_Width_1024) // 화면의 크기에 따라
            {
                mUC_ChargingMain_Include_Qrcode_1024.disposeQRcode();
            }
            else
            {
                mUC_ChargingMain_Include_Qrcode.disposeQRcode();
            }
                
            mUC_MainPage.setBottombar_Weather();  // 화면밑에 날씨정보 추가
            
            if(MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Main_1024.setIsCanUse(isCanUse); // 시작버튼을 눌러주세요 출력됨

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Main_1024);
            }
            else
            {
                mUC_ChargingMain_Include_Main.setIsCanUse(isCanUse);  

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Main);
            }
            

            mUC_MainPage.setVisible_Button_Back(false);
        }

        public void setView_ChargingMain_Qrcode(string qrcode)
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Qrcode_1024.makeQRcode(qrcode);
            }
            else
            {
                mUC_ChargingMain_Include_Qrcode.makeQRcode(qrcode);
            }
                
            mUC_MainPage.setBottombar_ProcessStep();

            if (MyApplication.bIsScreen_Width_1024)
            {
                setView_ChargingMain_Front(mUC_ChargingMain_Include_Qrcode_1024);
            }
            else
            {
                setView_ChargingMain_Front(mUC_ChargingMain_Include_Qrcode);
            }
            

            mUC_MainPage.setVisible_Button_Back(true);
        }

        public void setView_ChargingMain_Wait_Retrieve_Battery_Complete(int order, int indexArray)
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_AlarmComplete(indexArray);
                switch (order)
                {
                    case 1:
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.setContent_Retrieve_Battery_1st_Complete();
                        break;
                    case 2:
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.setContent_Retrieve_Battery_2nd_Complete();
                        break;
                }
            }
            else
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_AlarmComplete(indexArray);
                switch (order)
                {
                    case 1:
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery.setContent_Retrieve_Battery_1st_Complete();
                        break;
                    case 2:
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery.setContent_Retrieve_Battery_2nd_Complete();
                        break;
                }
            }
            
            mUC_MainPage.setVisible_Button_Back(false);
        }

        public void setView_ChargingMain_Wait_Retrieve_Battery()
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024);

                mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(2, 3);
            }
            else
            {
                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery);

                mUC_ChargingMain_Include_Wait_Retrieve_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(2, 3);
            }
            
            mUC_MainPage.setVisible_Button_Back(false);
        }

        public void setView_ChargingMain_Wait_Retrieve_Battery(string divide, int order, int indexArray)
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray);
            }
            else
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray);
            }
            
            mUC_MainPage.setVisible_Button_Back(false);
            switch (order)
            {
                case 1:
                    if (MyApplication.bIsScreen_Width_1024)
                    {
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.setContent_Retrieve_Battery_1st(indexArray + 1);

                        setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024);
                    }
                    else
                    {
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery.setContent_Retrieve_Battery_1st(indexArray + 1);

                        setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery);
                    }
                    break;
                case 2:
                    if (MyApplication.bIsScreen_Width_1024)
                    {
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.setContent_Retrieve_Battery_2nd(indexArray + 1);
                    }
                    else
                    {
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery.setContent_Retrieve_Battery_2nd(indexArray + 1);
                    }    
                    break;
                case 3:
                    if (MyApplication.bIsScreen_Width_1024)
                    {
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.setContent_Retrieve_Battery(indexArray + 1);
                    }
                    else
                    {
                        mUC_ChargingMain_Include_Wait_Retrieve_Battery.setContent_Retrieve_Battery_2nd(indexArray + 1);
                    }
                    break;
            }
            
            
        }

        public void setView_ChargingMain_Wait_Retrieve_Battery(int indexArray_Battery1, int indexArray_Battery2)
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray_Battery1, indexArray_Battery2);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024);
            }else
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray_Battery1, indexArray_Battery2);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery);
            }
            
        }

        public void setView_ChargingMain_Process_Certification_Battery()
        {
            mUC_ChargingMain_Include_Process_Certification_Battery.setText_Title("회수된 배터리팩을\n인증중입니다.");
            mUC_MainPage.setVisible_Button_Back(false);

            setView_ChargingMain_Front(mUC_ChargingMain_Include_Process_Certification_Battery);
        }

        public void setView_ChargingMain_Complete_Certification_Battery()
        {
            mUC_ChargingMain_Include_Process_Certification_Battery.setText_Title("인증되었습니다.");

            mUC_MainPage.setVisible_Button_Back(false);
        }

        public void setView_ChargingMain_Failed_Certification_Battery()
        {

            mUC_ChargingMain_Include_Process_Certification_Battery.setText_Title("배터리 인증에 실패하였습니다.\n삽입하신 배터리를 회수하세요.");
            mUC_MainPage.setVisible_Button_Back(false);

            setView_ChargingMain_Front(mUC_ChargingMain_Include_Process_Certification_Battery);
        }

        public void setView_ChargingMain_Wait_Retrieve_Battery_When_Failed_Certification_Battery()
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(0, 1);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024);
            }
            else
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(0, 1);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery);
            }
                
        }

        public void setView_ChargingMain_Wait_Retrieve_Battery_When_Failed_Certification_Battery(int indexArray_Battery1, int indexArray_Battery2)
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray_Battery1, indexArray_Battery2);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024);
            }
            else
            {
                mUC_ChargingMain_Include_Wait_Retrieve_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray_Battery1, indexArray_Battery2);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Retrieve_Battery);
            }
                
        }

        public void setView_ChargingMain_Wait_Insert_Battery_Complete(int order, int indexArray)
        {

            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Insert_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_AlarmComplete(indexArray);
                switch (order)
                {
                    case 1:
                        mUC_ChargingMain_Include_Wait_Insert_Battery_1024.setContent_Insert_Battery_1st_Complete();
                        break;
                    case 2:
                        mUC_ChargingMain_Include_Wait_Insert_Battery_1024.setContent_Insert_Battery_2nd_Complete();
                        break;
                    case 3:
                        mUC_ChargingMain_Include_Wait_Insert_Battery_1024.setContent_Insert_Battery_Complete();
                        break;
                }
            }
            else
            {
                mUC_ChargingMain_Include_Wait_Insert_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_AlarmComplete(indexArray);
                switch (order)
                {
                    case 1:
                        mUC_ChargingMain_Include_Wait_Insert_Battery.setContent_Insert_Battery_1st_Complete();
                        break;
                    case 2:
                        mUC_ChargingMain_Include_Wait_Insert_Battery.setContent_Insert_Battery_2nd_Complete();
                        break;
                }
            }
                
            
            mUC_MainPage.setVisible_Button_Back(false);
        }

        public void setView_ChargingMain_Wait_Insert_Battery(int indexArray_Battery1, int indexArray_Battery2)
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Insert_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray_Battery1, indexArray_Battery2);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Insert_Battery_1024);
            }
            else
            {
                mUC_ChargingMain_Include_Wait_Insert_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray_Battery1, indexArray_Battery2);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Insert_Battery);
            }
                
        }

        public void setView_ChargingMain_Wait_Insert_Battery_AnimcationInsertBattery()
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mApplication.DataManager_UserControl.mUC_ChargingMain_Include_Wait_Insert_Battery_1024.animationInsertBattery();
            }
            else
            {
                mApplication.DataManager_UserControl.mUC_ChargingMain_Include_Wait_Insert_Battery.animationInsertBattery();
            }
                
        }

        

        public void setView_ChargingMain_Wait_Insert_Battery(string divide, int order, int indexArray)  // indexarray = 슬롯 번호
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Insert_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray);
                switch (order)
                {
                    case 1:
                        mUC_ChargingMain_Include_Wait_Insert_Battery_1024.setContent_Insert_Battery_1st();
                        setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Insert_Battery_1024);
                        break;
                    case 2:
                        mUC_ChargingMain_Include_Wait_Insert_Battery_1024.setContent_Insert_Battery_2nd();
                        break;
                    case 3:
                        mUC_ChargingMain_Include_Wait_Insert_Battery_1024.setContent_Insert_Battery();
                        setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Insert_Battery_1024);
                        break;
                }
            }
            else
            {
                mUC_ChargingMain_Include_Wait_Insert_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(indexArray);
                switch (order)
                {
                    case 1:
                        mUC_ChargingMain_Include_Wait_Insert_Battery.setContent_Insert_Battery_1st();

                        setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Insert_Battery);
                        break;
                    case 2:
                        mUC_ChargingMain_Include_Wait_Insert_Battery.setContent_Insert_Battery_2nd();
                        break;
                }
            }
                
            
            mUC_MainPage.setVisible_Button_Back(false);
        }

        public void setView_ChargingMain_Wait_Insert_Battery()
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_ChargingMain_Include_Wait_Insert_Battery_1024.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(0, 1);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Insert_Battery_1024);
            }
            else
            {
                mUC_ChargingMain_Include_Wait_Insert_Battery.UC_ChargingMain_Unit_Kiosk.setKioskState_Alarm(0, 1);
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Wait_Insert_Battery);
            }
                
        }

        public void setView_ChargingMain_Use_Complete()
        {
            if (MyApplication.bIsScreen_Width_1024)
            {
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Use_Complete_1024);
            }
            else
            {
                mUC_MainPage.setVisible_Button_Back(false);

                setView_ChargingMain_Front(mUC_ChargingMain_Include_Use_Complete);
            }
                
        }


        public P788_BCC_UC_ChargingMain_Include_Use_Complete mUC_ChargingMain_Include_Use_Complete = null;
        public P788_BCC_UC_ChargingMain_Include_Wait_Insert_Battery mUC_ChargingMain_Include_Wait_Insert_Battery = null;
        public P788_BCC_UC_ChargingMain_Include_Process_Certification_Battery mUC_ChargingMain_Include_Process_Certification_Battery = null;
        public P788_BCC_UC_ChargingMain_Include_Wait_Retrieve_Battery mUC_ChargingMain_Include_Wait_Retrieve_Battery = null;
        
        public P788_BCC_UC_ChargingMain_Include_Qrcode mUC_ChargingMain_Include_Qrcode = null;
        public P788_BCC_UC_ChargingMain_Include_Main mUC_ChargingMain_Include_Main = null;
        public P788_UC_MainPage mUC_MainPage = null;

        public P1024_BCC_UC_ChargingMain_Include_Use_Complete mUC_ChargingMain_Include_Use_Complete_1024 = null;
        public P1024_BCC_UC_ChargingMain_Include_Wait_Insert_Battery mUC_ChargingMain_Include_Wait_Insert_Battery_1024 = null;
        public P1024_BCC_UC_ChargingMain_Include_Wait_Retrieve_Battery mUC_ChargingMain_Include_Wait_Retrieve_Battery_1024 = null;
                
        public P1024_BCC_UC_ChargingMain_Include_Qrcode mUC_ChargingMain_Include_Qrcode_1024 = null;
        public P1024_BCC_UC_ChargingMain_Include_Main mUC_ChargingMain_Include_Main_1024 = null;

        public UC_MainPage_Notify_1Button mUC_Main_Notify_1Button = null;
        public UC_MainPage_Notify_2Button mUC_Main_Notify_2Button = null;

        protected void setView_ChargingMain_Front(UserControl usercontrol)
        {
            if (!mUC_MainPage.getPanel_Main().Controls.Contains(usercontrol))
                mUC_MainPage.getPanel_Main().Controls.Add(usercontrol);

            if(mUC_MainPage.getPanel_Main().Controls.GetChildIndex(usercontrol) > 0)
                mUC_MainPage.getPanel_Main().Controls.SetChildIndex(usercontrol, 0);
        }
    }
}
 
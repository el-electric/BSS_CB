using BatteryChangeCharger.Applications;
using BatteryChangeCharger.Controller;
using BatteryChangeCharger.ControlManager;
using BatteryChangeCharger.CorrectionMode.Self_diagnosis;
using BatteryChangeCharger.Custom_UsercControl;
using BatteryChangeCharger.Interface_Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.ControlManager
{
    public class BCC_ControlManager_SelfDiagnosis : Controller_Base,IControlManager_UserControl, ISelectListener
    {
        protected UC_ProcessStep_Common mUC_ProcessStep = null;
        protected string[] STRING_STEP = {"Home", "통신연결", "IO 확인", "운영배터리 셋업", "수동 충전 테스트", "점검 완료"};

        public BCC_ControlManager_SelfDiagnosis(MyApplication application) : base(application, 0)
        {
            
        }

        public UserControl getUserControl()
        {
            return mUC_ProcessStep;
        }

        public void onSelectChanged(int indexArray)
        {
            switch (indexArray)
            {
                case 0:
                    mUC_ProcessStep.Panel_Main.Controls.Clear();
                    mUC_ProcessStep.Panel_Main.Controls.Add(mUC_SelfDiagnosis_Include_Home);
                    break;
                case 1:
                    mUC_ProcessStep.Panel_Main.Controls.Clear();
                    mUC_ProcessStep.Panel_Main.Controls.Add(mUC_SelfDiagnosis_Include_Test_Comm);
                    break;
                case 2:
                    mUC_ProcessStep.Panel_Main.Controls.Clear();
                    mUC_ProcessStep.Panel_Main.Controls.Add(mUC_SelfDiagnosis_Include_Test_IO);
                    break;
                case 3:
                    mUC_ProcessStep.Panel_Main.Controls.Clear();
                    mUC_ProcessStep.Panel_Main.Controls.Add(mUC_SelfDiagnosis_Include_Setup_Battery);
                    break;
                case 4:
                    mUC_ProcessStep.Panel_Main.Controls.Clear();
                    mUC_ProcessStep.Panel_Main.Controls.Add(UC_SelfDiagnosis_Include_Test_Charging);
                    break;
                case 5:
                    mUC_ProcessStep.Panel_Main.Controls.Clear();
                    mUC_ProcessStep.Panel_Main.Controls.Add(mUC_SelfDiagnosis_Include_Complete);
                    break;
            }
        }

        public override void process()
        {
            
        }

        public void setView()
        {
            mUC_ProcessStep = new UC_ProcessStep_Common();
            //mUC_ProcessStep.Title = "자가진단 모드";

            foreach(string text in STRING_STEP)
            {
                mUC_ProcessStep.addButton(text);
            }

            mUC_SelfDiagnosis_Include_Home = new BCC_UC_SelfDiagnosis_Include_Home();

            mUC_SelfDiagnosis_Include_Test_Comm = new BCC_UC_SelfDiagnosis_Include_Test_Comm();
            mUC_SelfDiagnosis_Include_Test_IO = new BCC_UC_SelfDiagnosis_Include_Test_IO();
            mUC_SelfDiagnosis_Include_Setup_Battery = new BCC_UC_SelfDiagnosis_Include_Setup_Battery();
            UC_SelfDiagnosis_Include_Test_Charging = new BCC_UC_SelfDiagnosis_Include_Test_Charging();
            mUC_SelfDiagnosis_Include_Complete = new BCC_UC_SelfDiagnosis_Include_Complete();

            mUC_ProcessStep.setListener_Selected(this);
            mUC_ProcessStep.activateButton(0);
            

            
        }


        protected BCC_UC_SelfDiagnosis_Include_Home mUC_SelfDiagnosis_Include_Home = null;
        protected BCC_UC_SelfDiagnosis_Include_Test_Comm mUC_SelfDiagnosis_Include_Test_Comm = null;
        protected BCC_UC_SelfDiagnosis_Include_Test_IO mUC_SelfDiagnosis_Include_Test_IO = null;
        protected BCC_UC_SelfDiagnosis_Include_Setup_Battery mUC_SelfDiagnosis_Include_Setup_Battery = null;
        protected BCC_UC_SelfDiagnosis_Include_Test_Charging UC_SelfDiagnosis_Include_Test_Charging = null;
        protected BCC_UC_SelfDiagnosis_Include_Complete mUC_SelfDiagnosis_Include_Complete = null;


    }
}

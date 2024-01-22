using BatteryChangeCharger.Applications;
using BatteryChangeCharger.Controller;
using BatteryChangeCharger.Interface_Common;
using BatteryChangeCharger.StaticVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.Custom_UsercControl
{
    public partial class UC_MainPage_Notify_1Button : UserControl
    {
        public UC_MainPage_Notify_1Button()
        {
            InitializeComponent();
        }

        public string Text_Title
        {
            set { label_title.Text = value; }
        }
        public string Text_Subtitle
        {
            set { label_subtitle.Text = value; }
        }

        protected IClickListener mClickListener_Confirm = null;
        public void setOnClickListener_Confirm(IClickListener listener)
        {
            mClickListener_Confirm = listener;
        }

        public Button Button_Confirm
        {
            get { return button_confirm; }
        }
        

        protected Controller_Base mController = null;
        public Controller_Base Controller
        {
            get { return mController; }
            set { mController = value; }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(mClickListener_Confirm != null)
            {
                mClickListener_Confirm.onClick(this);
            }
            else
            {
                if (mController != null)
                    mController.addEvent(CONST_EVENT.EVENT_CLICK_BUTTON_CONFIRM);
                else
                    MyApplication.getInstance().Controller_Main.addEvent(CONST_EVENT.EVENT_CLICK_BUTTON_CONFIRM);
            }
        }

        
    }
}

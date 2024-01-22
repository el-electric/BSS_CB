using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{

    
    public partial class P788_UC_Bottombar_ProcessStep : UserControl
    {
        
        public P788_UC_Bottombar_ProcessStep()
        {
            InitializeComponent();
            timer1.Enabled = true;
            mLabel_Step = new Label[] { label_step_1, label_step_2, label_step_3, label_step_4, label_step_5, label_step_6 };
            setBottomBar_Step(0);
        }

        Label[] mLabel_Step = null;
        protected int mBottomBar_Step = 0;
        public void setBottomBar_Step(int step)
        {
            switch (step)
            {
                case 0:
                    timer1.Stop();
                    break;
                default:
                    timer1.Start();
                    break;
            }
            mBottomBar_Step = step;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < mLabel_Step.Length; i++)
            {
                if (mBottomBar_Step == i)
                {
                    mLabel_Step[i].Visible = !mLabel_Step[i].Visible;
                    mLabel_Step[i].ForeColor = Color.Red;
                }
                else
                {
                    mLabel_Step[i].Visible = true;
                    mLabel_Step[i].ForeColor = Color.White;
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label_step_3_Click(object sender, EventArgs e)
        {

        }
    }
}

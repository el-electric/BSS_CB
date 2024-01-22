using BatteryChangeCharger.Applications;
using BatteryChangeCharger.StaticVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.CorrectionMode.Self_diagnosis
{
    public partial class BCC_UC_SelfDiagnosis_Include_Complete : UserControl
    {
        public BCC_UC_SelfDiagnosis_Include_Complete()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button_confirm_Click(object sender, EventArgs e)
        {
            MyApplication.getInstance().Controller_Main.addEvent(CONST_EVENT.EVENT_PROCESS_COMPLETE);
        }
    }
}

using BatteryChangeCharger.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Self_diagnosis
{
    public partial class P788_BCC_UC_SelfDiagnosis_Include_Test_Charging : UserControl
    {
        public P788_BCC_UC_SelfDiagnosis_Include_Test_Charging()
        {
            InitializeComponent();
        }

        protected int mChannelIndex_Selected = 1;

        public void setChargingCommand(int index, bool setting)
        {
             /*MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets[index - 1].mPacket_c1_Send.setCommand_Charging(setting);*/
        }

        void setSelectedIndex(int index)
        {
            if (index == mChannelIndex_Selected)
                return;

            mChannelIndex_Selected = index;
            label_socketnumber.Text = "" + mChannelIndex_Selected;
        }

        private void button_ch1_Click(object sender, EventArgs e)
        {
            int index = 1;
            setSelectedIndex(index);
        }

        private void button_ch2_Click(object sender, EventArgs e)
        {
            int index = 2;
            setSelectedIndex(index);
        }

        private void button_ch3_Click(object sender, EventArgs e)
        {
            int index = 3;
            setSelectedIndex(index);
        }

        private void button_ch4_Click(object sender, EventArgs e)
        {
            int index = 4;
            setSelectedIndex(index);
        }

        private void button_ch5_Click(object sender, EventArgs e)
        {
            int index = 5;
            setSelectedIndex(index);
        }

        private void button_ch6_Click(object sender, EventArgs e)
        {
            int index = 6;
            setSelectedIndex(index);
        }

        private void button_ch7_Click(object sender, EventArgs e)
        {
            int index = 7;
            setSelectedIndex(index);
        }

        private void button_ch8_Click(object sender, EventArgs e)
        {
            int index = 8;
            setSelectedIndex(index);
        }

        private void button_chargingstart_Click(object sender, EventArgs e)
        {
            setChargingCommand(mChannelIndex_Selected - 1, true);
        }

        private void button_chargingstop_Click(object sender, EventArgs e)
        {
            setChargingCommand(mChannelIndex_Selected - 1, false);
        }
    }
}

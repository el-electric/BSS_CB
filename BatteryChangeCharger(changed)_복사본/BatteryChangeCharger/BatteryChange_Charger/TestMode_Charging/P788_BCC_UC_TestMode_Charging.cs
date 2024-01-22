using BatteryChangeCharger.Applications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.TestMode_Charging
{
    public partial class P788_BCC_UC_TestMode_Charging : UserControl
    {
        protected UC_TestMode_LayoutUnit_Charger[] mLayouts = new UC_TestMode_LayoutUnit_Charger[8];

        public P788_BCC_UC_TestMode_Charging()
        {
            InitializeComponent();

            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            
            for (int i = 1; i < 9; i++)
            {
                mLayouts[i - 1] = new UC_TestMode_LayoutUnit_Charger(i);
                flowLayoutPanel1.Controls.Add(mLayouts[i - 1]);
            }
            // new TouchScrollPanel(flowLayoutPanel1);

            updateView();

            timer1.Enabled = true;
            timer1.Start();
        }

        public void updateView()
        {
            for (int i = 1; i < 9; i++)
            {
                mLayouts[i - 1].updateView();
            }
        }

        private void btn_main_door_open_Click(object sender, EventArgs e)
        {
            /*MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Send.operation_DoorOpen(true);*/
        }

        private void btn_main_door_close_Click(object sender, EventArgs e)
        {
            /*MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Send.operation_DoorOpen(false);*/
        }

        private void btn_main_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btn_main_setting_Click(object sender, EventArgs e)
        {

        }

        private void Form_TestMode_Charging_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updateView();
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            updateView();
        }
    }
}

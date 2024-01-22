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
    public partial class P788_BCC_UC_SelfDiagnosis_Include_Setup_Battery : UserControl
    {
        protected Label[] mLabel_SocketAdd = new Label[8];
        protected bool[] mSelected_Socket = { false, false, false, false, false, false, false, false };

        public P788_BCC_UC_SelfDiagnosis_Include_Setup_Battery()
        {
            InitializeComponent();

            for (int i = 0; i < 8; i++)
            {
                mLabel_SocketAdd[i] = new Label();
                mLabel_SocketAdd[i].BackColor = System.Drawing.Color.White;
                mLabel_SocketAdd[i].BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                mLabel_SocketAdd[i].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                mLabel_SocketAdd[i].Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                mLabel_SocketAdd[i].Location = new System.Drawing.Point(170, 764);
                mLabel_SocketAdd[i].Size = new System.Drawing.Size(70, 80);
                mLabel_SocketAdd[i].TabIndex = 8;
                mLabel_SocketAdd[i].Text = (i + 1) + "\n번 소켓";
                mLabel_SocketAdd[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
        }

        public void onSelectButton(int indexArray)
        {
            if (mSelected_Socket[indexArray] == false)
            {
                mSelected_Socket[indexArray] = true;
            }
            else
            {
                mSelected_Socket[indexArray] = false;
            }

            flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < mSelected_Socket.Length; i++)
            {
                if (mSelected_Socket[i] == true)
                {
                    flowLayoutPanel1.Controls.Add(mLabel_SocketAdd[i]);
                }
            }
        }

        private void button_ch1_Click(object sender, EventArgs e)
        {
            int indexArray = 0;
            onSelectButton(indexArray);
        }

        private void button_ch2_Click(object sender, EventArgs e)
        {
            int indexArray = 1;
            onSelectButton(indexArray);
        }

        private void button_ch3_Click(object sender, EventArgs e)
        {
            int indexArray = 2;
            onSelectButton(indexArray);
        }

        private void button_ch4_Click(object sender, EventArgs e)
        {
            int indexArray = 3;
            onSelectButton(indexArray);
        }

        private void button_ch5_Click(object sender, EventArgs e)
        {
            int indexArray = 4;
            onSelectButton(indexArray);
        }

        private void button_ch6_Click(object sender, EventArgs e)
        {
            int indexArray = 5;
            onSelectButton(indexArray);
        }

        private void button_ch7_Click(object sender, EventArgs e)
        {
            int indexArray = 6;
            onSelectButton(indexArray);
        }

        private void button_ch8_Click(object sender, EventArgs e)
        {
            int indexArray = 7;
            onSelectButton(indexArray);
        }

        private void btn_door_open_all_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 8; i++)
            {
                /*MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Send.operation_DoorOpen(true);*/
            }
        }

        private void btn_door_open_part_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                /*if(mSelected_Socket[i])*/
                    /*MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Send.operation_DoorOpen(true);*/
            }
        }

        private void btn_door_close_part_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                /*if (mSelected_Socket[i])*/
                    /*MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Send.operation_DoorOpen(false);*/
            }
        }

        private void btn_door_close_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
            {
                /*MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Send.operation_DoorOpen(false);*/
            }
        }
    }
}

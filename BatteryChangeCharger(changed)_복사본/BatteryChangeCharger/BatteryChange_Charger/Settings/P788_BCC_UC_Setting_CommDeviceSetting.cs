using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Settings
{
    public partial class P788_BCC_UC_Setting_CommDeviceSetting : UserControl
    {
        string mCommPath_Default_IO = "";
        string mCommPath_Default_NFC = "";

        public P788_BCC_UC_Setting_CommDeviceSetting()
        {
            InitializeComponent();
            label_compath_io.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.PATH_SERIAL_IOBOARD);
            label_compath_nfc.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.PATH_SERIAL_NFC_MAIN);

            mCommPath_Default_IO = label_compath_io.Text;
            mCommPath_Default_NFC = label_compath_nfc.Text;
            
            refreshCommpath();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            refreshCommpath();


        }


        const int SIZE_BUTTON_WIDTH = 90;
        const int SIZE_BUTTON_HEIGHT = 70;

        public void refreshCommpath()
        {
            string[] comlist = System.IO.Ports.SerialPort.GetPortNames();

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            //COM Port가 있는 경우에만 콤보 박스에 추가.\

            Button btn2 = new Button();
            btn2.BackColor = System.Drawing.Color.DarkGray;
            //btn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            btn2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            btn2.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            btn2.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
            btn2.Text = "";
            btn2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btn2.Click += comlist_io_Click;
            flowLayoutPanel1.Controls.Add(btn2);

            Button btn3 = new Button();
            btn3.BackColor = System.Drawing.Color.DarkGray;
            //btn3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            btn3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            btn3.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            btn3.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
            btn3.Text = "";
            btn3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btn3.Click += comlist_nfc_Click;
            flowLayoutPanel2.Controls.Add(btn3);

            if (comlist.Length > 0)
            {
                for (int i = 0; i < comlist.Length; i++)
                {
                    Button btn = new Button();
                    btn.BackColor = System.Drawing.Color.White;
                    //btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
                    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                    btn.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
                    btn.Text = comlist[i];
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btn.Click += comlist_io_Click;
                    flowLayoutPanel1.Controls.Add(btn);
                }
                for (int i = 0; i < comlist.Length; i++)
                {
                    Button btn = new Button();
                    btn.BackColor = System.Drawing.Color.White;
                    //btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
                    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                    btn.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
                    btn.Text = comlist[i];
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btn.Click += comlist_nfc_Click;
                    flowLayoutPanel2.Controls.Add(btn);
                }

            }

            //for (int i = 0; i < 20; i++)
            //{
            //    Button btn = new Button();
            //    btn.BackColor = System.Drawing.Color.White;
            //    //btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            //    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            //    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            //    btn.Size = new System.Drawing.Size(SIZE_BUTTON_WIDTH, SIZE_BUTTON_HEIGHT);
            //    btn.Text = "asdfsdaf";
            //    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //    btn.Click += comlist_io_Click;
            //    flowLayoutPanel1.Controls.Add(btn);
            //}

            // touchScrollPanel1 = new TouchScrollPanel(flowLayoutPanel1);
            // touchScrollPanel2 = new TouchScrollPanel(flowLayoutPanel2);
        }
        // TouchScrollPanel touchScrollPanel1 = null;
        // TouchScrollPanel touchScrollPanel2 = null;
        private void comlist_io_Click(object sender, EventArgs e)
        {
            label_compath_io.Text = ((Button)sender).Text;
        }

        private void comlist_nfc_Click(object sender, EventArgs e)
        {
            label_compath_nfc.Text = ((Button)sender).Text;
        }

        private void button_refresh_list_Click(object sender, EventArgs e)
        {
            refreshCommpath();
        }

        private void button_init_Click(object sender, EventArgs e)
        {
            label_compath_io.Text = mCommPath_Default_IO;
            label_compath_nfc.Text = mCommPath_Default_NFC;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if(label_compath_io.Text.Length >= 4)
                MyApplication.getInstance().Manager_SettingData_Main.setSettingData(EINDEX_SETTING_MAIN.PATH_SERIAL_IOBOARD, label_compath_io.Text);

            if (label_compath_io.Text.Length >= 4)
                MyApplication.getInstance().Manager_SettingData_Main.setSettingData(EINDEX_SETTING_MAIN.PATH_SERIAL_NFC_MAIN, label_compath_nfc.Text);
        }
    }
}

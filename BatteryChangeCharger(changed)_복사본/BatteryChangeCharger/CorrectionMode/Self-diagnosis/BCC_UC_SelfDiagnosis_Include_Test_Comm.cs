using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.CorrectionMode.Self_diagnosis
{
    public partial class BCC_UC_SelfDiagnosis_Include_Test_Comm : UserControl
    {
        

        public BCC_UC_SelfDiagnosis_Include_Test_Comm()
        {
            InitializeComponent();
            label_compath_io.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.PATH_SERIAL_IOBOARD);
            label_compath_nfc.Text = MyApplication.getInstance().Manager_SettingData_Main.getSettingData(EINDEX_SETTING_MAIN.PATH_SERIAL_NFC_MAIN);
            refreshCommpath();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            refreshCommpath();


        }

        private void refreshCommpath()
        {
            string[] comlist = System.IO.Ports.SerialPort.GetPortNames();

            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel2.Controls.Clear();
            //COM Port가 있는 경우에만 콤보 박스에 추가.\

            Label btn2 = new Label();
            btn2.BackColor = System.Drawing.Color.Blue;
            btn2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            btn2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            btn2.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            btn2.Size = new System.Drawing.Size(70, 80);
            btn2.Text = "";
            btn2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btn2.Click += comlist_io_Click;
            flowLayoutPanel1.Controls.Add(btn2);

            Label btn3 = new Label();
            btn3.BackColor = System.Drawing.Color.Blue;
            btn3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            btn3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            btn3.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
            btn3.Size = new System.Drawing.Size(70, 80);
            btn3.Text = "";
            btn3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            btn3.Click += comlist_nfc_Click;
            flowLayoutPanel1.Controls.Add(btn2);

            if (comlist.Length > 0)
            {
                for (int i = 0; i < comlist.Length; i++)
                {
                    Label btn = new Label();
                    btn.BackColor = System.Drawing.Color.White;
                    btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
                    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                    btn.Size = new System.Drawing.Size(70, 80);
                    btn.Text = comlist[i];
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btn.Click += comlist_io_Click;
                    flowLayoutPanel1.Controls.Add(btn);
                }
                for (int i = 0; i < comlist.Length; i++)
                {
                    Label btn = new Label();
                    btn.BackColor = System.Drawing.Color.White;
                    btn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.System;
                    btn.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Bold);
                    btn.Size = new System.Drawing.Size(70, 80);
                    btn.Text = comlist[i];
                    btn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                    btn.Click += comlist_nfc_Click;
                    flowLayoutPanel2.Controls.Add(btn);
                }

            }
        }

        private void comlist_io_Click(object sender, EventArgs e)
        {
            label_compath_io.Text = ((Label)sender).Text;

            MyApplication.getInstance().Manager_SettingData_Main.setSettingData_Database(EINDEX_SETTING_MAIN.PATH_SERIAL_IOBOARD, label_compath_io.Text);
        }

        private void comlist_nfc_Click(object sender, EventArgs e)
        {
            label_compath_nfc.Text = ((Label)sender).Text;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.CorrectionMode
{
    public partial class Form_CorrectionMode : Form
    {
        public Form_CorrectionMode()
        {
            InitializeComponent();

            
            //flowLayoutPanel_ioboard.Controls.Add(new UC_CorrectionMode_IOBoard());
        }

        public void setView()
        {
            comboBox_kiosk.SelectedIndex = 0;
            comboBox_socket.SelectedIndex = 0;

            flowLayoutPanel_ioboard_send.Controls.Add(new UC_CorrectionMode_IOBoard_Send_Common());
            flowLayoutPanel_ioboard_send.Controls.Add(new UC_CorrectionMode_IOBoard_Send_z1(this));
            flowLayoutPanel_ioboard_receive.Controls.Add(new UC_CorrectionMode_IOBoard_Receive_Common());
            flowLayoutPanel_ioboard_receive.Controls.Add(new UC_CorrectionMode_IOBoard_Receive_z1(this));


            flowLayoutPanel_nfcboard_send.Controls.Add(new UC_CorrectionMode_NFCBoard_Send_Common());
            flowLayoutPanel_nfcboard_send.Controls.Add(new UC_CorrectionMode_NFCBoard_Send_z1(this));
            flowLayoutPanel_nfcboard_receive.Controls.Add(new UC_CorrectionMode_NFCBoard_Receive_Common());
            flowLayoutPanel_nfcboard_receive.Controls.Add(new UC_CorrectionMode_NFCBoard_Receive_z1(this));
        }


        public ComboBox ComboBox_Kiosk
        {
            get { return comboBox_kiosk; }
        }

        public ComboBox ComboBox_Socket
        {
            get { return comboBox_socket; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}

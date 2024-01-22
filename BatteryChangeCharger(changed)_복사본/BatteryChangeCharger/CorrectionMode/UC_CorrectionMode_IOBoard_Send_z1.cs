using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.CorrectionMode
{
    public partial class UC_CorrectionMode_IOBoard_Send_z1 : UserControl
    {
        protected UC_CorrectionMode_IOBoard_Send_z1_Socket[] mUC_Sockets = new UC_CorrectionMode_IOBoard_Send_z1_Socket[8];



        private UC_CorrectionMode_IOBoard_Send_z1()
        {
            InitializeComponent();

            for (int i = 0; i < 8; i++)
            {
                mUC_Sockets[i] = new UC_CorrectionMode_IOBoard_Send_z1_Socket(i + 1);
                flowLayoutPanel1.Controls.Add(mUC_Sockets[i]);
            }
        }

        public UC_CorrectionMode_IOBoard_Send_z1(Form_CorrectionMode form_CorrectionMode)
        {
            InitializeComponent();

            for (int i = 0; i < 8; i++)
            {
                mUC_Sockets[i] = new UC_CorrectionMode_IOBoard_Send_z1_Socket(i + 1);
                flowLayoutPanel1.Controls.Add(mUC_Sockets[i]);
            }

            mForm_CorrectionMode = form_CorrectionMode;
            //mForm_CorrectionMode.ComboBox_Kiosk.SelectedIndexChanged += ComboBox_Kiosk_SelectedIndexChanged;
            //mForm_CorrectionMode.ComboBox_Socket.SelectedIndexChanged += ComboBox_Socket_SelectedIndexChanged;
        }
        protected Form_CorrectionMode mForm_CorrectionMode = null;

        private void ComboBox_Kiosk_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ComboBox_Socket_SelectedIndexChanged(object sender, EventArgs e)
        {
            int channelIndex = ((ComboBox)sender).SelectedIndex;
            switch (channelIndex)
            {
                case 0:
                    flowLayoutPanel1.Controls.Clear();
                    for (int i = 0; i < 8; i++)
                    {
                        flowLayoutPanel1.Controls.Add(mUC_Sockets[i]);
                    }
                    break;
                default:
                    flowLayoutPanel1.Controls.Clear();
                    flowLayoutPanel1.Controls.Add(mUC_Sockets[channelIndex - 1]);
                    break;
            }
        }
    }
}

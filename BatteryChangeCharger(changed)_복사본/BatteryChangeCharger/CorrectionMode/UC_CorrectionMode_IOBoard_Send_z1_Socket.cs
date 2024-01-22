using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.CorrectionMode
{
    public partial class UC_CorrectionMode_IOBoard_Send_z1_Socket : UserControl
    {
        public UC_CorrectionMode_IOBoard_Send_z1_Socket()
        {
            InitializeComponent();
        }

        protected int mChannelIndex = 0;
        public UC_CorrectionMode_IOBoard_Send_z1_Socket(int channelIndex)
        {
            InitializeComponent();
            mChannelIndex = channelIndex;
            label_socket_number.Text = ""+mChannelIndex;
        }
    }
}

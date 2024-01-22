using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.CorrectionMode
{
    public partial class UC_CorrectionMode_IOBoard_Receive_z1 : UserControl
    {
        private UC_CorrectionMode_IOBoard_Receive_z1()
        {
            InitializeComponent();
        }

        public UC_CorrectionMode_IOBoard_Receive_z1(Form_CorrectionMode form_CorrectionMode)
        {
            InitializeComponent();
            mForm_CorrectionMode = form_CorrectionMode;
            mForm_CorrectionMode.ComboBox_Kiosk.SelectedIndexChanged += ComboBox_Kiosk_SelectedIndexChanged;
            mForm_CorrectionMode.ComboBox_Socket.SelectedIndexChanged += ComboBox_Socket_SelectedIndexChanged;
        }
        protected Form_CorrectionMode mForm_CorrectionMode = null;

        private void ComboBox_Kiosk_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void ComboBox_Socket_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}

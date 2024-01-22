using BatteryChangeCharger.Manager;
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
    public partial class Form_Confirm_Program_Reset : Form
    {
        public Form_Confirm_Program_Reset()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            TopMost = true;
        }

        private void button_program_finish_Click(object sender, EventArgs e)
        {
            Manager_Application.finishApplication();
        }

        private void button_restart_Click(object sender, EventArgs e)
        {
            Manager_Application.finishApplication_ConfirmPopup();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_restart_system_Click(object sender, EventArgs e)
        {
            Manager_Application.restartSystem_ConfirmPopup();
        }
    }
}

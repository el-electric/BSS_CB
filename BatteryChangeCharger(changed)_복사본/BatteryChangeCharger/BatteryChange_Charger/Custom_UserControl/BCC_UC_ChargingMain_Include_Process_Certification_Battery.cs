﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    public partial class BCC_UC_ChargingMain_Include_Process_Certification_Battery : UserControl
    {
        public BCC_UC_ChargingMain_Include_Process_Certification_Battery()
        {
            InitializeComponent();
        }

        public void setText_Title(string text)
        {
            label_title.Text = text;
        }

    }
}

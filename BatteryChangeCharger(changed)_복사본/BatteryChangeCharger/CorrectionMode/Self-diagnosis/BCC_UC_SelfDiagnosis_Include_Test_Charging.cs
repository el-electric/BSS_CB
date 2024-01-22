using BatteryChangeCharger.Interface_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.CorrectionMode.Self_diagnosis
{

    public partial class BCC_UC_SelfDiagnosis_Include_Test_Charging : UserControl, ISelectListener
    {
        public BCC_UC_SelfDiagnosis_Include_Test_Charging()
        {
            InitializeComponent();

            bcC_UC_ChargingMain_Unit_kiosk1.setSelectListener(this);
        }

        public void onSelectChanged(int indexArray)
        {
            
        }
    }
}

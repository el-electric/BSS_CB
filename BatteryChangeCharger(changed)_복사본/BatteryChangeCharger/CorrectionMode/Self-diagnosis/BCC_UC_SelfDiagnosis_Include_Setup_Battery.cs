using BatteryChangeCharger.ChargerVariable;
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
    public partial class BCC_UC_SelfDiagnosis_Include_Setup_Battery : UserControl, ISelectListener
    {
        Label[] mLabel_SocketAdd = new Label[8];
        bool[] mSelected_Socket = {false, false, false, false, false, false, false, false };
        public BCC_UC_SelfDiagnosis_Include_Setup_Battery()
        {
            InitializeComponent();
            bcC_UC_ChargingMain_Unit_kiosk1.setSelectListener(this);

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
                mLabel_SocketAdd[i].Text = (i+1)+"\n번 소켓";
                mLabel_SocketAdd[i].TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            }
        }

        public void onSelectChanged(int indexArray)
        {
            if(mSelected_Socket[indexArray] == false)
            {
                mSelected_Socket[indexArray] = true;
            }else
            {
                mSelected_Socket[indexArray] = false;
            }

            flowLayoutPanel1.Controls.Clear();
            for(int i = 0; i < mSelected_Socket.Length; i++)
            {
                if (mSelected_Socket[i] == true)
                {
                    flowLayoutPanel1.Controls.Add(mLabel_SocketAdd[i]);
                }
            }


            //if (bcC_UC_ChargingMain_Unit_kiosk1.State_Kiosk[indexArray] == CSocketState.BLANK)
            //{
            //    bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(indexArray, CSocketState.NOTIFY_ALARM);
            //}
            //else
            //{
            //    bcC_UC_ChargingMain_Unit_kiosk1.setKioskState(indexArray, CSocketState.BLANK);
            //}

        }
    }
}

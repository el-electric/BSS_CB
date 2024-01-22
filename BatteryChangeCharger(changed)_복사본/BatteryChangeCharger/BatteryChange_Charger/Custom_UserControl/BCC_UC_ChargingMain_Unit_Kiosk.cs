using BatteryChangeCharger.Applications;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.IOBoard.Packet;
using BatteryChangeCharger.BatteryChange_Charger.SerialPorts.NFC_Board;
using BatteryChangeCharger.ChargerVariable;
using BatteryChangeCharger.Interface_Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    public partial class BCC_UC_ChargingMain_Unit_kiosk : UserControl
    {
        protected readonly static Color[] COLORS = { Color.LightGray, Color.DarkGray, Color.Lime, Color.DarkGreen, Color.DarkRed, Color.Red, Color.Black };
        protected int[] mState = {CSocketState.BLANK, CSocketState.BLANK, CSocketState.BLANK, CSocketState.BLANK,
        CSocketState.BLANK, CSocketState.BLANK, CSocketState.BLANK, CSocketState.BLANK};
        //public const int BLANK = 0;
        //public const int WAIT = 1;
        //public const int CHARGING = 2;
        //public const int CHARGING_COMPLETE = 3;
        //public const int CHARGING_FAIL = 4;
        //public const int NOTIFY_ALARM = 5;
        public int[] State_Kiosk
        {
            get { return mState; }
        }

        bool bIsStart_Timer = false;

        const int P1920_COLUMN_01 = 42;
        const int P1920_COLUMN_02 = 136;
        const int P1920_COLUMN_03 = 229;

        const int P1920_ROW_01 = 20;
        const int P1920_ROW_02 = 177;
        const int P1920_ROW_03 = 325;


        const int P768_COLUMN_01 = P1920_COLUMN_01 - 7;
        const int P768_COLUMN_02 = P1920_COLUMN_02 - 19;
        const int P768_COLUMN_03 = P1920_COLUMN_03 - 33;

        const int P768_ROW_01 = P1920_ROW_01 + 3;
        const int P768_ROW_02 = P1920_ROW_02 + 10;
        const int P768_ROW_03 = P1920_ROW_03 + 24;

        public BCC_UC_ChargingMain_Unit_kiosk()
        {
            InitializeComponent();

            mLabel_Kiosk_State[0] = btn_ch1;
            mLabel_Kiosk_State[1] = btn_ch2;
            mLabel_Kiosk_State[2] = btn_ch3;
            mLabel_Kiosk_State[3] = btn_ch4;
            mLabel_Kiosk_State[4] = btn_ch5;
            mLabel_Kiosk_State[5] = btn_ch6;
            mLabel_Kiosk_State[6] = btn_ch7;
            mLabel_Kiosk_State[7] = btn_ch8;

            for(int i = 0; i < mLabel_Kiosk_State.Length; i++)
            {
                mLabel_Kiosk_State[i].Click += onClickState;
            }

            pictureBox_socket_01.Parent = pictureBox1;
            pictureBox_socket_02.Parent = pictureBox1;
            pictureBox_socket_03.Parent = pictureBox1;
            pictureBox_socket_04.Parent = pictureBox1;
            pictureBox_socket_05.Parent = pictureBox1;
            pictureBox_socket_06.Parent = pictureBox1;
            pictureBox_socket_07.Parent = pictureBox1;
            pictureBox_socket_08.Parent = pictureBox1;



            int column_01 = 0;
            int column_02 = 0;
            int column_03 = 0;

            int row_01 = 0;
            int row_02 = 0;
            int row_03 = 0;
            if (Screen.PrimaryScreen.Bounds.Size.Width >= 1900)
            {
                column_01 = P1920_COLUMN_01;
                column_02 = P1920_COLUMN_02;
                column_03 = P1920_COLUMN_03;

                row_01 = P1920_ROW_01; 
                row_02 = P1920_ROW_02; 
                row_03 = P1920_ROW_03;
            }
            else
            {

                column_01 = P768_COLUMN_01;
                column_02 = P768_COLUMN_02;
                column_03 = P768_COLUMN_03;

                row_01 = P768_ROW_01;
                row_02 = P768_ROW_02;
                row_03 = P768_ROW_03;
            }


            pictureBox_socket_01.Location = new Point(column_01, row_01);
            pictureBox_socket_02.Location = new Point(column_03, row_01);

            pictureBox_socket_03.Location = new Point(column_01, row_02);
            pictureBox_socket_04.Location = new Point(column_02, row_02);
            pictureBox_socket_05.Location = new Point(column_03, row_02);

            pictureBox_socket_06.Location = new Point(column_01, row_03);
            pictureBox_socket_07.Location = new Point(column_02, row_03);
            pictureBox_socket_08.Location = new Point(column_03, row_03);

            pictureBox_socket_01.Visible = false;
            pictureBox_socket_02.Visible = false;
                                 
            pictureBox_socket_03.Visible = false;
            pictureBox_socket_04.Visible = false;
            pictureBox_socket_05.Visible = false;
                                 
            pictureBox_socket_06.Visible = false;
            pictureBox_socket_07.Visible = false;
            pictureBox_socket_08.Visible = false;

            mPb_Kiosk_State[0] = pictureBox_socket_01;
            mPb_Kiosk_State[1] = pictureBox_socket_02;
            mPb_Kiosk_State[2] = pictureBox_socket_03;
            mPb_Kiosk_State[3] = pictureBox_socket_04;
            mPb_Kiosk_State[4] = pictureBox_socket_05;
            mPb_Kiosk_State[5] = pictureBox_socket_06;
            mPb_Kiosk_State[6] = pictureBox_socket_07;
            mPb_Kiosk_State[7] = pictureBox_socket_08;

            
        }

        public void updateSlot_By_Packet()
        {
            IOBoard_Packet_z1_Receive packet_zi = MyApplication.getInstance().SerialPort_IOBoard.getManager_Send().mManager_Packet.mPacket_z1_Receive;

            NFCBoard_CommManager_Packet[] packets_nfc = MyApplication.getInstance().SerialPort_NFCBoard.getManager_Send().mPackets;

            
            for(int i = 0; i < packets_nfc.Length; i++)
            {
               /* if(packet_zi.isBatteryExist(i+1))
                {
                    if(packets_nfc[i].mPacket_c1_Receive.Output_Voltage_S10 > 0 && packets_nfc[i].mPacket_c1_Receive.Output_Current_S10 > 0)
                        setKioskState(i, CSocketState.CHARGING);
                    else
                        setKioskState(i, CSocketState.CHARGING_COMPLETE);
                }
                else
                {
                    setKioskState(i, CSocketState.BLANK);
                }*/
                
            }
        }



        public void setSlot_Soc(int index, int soc)
        {
            string socText = "";
            if (soc >= 100)
                socText = soc + "%";
            else
                socText = " " + soc + "%";

            switch (index)
            {
                case 1:
                    if(soc < 0)
                    {
                        // soc_ch1.Visible = false;
                        bProgressBar_ch1.Visible = false;
                    }
                    else
                    {
                       // soc_ch1.Visible = true;
                         bProgressBar_ch1.Visible = true;
                        // soc_ch1.Text = socText;
                        bProgressBar_ch1.Value = soc;
                    }
                    break;
                case 2:
                    if (soc < 0)
                    {
                        soc_ch2.Visible = false;
                        bProgressBar_ch2.Visible = false;
                    }
                    else
                    {
                        soc_ch2.Visible = true;
                        bProgressBar_ch2.Visible = true;
                        soc_ch2.Text = socText;
                        bProgressBar_ch2.Value = soc;
                    }
                    break;
                case 3:
                    if (soc < 0)
                    {
                        soc_ch3.Visible = false;
                        bProgressBar_ch3.Visible = false;
                    }
                    else
                    {
                        soc_ch3.Visible = true;
                        bProgressBar_ch3.Visible = true;
                        soc_ch3.Text = socText;
                        bProgressBar_ch3.Value = soc;
                    }
                    break;
                case 4:
                    if (soc < 0)
                    {
                        soc_ch4.Visible = false;
                        bProgressBar_ch4.Visible = false;
                    }
                    else
                    {
                        soc_ch4.Visible = true;
                        bProgressBar_ch4.Visible = true;
                        soc_ch4.Text = socText;
                        bProgressBar_ch4.Value = soc;
                    }
                    break;
                case 5:
                    if (soc < 0)
                    {
                        soc_ch5.Visible = false;
                        bProgressBar_ch5.Visible = false;
                    }
                    else
                    {
                        soc_ch5.Visible = true;
                        bProgressBar_ch5.Visible = true;
                        soc_ch5.Text = socText;
                        bProgressBar_ch5.Value = soc;
                    }
                    break;
                case 6:
                    if (soc < 0)
                    {
                        soc_ch6.Visible = false;
                        bProgressBar_ch6.Visible = false;
                    }
                    else
                    {
                        soc_ch6.Visible = true;
                        bProgressBar_ch6.Visible = true;
                        soc_ch6.Text = socText;
                        bProgressBar_ch6.Value = soc;
                    }
                    break;
                case 7:
                    if (soc < 0)
                    {
                        soc_ch7.Visible = false;
                        bProgressBar_ch7.Visible = false;
                    }
                    else
                    {
                        soc_ch7.Visible = true;
                        bProgressBar_ch7.Visible = true;
                        soc_ch7.Text = socText;
                        bProgressBar_ch7.Value = soc;
                    }
                    break;
                case 8:
                    if (soc < 0)
                    {
                        soc_ch8.Visible = false;
                        bProgressBar_ch8.Visible = false;
                    }
                    else
                    {
                        soc_ch8.Visible = true;
                        bProgressBar_ch8.Visible = true;
                        soc_ch8.Text = socText;
                        bProgressBar_ch8.Value = soc;
                    }
                    break;
            }
        }


        public void onClickState(object obj, EventArgs args)
        {
            if (mSelectListener == null) return;

            for (int i = 0; i < mLabel_Kiosk_State.Length; i++)
            {
                if(System.Object.ReferenceEquals(obj, mLabel_Kiosk_State[i]))
                {
                    mSelectListener.onSelectChanged(i);
                }
            }
        }

        protected ISelectListener mSelectListener = null;
        public void setSelectListener(ISelectListener listener)
        {
            mSelectListener = listener;
        }

        public void setKioskState_Alarm(int indexArray1, int indexArray2)
        {
            for(int i = 0; i < 8; i++)
            {
                if (i == indexArray1 || i == indexArray2)
                    setKioskState(i, CSocketState.NOTIFY_ALARM);
                else
                    setKioskState(i, CSocketState.BLANK);
            }
        }

        public void setKioskState_Alarm(int indexArray1) // 슬롯번호
        {
            for (int i = 0; i < 8; i++)
            {
                if (i == indexArray1)
                    setKioskState(i, CSocketState.NOTIFY_ALARM);
                else
                    setKioskState(i, CSocketState.BLANK);
            }
        }

        public void setKioskState_AlarmComplete(int indexArray1)
        {
            for (int i = 0; i < 8; i++)
            {
                if (i == indexArray1)
                    setKioskState(i, CSocketState.NOTIFY_ALARM_COMPLETE);
                else
                    setKioskState(i, CSocketState.BLANK);
            }
        }

        public void setKioskState(int indexArray, int state)  // 슬롯 번호와 notify alam
        {
            if (mState[indexArray] == state)
                return;

            mState[indexArray] = state;

            if (mLabel_Kiosk_State[indexArray].FlatAppearance.BorderColor != COLORS[state])
            {
                mLabel_Kiosk_State[indexArray].FlatAppearance.BorderColor = COLORS[state];
                mLabel_Kiosk_State[indexArray].Text = CSocketState.TEXT_SOCKETSTATE[state];
            }

            if(state != CSocketState.NOTIFY_ALARM && state != CSocketState.NOTIFY_ALARM_COMPLETE
                    //&& state != CSocketState.CHARGING
                    //&& state != CSocketState.CHARGING_COMPLETE
                    )
            {
                mLabel_Kiosk_State[indexArray].FlatAppearance.BorderSize = 4;
                mPb_Kiosk_State[indexArray].Image = null;
                mPb_Kiosk_State[indexArray].Visible = false;
            }
            else
            {
                mPb_Kiosk_State[indexArray].Visible = true;
            }


            bool isNeedTimer = false;
            foreach(int socketstate in mState)
            {
                if (socketstate == CSocketState.NOTIFY_ALARM
                    || socketstate == CSocketState.NOTIFY_ALARM_COMPLETE
                    //|| socketstate == CSocketState.CHARGING
                    //|| socketstate == CSocketState.CHARGING_COMPLETE
                    )
                    isNeedTimer = true;
            }

            if(isNeedTimer)
            {
                if (!bIsStart_Timer)
                {
                    timer1.Start();
                    bIsStart_Timer = true;
                }
            }
            else
            {
                if (bIsStart_Timer)
                {
                    timer1.Stop();
                    bIsStart_Timer = false;
                }
            }
            
                
        }

        public void setSlotDoor_State(int slotnum, bool state)
        {
            mPb_Kiosk_State[slotnum].Visible = state;
        }

        protected Button[] mLabel_Kiosk_State = new Button[8];
        protected PictureBox[] mPb_Kiosk_State = new PictureBox[8];

        private void timer1_Tick(object sender, EventArgs e)
        {
            int index = -1;
            Color tempColor = Color.Empty;
            int borderSize = 0;
            Color textColor = Color.Empty;
            for (int i = 0; i < mState.Length; i++)
            {
                if (
                    mState[i] == CSocketState.NOTIFY_ALARM
                    || mState[i] == CSocketState.NOTIFY_ALARM_COMPLETE
                    //|| mState[i] == CSocketState.CHARGING
                    //|| mState[i] == CSocketState.CHARGING_COMPLETE
                    )
                {
                    if (index < 0) index = i;

                    if (tempColor == Color.Empty)
                    {
                        if (mLabel_Kiosk_State[index].FlatAppearance.BorderColor != COLORS[mState[index]])
                        {
                            tempColor = COLORS[mState[i]];
                            borderSize = 5;
                            textColor = Color.Black;
                            mPb_Kiosk_State[index].Image = BatteryChangeCharger.Properties.Resources.PB4EvEnergy_open;

                        }
                        else
                        {
                            mPb_Kiosk_State[index].Image = BatteryChangeCharger.Properties.Resources.PB4EvEnergy_bin;
                            textColor = Color.White;
                            borderSize = 1;
                            tempColor = COLORS[CSocketState.BLANK];
                        }
                    }

                    mLabel_Kiosk_State[i].ForeColor = textColor;

                    mLabel_Kiosk_State[i].FlatAppearance.BorderSize = borderSize;
                    mLabel_Kiosk_State[i].FlatAppearance.BorderColor = tempColor;
                }
            }
        }
    }
}

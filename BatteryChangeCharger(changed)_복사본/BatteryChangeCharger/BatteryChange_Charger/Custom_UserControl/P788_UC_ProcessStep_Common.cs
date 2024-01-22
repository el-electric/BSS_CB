using BatteryChangeCharger.Interface_Common;
using BatteryChangeCharger.Theme;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    public partial class P788_UC_ProcessStep_Common : UserControl
    {
        private const int HEIGHT_ROW = 120;
        private const int WIDTH_ROW = 100;
        private const float FONTSIZE_SELECTED = 20.0f;
        private const float FONTSIZE_NORMAL = 13.0f;
        
        public P788_UC_ProcessStep_Common()
        {
            InitializeComponent();
            random = new Random();
        }

        //public string Title
        //{
        //    set 
        //    { 
        //        label_title.Text = value; 
        //    }
        //}

        //public string Subtitle
        //{
        //    set
        //    {
        //        label_subtitle.Text = value;
        //    }
        //}

        public Panel Panel_Main
        {
            get
            {
                return panel_main;
            }
        }

        private Button currentButton;
        private Random random;
        private int mTempIndex = 0;

        private int mSelectedIndex = 0;

        private Color selectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (mTempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }

            mTempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        public void activateButton(int index)
        {
            if (flowLayoutPanel_Menu.Controls.Count > 0
                && flowLayoutPanel_Menu.Controls.Count > index)
                activateButton(flowLayoutPanel_Menu.Controls[index]);
        }

        private void activateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = selectThemeColor();
                    currentButton = (Button)btnSender;
                    //currentButton.BackColor = color;
                    currentButton.BackColor = Color.Teal;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new Font("맑은 고딕", FONTSIZE_SELECTED, FontStyle.Bold);

                    if (mListener_Selected != null)
                    {
                        for (int i = 0; i < flowLayoutPanel_Menu.Controls.Count; i++)
                        {
                            if (currentButton == (Button)flowLayoutPanel_Menu.Controls[i])
                            {
                                mSelectedIndex = i;
                                mListener_Selected.onSelectChanged(mSelectedIndex);
                            }
                        }
                    }
                }
            }
        }


        private void DisableButton()
        {
            foreach (Control pBtn in flowLayoutPanel_Menu.Controls)
            {
                if (pBtn.GetType() == typeof(Button))
                {
                    pBtn.BackColor = Color.DarkSlateGray;
                    pBtn.ForeColor = Color.DarkGray;
                    pBtn.Font = new Font("맑은 고딕", FONTSIZE_NORMAL, FontStyle.Bold);
                }
            }
        }



        public void addButton(string text)
        {
            Button button = new Button();

            button.FlatAppearance.BorderSize = 0;
            button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button.ForeColor = System.Drawing.Color.Black;
            button.Text = text;
            button.Size = new System.Drawing.Size(WIDTH_ROW, HEIGHT_ROW);
            button.UseVisualStyleBackColor = true;
            flowLayoutPanel_Menu.Controls.Add(button);

            button.Click += btnClicked;
        }

        private void btnClicked(object obj, EventArgs args)
        {
            activateButton(obj);
        }

        protected ISelectListener mListener_Selected = null;
        public void setListener_Selected(ISelectListener listener)
        {
            if (listener != null)
            {
                mListener_Selected = listener;
                foreach (Control pBtn in flowLayoutPanel_Menu.Controls)
                {
                    if (pBtn.GetType() == typeof(Button))
                    {
                        pBtn.Enabled = true;
                    }
                }
            }
            else
            {

                foreach (Control pBtn in flowLayoutPanel_Menu.Controls)
                {
                    if (pBtn.GetType() == typeof(Button))
                    {
                        pBtn.Enabled = false;
                    }
                }
            }
        }
    }




}

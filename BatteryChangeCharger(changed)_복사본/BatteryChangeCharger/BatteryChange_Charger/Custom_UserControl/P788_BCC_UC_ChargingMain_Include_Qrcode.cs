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
    public partial class P788_BCC_UC_ChargingMain_Include_Qrcode : UserControl
    {
        private const string FILENAME_QRCODE = "Temp_QRCode.png";
        public P788_BCC_UC_ChargingMain_Include_Qrcode()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true); 
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true); 
            this.UpdateStyles();
        }


        public void disposeQRcode()
        {
            //if(mTemp_Qrcode != null)
            //{
            //    mTemp_Qrcode.Dispose();
            //    mTemp_Qrcode = null;
            //}
        }

        public void makeQRcode(string url)
        {
            //mTransition = new BunifuAnimatorNS.BunifuTransition();
            //bunifuLabel1.Visible = false;

            ZXing.BarcodeWriter barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = ZXing.BarcodeFormat.QR_CODE;
            barcodeWriter.Options.Width = this.pictureBox_qrcode.Width;
            barcodeWriter.Options.Height = this.pictureBox_qrcode.Height;
            Bitmap image = barcodeWriter.Write(url);
            pictureBox_qrcode.Image = image;
            
            //mTransition.ShowSync(bunifuLabel1, false, BunifuAnimatorNS.Animation.HorizSlide);
        }
    }
}

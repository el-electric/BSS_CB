﻿
namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    partial class P788_BCC_UC_ChargingMain_Include_Wait_Insert_Battery
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_content = new System.Windows.Forms.Label();
            this.panel_kiosk = new System.Windows.Forms.Panel();
            this.circularProgressBar1 = new BatteryChangeCharger.Widgets.CircularProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bcC_UC_ChargingMain_Unit_kiosk1 = new BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl.BCC_UC_ChargingMain_Unit_kiosk();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_kiosk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.Controls.Add(this.label_content, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel_kiosk, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 912);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label_content
            // 
            this.label_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_content.Font = new System.Drawing.Font("맑은 고딕", 26.25F, System.Drawing.FontStyle.Bold);
            this.label_content.ForeColor = System.Drawing.Color.White;
            this.label_content.Location = new System.Drawing.Point(13, 697);
            this.label_content.Name = "label_content";
            this.label_content.Size = new System.Drawing.Size(742, 150);
            this.label_content.TabIndex = 6;
            this.label_content.Text = "화면에 표시된 투입구로 배터리를 반납하여 주십시오.";
            this.label_content.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_kiosk
            // 
            this.panel_kiosk.Controls.Add(this.circularProgressBar1);
            this.panel_kiosk.Controls.Add(this.pictureBox1);
            this.panel_kiosk.Controls.Add(this.label1);
            this.panel_kiosk.Controls.Add(this.bcC_UC_ChargingMain_Unit_kiosk1);
            this.panel_kiosk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_kiosk.Location = new System.Drawing.Point(13, 17);
            this.panel_kiosk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_kiosk.Name = "panel_kiosk";
            this.panel_kiosk.Size = new System.Drawing.Size(742, 678);
            this.panel_kiosk.TabIndex = 5;
            // 
            // circularProgressBar1
            // 
            this.circularProgressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.circularProgressBar1.BarColor1 = System.Drawing.Color.DodgerBlue;
            this.circularProgressBar1.BarColor2 = System.Drawing.Color.DodgerBlue;
            this.circularProgressBar1.BarWidth = 14F;
            this.circularProgressBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold);
            this.circularProgressBar1.ForeColor = System.Drawing.Color.DimGray;
            this.circularProgressBar1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.circularProgressBar1.LineColor = System.Drawing.Color.DimGray;
            this.circularProgressBar1.LineWidth = 1;
            this.circularProgressBar1.Location = new System.Drawing.Point(537, 382);
            this.circularProgressBar1.Maximum = ((long)(100));
            this.circularProgressBar1.MinimumSize = new System.Drawing.Size(100, 100);
            this.circularProgressBar1.Name = "circularProgressBar1";
            this.circularProgressBar1.ProgressShape = BatteryChangeCharger.Widgets.CircularProgressBar._ProgressShape.Flat;
            this.circularProgressBar1.Size = new System.Drawing.Size(130, 130);
            this.circularProgressBar1.TabIndex = 11;
            this.circularProgressBar1.Text = "57";
            this.circularProgressBar1.TextMode = BatteryChangeCharger.Widgets.CircularProgressBar._TextMode.Percentage;
            this.circularProgressBar1.Value = ((long)(57));
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::BatteryChangeCharger.Properties.Resources.PB4_Model11;
            this.pictureBox1.Location = new System.Drawing.Point(513, 189);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(191, 191);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(555, 515);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "배터리 잔량";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bcC_UC_ChargingMain_Unit_kiosk1
            // 
            this.bcC_UC_ChargingMain_Unit_kiosk1.BackColor = System.Drawing.Color.Transparent;
            this.bcC_UC_ChargingMain_Unit_kiosk1.Location = new System.Drawing.Point(17, 0);
            this.bcC_UC_ChargingMain_Unit_kiosk1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bcC_UC_ChargingMain_Unit_kiosk1.Name = "bcC_UC_ChargingMain_Unit_kiosk1";
            this.bcC_UC_ChargingMain_Unit_kiosk1.Size = new System.Drawing.Size(478, 667);
            this.bcC_UC_ChargingMain_Unit_kiosk1.TabIndex = 0;
            // 
            // P788_BCC_UC_ChargingMain_Include_Wait_Insert_Battery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "P788_BCC_UC_ChargingMain_Include_Wait_Insert_Battery";
            this.Size = new System.Drawing.Size(768, 912);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_kiosk.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_kiosk;
        private BCC_UC_ChargingMain_Unit_kiosk bcC_UC_ChargingMain_Unit_kiosk1;
        private System.Windows.Forms.Label label_content;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Widgets.CircularProgressBar circularProgressBar1;
    }
}

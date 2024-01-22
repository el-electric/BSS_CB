
namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    partial class P1024_BCC_UC_ChargingMain_Include_Main
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
            this.panel_kiosk = new System.Windows.Forms.Panel();
            this.label_content = new System.Windows.Forms.Label();
            this.pictureBox_Start = new System.Windows.Forms.PictureBox();
            this.bcC_UC_ChargingMain_Unit_kiosk1 = new BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl.BCC_UC_ChargingMain_Unit_kiosk();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_kiosk.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Start)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.Controls.Add(this.panel_kiosk, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1024, 652);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel_kiosk
            // 
            this.panel_kiosk.BackColor = System.Drawing.Color.Transparent;
            this.panel_kiosk.Controls.Add(this.label_content);
            this.panel_kiosk.Controls.Add(this.pictureBox_Start);
            this.panel_kiosk.Controls.Add(this.bcC_UC_ChargingMain_Unit_kiosk1);
            this.panel_kiosk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_kiosk.Location = new System.Drawing.Point(18, 12);
            this.panel_kiosk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_kiosk.Name = "panel_kiosk";
            this.panel_kiosk.Size = new System.Drawing.Size(988, 628);
            this.panel_kiosk.TabIndex = 5;
            // 
            // label_content
            // 
            this.label_content.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_content.ForeColor = System.Drawing.Color.White;
            this.label_content.Location = new System.Drawing.Point(436, 417);
            this.label_content.Name = "label_content";
            this.label_content.Size = new System.Drawing.Size(549, 207);
            this.label_content.TabIndex = 9;
            this.label_content.Text = "시작 버튼을 눌러주세요.";
            this.label_content.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox_Start
            // 
            this.pictureBox_Start.Image = global::BatteryChangeCharger.Properties.Resources.Btn_s1;
            this.pictureBox_Start.Location = new System.Drawing.Point(572, 220);
            this.pictureBox_Start.Name = "pictureBox_Start";
            this.pictureBox_Start.Size = new System.Drawing.Size(286, 188);
            this.pictureBox_Start.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_Start.TabIndex = 0;
            this.pictureBox_Start.TabStop = false;
            this.pictureBox_Start.Click += new System.EventHandler(this.button_start_Click_2);
            this.pictureBox_Start.MouseEnter += new System.EventHandler(this.pictureBox_Start_MouseEnter);
            this.pictureBox_Start.MouseLeave += new System.EventHandler(this.pictureBox_Start_MouseLeave);
            this.pictureBox_Start.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_Start_MouseUp);
            // 
            // bcC_UC_ChargingMain_Unit_kiosk1
            // 
            this.bcC_UC_ChargingMain_Unit_kiosk1.BackColor = System.Drawing.Color.Transparent;
            this.bcC_UC_ChargingMain_Unit_kiosk1.Location = new System.Drawing.Point(3, 32);
            this.bcC_UC_ChargingMain_Unit_kiosk1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bcC_UC_ChargingMain_Unit_kiosk1.Name = "bcC_UC_ChargingMain_Unit_kiosk1";
            this.bcC_UC_ChargingMain_Unit_kiosk1.Size = new System.Drawing.Size(478, 667);
            this.bcC_UC_ChargingMain_Unit_kiosk1.TabIndex = 1;
            // 
            // P1024_BCC_UC_ChargingMain_Include_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "P1024_BCC_UC_ChargingMain_Include_Main";
            this.Size = new System.Drawing.Size(1024, 652);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_kiosk.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Start)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel_kiosk;
        private System.Windows.Forms.Label label_content;
        private System.Windows.Forms.PictureBox pictureBox_Start;
        private BCC_UC_ChargingMain_Unit_kiosk bcC_UC_ChargingMain_Unit_kiosk1;
    }
}

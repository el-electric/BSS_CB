
namespace BatteryChangeCharger.BatteryChange_Charger.Settings
{
    partial class P788_BCC_UC_Setting_Main
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
            this.button_setupforengineer = new System.Windows.Forms.Button();
            this.button_charging_testmode = new System.Windows.Forms.Button();
            this.button_chargingcontrol_smartcardreader = new System.Windows.Forms.Button();
            this.button_setting_comm = new System.Windows.Forms.Button();
            this.button_setting_station = new System.Windows.Forms.Button();
            this.button_view_charginglist = new System.Windows.Forms.Button();
            this.button_program_finish = new System.Windows.Forms.Button();
            this.button_doorsetting = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_setupforengineer
            // 
            this.button_setupforengineer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_setupforengineer.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setupforengineer.Location = new System.Drawing.Point(504, 358);
            this.button_setupforengineer.Name = "button_setupforengineer";
            this.button_setupforengineer.Size = new System.Drawing.Size(182, 230);
            this.button_setupforengineer.TabIndex = 9;
            this.button_setupforengineer.Text = "장비점검\r\n(설치)\r\n(준비중)";
            this.button_setupforengineer.UseVisualStyleBackColor = true;
            // 
            // button_charging_testmode
            // 
            this.button_charging_testmode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_charging_testmode.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_charging_testmode.Location = new System.Drawing.Point(78, 358);
            this.button_charging_testmode.Name = "button_charging_testmode";
            this.button_charging_testmode.Size = new System.Drawing.Size(182, 230);
            this.button_charging_testmode.TabIndex = 10;
            this.button_charging_testmode.Text = "충전\r\n테스트모드";
            this.button_charging_testmode.UseVisualStyleBackColor = true;
            this.button_charging_testmode.Click += new System.EventHandler(this.button_charging_testmode_Click);
            // 
            // button_chargingcontrol_smartcardreader
            // 
            this.button_chargingcontrol_smartcardreader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_chargingcontrol_smartcardreader.Enabled = false;
            this.button_chargingcontrol_smartcardreader.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_chargingcontrol_smartcardreader.Location = new System.Drawing.Point(291, 358);
            this.button_chargingcontrol_smartcardreader.Name = "button_chargingcontrol_smartcardreader";
            this.button_chargingcontrol_smartcardreader.Size = new System.Drawing.Size(182, 230);
            this.button_chargingcontrol_smartcardreader.TabIndex = 11;
            this.button_chargingcontrol_smartcardreader.Text = "충전제어(Manual)\r\n-\r\nSmartCardReader\r\n-\r\n(사용안함)";
            this.button_chargingcontrol_smartcardreader.UseVisualStyleBackColor = true;
            this.button_chargingcontrol_smartcardreader.Click += new System.EventHandler(this.button_chargingcontrol_smartcardreader_Click);
            // 
            // button_setting_comm
            // 
            this.button_setting_comm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_setting_comm.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setting_comm.Location = new System.Drawing.Point(291, 97);
            this.button_setting_comm.Name = "button_setting_comm";
            this.button_setting_comm.Size = new System.Drawing.Size(182, 230);
            this.button_setting_comm.TabIndex = 11;
            this.button_setting_comm.Text = "통신 및 장치설정";
            this.button_setting_comm.UseVisualStyleBackColor = true;
            this.button_setting_comm.Click += new System.EventHandler(this.button_setting_comm_Click);
            // 
            // button_setting_station
            // 
            this.button_setting_station.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_setting_station.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_setting_station.Location = new System.Drawing.Point(78, 97);
            this.button_setting_station.Name = "button_setting_station";
            this.button_setting_station.Size = new System.Drawing.Size(182, 230);
            this.button_setting_station.TabIndex = 10;
            this.button_setting_station.Text = "스테이션 구성\r\n(준비중)";
            this.button_setting_station.UseVisualStyleBackColor = true;
            // 
            // button_view_charginglist
            // 
            this.button_view_charginglist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_view_charginglist.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_view_charginglist.Location = new System.Drawing.Point(504, 97);
            this.button_view_charginglist.Name = "button_view_charginglist";
            this.button_view_charginglist.Size = new System.Drawing.Size(182, 230);
            this.button_view_charginglist.TabIndex = 9;
            this.button_view_charginglist.Text = "충전내역 확인\r\n(준비중)";
            this.button_view_charginglist.UseVisualStyleBackColor = true;
            // 
            // button_program_finish
            // 
            this.button_program_finish.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_program_finish.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_program_finish.Location = new System.Drawing.Point(291, 619);
            this.button_program_finish.Name = "button_program_finish";
            this.button_program_finish.Size = new System.Drawing.Size(182, 230);
            this.button_program_finish.TabIndex = 11;
            this.button_program_finish.Text = "프로그램 종료/재시작";
            this.button_program_finish.UseVisualStyleBackColor = true;
            this.button_program_finish.Click += new System.EventHandler(this.button_program_finish_Click);
            // 
            // button_doorsetting
            // 
            this.button_doorsetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_doorsetting.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button_doorsetting.Location = new System.Drawing.Point(78, 619);
            this.button_doorsetting.Name = "button_doorsetting";
            this.button_doorsetting.Size = new System.Drawing.Size(182, 230);
            this.button_doorsetting.TabIndex = 10;
            this.button_doorsetting.Text = "도어 관련 설정";
            this.button_doorsetting.UseVisualStyleBackColor = true;
            this.button_doorsetting.Click += new System.EventHandler(this.button_testmode_door_Click);
            // 
            // button3
            // 
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button3.Location = new System.Drawing.Point(504, 619);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(182, 230);
            this.button3.TabIndex = 9;
            this.button3.Text = " ";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 7;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.Controls.Add(this.button_setting_station, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button3, 5, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_setting_comm, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_program_finish, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_doorsetting, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.button_view_charginglist, 5, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_charging_testmode, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_chargingcontrol_smartcardreader, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_setupforengineer, 5, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 950);
            this.tableLayoutPanel1.TabIndex = 12;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // P788_BCC_UC_Setting_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "P788_BCC_UC_Setting_Main";
            this.Size = new System.Drawing.Size(768, 950);
            this.Load += new System.EventHandler(this.P788_BCC_UC_Setting_Main_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_setupforengineer;
        private System.Windows.Forms.Button button_charging_testmode;
        private System.Windows.Forms.Button button_chargingcontrol_smartcardreader;
        private System.Windows.Forms.Button button_setting_comm;
        private System.Windows.Forms.Button button_setting_station;
        private System.Windows.Forms.Button button_view_charginglist;
        private System.Windows.Forms.Button button_program_finish;
        private System.Windows.Forms.Button button_doorsetting;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}

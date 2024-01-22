
namespace BatteryChangeCharger.BatteryChange_Charger.TestMode_Charging
{
    partial class P788_BCC_UC_TestMode_Charging
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
            this.components = new System.ComponentModel.Container();
            this.bunifuGroupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btn_main_apply = new System.Windows.Forms.Button();
            this.btn_main_current = new System.Windows.Forms.Button();
            this.btn_main_voltage = new System.Windows.Forms.Button();
            this.bunifuGroupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_main_door_close = new System.Windows.Forms.Button();
            this.btn_main_door_open = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_update = new System.Windows.Forms.Button();
            this.bunifuGroupBox1.SuspendLayout();
            this.bunifuGroupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // bunifuGroupBox1
            // 
            this.bunifuGroupBox1.Controls.Add(this.comboBox1);
            this.bunifuGroupBox1.Controls.Add(this.label1);
            this.bunifuGroupBox1.Controls.Add(this.label16);
            this.bunifuGroupBox1.Controls.Add(this.button_update);
            this.bunifuGroupBox1.Controls.Add(this.btn_main_apply);
            this.bunifuGroupBox1.Controls.Add(this.btn_main_current);
            this.bunifuGroupBox1.Controls.Add(this.btn_main_voltage);
            this.bunifuGroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bunifuGroupBox1.Location = new System.Drawing.Point(3, 3);
            this.bunifuGroupBox1.Name = "bunifuGroupBox1";
            this.bunifuGroupBox1.Size = new System.Drawing.Size(542, 64);
            this.bunifuGroupBox1.TabIndex = 4;
            this.bunifuGroupBox1.TabStop = false;
            this.bunifuGroupBox1.Text = "충전전력변경";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(8, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 38);
            this.comboBox1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.label1.Location = new System.Drawing.Point(231, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 44);
            this.label1.TabIndex = 13;
            this.label1.Text = "V";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.label16.Location = new System.Drawing.Point(337, 15);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 44);
            this.label16.TabIndex = 13;
            this.label16.Text = "A";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_main_apply
            // 
            this.btn_main_apply.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_main_apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_main_apply.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_main_apply.ForeColor = System.Drawing.Color.White;
            this.btn_main_apply.Location = new System.Drawing.Point(362, 16);
            this.btn_main_apply.Name = "btn_main_apply";
            this.btn_main_apply.Size = new System.Drawing.Size(79, 44);
            this.btn_main_apply.TabIndex = 3;
            this.btn_main_apply.Text = "적  용";
            this.btn_main_apply.UseVisualStyleBackColor = false;
            // 
            // btn_main_current
            // 
            this.btn_main_current.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_main_current.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_main_current.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_main_current.ForeColor = System.Drawing.Color.White;
            this.btn_main_current.Location = new System.Drawing.Point(256, 15);
            this.btn_main_current.Name = "btn_main_current";
            this.btn_main_current.Size = new System.Drawing.Size(79, 44);
            this.btn_main_current.TabIndex = 3;
            this.btn_main_current.Text = "15";
            this.btn_main_current.UseVisualStyleBackColor = false;
            // 
            // btn_main_voltage
            // 
            this.btn_main_voltage.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_main_voltage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_main_voltage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_main_voltage.ForeColor = System.Drawing.Color.White;
            this.btn_main_voltage.Location = new System.Drawing.Point(151, 15);
            this.btn_main_voltage.Name = "btn_main_voltage";
            this.btn_main_voltage.Size = new System.Drawing.Size(79, 44);
            this.btn_main_voltage.TabIndex = 3;
            this.btn_main_voltage.Text = "57.4";
            this.btn_main_voltage.UseVisualStyleBackColor = false;
            // 
            // bunifuGroupBox2
            // 
            this.bunifuGroupBox2.Controls.Add(this.btn_main_door_close);
            this.bunifuGroupBox2.Controls.Add(this.btn_main_door_open);
            this.bunifuGroupBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bunifuGroupBox2.Location = new System.Drawing.Point(551, 3);
            this.bunifuGroupBox2.Name = "bunifuGroupBox2";
            this.bunifuGroupBox2.Size = new System.Drawing.Size(214, 64);
            this.bunifuGroupBox2.TabIndex = 5;
            this.bunifuGroupBox2.TabStop = false;
            this.bunifuGroupBox2.Text = "전체도어";
            // 
            // btn_main_door_close
            // 
            this.btn_main_door_close.BackColor = System.Drawing.Color.Chocolate;
            this.btn_main_door_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_main_door_close.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_main_door_close.ForeColor = System.Drawing.Color.White;
            this.btn_main_door_close.Location = new System.Drawing.Point(114, 15);
            this.btn_main_door_close.Name = "btn_main_door_close";
            this.btn_main_door_close.Size = new System.Drawing.Size(85, 44);
            this.btn_main_door_close.TabIndex = 3;
            this.btn_main_door_close.Text = "닫기";
            this.btn_main_door_close.UseVisualStyleBackColor = false;
            // 
            // btn_main_door_open
            // 
            this.btn_main_door_open.BackColor = System.Drawing.Color.Chocolate;
            this.btn_main_door_open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_main_door_open.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_main_door_open.ForeColor = System.Drawing.Color.White;
            this.btn_main_door_open.Location = new System.Drawing.Point(15, 15);
            this.btn_main_door_open.Name = "btn_main_door_open";
            this.btn_main_door_open.Size = new System.Drawing.Size(85, 44);
            this.btn_main_door_open.TabIndex = 3;
            this.btn_main_door_open.Text = "열기";
            this.btn_main_door_open.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 73);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(762, 874);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel1.Controls.Add(this.bunifuGroupBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.bunifuGroupBox2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 950);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // button_update
            // 
            this.button_update.BackColor = System.Drawing.Color.OrangeRed;
            this.button_update.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_update.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_update.ForeColor = System.Drawing.Color.White;
            this.button_update.Location = new System.Drawing.Point(457, 15);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(79, 44);
            this.button_update.TabIndex = 3;
            this.button_update.Text = "Update";
            this.button_update.UseVisualStyleBackColor = false;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // P788_BCC_UC_TestMode_Charging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "P788_BCC_UC_TestMode_Charging";
            this.Size = new System.Drawing.Size(768, 950);
            this.bunifuGroupBox1.ResumeLayout(false);
            this.bunifuGroupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox bunifuGroupBox1;
        private System.Windows.Forms.GroupBox bunifuGroupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_main_apply;
        private System.Windows.Forms.Button btn_main_current;
        private System.Windows.Forms.Button btn_main_voltage;
        private System.Windows.Forms.Button btn_main_door_close;
        private System.Windows.Forms.Button btn_main_door_open;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_update;
    }
}


namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    partial class BCC_UC_ChargingMain_Include_Main
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
            this.bcC_UC_ChargingMain_Unit_kiosk1 = new BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl.BCC_UC_ChargingMain_Unit_kiosk();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button_start = new System.Windows.Forms.Button();
            this.label_content = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel_kiosk.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.panel_kiosk, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 700F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1914, 964);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel_kiosk
            // 
            this.panel_kiosk.BackColor = System.Drawing.Color.Transparent;
            this.panel_kiosk.Controls.Add(this.bcC_UC_ChargingMain_Unit_kiosk1);
            this.panel_kiosk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_kiosk.Location = new System.Drawing.Point(303, 134);
            this.panel_kiosk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel_kiosk.Name = "panel_kiosk";
            this.panel_kiosk.Size = new System.Drawing.Size(558, 696);
            this.panel_kiosk.TabIndex = 5;
            // 
            // bcC_UC_ChargingMain_Unit_kiosk1
            // 
            this.bcC_UC_ChargingMain_Unit_kiosk1.BackColor = System.Drawing.Color.Transparent;
            this.bcC_UC_ChargingMain_Unit_kiosk1.Location = new System.Drawing.Point(41, 16);
            this.bcC_UC_ChargingMain_Unit_kiosk1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bcC_UC_ChargingMain_Unit_kiosk1.Name = "bcC_UC_ChargingMain_Unit_kiosk1";
            this.bcC_UC_ChargingMain_Unit_kiosk1.Size = new System.Drawing.Size(478, 667);
            this.bcC_UC_ChargingMain_Unit_kiosk1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button_start, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.label_content, 0, 3);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(867, 135);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 4;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1044, 694);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // button_start
            // 
            this.button_start.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_start.Font = new System.Drawing.Font("맑은 고딕", 27.75F, System.Drawing.FontStyle.Bold);
            this.button_start.Location = new System.Drawing.Point(350, 198);
            this.button_start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(344, 146);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "시    작";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click_1);
            // 
            // label_content
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.label_content, 3);
            this.label_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_content.Font = new System.Drawing.Font("맑은 고딕", 26.25F, System.Drawing.FontStyle.Bold);
            this.label_content.Location = new System.Drawing.Point(3, 543);
            this.label_content.Name = "label_content";
            this.label_content.Size = new System.Drawing.Size(1038, 151);
            this.label_content.TabIndex = 3;
            this.label_content.Text = "시작 버튼을 눌러주세요.";
            this.label_content.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 135);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(294, 694);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // BCC_UC_ChargingMain_Include_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BCC_UC_ChargingMain_Include_Main";
            this.Size = new System.Drawing.Size(1914, 964);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel_kiosk.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_content;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Panel panel_kiosk;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private BCC_UC_ChargingMain_Unit_kiosk bcC_UC_ChargingMain_Unit_kiosk1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}

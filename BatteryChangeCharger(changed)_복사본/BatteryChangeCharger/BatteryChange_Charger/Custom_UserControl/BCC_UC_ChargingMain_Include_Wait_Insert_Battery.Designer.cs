
namespace BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl
{
    partial class BCC_UC_ChargingMain_Include_Wait_Insert_Battery
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_content = new System.Windows.Forms.Label();
            this.panel_kiosk = new System.Windows.Forms.Panel();
            this.bcC_UC_ChargingMain_Unit_kiosk1 = new BatteryChangeCharger.BatteryChange_Charger.Custom_UserControl.BCC_UC_ChargingMain_Unit_kiosk();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel_kiosk.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel_kiosk, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 700F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1914, 964);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label_content);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(867, 134);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1044, 696);
            this.panel1.TabIndex = 4;
            // 
            // label_content
            // 
            this.label_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_content.Font = new System.Drawing.Font("맑은 고딕", 26.25F, System.Drawing.FontStyle.Bold);
            this.label_content.Location = new System.Drawing.Point(0, 0);
            this.label_content.Name = "label_content";
            this.label_content.Size = new System.Drawing.Size(1044, 696);
            this.label_content.TabIndex = 3;
            this.label_content.Text = "화면에 표시된 투입구로 배터리를 반납하여 주십시오.";
            this.label_content.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_kiosk
            // 
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
            this.bcC_UC_ChargingMain_Unit_kiosk1.Location = new System.Drawing.Point(42, 12);
            this.bcC_UC_ChargingMain_Unit_kiosk1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bcC_UC_ChargingMain_Unit_kiosk1.Name = "bcC_UC_ChargingMain_Unit_kiosk1";
            this.bcC_UC_ChargingMain_Unit_kiosk1.Size = new System.Drawing.Size(478, 667);
            this.bcC_UC_ChargingMain_Unit_kiosk1.TabIndex = 0;
            // 
            // BCC_UC_ChargingMain_Include_Wait_Insert_Battery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "BCC_UC_ChargingMain_Include_Wait_Insert_Battery";
            this.Size = new System.Drawing.Size(1914, 964);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel_kiosk.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_content;
        private System.Windows.Forms.Panel panel_kiosk;
        private BCC_UC_ChargingMain_Unit_kiosk bcC_UC_ChargingMain_Unit_kiosk1;
    }
}

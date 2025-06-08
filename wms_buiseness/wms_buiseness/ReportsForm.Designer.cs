namespace wms_buiseness
{
    partial class ReportsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbReportType = new System.Windows.Forms.ComboBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.btnPrintReceipt = new System.Windows.Forms.Button();
            this.lblReportTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.SuspendLayout();
            // 
            // cbReportType
            // 
            this.cbReportType.FormattingEnabled = true;
            this.cbReportType.Location = new System.Drawing.Point(12, 57);
            this.cbReportType.Name = "cbReportType";
            this.cbReportType.Size = new System.Drawing.Size(121, 21);
            this.cbReportType.TabIndex = 0;
            this.cbReportType.SelectedIndexChanged += new System.EventHandler(this.cbReportType_SelectedIndexChanged);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(12, 104);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 1;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.dtpFrom_ValueChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(12, 149);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 2;
            this.dtpTo.ValueChanged += new System.EventHandler(this.dtpTo_ValueChanged);
            // 
            // dgvReport
            // 
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Location = new System.Drawing.Point(229, 57);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.RowHeadersWidth = 51;
            this.dgvReport.Size = new System.Drawing.Size(513, 235);
            this.dgvReport.TabIndex = 3;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(12, 223);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(154, 32);
            this.btnGenerate.TabIndex = 4;
            this.btnGenerate.Text = "Сформировать отчет";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click_1);
            // 
            // btnPrintReceipt
            // 
            this.btnPrintReceipt.Location = new System.Drawing.Point(12, 261);
            this.btnPrintReceipt.Name = "btnPrintReceipt";
            this.btnPrintReceipt.Size = new System.Drawing.Size(154, 31);
            this.btnPrintReceipt.TabIndex = 5;
            this.btnPrintReceipt.Text = "Печать чека";
            this.btnPrintReceipt.UseVisualStyleBackColor = true;
            this.btnPrintReceipt.Click += new System.EventHandler(this.btnPrintReceipt_Click);
            // 
            // lblReportTitle
            // 
            this.lblReportTitle.AutoSize = true;
            this.lblReportTitle.Location = new System.Drawing.Point(9, 192);
            this.lblReportTitle.Name = "lblReportTitle";
            this.lblReportTitle.Size = new System.Drawing.Size(68, 15);
            this.lblReportTitle.TabIndex = 6;
            this.lblReportTitle.Text = "Заголовок";
            this.lblReportTitle.Click += new System.EventHandler(this.lblReportTitle_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Тип отчета";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Дата от:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "Дата к:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(333, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Система чеков";
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 317);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblReportTitle);
            this.Controls.Add(this.btnPrintReceipt);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.dgvReport);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cbReportType);
            this.Name = "ReportsForm";
            this.Text = "ReportsForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbReportType;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DataGridView dgvReport;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.Button btnPrintReceipt;
        private System.Windows.Forms.Label lblReportTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
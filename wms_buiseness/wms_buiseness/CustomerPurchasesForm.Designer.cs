namespace wms_buiseness
{
    partial class CustomerPurchasesForm
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
            this.dgvPurchases = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPurchases
            // 
            this.dgvPurchases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPurchases.Location = new System.Drawing.Point(15, 51);
            this.dgvPurchases.Name = "dgvPurchases";
            this.dgvPurchases.RowHeadersWidth = 51;
            this.dgvPurchases.Size = new System.Drawing.Size(444, 387);
            this.dgvPurchases.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "История покупок:";
            // 
            // CustomerPurchasesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPurchases);
            this.Name = "CustomerPurchasesForm";
            this.Text = "CustomerPurchasesForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchases)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPurchases;
        private System.Windows.Forms.Label label1;
    }
}
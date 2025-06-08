namespace wms_buiseness
{
    partial class SalesForm
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
            this.cbCustomer = new System.Windows.Forms.ComboBox();
            this.cbProducts = new System.Windows.Forms.ComboBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.dtpSaleDate = new System.Windows.Forms.DateTimePicker();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnAddProduct = new System.Windows.Forms.Button();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnCompleteSale = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCustomer
            // 
            this.cbCustomer.FormattingEnabled = true;
            this.cbCustomer.Location = new System.Drawing.Point(14, 192);
            this.cbCustomer.Name = "cbCustomer";
            this.cbCustomer.Size = new System.Drawing.Size(121, 21);
            this.cbCustomer.TabIndex = 0;
            // 
            // cbProducts
            // 
            this.cbProducts.FormattingEnabled = true;
            this.cbProducts.Location = new System.Drawing.Point(14, 232);
            this.cbProducts.Name = "cbProducts";
            this.cbProducts.Size = new System.Drawing.Size(121, 21);
            this.cbProducts.TabIndex = 1;
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(15, 280);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(120, 20);
            this.numQuantity.TabIndex = 2;
            // 
            // dgvItems
            // 
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Location = new System.Drawing.Point(179, 38);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersWidth = 51;
            this.dgvItems.Size = new System.Drawing.Size(588, 272);
            this.dgvItems.TabIndex = 3;
            // 
            // dtpSaleDate
            // 
            this.dtpSaleDate.Location = new System.Drawing.Point(15, 329);
            this.dtpSaleDate.Name = "dtpSaleDate";
            this.dtpSaleDate.Size = new System.Drawing.Size(200, 20);
            this.dtpSaleDate.TabIndex = 4;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Location = new System.Drawing.Point(116, 50);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(41, 15);
            this.lblSubtotal.TabIndex = 5;
            this.lblSubtotal.Text = "label1";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Location = new System.Drawing.Point(116, 71);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(41, 15);
            this.lblDiscount.TabIndex = 6;
            this.lblDiscount.Text = "label1";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(116, 95);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(41, 15);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "label1";
            // 
            // btnAddProduct
            // 
            this.btnAddProduct.Location = new System.Drawing.Point(15, 362);
            this.btnAddProduct.Name = "btnAddProduct";
            this.btnAddProduct.Size = new System.Drawing.Size(121, 23);
            this.btnAddProduct.TabIndex = 8;
            this.btnAddProduct.Text = "Добавить товар";
            this.btnAddProduct.UseVisualStyleBackColor = true;
            this.btnAddProduct.Click += new System.EventHandler(this.btnAddProduct_Click);
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.Location = new System.Drawing.Point(15, 391);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(121, 23);
            this.btnRemoveItem.TabIndex = 9;
            this.btnRemoveItem.Text = "Удалить товар";
            this.btnRemoveItem.UseVisualStyleBackColor = true;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnCompleteSale
            // 
            this.btnCompleteSale.Location = new System.Drawing.Point(15, 420);
            this.btnCompleteSale.Name = "btnCompleteSale";
            this.btnCompleteSale.Size = new System.Drawing.Size(142, 26);
            this.btnCompleteSale.TabIndex = 10;
            this.btnCompleteSale.Text = "Завершить продажу";
            this.btnCompleteSale.UseVisualStyleBackColor = true;
            this.btnCompleteSale.Click += new System.EventHandler(this.btnCompleteSale_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 15);
            this.label1.TabIndex = 11;
            this.label1.Text = "Выберите заказчика:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 216);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Выберите продукт:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "Выберите количество:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Дата продажи:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 15);
            this.label5.TabIndex = 15;
            this.label5.Text = "Процент скидки:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 15);
            this.label6.TabIndex = 16;
            this.label6.Text = "Сумма продажи:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 95);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "Сумма к оплате:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(348, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 15);
            this.label8.TabIndex = 18;
            this.label8.Text = "Продажи";
            // 
            // SalesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCompleteSale);
            this.Controls.Add(this.btnRemoveItem);
            this.Controls.Add(this.btnAddProduct);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblDiscount);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.dtpSaleDate);
            this.Controls.Add(this.dgvItems);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.cbProducts);
            this.Controls.Add(this.cbCustomer);
            this.Name = "SalesForm";
            this.Text = "SalesForm";
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCustomer;
        private System.Windows.Forms.ComboBox cbProducts;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.DateTimePicker dtpSaleDate;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnAddProduct;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnCompleteSale;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}
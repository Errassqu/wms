using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wms_buiseness
{
    public partial class ReceiptViewForm : Form
    {
        public ReceiptViewForm(string receiptText)
        {
            InitializeComponent();
            txtReceipt.Text = receiptText;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (s, ev) =>
            {
                ev.Graphics.DrawString(txtReceipt.Text,
                    new Font("Courier New", 10),
                    Brushes.Black,
                    ev.MarginBounds);
            };

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = pd;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                pd.Print();
            }
        }
    }
}

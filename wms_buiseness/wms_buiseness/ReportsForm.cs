using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wms_buiseness
{
    public partial class ReportsForm : Form
    {
        private int _userId;
        private string _role;
        public ReportsForm(int userId, string role)
        {
            InitializeComponent();
            _userId = userId;
            _role = role;

            dtpFrom.Value = DateTime.Now.AddDays(-7);
            dtpTo.Value = DateTime.Now;
            LoadReportTypes();
        }
        private void LoadReportTypes()
        {
            cbReportType.Items.AddRange(new object[] {
            "Чеки по дате",
            "Товары по категориям",
            "Продажи по клиентам",
            "Остатки товаров"
        });
            cbReportType.SelectedIndex = 0;
        }
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            switch (cbReportType.SelectedIndex)
            {
                case 0: GenerateReceiptsReport(); break;
                case 1: GenerateCategoryReport(); break;
                case 2: GenerateCustomerSalesReport(); break;
                case 3: GenerateStockReport(); break;
            }
        }
        private void GenerateReceiptsReport()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        @"SELECT 
                        s.SaleID,
                        FORMAT(s.SaleDate, 'dd.MM.yyyy HH:mm') AS SaleDate,
                        c.Name AS Customer,
                        u.Username AS Cashier,
                        s.Total AS Amount
                      FROM Sales s
                      LEFT JOIN Customers c ON s.CustomerID = c.CustomerID
                      JOIN Users u ON s.UserID = u.UserID
                      WHERE s.SaleDate BETWEEN @from AND @to
                      ORDER BY s.SaleDate DESC", conn);

                    cmd.Parameters.AddWithValue("@from", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@to", dtpTo.Value.Date.AddDays(1));

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvReport.DataSource = table;
                    lblReportTitle.Text = $"Чеки за период: {dtpFrom.Value:dd.MM.yyyy} - {dtpTo.Value:dd.MM.yyyy}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка генерации отчета: {ex.Message}");
            }
        }

        private void btnPrintReceipt_Click(object sender, EventArgs e)
        {
            if (dgvReport.SelectedRows.Count == 0 || cbReportType.SelectedIndex != 0) return;

            var saleId = (int)dgvReport.SelectedRows[0].Cells["SaleID"].Value;
            PrintReceipt(saleId);
        }
        private void PrintReceipt(int saleId)
        {
            try
            {
                var receipt = new StringBuilder();

                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Заголовок чека
                    var cmd = new SqlCommand(
                        @"SELECT 
                        s.SaleDate, 
                        c.Name AS Customer,
                        u.Username AS Cashier,
                        s.Total
                      FROM Sales s
                      LEFT JOIN Customers c ON s.CustomerID = c.CustomerID
                      JOIN Users u ON s.UserID = u.UserID
                      WHERE s.SaleID = @saleId", conn);
                    cmd.Parameters.AddWithValue("@saleId", saleId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            receipt.AppendLine("=== ЧЕК ===");
                            receipt.AppendLine($"Дата: {reader["SaleDate"]}");
                            receipt.AppendLine($"Кассир: {reader["Cashier"]}");
                            receipt.AppendLine($"Клиент: {reader["Customer"] ?? "Не указан"}");
                            receipt.AppendLine("------------------------");
                        }
                    }

                    // Товары в чеке
                    cmd = new SqlCommand(
                        @"SELECT 
                        p.Name, 
                        si.Quantity, 
                        si.UnitPrice, 
                        (si.Quantity * si.UnitPrice) AS Total
                      FROM SaleItems si
                      JOIN Products p ON si.ProductID = p.ProductID
                      WHERE si.SaleID = @saleId", conn);
                    cmd.Parameters.AddWithValue("@saleId", saleId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            receipt.AppendLine($"{reader["Name"]}");
                            receipt.AppendLine($"{reader["Quantity"]} x {reader["UnitPrice"]:C2} = {reader["Total"]:C2}");
                        }
                    }

                    receipt.AppendLine("------------------------");
                    receipt.AppendLine($"ИТОГО: {cmd.Parameters["@total"].Value:C2}");
                    receipt.AppendLine("=== Спасибо за покупку! ===");
                }

                // Здесь должна быть логика печати, но для примера выведем в MessageBox
                var printForm = new ReceiptViewForm(receipt.ToString());
                printForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка печати чека: {ex.Message}");
            }
        }
        private void GenerateCategoryReport()
        {
            // Аналогично, с группировкой по категориям
        }

        private void GenerateCustomerSalesReport()
        {
            // Отчет по продажам клиентов
        }

        private void GenerateStockReport()
        {
            // Отчет по остаткам товаров
        }

        private void btnGenerate_Click_1(object sender, EventArgs e)
        {

        }

        private void lblReportTitle_Click(object sender, EventArgs e)
        {

        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cbReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

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
    public partial class CustomerPurchasesForm : Form
    {
        public CustomerPurchasesForm(int customerId, string customerName)
        {
            InitializeComponent();
            this.Text = $"Покупки клиента: {customerName}";
            LoadPurchases(customerId);
        }
        private void LoadPurchases(int customerId)
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
                        COUNT(si.SaleItemID) AS ItemsCount,
                        s.Total AS Amount,
                        u.Username AS Cashier
                      FROM Sales s
                      JOIN Users u ON s.UserID = u.UserID
                      LEFT JOIN SaleItems si ON s.SaleID = si.SaleID
                      WHERE s.CustomerID = @customerId
                      GROUP BY s.SaleID, s.SaleDate, s.Total, u.Username
                      ORDER BY s.SaleDate DESC", conn);
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvPurchases.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки покупок: {ex.Message}");
            }
        }
    }
}

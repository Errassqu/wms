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
    public partial class CustomersForm : Form
    {
        public CustomersForm()
        {
            InitializeComponent();
            LoadCustomers();
            ConfigureGrid();
        }
        private void ConfigureGrid()
        {
            dgvCustomers.AutoGenerateColumns = false;

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "CustomerID",
                HeaderText = "ID",
                Visible = false
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Имя",
                Width = 150
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Телефон",
                Width = 120
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LoyaltyDiscount",
                HeaderText = "Скидка (%)",
                Width = 80
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TotalPurchases",
                HeaderText = "Всего покупок",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastPurchaseDate",
                HeaderText = "Последняя покупка",
                Width = 120
            });
        }
        private void LoadCustomers()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        @"SELECT 
                        CustomerID, 
                        Name, 
                        Phone, 
                        LoyaltyDiscount,
                        TotalPurchases,
                        FORMAT(LastPurchaseDate, 'dd.MM.yyyy HH:mm') AS LastPurchaseDate
                      FROM Customers", conn);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvCustomers.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new CustomerEditForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCustomers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0) return;

            var customerId = (int)dgvCustomers.SelectedRows[0].Cells["CustomerID"].Value;
            var form = new CustomerEditForm(customerId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCustomers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0) return;

            var customerId = (int)dgvCustomers.SelectedRows[0].Cells["CustomerID"].Value;
            var customerName = dgvCustomers.SelectedRows[0].Cells["Name"].Value.ToString();

            if (MessageBox.Show($"Удалить клиента '{customerName}'?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.ExecuteNonQuery(
                        "DELETE FROM Customers WHERE CustomerID = @id",
                        new SqlParameter("@id", customerId));

                    LoadCustomers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}");
                }
            }
        }

        private void btnViewPurchases_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count == 0) return;

            var customerId = (int)dgvCustomers.SelectedRows[0].Cells["CustomerID"].Value;
            var customerName = dgvCustomers.SelectedRows[0].Cells["Name"].Value.ToString();

            var form = new CustomerPurchasesForm(customerId, customerName);
            form.ShowDialog();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadCustomers();
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT CustomerID, Name, Phone, LoyaltyDiscount, TotalPurchases, " +
                        "FORMAT(LastPurchaseDate, 'dd.MM.yyyy HH:mm') AS LastPurchaseDate " +
                        "FROM Customers WHERE Name LIKE @search OR Phone LIKE @search", conn);

                    cmd.Parameters.AddWithValue("@search", $"%{searchText}%");

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvCustomers.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }
        }
    }
    
}

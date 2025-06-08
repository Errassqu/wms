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
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();
            dgvProducts.AutoGenerateColumns = false;
            LoadProducts();
            ConfigureGrid();
        }
        private void ConfigureGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.Clear();

            // Настраиваем колонки вручную для лучшего контроля
            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductID", // Важно: имя поля в DataSource
                HeaderText = "ID",
                Visible = false // Скрываем, но данные будут доступны
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Название",
                Width = 200
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Category",
                HeaderText = "Категория",
                Width = 150
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Price",
                HeaderText = "Цена",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Остаток",
                Width = 80
            });
        }
        private void LoadProducts()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT ProductID, Name, Category, Price, Quantity FROM Products", conn);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvProducts.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new ProductEditForm(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProducts(); // Обновляем список
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0) return;

            // Получаем ID из привязанных данных, а не из видимых столбцов
            var selectedRow = dgvProducts.SelectedRows[0];
            var productId = (int)((DataRowView)selectedRow.DataBoundItem)["ProductID"];

            var form = new ProductEditForm(productId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadProducts();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                if (dgvProducts.SelectedRows.Count == 0) return;

                    var productId = (int)dgvProducts.SelectedRows[0].Cells["ProductID"].Value;
                    var productName = dgvProducts.SelectedRows[0].Cells["Name"].Value.ToString();

                if (MessageBox.Show($"Удалить товар '{productName}'?", "Подтверждение",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        DatabaseHelper.ExecuteNonQuery(
                            "DELETE FROM Products WHERE ProductID = @id",
                            new SqlParameter("@id", productId));

                        LoadProducts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления: {ex.Message}");
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                LoadProducts();
                return;
            }

            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT ProductID, Name, Category, Price, Quantity FROM Products " +
                        "WHERE Name LIKE @search OR Category LIKE @search", conn);

                    cmd.Parameters.AddWithValue("@search", $"%{searchText}%");

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvProducts.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }
        }
    }
}

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
    public partial class ProductEditForm : Form
    {
        private int? _productId;
        public ProductEditForm(int? productId)
        {
            InitializeComponent();
            _productId = productId;

            if (_productId.HasValue)
            {
                LoadProductData();
                this.Text = "Редактирование товара";
            }
            else
            {
                this.Text = "Добавление товара";
            }
        }
        private void LoadProductData()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT Name, Category, Price, Quantity, Barcode FROM Products " +
                        "WHERE ProductID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", _productId.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["Name"].ToString();
                            txtCategory.Text = reader["Category"].ToString();
                            numPrice.Value = Convert.ToDecimal(reader["Price"]);
                            numQuantity.Value = Convert.ToInt32(reader["Quantity"]);
                            txtBarcode.Text = reader["Barcode"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Введите название товара");
                    return;
                }

                try
                {
                    var parameters = new SqlParameter[]
                    {
                new SqlParameter("@name", txtName.Text.Trim()),
                new SqlParameter("@category", txtCategory.Text.Trim()),
                new SqlParameter("@price", numPrice.Value),
                new SqlParameter("@quantity", (int)numQuantity.Value),
                new SqlParameter("@barcode", txtBarcode.Text.Trim())
                    };

                    if (_productId.HasValue)
                    {
                        // Обновление существующего товара
                        DatabaseHelper.ExecuteNonQuery(
                            "UPDATE Products SET Name = @name, Category = @category, " +
                            "Price = @price, Quantity = @quantity, Barcode = @barcode " +
                            "WHERE ProductID = @id",
                            parameters.Concat(new[] { new SqlParameter("@id", _productId.Value) }).ToArray());
                    }
                    else
                    {
                        // Добавление нового товара
                        DatabaseHelper.ExecuteNonQuery(
                            "INSERT INTO Products (Name, Category, Price, Quantity, Barcode) " +
                            "VALUES (@name, @category, @price, @quantity, @barcode)",
                            parameters);
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения: {ex.Message}");
                }
            }
        }
    }
}

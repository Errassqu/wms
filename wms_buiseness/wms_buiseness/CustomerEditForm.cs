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
    public partial class CustomerEditForm : Form
    {
        private int? _customerId;
        public CustomerEditForm(int? customerId = null)
        {
            InitializeComponent();
            _customerId = customerId;

            if (_customerId.HasValue)
            {
                this.Text = "Редактирование клиента";
                LoadCustomerData();
            }
            else
            {
                this.Text = "Добавление клиента";
            }
        }
        private void LoadCustomerData()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT Name, Phone, LoyaltyDiscount FROM Customers WHERE CustomerID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", _customerId.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtName.Text = reader["Name"].ToString();
                            txtPhone.Text = reader["Phone"].ToString();
                            numDiscount.Value = Convert.ToDecimal(reader["LoyaltyDiscount"]);
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
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите имя клиента");
                return;
            }

            try
            {
                var parameters = new SqlParameter[]
                {
                new SqlParameter("@name", txtName.Text.Trim()),
                new SqlParameter("@phone", txtPhone.Text.Trim()),
                new SqlParameter("@discount", numDiscount.Value)
                };

                if (_customerId.HasValue)
                {
                    // Обновление
                    DatabaseHelper.ExecuteNonQuery(
                        "UPDATE Customers SET Name = @name, Phone = @phone, " +
                        "LoyaltyDiscount = @discount WHERE CustomerID = @id",
                        parameters.Concat(new[] { new SqlParameter("@id", _customerId.Value) }).ToArray());
                }
                else
                {
                    // Добавление
                    DatabaseHelper.ExecuteNonQuery(
                        "INSERT INTO Customers (Name, Phone, LoyaltyDiscount) " +
                        "VALUES (@name, @phone, @discount)",
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

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void CustomerEditForm_Load(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numDiscount_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    
}

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
    public partial class UserEditForm : Form
    {
        private int? _userId;
        public UserEditForm(int? userId)
        {
            InitializeComponent();
            _userId = userId;
            cbRole.Items.AddRange(new[] { "Admin", "User", "Manager" });

            if (_userId.HasValue)
            {
                this.Text = "Редактирование пользователя";
                LoadUserData();
            }
            else
            {
                this.Text = "Добавление пользователя";
            }
        }
        private void LoadUserData()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT Username, Role FROM Users WHERE UserID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", _userId.Value);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtUsername.Text = reader["Username"].ToString();
                            cbRole.SelectedItem = reader["Role"].ToString();
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
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Введите логин пользователя");
                return;
            }

            if (cbRole.SelectedItem == null)
            {
                MessageBox.Show("Выберите роль");
                return;
            }

            if (!_userId.HasValue && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            try
            {
                var parameters = new List<SqlParameter>
            {
                new SqlParameter("@username", txtUsername.Text.Trim()),
                new SqlParameter("@role", cbRole.SelectedItem.ToString())
            };

                string sql;
                if (_userId.HasValue)
                {
                    // Обновление пользователя
                    sql = "UPDATE Users SET Username = @username, Role = @role";

                    // Если указан новый пароль
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        sql += ", PasswordHash = @password";
                        parameters.Add(new SqlParameter("@password", DatabaseHelper.HashPassword(txtPassword.Text)));
                    }

                    sql += " WHERE UserID = @id";
                    parameters.Add(new SqlParameter("@id", _userId.Value));
                }
                else
                {
                    // Добавление нового пользователя
                    sql = "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@username, @password, @role)";
                    parameters.Add(new SqlParameter("@password", DatabaseHelper.HashPassword(txtPassword.Text)));
                }

                DatabaseHelper.ExecuteNonQuery(sql, parameters.ToArray());

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

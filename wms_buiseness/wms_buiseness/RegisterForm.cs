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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Пароли не совпадают");
                return;
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();

                // Проверка существования пользователя
                var checkCmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Users WHERE Username = @username", conn);
                checkCmd.Parameters.AddWithValue("@username", txtUsername.Text);
                int exists = (int)checkCmd.ExecuteScalar();

                if (exists > 0)
                {
                    MessageBox.Show("Пользователь с таким именем уже существует");
                    return;
                }

                // Создание нового пользователя
                var cmd = new SqlCommand(
                    "INSERT INTO Users (Username, PasswordHash, Role, FailedLoginAttempts) " +
                    "VALUES (@username, @password, 'User', 0)", conn);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", DatabaseHelper.HashPassword(txtPassword.Text));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Регистрация успешна!");
                this.Close();
            }
        }
    }
}

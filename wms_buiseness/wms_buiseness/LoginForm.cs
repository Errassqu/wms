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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Проверка блокировки
            if (DatabaseHelper.IsUserLocked(username))
            {
                MessageBox.Show("Ваш аккаунт заблокирован на 24 часа из-за слишком большого количества неудачных попыток входа.");
                return;
            }

            // Проверка учетных данных
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "SELECT UserID, PasswordHash, Role FROM Users WHERE Username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedHash = reader["PasswordHash"].ToString();
                        string inputHash = DatabaseHelper.HashPassword(password);

                        if (storedHash == inputHash)
                        {
                            // Успешный вход
                            LogLoginAttempt(username, true);
                            ResetFailedAttempts(username);

                            // Открываем главную форму
                            var mainForm = new MainForm((int)reader["UserID"], reader["Role"].ToString());
                            mainForm.Show();
                            this.Hide();
                            return;
                        }
                    }
                }

                // Неудачная попытка входа
                LogLoginAttempt(username, false);
                IncrementFailedAttempts(username);
                MessageBox.Show("Неверное имя пользователя или пароль");
            }
        }
        private void LogLoginAttempt(string username, bool isSuccess)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "INSERT INTO LoginLogs (UserID, LoginTime, Success) " +
                    "VALUES ((SELECT UserID FROM Users WHERE Username = @username), @time, @success)", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@time", DateTime.Now);
                cmd.Parameters.AddWithValue("@success", isSuccess);
                cmd.ExecuteNonQuery();
            }
        }
        private void IncrementFailedAttempts(string username)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Users SET FailedLoginAttempts = FailedLoginAttempts + 1 " +
                    "WHERE Username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();

                // Проверяем, нужно ли блокировать
                cmd = new SqlCommand(
                    "SELECT FailedLoginAttempts FROM Users WHERE Username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                int attempts = (int)cmd.ExecuteScalar();

                if (attempts >= 3)
                {
                    cmd = new SqlCommand(
                        "UPDATE Users SET LockedUntil = DATEADD(day, 1, GETDATE()) " +
                        "WHERE Username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        private void ResetFailedAttempts(string username)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(
                    "UPDATE Users SET FailedLoginAttempts = 0, LockedUntil = NULL " +
                    "WHERE Username = @username", conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.ExecuteNonQuery();
            }
        }

        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            var registerForm = new RegisterForm();
            registerForm.ShowDialog();
        }
    }
}

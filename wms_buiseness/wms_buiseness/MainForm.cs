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
    public partial class MainForm : Form
    {
        private int _userId;
        private string _role;
        public MainForm(int userId, string role)
        {
            InitializeComponent();
            _userId = userId;
            _role = role;

            ConfigureMenu();
            LoadDashboard();
        }
        private void ConfigureMenu()
        {
            // Скрываем админские функции для обычных пользователей
            usersToolStripMenuItem.Visible = (_role == "Admin");

            // Устанавливаем заголовок окна
            this.Text = $"WMS System - {_role} Mode";
        }
        private void LoadDashboard()
        {
            lblWelcome.Text = $"Добро пожаловать, {GetUserName(_userId)}!";
            lblRole.Text = $"Ваша роль: {_role}";
            LoadRecentSales();

        }
        private string GetUserName(int userId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT Username FROM Users WHERE UserID = @id", conn);
                cmd.Parameters.AddWithValue("@id", userId);
                return cmd.ExecuteScalar()?.ToString() ?? "User";
            }
        }

        private void LoadRecentSales()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        @"SELECT TOP 5 s.SaleID, s.SaleDate, c.Name AS Customer, s.Total 
                  FROM Sales s
                  LEFT JOIN Customers c ON s.CustomerID = c.CustomerID
                  ORDER BY s.SaleDate DESC", conn);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvRecentSales.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки продаж: {ex.Message}");
            }
        }

        private void reportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ReportsForm(_userId, _role);
            form.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "wms_buisenessDataSet.Sales". При необходимости она может быть перемещена или удалена.
            this.salesTableAdapter.Fill(this.wms_buisenessDataSet.Sales);

        }

        private void customersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new CustomersForm();
            form.ShowDialog();
        }

        private void salesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new SalesForm(_userId);
            form.ShowDialog();
            LoadRecentSales();
        }

        private void productsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new ProductsForm();
            form.ShowDialog();
            LoadRecentSales(); // Обновляем данные после закрытия
        }

        private void usersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new UsersForm();
            form.ShowDialog();
        }

        private void logoutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }
    }
}

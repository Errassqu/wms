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
    public partial class UsersForm : Form
    {
        public UsersForm()
        {
            InitializeComponent();
            LoadUsers();
            ConfigureGrid();
        }
        private void ConfigureGrid()
        {
            dgvUsers.AutoGenerateColumns = false;

            // Настройка столбцов
            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UserID",
                HeaderText = "ID",
                Visible = false
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Username",
                HeaderText = "Логин",
                Width = 150
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Role",
                HeaderText = "Роль",
                Width = 100
            });

            dgvUsers.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "IsLocked",
                HeaderText = "Заблокирован",
                Width = 100
            });

            dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LastLogin",
                HeaderText = "Последний вход",
                Width = 150
            });
        }
        private void LoadUsers()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        @"SELECT 
                        UserID, 
                        Username, 
                        Role, 
                        CASE WHEN LockedUntil > GETDATE() THEN 1 ELSE 0 END AS IsLocked,
                        CONVERT(varchar, LastLoginDate, 104) + ' ' + 
                        CONVERT(varchar, LastLoginDate, 108) AS LastLogin
                      FROM Users", conn);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvUsers.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пользователей: {ex.Message}");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new UserEditForm(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            var userId = (int)dgvUsers.SelectedRows[0].Cells["UserID"].Value;
            var form = new UserEditForm(userId);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            var userId = (int)dgvUsers.SelectedRows[0].Cells["UserID"].Value;
            var username = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString();

            if (MessageBox.Show($"Удалить пользователя '{username}'?", "Подтверждение",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    DatabaseHelper.ExecuteNonQuery(
                        "DELETE FROM Users WHERE UserID = @id",
                        new SqlParameter("@id", userId));

                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}");
                }
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            var userId = (int)dgvUsers.SelectedRows[0].Cells["UserID"].Value;

            try
            {
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE Users SET FailedLoginAttempts = 0, LockedUntil = NULL WHERE UserID = @id",
                    new SqlParameter("@id", userId));

                LoadUsers();
                MessageBox.Show("Пользователь разблокирован");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка разблокировки: {ex.Message}");
            }
        }

        private void btnViewLogs_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;

            var userId = (int)dgvUsers.SelectedRows[0].Cells["UserID"].Value;
            var username = dgvUsers.SelectedRows[0].Cells["Username"].Value.ToString();

            var form = new UserLogsForm(userId, username);
            form.ShowDialog();
        }
    }
}

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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace wms_buiseness
{
    public partial class UserLogsForm : Form
    {
        public UserLogsForm(int userId, string username)
        {
            InitializeComponent();
            this.Text = $"Логи пользователя: {username}";
            LoadLogs(userId);
        }
        private void LoadLogs(int userId)
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        @"SELECT 
                        CONVERT(varchar, LoginTime, 104) + ' ' + 
                        CONVERT(varchar, LoginTime, 108) AS LoginTime,
                        CASE WHEN Success = 1 THEN 'Успешно' ELSE 'Ошибка' END AS Status
                      FROM LoginLogs 
                      WHERE UserID = @userId
                      ORDER BY LoginTime DESC", conn);
                    cmd.Parameters.AddWithValue("@userId", userId);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    dgvLogs.DataSource = table;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки логов: {ex.Message}");
            }
        }
    }
}

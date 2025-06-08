using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace wms_buiseness
{
    internal static class DatabaseHelper
    {
        // Замените эту строку на ваше реальное подключение
        private static string connectionString = "Data Source=LENOVOIPG3;Initial Catalog=wms_buiseness;User ID=user1;Password=1;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Метод для хеширования пароля с использованием SHA256
        public static string HashPassword(string password)
        {
            // Добавляем "соль" для повышения безопасности
            const string salt = "l3n0v01pg3&@sda@#jsanoobs&~326890"; // В реальном проекте используйте уникальную соль для каждого пользователя

            using (var sha256 = SHA256.Create())
            {
                // Комбинируем пароль и соль перед хешированием
                var combined = Encoding.UTF8.GetBytes(password + salt);
                var hashBytes = sha256.ComputeHash(combined);

                // Конвертируем байты в строку HEX
                var builder = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }

        // Проверка, заблокирован ли пользователь
        public static bool IsUserLocked(string username)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT LockedUntil FROM Users WHERE Username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    var result = cmd.ExecuteScalar();
                    if (result == null || result == DBNull.Value)
                        return false;

                    var lockedUntil = (DateTime)result;
                    return lockedUntil > DateTime.Now;
                }
                catch (Exception ex)
                {
                    // В реальном проекте следует добавить логирование ошибок
                    Console.WriteLine($"Error checking user lock: {ex.Message}");
                    return false;
                }
            }
        }

        // Метод для выполнения SQL-запросов без возвращаемого результата
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Метод для выполнения SQL-запросов с возвратом одного значения
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
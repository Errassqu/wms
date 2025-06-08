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
    public partial class SalesForm : Form
    {
        private int _userId;
        private List<SaleItem> _currentItems = new List<SaleItem>();
        private decimal _total = 0;

        public SalesForm(int userId)
        {
            InitializeComponent();
            _userId = userId;
            InitializeForm();
        }
        private void InitializeForm()
        {
            dtpSaleDate.Value = DateTime.Now;
            LoadProducts();
            LoadCustomers();
            UpdateTotal();
        }
        private void LoadProducts()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand(
                        "SELECT ProductID, Name, Price FROM Products WHERE Quantity > 0", conn);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    cbProducts.DataSource = table;
                    cbProducts.DisplayMember = "Name";
                    cbProducts.ValueMember = "ProductID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}");
            }
        }
        private void LoadCustomers()
        {
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT CustomerID, Name FROM Customers", conn);

                    var adapter = new SqlDataAdapter(cmd);
                    var table = new DataTable();
                    adapter.Fill(table);

                    // Добавляем пустую строку для продаж без клиента
                    var emptyRow = table.NewRow();
                    emptyRow["CustomerID"] = DBNull.Value;
                    emptyRow["Name"] = "[Без клиента]";
                    table.Rows.InsertAt(emptyRow, 0);

                    cbCustomer.DataSource = table;
                    cbCustomer.DisplayMember = "Name";
                    cbCustomer.ValueMember = "CustomerID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}");
            }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if (cbProducts.SelectedItem == null || numQuantity.Value <= 0) return;

            var product = (DataRowView)cbProducts.SelectedItem;
            var item = new SaleItem
            {
                ProductID = (int)product["ProductID"],
                ProductName = product["Name"].ToString(),
                Price = Convert.ToDecimal(product["Price"]),
                Quantity = (int)numQuantity.Value
            };

            _currentItems.Add(item);
            RefreshItemsList();
            UpdateTotal();
        }
        private void RefreshItemsList()
        {
            dgvItems.DataSource = null;
            dgvItems.DataSource = _currentItems.Select(x => new
            {
                x.ProductName,
                x.Price,
                x.Quantity,
                Total = x.Price * x.Quantity
            }).ToList();
        }
        private void UpdateTotal()
        {
            _total = _currentItems.Sum(x => x.Price * x.Quantity);

            // Применяем скидку клиента
            decimal discount = 0;
            if (cbCustomer.SelectedIndex > 0)
            {
                var customer = (DataRowView)cbCustomer.SelectedItem;
                discount = Convert.ToDecimal(customer["LoyaltyDiscount"]) / 100 * _total;
            }

            lblSubtotal.Text = _total.ToString("C2");
            lblDiscount.Text = discount.ToString("C2");
            lblTotal.Text = (_total - discount).ToString("C2");
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0) return;

            int index = dgvItems.SelectedRows[0].Index;
            _currentItems.RemoveAt(index);
            RefreshItemsList();
            UpdateTotal();
        }

        private void btnCompleteSale_Click(object sender, EventArgs e)
        {
            if (_currentItems.Count == 0)
            {
                MessageBox.Show("Добавьте товары к продаже");
                return;
            }
            try
            {
                using (var conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // 1. Создаем запись о продаже
                            var saleCmd = new SqlCommand(
                                @"INSERT INTO Sales (CustomerID, UserID, SaleDate, Total) 
                              VALUES (@customerId, @userId, @date, @total);
                              SELECT SCOPE_IDENTITY();", conn, transaction);

                            saleCmd.Parameters.AddWithValue("@userId", _userId);
                            saleCmd.Parameters.AddWithValue("@date", dtpSaleDate.Value);
                            saleCmd.Parameters.AddWithValue("@total", _total);

                            if (cbCustomer.SelectedIndex > 0)
                                saleCmd.Parameters.AddWithValue("@customerId", cbCustomer.SelectedValue);
                            else
                                saleCmd.Parameters.AddWithValue("@customerId", DBNull.Value);

                            int saleId = Convert.ToInt32(saleCmd.ExecuteScalar());

                            // 2. Добавляем товары
                            foreach (var item in _currentItems)
                            {
                                var itemCmd = new SqlCommand(
                                    @"INSERT INTO SaleItems (SaleID, ProductID, Quantity, UnitPrice) 
                                  VALUES (@saleId, @productId, @quantity, @price);
                                  
                                  UPDATE Products SET Quantity = Quantity - @quantity 
                                  WHERE ProductID = @productId;", conn, transaction);

                                itemCmd.Parameters.AddWithValue("@saleId", saleId);
                                itemCmd.Parameters.AddWithValue("@productId", item.ProductID);
                                itemCmd.Parameters.AddWithValue("@quantity", item.Quantity);
                                itemCmd.Parameters.AddWithValue("@price", item.Price);

                                itemCmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Продажа успешно оформлена!");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка оформления продажи: {ex.Message}");
            }
        }
    }
    public class SaleItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
namespace electroShop
{
    public partial class addEditProductForm : Form
    {
        string connectionStr = "Database=electro_shop;Password=1;Username=postgres;Host=localhost";
        public string ProductName { get; set; }
        public addEditProductForm(string productName)
        {
            InitializeComponent();
            this.ProductName = productName;
        }

        private void fillComboBoxes(string table, ComboBox comboBox, string columnName)
        {
            comboBox.DataSource = null;
            comboBox.Items.Clear();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                conn.Open();
                var sql = $"SELECT id, {columnName} FROM {table}";
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new { Id = reader["id"], Name = reader["name"].ToString() };
                        comboBox.Items.Add(item);
                    }
                }

            }
            comboBox.DisplayMember = "Name";
            comboBox.ValueMember = "Id";
            comboBox.SelectedIndex = 0;

        }

        private void LoadProduct()
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                conn.Open();
                var sql = @"SELECT * FROM products WHERE name = @name";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("id", ProductName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtArticle.Text = reader["article"].ToString();
                            txtName.Text = reader["name"].ToString();
                            SelectComboItem(cmbCategory, Convert.ToInt32(reader["category_id"]));
                            SelectComboItem(cmbManufactory, Convert.ToInt32(reader["manufacturer_id"]));
                            SelectComboItem(cmbSupplier, Convert.ToInt32(reader["supplier_id"]));
                            SelectComboItem(cmbType, Convert.ToInt32(reader["type_id"]));
                            numPrice.Value = Convert.ToDecimal(reader["price"]);
                            numQuantity.Value = Convert.ToInt32(reader["stock"]);
                            numDiscount.Value = Convert.ToInt32(reader["discount"]);
                        }
                    }
                }
            }
        }

        private void SelectComboItem(ComboBox comboBox, int id)
        {
            foreach (dynamic item in comboBox.Items)
            {
                if (item.Id.ToString() == id.ToString())
                {
                    comboBox.SelectedItem = item;
                    break;
                }
            }
        }






        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            fillComboBoxes("product_categories", cmbCategory, "name");
            fillComboBoxes("manufacturers", cmbManufactory, "name");
            fillComboBoxes("suppliers", cmbSupplier, "name");
            fillComboBoxes("item_types", cmbType, "name");

            if (ProductName == "")
            {
                this.Text = "Добавление товара";
                txtDescription.Text = "";
                txtName.Text = "";
                numDiscount.Value = 0;
                numPrice.Value = 0;
                numQuantity.Value = 0;
                cmbCategory.SelectedIndex = 0;
                cmbManufactory.SelectedIndex = 0;
                cmbSupplier.SelectedIndex = 0;
                cmbType.SelectedIndex = 0;
            }
            else
            {
                this.Text = "Редактирование товара";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtArticle.Text))
            {
                MessageBox.Show("Пожалуйста заполните артикул и название");
                return;
            }
            if (cmbCategory.SelectedItem == null || cmbManufactory.SelectedItem == null || cmbSupplier == null || cmbType == null)
            {
                MessageBox.Show("Выберете категорию и поставщика с производителем");
                return;
            }
            string photoNameDb = "";
            if (pictureBox1.Tag != null)
            {
                string pathFrom = pictureBox1.Tag.ToString();
                photoNameDb = System.IO.Path.GetFileName(pathFrom);
                if (pathFrom.Contains("\\"))
                {
                    try { 

                        System.IO.File.Copy(pathFrom, "Images\\" + photoNameDb, true);
                    }
                    catch { } 
                }
            }
            try
            {
                using (var conn = new NpgsqlConnection(connectionStr))
                {
                    conn.Open();
                    string sql;
                    if (ProductName == null)
                    {
                        sql = @"INSERT INTO PRODUCTS (article,name , category_id, manufacturer_id, supplier_id, type_id, price, discount, stock, description) 
                           VALUES (@article, @name, @category, @manufacturer, @supplier, @type, @price, @discount ,@stock, @description)";
                    }
                    else
                    {
                        sql = @"UPDATE products SET article = @article, name = @name, category_id = @category, manufacturer_id = @manufacturer, supplier_id = @supplier, type_id = @type, price = @price, discount = @discount, stock = @stock, description = @descipriton";
                    }
                    using (var cmd = new NpgsqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("article", txtArticle.ToString());
                        cmd.Parameters.AddWithValue("name", txtName.ToString());
                        cmd.Parameters.AddWithValue("category", ((dynamic)cmbCategory.SelectedItem).Id);
                        cmd.Parameters.AddWithValue("manufacturer", ((dynamic)cmbManufactory.SelectedItem).Id);
                        cmd.Parameters.AddWithValue("supplier", ((dynamic)cmbSupplier.SelectedItem).Id);
                        cmd.Parameters.AddWithValue("type", ((dynamic)cmbType.SelectedItem).Id);
                        cmd.Parameters.AddWithValue("price", numPrice.Value);
                        cmd.Parameters.AddWithValue("discount", numDiscount.Value);
                        cmd.Parameters.AddWithValue("stock", numQuantity.Value);
                        cmd.Parameters.AddWithValue("description", txtDescription.Text);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Товар сохранен");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MainForm form = new MainForm();
            form.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation= ofd.FileName;
                pictureBox1.Tag = ofd.FileName;
            }
        }
    }
}

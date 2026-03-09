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
    public partial class MainForm : Form
    {
        string connectionStr = "Database=electro_shop;Password=1;Username=postgres;Host=localhost";

        public MainForm()
        {
            InitializeComponent();

        }

        private void FillCmbSupplier()
        {
            cmbSupplier.Items.Clear();
            cmbSupplier.Items.Add("Все производители");
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                conn.Open();
                var sql = "SELECT name FROM suppliers";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        cmbSupplier.Items.Add(reader["name"].ToString());

                    }
                    cmbSupplier.SelectedIndex = 0;
                }
            }

        }

        public void UpdateProducts()
        {
            flowLayoutPanel1.Controls.Clear();
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                conn.Open();
                var sql = "SELECT p.article, p.name as product_name, c.name as category_name, m.name as manufactory_name, s.name as supplier_name, " +
                    "t.name as types_name, p.price, p.discount, p.stock, p.description, p.photo_url" +
                    " FROM products p JOIN product_categories c ON p.category_id = c.id" +
                    " JOIN manufacturers m ON p.manufacturer_id = m.id" +
                    " JOIN suppliers s ON p.supplier_id = s.id" +
                    " JOIN item_types t ON p.type_id = t.id WHERE 1 = 1";
                if (!string.IsNullOrEmpty(txtSearch.Text))
                {
                    sql += $" AND (p.name ILIKE '%{txtSearch.Text}%')";
                }
                if (cmbSupplier.SelectedIndex > 0)
                {
                    string selectedSup = cmbSupplier.SelectedItem.ToString();
                    sql += $" AND s.name = '{selectedSup}'";
                }
                if (numQuantity.Value > 0)
                {
                    decimal numQuantities = numQuantity.Value;
                    sql += $" AND p.stock = {numQuantities}";
                }
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductControl control = new ProductControl();
                        control.SetData(
                            reader["article"].ToString(),
                            reader["product_name"].ToString(),
                            reader["category_name"].ToString(),
                            reader["manufactory_name"].ToString(),
                            reader["supplier_name"].ToString(),
                            reader["types_name"].ToString(),
                            Convert.ToDecimal(reader["price"]),
                            Convert.ToInt32(reader["discount"]),
                            Convert.ToInt32(reader["stock"]),
                            reader["description"].ToString(),
                            reader["photo_url"].ToString());
                        flowLayoutPanel1.Controls.Add(control);

                    }
                }

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            UpdateProducts();
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateProducts();
        }

        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            UpdateProducts();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addEditProductForm form = new addEditProductForm(null);
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateProducts();
            }
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            ordersForm form = new ordersForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                UpdateProducts();
            }

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FillCmbSupplier();
            UpdateProducts();
            int curRole = Program.currentRole;
            switch (curRole)
            {
                case 0:
                    btnAddEditForm.Visible = false;
                    btnOrders.Visible = false;
                    txtSearch.Visible = false;
                    cmbSupplier.Visible = false;
                    numQuantity.Visible = false;
                    break;
                case 1:
                    btnAddEditForm.Visible = true;
                    btnOrders.Visible = true;
                    txtSearch.Visible = true;
                    cmbSupplier.Visible = true;
                    numQuantity.Visible = true;
                    break;
                case 2:
                    btnOrders.Visible = true;
                    txtSearch.Visible = true;
                    cmbSupplier.Visible = true;
                    numQuantity.Visible = true;
                    btnAddEditForm.Visible = false;
                    break;
                case 3:
                    btnAddEditForm.Visible = false;
                    btnOrders.Visible = false;
                    txtSearch.Visible = false;
                    cmbSupplier.Visible = false;
                    numQuantity.Visible = false;
                    break;
                default:
                    btnAddEditForm.Visible = false;
                    btnOrders.Visible = false;
                    txtSearch.Visible = false;
                    cmbSupplier.Visible = false;
                    numQuantity.Visible = false;
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.currentRole = 0;
            LoginForm form = new LoginForm();
            form.ShowDialog();
            this.Close();
        }
    }


}

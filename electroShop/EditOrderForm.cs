using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace electroShop
{
    public partial class EditOrderForm : Form
    {
        string connStr = "Database=zooShop;Username=postgres;Host=localhost;Password=1;Port=5432";
        public int OrderID = 0;
        public EditOrderForm(int id)
        {
            InitializeComponent();
            this.OrderID = id;
        }
        private void fillComboBoxes(string table, ComboBox comboBox, string columnName)
        {
            comboBox.DataSource = null;
            comboBox.Items.Clear();
            using (NpgsqlConnection conn = new NpgsqlConnection(connStr))
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
        private void SelectComboItem(ComboBox comboBox, object id)
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
        private void EditOrderForm_Load(object sender, EventArgs e)
        {
            fillComboBoxes("users", cmbClient, "fio");
            fillComboBoxes("pickup_points", cmbPickup, "address");
            fillComboBoxes("order_staus", cmbStatus, "name");

            if (OrderID == 0)
            {
                this.Text = "Новый заказ";
                txtCode.Text = new Random().Next(1000, 9999).ToString();
            }
            else
            {
                this.Text = "Редактирование";
                txtCode.Enabled = false;
                using (var conn = new NpgsqlConnection(connStr))
                {
                    conn.Open();
                    var cmd = new NpgsqlCommand("SELECT * FROM orders WHERE id = " + OrderID, conn);
                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        dtpDelivery.Value = Convert.ToDateTime(reader["date_of_delivery"]);
                        txtCode.Text = reader["code_of_pick"].ToString();
                        SelectComboItem(cmbClient, reader["user_id"]);
                        SelectComboItem(cmbPickup, reader["pickup_point_id"]);
                        SelectComboItem(cmbStatus, reader["order_status_id"]);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int currentId = OrderID; 
            using (var conn = new NpgsqlConnection(connStr))
            {
                conn.Open();
                if (OrderID == 0)
                {
                    string sql = @"INSERT INTO orders (date_of_purchase, date_of_delivery, pickup_point_id, user_id, order_status_id, code_of_pick) 
                                   VALUES (CURRENT_DATE, @deliv, @pickup, @client, @status, @code) RETURNING id";
                    var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("deliv", dtpDelivery.Value);
                    cmd.Parameters.AddWithValue("pickup", ((dynamic)cmbPickup.SelectedItem).Id);
                    cmd.Parameters.AddWithValue("client", ((dynamic)cmbClient.SelectedItem).Id);
                    cmd.Parameters.AddWithValue("status", ((dynamic)cmbStatus.SelectedItem).Id);
                    cmd.Parameters.AddWithValue("code", Convert.ToInt32(txtCode.Text));
                    currentId = (int)cmd.ExecuteScalar();
                }
                else 
                {
                    string sql = @"UPDATE orders SET date_of_delivery=@deliv, pickup_point_id=@pickup, user_id=@client, order_status_id=@status 
                                   WHERE id=" + OrderID;
                    var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("deliv", dtpDelivery.Value);
                    cmd.Parameters.AddWithValue("pickup", ((dynamic)cmbPickup.SelectedItem).Id);
                    cmd.Parameters.AddWithValue("client", ((dynamic)cmbClient.SelectedItem).Id);
                    cmd.Parameters.AddWithValue("status", ((dynamic)cmbStatus.SelectedItem).Id);
                    cmd.ExecuteNonQuery();
                    new NpgsqlCommand($"DELETE FROM product_orders WHERE order_id = {currentId}", conn).ExecuteNonQuery();
                }
                if (!string.IsNullOrEmpty(txtArticles.Text))
                {
                    string[] articles = txtArticles.Text.Split(',');
                    foreach (string art in articles)
                    {
                        string cleanArt = art.Trim(); 
                        if (cleanArt != "")
                        {
                            var cmdD = new NpgsqlCommand("INSERT INTO product_orders (order_id, article, quantity) VALUES (@oid, @art, 1)", conn);
                            cmdD.Parameters.AddWithValue("oid", currentId);
                            cmdD.Parameters.AddWithValue("art", cleanArt);
                            cmdD.ExecuteNonQuery();
                        }
                    }
                }
            }
            MessageBox.Show("Успех!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
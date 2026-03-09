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
    public partial class ordersForm : Form
    {
        public ordersForm()
        {
            InitializeComponent();
        }
        public void LoadOrders()
        {
            flowLayoutPanel1.Controls.Clear();
            using (var conn = new NpgsqlConnection("Database=electro_shop;Password=1;Username=postgres;Host=localhost"))
            {
                conn.Open();
                string sql = @"
            SELECT o.id, o.order_date, s.name as status_name, u.fio as client_name
            FROM orders o
            JOIN order_statuses s ON o.status_id = s.id
            JOIN users u ON o.client_id = u.id
            ORDER BY o.order_date DESC";
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        OrderControl item = new OrderControl();
                        item.SetData(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToDateTime(reader["order_date"]),
                            reader["status_name"].ToString(),
                            reader["client_name"].ToString()
                        );

                        flowLayoutPanel1.Controls.Add(item);
                    }
                }
            }
        }

        private void orderForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {

        }
    }
}

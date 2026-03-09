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
    public partial class OrderControl : UserControl
    {
        int OrderID { get; set; }
        public OrderControl()
        {
            InitializeComponent();
        }
        public void SetData(int id, DateTime date, string status, string client)
        {
            this.OrderID = id;
            lblId.Text = "Заказ " + id.ToString();
            lblDate.Text = "Дата: " + date.ToShortDateString();
            lblStatus.Text = "Статус: " + status;
            lblClient.Text = "Клиент: " + client;
            btnEdit.Visible = (Program.currentRole == 1);
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EditOrderForm form = new EditOrderForm(this.OrderID);
            if (form.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (this.ParentForm is ordersForm parent)
                    {
                        parent.LoadOrders();
                    }
                }
                catch { }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить заказ?", "Внимание", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    using (var conn = new NpgsqlConnection("Database=zooShop;Username=postgres;Host=localhost;Password=1;Port=5432"))
                    {
                        conn.Open();
                        new NpgsqlCommand($"DELETE FROM product_orders WHERE order_id = {this.OrderID}", conn).ExecuteNonQuery();
                        new NpgsqlCommand($"DELETE FROM orders WHERE id = {this.OrderID}", conn).ExecuteNonQuery();
                    }
                    MessageBox.Show("Заказ удален!");
                    this.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
    }
}

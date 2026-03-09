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
    public partial class ProductControl : UserControl
    {
        string connectionStr = "Database=electro_shop;Password=1;Username=postgres;Host=localhost";
        public string productArticle { get; private set; }

        public ProductControl()
        {
            InitializeComponent();
        }
        public void SetData(string article, string name, string category, string manufactory, string supplier, string type, decimal price, int discount, int quantity, string desription, string photoUrl)
        {
            this.productArticle = article;
            lblName.Text = "Название: " + name;
            lblCategory.Text = "Категория: " + category;
            lblManufacturer.Text = "Производитель: " + manufactory;
            lblSupplier.Text = "Поставщик: " + supplier;
            lblType.Text = "Тип: " + type;
            lblQuantity.Text = $"Количество на складе: {quantity}";
            lblDescription.Text = "Описание: " + desription;
            if (discount > 0)
            {
                decimal newPrice = price - (price * discount / 100);
                lblPrice.Text = $"{price:F2}";
                lblPrice.ForeColor = Color.Red;
                lblPrice.Font = new Font(lblPrice.Font, FontStyle.Strikeout);
                lblPrice.Visible = true;
                lblNewPrice.Text = $"{newPrice:F2} руб.";
                lblNewPrice.ForeColor = Color.Black;
                lblNewPrice.Font = new Font(lblNewPrice.Font, FontStyle.Bold);
                lblNewPrice.Visible = true;

                lblDiscount.Text = $"Скидка: {discount}%";
            }
            else
            {
                lblPrice.Visible = false;
                lblNewPrice.Text = $"{price:F2} руб.";
                lblNewPrice.ForeColor = Color.Black;
                lblNewPrice.Font = new Font(lblNewPrice.Font, FontStyle.Bold);
                lblNewPrice.Visible = true;

                lblDiscount.Text = "";
            }

            this.BackColor = Color.White;

            if (quantity == 0)
            {
                this.BackColor = ColorTranslator.FromHtml("#ADD8E6"); 
            }
            else if (discount > 15)
            {
                this.BackColor = ColorTranslator.FromHtml("#67D31D"); 
            }

            if (Program.currentRole == 1)
            {
                btnRedact.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnRedact.Visible = false;
                btnDelete.Visible = false;
            }

            string cleanPhotoName = photoUrl?.Trim();
            string folder = Path.Combine(Application.StartupPath, "Images");
            if (string.IsNullOrWhiteSpace(cleanPhotoName) || !File.Exists(Path.Combine(folder, cleanPhotoName)))
            {
                pictureBox1.ImageLocation = Path.Combine(folder, "picture.png");
            }
            else
            {
                pictureBox1.ImageLocation = Path.Combine(folder, cleanPhotoName);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Вы уверены что хотите удалить товар?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
                    {
                        conn.Open();
                        var sql = @"DELETE FROM products WHERE article = @art";
                        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                        {
                            cmd.Parameters.AddWithValue("art", this.productArticle);
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Товар успешно удален");
                        this.Visible = false; 
                    }
                }
                catch (PostgresException ex)
                {
                    MessageBox.Show("Нельзя удалить этот товар, так как он присутствует в заказах!", "Ошибка удаления", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось удалить товар: " + ex.Message);
                }
            }
        }

        private void btnRedact_Click(object sender, EventArgs e)
        {
            addEditProductForm form = new addEditProductForm(this.productArticle);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (this.ParentForm is MainForm mainForm)
                {
                    mainForm.UpdateProducts();
                }
            }
        }

        private void ProductControl_Load(object sender, EventArgs e)
        {
        }
    }
}
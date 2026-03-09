using Npgsql;
namespace electroShop
{
    public partial class LoginForm : Form
    {

        string connectionStr = "Database=electro_shop;Password=1;Username=postgres;Host=localhost";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль");
                return;
            }
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                conn.Open();
                var sql = @"SELECT * FROM users WHERE login = @login AND password = @password";
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("login", login);
                    cmd.Parameters.AddWithValue("password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Program.currentRole = Convert.ToInt32(reader["role_id"]);
                            MainForm form = new MainForm();
                            form.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неправильный логин или пароль");
                            return;
                        }
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.currentRole = 0;
            MainForm form = new MainForm();
            form.ShowDialog();
            this.Close();
        }
    }
}

namespace electroShop
{
    partial class ProductControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        private void InitializeComponent()
        {
            pictureBox1 = new PictureBox();
            lblName = new Label();
            lblCategory = new Label();
            lblManufacturer = new Label();
            lblSupplier = new Label();
            lblType = new Label();
            lblQuantity = new Label();
            lblDescription = new Label();
            lblPrice = new Label();
            lblNewPrice = new Label();
            lblDiscount = new Label();
            btnRedact = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.White;
            pictureBox1.Location = new Point(15, 15);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(200, 180);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblName.ForeColor = Color.FromArgb(33, 37, 41);
            lblName.Location = new Point(230, 15);
            lblName.Name = "lblName";
            lblName.Size = new Size(153, 25);
            lblName.TabIndex = 1;
            lblName.Text = "Наименование";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.ForeColor = Color.FromArgb(108, 117, 125);
            lblCategory.Location = new Point(230, 45);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(83, 21);
            lblCategory.TabIndex = 2;
            lblCategory.Text = "Категория";
            // 
            // lblManufacturer
            // 
            lblManufacturer.AutoSize = true;
            lblManufacturer.ForeColor = Color.FromArgb(108, 117, 125);
            lblManufacturer.Location = new Point(230, 66);
            lblManufacturer.Name = "lblManufacturer";
            lblManufacturer.Size = new Size(121, 21);
            lblManufacturer.TabIndex = 3;
            lblManufacturer.Text = "Производитель";
            // 
            // lblSupplier
            // 
            lblSupplier.AutoSize = true;
            lblSupplier.ForeColor = Color.FromArgb(108, 117, 125);
            lblSupplier.Location = new Point(230, 87);
            lblSupplier.Name = "lblSupplier";
            lblSupplier.Size = new Size(90, 21);
            lblSupplier.TabIndex = 4;
            lblSupplier.Text = "Поставщик";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.ForeColor = Color.FromArgb(108, 117, 125);
            lblType.Location = new Point(230, 108);
            lblType.Name = "lblType";
            lblType.Size = new Size(36, 21);
            lblType.TabIndex = 5;
            lblType.Text = "Тип";
            // 
            // lblQuantity
            // 
            lblQuantity.AutoSize = true;
            lblQuantity.ForeColor = Color.FromArgb(33, 37, 41);
            lblQuantity.Location = new Point(230, 129);
            lblQuantity.Name = "lblQuantity";
            lblQuantity.Size = new Size(166, 21);
            lblQuantity.TabIndex = 6;
            lblQuantity.Text = "Количество на складе";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.ForeColor = Color.FromArgb(33, 37, 41);
            lblDescription.Location = new Point(230, 150);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(81, 21);
            lblDescription.TabIndex = 7;
            lblDescription.Text = "Описание";
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblPrice.ForeColor = Color.FromArgb(108, 117, 125);
            lblPrice.Location = new Point(540, 60);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(57, 25);
            lblPrice.TabIndex = 8;
            lblPrice.Text = "Цена";
            // 
            // lblNewPrice
            // 
            lblNewPrice.AutoSize = true;
            lblNewPrice.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblNewPrice.ForeColor = Color.FromArgb(0, 120, 215);
            lblNewPrice.Location = new Point(540, 85);
            lblNewPrice.Name = "lblNewPrice";
            lblNewPrice.Size = new Size(132, 30);
            lblNewPrice.TabIndex = 9;
            lblNewPrice.Text = "Новая цена";
            // 
            // lblDiscount
            // 
            lblDiscount.BackColor = Color.FromArgb(255, 193, 7);
            lblDiscount.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            lblDiscount.ForeColor = Color.FromArgb(33, 37, 41);
            lblDiscount.Location = new Point(545, 15);
            lblDiscount.Name = "lblDiscount";
            lblDiscount.Size = new Size(140, 30);
            lblDiscount.TabIndex = 10;
            lblDiscount.Text = "Скидка: 0%";
            lblDiscount.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnRedact
            // 
            btnRedact.BackColor = Color.FromArgb(0, 120, 215);
            btnRedact.Cursor = Cursors.Hand;
            btnRedact.FlatAppearance.BorderSize = 0;
            btnRedact.FlatStyle = FlatStyle.Flat;
            btnRedact.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnRedact.ForeColor = Color.White;
            btnRedact.Location = new Point(545, 125);
            btnRedact.Name = "btnRedact";
            btnRedact.Size = new Size(140, 35);
            btnRedact.TabIndex = 11;
            btnRedact.Text = "Редактировать";
            btnRedact.UseVisualStyleBackColor = false;
            btnRedact.Click += btnRedact_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(220, 53, 69);
            btnDelete.Cursor = Cursors.Hand;
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 204);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(545, 165);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(140, 35);
            btnDelete.TabIndex = 12;
            btnDelete.Text = "Удалить";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // ProductControl
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btnDelete);
            Controls.Add(btnRedact);
            Controls.Add(lblDiscount);
            Controls.Add(lblNewPrice);
            Controls.Add(lblPrice);
            Controls.Add(lblDescription);
            Controls.Add(lblQuantity);
            Controls.Add(lblType);
            Controls.Add(lblSupplier);
            Controls.Add(lblManufacturer);
            Controls.Add(lblCategory);
            Controls.Add(lblName);
            Controls.Add(pictureBox1);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            Margin = new Padding(4);
            Name = "ProductControl";
            Size = new Size(700, 210);
            Load += ProductControl_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblManufacturer;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblNewPrice;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Button btnRedact;
        private System.Windows.Forms.Button btnDelete;
    }
}
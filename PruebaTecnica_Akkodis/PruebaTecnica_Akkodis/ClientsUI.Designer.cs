namespace PruebaTecnica_Akkodis
{
    partial class ClientsUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImportDataButton = new System.Windows.Forms.Button();
            this.FormatText = new System.Windows.Forms.TextBox();
            this.Format = new System.Windows.Forms.Button();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DNI = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Apellidos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaNacimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Telefono = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalClientes = new System.Windows.Forms.TextBox();
            this.Estado = new System.Windows.Forms.TextBox();
            this.Refresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ImportDataButton
            // 
            this.ImportDataButton.Location = new System.Drawing.Point(13, 13);
            this.ImportDataButton.Name = "ImportDataButton";
            this.ImportDataButton.Size = new System.Drawing.Size(141, 23);
            this.ImportDataButton.TabIndex = 0;
            this.ImportDataButton.Text = "Import File";
            this.ImportDataButton.UseVisualStyleBackColor = true;
            this.ImportDataButton.Click += new System.EventHandler(this.Import_Click);
            // 
            // FormatText
            // 
            this.FormatText.Location = new System.Drawing.Point(160, 13);
            this.FormatText.Name = "FormatText";
            this.FormatText.ReadOnly = true;
            this.FormatText.Size = new System.Drawing.Size(56, 22);
            this.FormatText.TabIndex = 1;
            this.FormatText.Text = "Format:";
            // 
            // Format
            // 
            this.Format.Location = new System.Drawing.Point(223, 12);
            this.Format.Name = "Format";
            this.Format.Size = new System.Drawing.Size(75, 23);
            this.Format.TabIndex = 2;
            this.Format.Text = "CSV";
            this.Format.UseVisualStyleBackColor = true;
            this.Format.Click += new System.EventHandler(this.Format_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(713, 12);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 3;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(13, 43);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(775, 23);
            this.ProgressBar.TabIndex = 5;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DNI,
            this.Nombre,
            this.Apellidos,
            this.FechaNacimiento,
            this.Telefono,
            this.Email});
            this.dataGridView1.Location = new System.Drawing.Point(13, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(775, 327);
            this.dataGridView1.TabIndex = 6;
            // 
            // DNI
            // 
            this.DNI.HeaderText = "DNI";
            this.DNI.MinimumWidth = 6;
            this.DNI.Name = "DNI";
            this.DNI.Width = 125;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 6;
            this.Nombre.Name = "Nombre";
            this.Nombre.Width = 125;
            // 
            // Apellidos
            // 
            this.Apellidos.HeaderText = "Apellidos";
            this.Apellidos.MinimumWidth = 6;
            this.Apellidos.Name = "Apellidos";
            this.Apellidos.Width = 125;
            // 
            // FechaNacimiento
            // 
            this.FechaNacimiento.HeaderText = "FechaNacimiento";
            this.FechaNacimiento.MinimumWidth = 6;
            this.FechaNacimiento.Name = "FechaNacimiento";
            this.FechaNacimiento.Width = 125;
            // 
            // Telefono
            // 
            this.Telefono.HeaderText = "Telefono";
            this.Telefono.MinimumWidth = 6;
            this.Telefono.Name = "Telefono";
            this.Telefono.Width = 125;
            // 
            // Email
            // 
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 6;
            this.Email.Name = "Email";
            this.Email.Width = 125;
            // 
            // TotalClientes
            // 
            this.TotalClientes.Location = new System.Drawing.Point(13, 416);
            this.TotalClientes.Name = "TotalClientes";
            this.TotalClientes.Size = new System.Drawing.Size(141, 22);
            this.TotalClientes.TabIndex = 7;
            this.TotalClientes.Text = "Total Clientes: 0";
            // 
            // Estado
            // 
            this.Estado.Location = new System.Drawing.Point(160, 416);
            this.Estado.Name = "Estado";
            this.Estado.Size = new System.Drawing.Size(141, 22);
            this.Estado.TabIndex = 8;
            this.Estado.Text = "Estado:";
            // 
            // Refresh
            // 
            this.Refresh.Location = new System.Drawing.Point(632, 12);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(75, 23);
            this.Refresh.TabIndex = 9;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // ClientsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Refresh);
            this.Controls.Add(this.Estado);
            this.Controls.Add(this.TotalClientes);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.Format);
            this.Controls.Add(this.FormatText);
            this.Controls.Add(this.ImportDataButton);
            this.Name = "ClientsUI";
            this.Text = "Clients Manager";
            this.Load += new System.EventHandler(this.ClientsUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ImportDataButton;
        private System.Windows.Forms.TextBox FormatText;
        private System.Windows.Forms.Button Format;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox TotalClientes;
        private System.Windows.Forms.TextBox Estado;
        private System.Windows.Forms.DataGridViewTextBoxColumn DNI;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Apellidos;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaNacimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Telefono;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.Button Refresh;
    }
}


namespace SistemaCitasMedicas.UI.Presentation.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblSidebarTitle;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Button btnEspecialidades;
        private System.Windows.Forms.Button btnPacientes;
        private System.Windows.Forms.Button btnMedicos;
        private System.Windows.Forms.Button btnAgendarCita;
        private System.Windows.Forms.Button btnConsultarCitas;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblSubWelcome;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelSidebar = new Panel();
            btnConsultarCitas = new Button();
            btnAgendarCita = new Button();
            btnMedicos = new Button();
            btnPacientes = new Button();
            btnEspecialidades = new Button();
            lblSidebarTitle = new Label();
            panelHeader = new Panel();
            lblHeaderTitle = new Label();
            panelContent = new Panel();
            lblSubWelcome = new Label();
            lblWelcome = new Label();
            panelSidebar.SuspendLayout();
            panelHeader.SuspendLayout();
            panelContent.SuspendLayout();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(44, 62, 80);
            panelSidebar.Controls.Add(btnConsultarCitas);
            panelSidebar.Controls.Add(btnAgendarCita);
            panelSidebar.Controls.Add(btnMedicos);
            panelSidebar.Controls.Add(btnPacientes);
            panelSidebar.Controls.Add(btnEspecialidades);
            panelSidebar.Controls.Add(lblSidebarTitle);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Margin = new Padding(3, 4, 3, 4);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(297, 800);
            panelSidebar.TabIndex = 0;
            // 
            // btnConsultarCitas
            // 
            btnConsultarCitas.BackColor = Color.FromArgb(52, 73, 94);
            btnConsultarCitas.Cursor = Cursors.Hand;
            btnConsultarCitas.FlatAppearance.BorderSize = 0;
            btnConsultarCitas.FlatStyle = FlatStyle.Flat;
            btnConsultarCitas.Font = new Font("Segoe UI", 11F);
            btnConsultarCitas.ForeColor = Color.White;
            btnConsultarCitas.Location = new Point(23, 520);
            btnConsultarCitas.Margin = new Padding(3, 4, 3, 4);
            btnConsultarCitas.Name = "btnConsultarCitas";
            btnConsultarCitas.Size = new Size(251, 60);
            btnConsultarCitas.TabIndex = 5;
            btnConsultarCitas.Text = "🔍 Consultar Citas";
            btnConsultarCitas.UseVisualStyleBackColor = false;
            btnConsultarCitas.Click += BtnConsultarCitas_Click;
            // 
            // btnAgendarCita
            // 
            btnAgendarCita.BackColor = Color.FromArgb(9, 132, 227);
            btnAgendarCita.Cursor = Cursors.Hand;
            btnAgendarCita.FlatAppearance.BorderSize = 0;
            btnAgendarCita.FlatStyle = FlatStyle.Flat;
            btnAgendarCita.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAgendarCita.ForeColor = Color.White;
            btnAgendarCita.Location = new Point(23, 427);
            btnAgendarCita.Margin = new Padding(3, 4, 3, 4);
            btnAgendarCita.Name = "btnAgendarCita";
            btnAgendarCita.Size = new Size(251, 60);
            btnAgendarCita.TabIndex = 4;
            btnAgendarCita.Text = "📅 Agendar Cita";
            btnAgendarCita.UseVisualStyleBackColor = false;
            btnAgendarCita.Click += BtnAgendarCita_Click;
            // 
            // btnMedicos
            // 
            btnMedicos.BackColor = Color.FromArgb(52, 73, 94);
            btnMedicos.Cursor = Cursors.Hand;
            btnMedicos.FlatAppearance.BorderSize = 0;
            btnMedicos.FlatStyle = FlatStyle.Flat;
            btnMedicos.Font = new Font("Segoe UI", 11F);
            btnMedicos.ForeColor = Color.White;
            btnMedicos.Location = new Point(23, 333);
            btnMedicos.Margin = new Padding(3, 4, 3, 4);
            btnMedicos.Name = "btnMedicos";
            btnMedicos.Size = new Size(251, 60);
            btnMedicos.TabIndex = 3;
            btnMedicos.Text = "👨‍⚕️ Médicos";
            btnMedicos.UseVisualStyleBackColor = false;
            btnMedicos.Click += BtnMedicos_Click;
            // 
            // btnPacientes
            // 
            btnPacientes.BackColor = Color.FromArgb(52, 73, 94);
            btnPacientes.Cursor = Cursors.Hand;
            btnPacientes.FlatAppearance.BorderSize = 0;
            btnPacientes.FlatStyle = FlatStyle.Flat;
            btnPacientes.Font = new Font("Segoe UI", 11F);
            btnPacientes.ForeColor = Color.White;
            btnPacientes.Location = new Point(23, 240);
            btnPacientes.Margin = new Padding(3, 4, 3, 4);
            btnPacientes.Name = "btnPacientes";
            btnPacientes.Size = new Size(251, 60);
            btnPacientes.TabIndex = 2;
            btnPacientes.Text = "👤 Pacientes";
            btnPacientes.UseVisualStyleBackColor = false;
            btnPacientes.Click += BtnPacientes_Click;
            // 
            // btnEspecialidades
            // 
            btnEspecialidades.BackColor = Color.FromArgb(52, 73, 94);
            btnEspecialidades.Cursor = Cursors.Hand;
            btnEspecialidades.FlatAppearance.BorderSize = 0;
            btnEspecialidades.FlatStyle = FlatStyle.Flat;
            btnEspecialidades.Font = new Font("Segoe UI", 11F);
            btnEspecialidades.ForeColor = Color.White;
            btnEspecialidades.Location = new Point(23, 147);
            btnEspecialidades.Margin = new Padding(3, 4, 3, 4);
            btnEspecialidades.Name = "btnEspecialidades";
            btnEspecialidades.Size = new Size(251, 60);
            btnEspecialidades.TabIndex = 1;
            btnEspecialidades.Text = "\U0001fa7a Especialidades";
            btnEspecialidades.UseVisualStyleBackColor = false;
            btnEspecialidades.Click += BtnEspecialidades_Click;
            // 
            // lblSidebarTitle
            // 
            lblSidebarTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblSidebarTitle.ForeColor = Color.White;
            lblSidebarTitle.Location = new Point(23, 40);
            lblSidebarTitle.Name = "lblSidebarTitle";
            lblSidebarTitle.Size = new Size(251, 53);
            lblSidebarTitle.TabIndex = 0;
            lblSidebarTitle.Text = "Panel Clínico";
            lblSidebarTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(9, 132, 227);
            panelHeader.Controls.Add(lblHeaderTitle);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(297, 0);
            panelHeader.Margin = new Padding(3, 4, 3, 4);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(846, 107);
            panelHeader.TabIndex = 1;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(29, 27);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(535, 46);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "Sistema de Gestión de Consultas";
            // 
            // panelContent
            // 
            panelContent.BackColor = Color.FromArgb(245, 246, 250);
            panelContent.Controls.Add(lblSubWelcome);
            panelContent.Controls.Add(lblWelcome);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(297, 107);
            panelContent.Margin = new Padding(3, 4, 3, 4);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(846, 693);
            panelContent.TabIndex = 2;
            // 
            // lblSubWelcome
            // 
            lblSubWelcome.Font = new Font("Segoe UI", 12F);
            lblSubWelcome.ForeColor = Color.FromArgb(127, 143, 166);
            lblSubWelcome.Location = new Point(57, 253);
            lblSubWelcome.Name = "lblSubWelcome";
            lblSubWelcome.Size = new Size(731, 80);
            lblSubWelcome.TabIndex = 1;
            lblSubWelcome.Text = "Utiliza el menú lateral para gestionar el catálogo de especialidades, registrar pacientes y médicos, y planificar las citas de la clínica.";
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = true;
            lblWelcome.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(44, 62, 80);
            lblWelcome.Location = new Point(51, 173);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(660, 54);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "¡Bienvenido al Portal Clínico V1.0!";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 800);
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Controls.Add(panelSidebar);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema de Gestión de Citas Médicas";
            panelSidebar.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            panelContent.PerformLayout();
            ResumeLayout(false);
        }
    }
}

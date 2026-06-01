using System;
using System.Linq;
using System.Windows.Forms;
using SistemaCitasMedicas.UI.Application.Services;
using SistemaCitasMedicas.UI.Domain.Entities;
using SistemaCitasMedicas.UI.Presentation.Helpers;

namespace SistemaCitasMedicas.UI.Presentation.Forms
{
    public partial class PacienteForm : Form
    {
        private readonly PacienteService _pacienteService;

        public PacienteForm(PacienteService pacienteService)
        {
            _pacienteService = pacienteService;
            InitializeComponent();
        }

        private void PacienteForm_Load(object sender, EventArgs e)
        {
            CargarPacientes();
            dtpFechaNacimiento.Value = DateTime.Today.AddYears(-25); // Sensible default
        }

        private void CargarPacientes()
        {
            dgvPacientes.DataSource = null;
            dgvPacientes.DataSource = _pacienteService.ListarPacientes().ToList();
            
            if (dgvPacientes.Columns["Id"] != null)
            {
                dgvPacientes.Columns["Id"].Visible = false;
            }
            
            if (dgvPacientes.Columns["Nombre"] != null)
            {
                dgvPacientes.Columns["Nombre"].Width = 100;
            }
            if (dgvPacientes.Columns["Apellido"] != null)
            {
                dgvPacientes.Columns["Apellido"].Width = 100;
            }
            if (dgvPacientes.Columns["Telefono"] != null)
            {
                dgvPacientes.Columns["Telefono"].Width = 100;
            }
            if (dgvPacientes.Columns["Email"] != null)
            {
                dgvPacientes.Columns["Email"].Width = 130;
            }
            if (dgvPacientes.Columns["FechaNacimiento"] != null)
            {
                dgvPacientes.Columns["FechaNacimiento"].HeaderText = "Nacimiento";
                dgvPacientes.Columns["FechaNacimiento"].DefaultCellStyle.Format = "dd/MM/yyyy";
                dgvPacientes.Columns["FechaNacimiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            var paciente = new Paciente
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                FechaNacimiento = dtpFechaNacimiento.Value
            };

            var resultado = _pacienteService.RegistrarPaciente(paciente);
            if (resultado.Exito)
            {
                MessageBox.Show(MensajesUI.RegistroExitoso, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                CargarPacientes();
            }
            else
            {
                MessageBox.Show(resultado.Mensaje, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearFields()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            dtpFechaNacimiento.Value = DateTime.Today.AddYears(-25);
        }
    }
}

using System;
using System.Linq;
using System.Windows.Forms;
using SistemaCitasMedicas.UI.Application.Services;
using SistemaCitasMedicas.UI.Domain.Entities;
using SistemaCitasMedicas.UI.Presentation.Helpers;

namespace SistemaCitasMedicas.UI.Presentation.Forms
{
    public partial class MedicoForm : Form
    {
        private readonly MedicoService _medicoService;
        private readonly EspecialidadService _especialidadService;

        public MedicoForm(MedicoService medicoService, EspecialidadService especialidadService)
        {
            _medicoService = medicoService;
            _especialidadService = especialidadService;
            InitializeComponent();
        }

        private void MedicoForm_Load(object sender, EventArgs e)
        {
            CargarEspecialidades();
            CargarMedicos();
        }

        private void CargarEspecialidades()
        {
            var especialidades = _especialidadService.ListarEspecialidades().ToList();
            cmbEspecialidad.DataSource = especialidades;
            cmbEspecialidad.DisplayMember = "Nombre";
            cmbEspecialidad.ValueMember = "Id";
            if (especialidades.Count == 0)
            {
                MessageBox.Show("Debe registrar al menos una especialidad antes de registrar médicos.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarMedicos()
        {
            var medicos = _medicoService.ListarMedicos().ToList();
            var especialidades = _especialidadService.ListarEspecialidades().ToDictionary(e => e.Id, e => e.Nombre);

            var displayList = medicos.Select(m => new {
                m.Id,
                m.Nombre,
                m.Apellido,
                m.Telefono,
                m.Email,
                Especialidad = especialidades.ContainsKey(m.EspecialidadId) ? especialidades[m.EspecialidadId] : "Sin Especialidad"
            }).ToList();

            dgvMedicos.DataSource = null;
            dgvMedicos.DataSource = displayList;

            if (dgvMedicos.Columns["Id"] != null)
            {
                dgvMedicos.Columns["Id"].Visible = false;
            }
            
            if (dgvMedicos.Columns["Nombre"] != null) dgvMedicos.Columns["Nombre"].Width = 100;
            if (dgvMedicos.Columns["Apellido"] != null) dgvMedicos.Columns["Apellido"].Width = 100;
            if (dgvMedicos.Columns["Telefono"] != null) dgvMedicos.Columns["Telefono"].Width = 100;
            if (dgvMedicos.Columns["Email"] != null) dgvMedicos.Columns["Email"].Width = 120;
            if (dgvMedicos.Columns["Especialidad"] != null)
            {
                dgvMedicos.Columns["Especialidad"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbEspecialidad.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una especialidad válida.", "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var medico = new Medico
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text,
                EspecialidadId = (Guid)cmbEspecialidad.SelectedValue
            };

            var resultado = _medicoService.RegistrarMedico(medico);
            if (resultado.Exito)
            {
                MessageBox.Show(MensajesUI.RegistroExitoso, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
                CargarMedicos();
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
            if (cmbEspecialidad.Items.Count > 0)
            {
                cmbEspecialidad.SelectedIndex = 0;
            }
        }
    }
}

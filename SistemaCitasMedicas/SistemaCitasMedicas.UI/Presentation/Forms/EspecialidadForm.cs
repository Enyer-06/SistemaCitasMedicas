using System;
using System.Linq;
using System.Windows.Forms;
using SistemaCitasMedicas.UI.Application.Services;
using SistemaCitasMedicas.UI.Domain.Entities;
using SistemaCitasMedicas.UI.Presentation.Helpers;

namespace SistemaCitasMedicas.UI.Presentation.Forms
{
    public partial class EspecialidadForm : Form
    {
        private readonly EspecialidadService _especialidadService;

        public EspecialidadForm(EspecialidadService especialidadService)
        {
            _especialidadService = especialidadService;
            InitializeComponent();
        }

        private void EspecialidadForm_Load(object sender, EventArgs e)
        {
            CargarEspecialidades();
        }

        private void CargarEspecialidades()
        {
            dgvEspecialidades.DataSource = null;
            dgvEspecialidades.DataSource = _especialidadService.ListarEspecialidades().ToList();
            
            if (dgvEspecialidades.Columns["Id"] != null)
            {
                dgvEspecialidades.Columns["Id"].Visible = false;
            }
            
            if (dgvEspecialidades.Columns["Nombre"] != null)
            {
                dgvEspecialidades.Columns["Nombre"].Width = 150;
            }
            
            if (dgvEspecialidades.Columns["Descripcion"] != null)
            {
                dgvEspecialidades.Columns["Descripcion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            var especialidad = new Especialidad
            {
                Nombre = txtNombre.Text,
                Descripcion = txtDescripcion.Text
            };

            var resultado = _especialidadService.RegistrarEspecialidad(especialidad);
            if (resultado.Exito)
            {
                MessageBox.Show(MensajesUI.RegistroExitoso, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNombre.Clear();
                txtDescripcion.Clear();
                CargarEspecialidades();
            }
            else
            {
                MessageBox.Show(resultado.Mensaje, "Error de Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SistemaCitasMedicas.UI.Application.Services;
using SistemaCitasMedicas.UI.Domain.Entities;
using SistemaCitasMedicas.UI.Domain.Enums;
using SistemaCitasMedicas.UI.Presentation.Helpers;

namespace SistemaCitasMedicas.UI.Presentation.Forms
{
    public partial class ConsultaCitaForm : Form
    {
        private readonly CitaService _citaService;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;

        public ConsultaCitaForm(CitaService citaService, PacienteService pacienteService, MedicoService medicoService)
        {
            _citaService = citaService;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            InitializeComponent();
        }

        private void ConsultaCitaForm_Load(object sender, EventArgs e)
        {
            CargarCombos();
            CargarCitas();
        }

        private void CargarCombos()
        {
            var pacientes = _pacienteService.ListarPacientes()
                .Select(p => new { Id = p.Id, NombreCompleto = $"{p.Nombre} {p.Apellido}" })
                .ToList();
            
            cmbPaciente.DataSource = pacientes;
            cmbPaciente.DisplayMember = "NombreCompleto";
            cmbPaciente.ValueMember = "Id";
            cmbPaciente.SelectedIndex = -1;

            var medicos = _medicoService.ListarMedicos()
                .Select(m => new { Id = m.Id, NombreCompleto = $"{m.Nombre} {m.Apellido}" })
                .ToList();
            
            cmbMedico.DataSource = medicos;
            cmbMedico.DisplayMember = "NombreCompleto";
            cmbMedico.ValueMember = "Id";
            cmbMedico.SelectedIndex = -1;
        }

        private void CargarCitas()
        {
            var citas = _citaService.ObtenerTodas().ToList();
            MostrarCitas(citas);
        }

        private void MostrarCitas(List<Cita> citas)
        {
            var pacientes = _pacienteService.ListarPacientes().ToDictionary(p => p.Id, p => $"{p.Nombre} {p.Apellido}");
            var medicos = _medicoService.ListarMedicos().ToDictionary(m => m.Id, m => $"{m.Nombre} {m.Apellido}");

            var displayList = citas.Select(c => new {
                c.Id,
                Paciente = pacientes.ContainsKey(c.PacienteId) ? pacientes[c.PacienteId] : "Desconocido",
                Médico = medicos.ContainsKey(c.MedicoId) ? medicos[c.MedicoId] : "Desconocido",
                Fecha = c.Fecha.ToString("dd/MM/yyyy"),
                Hora = c.Hora.ToString(@"hh\:mm"),
                Estado = c.Estado.ToString(),
                c.Motivo
            }).ToList();

            dgvCitas.DataSource = null;
            dgvCitas.DataSource = displayList;

            if (dgvCitas.Columns["Id"] != null)
            {
                dgvCitas.Columns["Id"].Visible = false;
            }
            
            if (dgvCitas.Columns["Paciente"] != null) dgvCitas.Columns["Paciente"].Width = 140;
            if (dgvCitas.Columns["Médico"] != null) dgvCitas.Columns["Médico"].Width = 140;
            if (dgvCitas.Columns["Fecha"] != null) dgvCitas.Columns["Fecha"].Width = 90;
            if (dgvCitas.Columns["Hora"] != null) dgvCitas.Columns["Hora"].Width = 70;
            if (dgvCitas.Columns["Estado"] != null) dgvCitas.Columns["Estado"].Width = 90;
            if (dgvCitas.Columns["Motivo"] != null)
            {
                dgvCitas.Columns["Motivo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void BtnFiltrarPaciente_Click(object sender, EventArgs e)
        {
            if (cmbPaciente.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un paciente de la lista para filtrar.", "Filtrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            Guid pacienteId = (Guid)cmbPaciente.SelectedValue;
            var citas = _citaService.ConsultarPorPaciente(pacienteId).ToList();
            MostrarCitas(citas);
        }

        private void BtnFiltrarMedico_Click(object sender, EventArgs e)
        {
            if (cmbMedico.SelectedValue == null)
            {
                MessageBox.Show("Seleccione un médico de la lista para filtrar.", "Filtrar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            Guid medicoId = (Guid)cmbMedico.SelectedValue;
            var citas = _citaService.ConsultarPorMedico(medicoId).ToList();
            MostrarCitas(citas);
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            cmbPaciente.SelectedIndex = -1;
            cmbMedico.SelectedIndex = -1;
            CargarCitas();
        }

        private void BtnCancelarCita_Click(object sender, EventArgs e)
        {
            if (dgvCitas.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una cita de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idVal = dgvCitas.CurrentRow.Cells["Id"].Value;
            if (idVal == null) return;
            Guid citaId = (Guid)idVal;

            var confirmacion = MessageBox.Show(MensajesUI.ConfirmarCancelacion, "Confirmar Cancelación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmacion == DialogResult.Yes)
            {
                var resultado = _citaService.CancelarCita(citaId);
                if (resultado.Exito)
                {
                    MessageBox.Show(MensajesUI.CitaCancelada, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnLimpiar_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(resultado.Mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnReprogramar_Click(object sender, EventArgs e)
        {
            if (dgvCitas.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una cita de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var idVal = dgvCitas.CurrentRow.Cells["Id"].Value;
            if (idVal == null) return;
            Guid citaId = (Guid)idVal;

            var cita = _citaService.ObtenerTodas().FirstOrDefault(c => c.Id == citaId);
            if (cita == null) return;

            if (cita.Estado == EstadoCita.Cancelada)
            {
                MessageBox.Show("No se puede reprogramar una cita que ya ha sido cancelada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var form = new ReprogramarCitaForm(_citaService, cita);
            if (form.ShowDialog() == DialogResult.OK)
            {
                BtnLimpiar_Click(sender, e);
            }
        }
    }
}

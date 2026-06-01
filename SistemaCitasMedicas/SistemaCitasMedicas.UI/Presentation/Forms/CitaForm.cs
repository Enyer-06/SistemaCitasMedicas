using System;
using System.Linq;
using System.Windows.Forms;
using SistemaCitasMedicas.UI.Application.DTOs;
using SistemaCitasMedicas.UI.Application.Services;
using SistemaCitasMedicas.UI.Presentation.Helpers;

namespace SistemaCitasMedicas.UI.Presentation.Forms
{
    public partial class CitaForm : Form
    {
        private readonly CitaService _citaService;
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;

        public CitaForm(CitaService citaService, PacienteService pacienteService, MedicoService medicoService)
        {
            _citaService = citaService;
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            InitializeComponent();
        }

        private void CitaForm_Load(object sender, EventArgs e)
        {
            CargarPacientes();
            CargarMedicos();
            CargarHorarios();
            dtpFecha.Value = DateTime.Today.AddDays(1); // Mañana por defecto
        }

        private void CargarPacientes()
        {
            var pacientes = _pacienteService.ListarPacientes()
                .Select(p => new { Id = p.Id, NombreCompleto = $"{p.Nombre} {p.Apellido}" })
                .ToList();
            
            cmbPaciente.DataSource = pacientes;
            cmbPaciente.DisplayMember = "NombreCompleto";
            cmbPaciente.ValueMember = "Id";
        }

        private void CargarMedicos()
        {
            var medicos = _medicoService.ListarMedicos()
                .Select(m => new { Id = m.Id, NombreCompleto = $"{m.Nombre} {m.Apellido}" })
                .ToList();
            
            cmbMedico.DataSource = medicos;
            cmbMedico.DisplayMember = "NombreCompleto";
            cmbMedico.ValueMember = "Id";
        }

        private void CargarHorarios()
        {
            cmbHora.Items.Clear();
            var inicio = new TimeSpan(8, 0, 0);
            var fin = new TimeSpan(18, 0, 0);
            var intervalo = TimeSpan.FromMinutes(30);

            var actual = inicio;
            while (actual <= fin)
            {
                cmbHora.Items.Add(actual.ToString(@"hh\:mm"));
                actual = actual.Add(intervalo);
            }
            
            if (cmbHora.Items.Count > 0)
            {
                cmbHora.SelectedIndex = 0;
            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (cmbPaciente.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un paciente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbMedico.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un médico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TimeSpan.TryParse(cmbHora.Text, out var horaSeleccionada))
            {
                MessageBox.Show("Seleccione un horario válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var dto = new CitaDTO
            {
                PacienteId = (Guid)cmbPaciente.SelectedValue,
                MedicoId = (Guid)cmbMedico.SelectedValue,
                Fecha = dtpFecha.Value,
                Hora = horaSeleccionada,
                Motivo = txtMotivo.Text
            };

            var resultado = _citaService.AgendarCita(dto);
            if (resultado.Exito)
            {
                MessageBox.Show(MensajesUI.CitaAgendada, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMotivo.Clear();
                dtpFecha.Value = DateTime.Today.AddDays(1);
                if (cmbHora.Items.Count > 0)
                {
                    cmbHora.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show(resultado.Mensaje, "Error al Agendar Cita", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

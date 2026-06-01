using System;
using System.Drawing;
using System.Windows.Forms;
using SistemaCitasMedicas.UI.Application.Services;

namespace SistemaCitasMedicas.UI.Presentation.Forms
{
    public partial class MainForm : Form
    {
        private readonly PacienteService _pacienteService;
        private readonly MedicoService _medicoService;
        private readonly EspecialidadService _especialidadService;
        private readonly CitaService _citaService;

        public MainForm(
            PacienteService pacienteService,
            MedicoService medicoService,
            EspecialidadService especialidadService,
            CitaService citaService)
        {
            _pacienteService = pacienteService;
            _medicoService = medicoService;
            _especialidadService = especialidadService;
            _citaService = citaService;
            InitializeComponent();
        }

        private void BtnEspecialidades_Click(object sender, EventArgs e)
        {
            var form = new EspecialidadForm(_especialidadService);
            form.ShowDialog();
        }

        private void BtnPacientes_Click(object sender, EventArgs e)
        {
            var form = new PacienteForm(_pacienteService);
            form.ShowDialog();
        }

        private void BtnMedicos_Click(object sender, EventArgs e)
        {
            var form = new MedicoForm(_medicoService, _especialidadService);
            form.ShowDialog();
        }

        private void BtnAgendarCita_Click(object sender, EventArgs e)
        {
            var form = new CitaForm(_citaService, _pacienteService, _medicoService);
            form.ShowDialog();
        }

        private void BtnConsultarCitas_Click(object sender, EventArgs e)
        {
            var form = new ConsultaCitaForm(_citaService, _pacienteService, _medicoService);
            form.ShowDialog();
        }
    }
}

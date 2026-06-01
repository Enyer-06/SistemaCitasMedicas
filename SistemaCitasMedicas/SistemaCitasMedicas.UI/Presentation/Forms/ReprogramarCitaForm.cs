using System;
using System.Windows.Forms;
using SistemaCitasMedicas.UI.Application.Services;
using SistemaCitasMedicas.UI.Domain.Entities;
using SistemaCitasMedicas.UI.Presentation.Helpers;

namespace SistemaCitasMedicas.UI.Presentation.Forms
{
    public partial class ReprogramarCitaForm : Form
    {
        private readonly CitaService _citaService;
        private readonly Cita _cita;

        public ReprogramarCitaForm(CitaService citaService, Cita cita)
        {
            _citaService = citaService;
            _cita = cita;
            InitializeComponent();
        }

        private void ReprogramarCitaForm_Load(object sender, EventArgs e)
        {
            CargarHorarios();
            dtpFecha.Value = _cita.Fecha > DateTime.Today ? _cita.Fecha : DateTime.Today.AddDays(1);
            cmbHora.Text = _cita.Hora.ToString(@"hh\:mm");
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
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(cmbHora.Text, out var horaSeleccionada))
            {
                MessageBox.Show("Por favor, seleccione un horario válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var resultado = _citaService.ReprogramarCita(_cita.Id, dtpFecha.Value, horaSeleccionada);
            if (resultado.Exito)
            {
                MessageBox.Show(MensajesUI.CitaReprogramada, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show(resultado.Mensaje, "Error al Reprogramar Cita", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

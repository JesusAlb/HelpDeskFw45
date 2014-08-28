using HelpDesk.Control.Catalogo;
using HelpDesk.Control.Solicitudes;
using HelpDesk.Control.Solicitudes.Incidentes;
using HelpDesk.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HelpDesk.ViewAltas
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewAltaIncidentes.xaml
    /// </summary>
    public partial class hdk_ViewAltaIncidentes : Window
    {
        private hdk_ControlIncidentes ci;
        private hdk_ControlAcceso Control;

        public hdk_ViewAltaIncidentes(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            ci = new hdk_ControlIncidentes(Control);
            ci.cargarComboTipo(cbTipo);
            cbPrio.Items.Add("Alta");
            cbPrio.Items.Add("Media");
            cbPrio.Items.Add("Normal");
        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            hdk_ControlEncuestas ce = new hdk_ControlEncuestas(Control);
            int idUsuario = Control.item.idUsuario;
            int numeroEncuestas = ce.obtenerNumeroEncuestasIn(idUsuario) + ce.obtenerNumeroEncuestasEv(idUsuario);
            if (numeroEncuestas == 0)
            {
                if (!String.IsNullOrWhiteSpace(cbTipo.Text) && !String.IsNullOrWhiteSpace(cbPrio.Text) && !String.IsNullOrWhiteSpace(txtDescripcion.Text))
                {
                    hdk_ControlUsuario cu = new hdk_ControlUsuario(Control);
                    int usSinAsignar = cu.obtenerIdUsuario_SinAsignar();
                    TimeSpan horaFn = TimeSpan.Parse("00:00:00");
                    if (ci.insertarIncidente(idUsuario, usSinAsignar, usSinAsignar, txtDescripcion.Text, "S/A", "S/A", Convert.ToInt32(cbTipo.SelectedValue), DateTime.Today, null, cbPrio.Text, DateTime.Now, null))
                    {
                        ce.insertarCalidadServicio(0, ci.obtenerUltimoIncidente());
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Llene todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Responda la encuesta antes de crear un incidente nuevo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Desc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}

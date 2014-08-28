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

namespace HelpDesk.ViewAltas.Solicitudes.Incidentes
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewAltaIncidenteCompleto.xaml
    /// </summary>
    public partial class hdk_ViewAltaIncidenteCompleto : Window
    {

        private hdk_ControlIncidentes ci;
        private hdk_ControlUsuario cu;
        private hdk_ControlAcceso Control;

        public hdk_ViewAltaIncidenteCompleto(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            ci = new hdk_ControlIncidentes(Control);
            cu = new hdk_ControlUsuario(Control);
            ci.cargarComboTipo(cbTipo);
            cu.cargarComboUsuarios(cbSoporte, 0);
            cu.cargarComboUsuarios(cbSeguimiento, 0);
            cu.cargarComboUsuarios(cbSolicitante, 1);
            this.cargarComboTiempo(cbMinutos, cbHora);
            this.cargarComboTiempo(cbMinutos1, cbHora1);
            this.cbSeguimiento.Text = "S/A";
            this.limitarFechas();
        }

        private void limitarFechas()
        {
            dpFechaSol.BlackoutDates.Add(new CalendarDateRange(
                DateTime.Today.AddDays(1),
                DateTime.Today.AddYears(1000)
                ));
            dpFechaCierre.BlackoutDates.Add(new CalendarDateRange(
                DateTime.Today.AddDays(1),
                DateTime.Today.AddYears(1000)
    ));
        }

        private void cargarComboTiempo(ComboBox cbmin, ComboBox cbhor)
        {
            for (int x = 0; x < 60; x++)
            {
                if (x < 24)
                {
                    if (x < 10)
                    {
                        cbhor.Items.Add("0" + x);
                        cbmin.Items.Add("0" + x);
                    }
                    else
                    {
                        cbhor.Items.Add("" + x);
                    }
                }

                if (x > 9)
                {
                    cbmin.Items.Add("" + x);
                }
            }
        }

        private bool validarEntrada()
        {
            if (!string.IsNullOrWhiteSpace(txtAcciones.Text) && !string.IsNullOrWhiteSpace(txtDescripcion.Text) &&
                !string.IsNullOrWhiteSpace(txtSolucion.Text) && !string.IsNullOrEmpty(cbSolicitante.Text) &&
                !string.IsNullOrEmpty(cbPrio.Text) && !string.IsNullOrEmpty(cbTipo.Text) && !string.IsNullOrEmpty(cbSoporte.Text) &&
                !string.IsNullOrEmpty(cbSeguimiento.Text) && !string.IsNullOrEmpty(dpFechaCierre.Text) && !string.IsNullOrEmpty(dpFechaCierre.Text) &&
                !string.IsNullOrEmpty(cbHora.Text) && !string.IsNullOrEmpty(cbMinutos.Text) &&
                !string.IsNullOrEmpty(cbHora1.Text) && !string.IsNullOrEmpty(cbMinutos1.Text))
            {
                if (!cbSoporte.Text.Equals("S/A"))
                {
                    if (dpFechaCierre.SelectedDate == dpFechaSol.SelectedDate)
                    {
                        if (cbHora.SelectedIndex > cbHora1.SelectedIndex)
                        {
                            MessageBox.Show("La fecha de solicitud supera a la fecha de cierre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                        else if (cbHora.SelectedIndex == cbHora1.SelectedIndex)
                        {
                            if (cbMinutos.SelectedIndex > cbMinutos1.SelectedIndex)
                            {
                                MessageBox.Show("La fecha de solicitud supera a la fecha de cierre", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    MessageBox.Show("Asigne el usuario de soporte", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                
            }
            else
            {
                MessageBox.Show("Llene todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
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

        private void txtDescripcion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtAcciones_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtSolucion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            if (this.validarEntrada())
            {
                if (ci.insertarIncidente(Convert.ToInt32(cbSolicitante.SelectedValue), Convert.ToInt32(cbSoporte.SelectedValue), Convert.ToInt32(cbSeguimiento.SelectedValue),
                                        txtDescripcion.Text, txtAcciones.Text, txtSolucion.Text, Convert.ToInt32(cbTipo.SelectedValue), dpFechaSol.SelectedDate.Value,
                                        dpFechaCierre.SelectedDate.Value, cbPrio.Text, Convert.ToDateTime(cbHora.Text + ":" + cbMinutos.Text), Convert.ToDateTime(cbHora1.Text + ":" + cbMinutos1.Text)))
                {

                    hdk_ControlEncuestas ce = new hdk_ControlEncuestas(Control);
                    ce.insertarCalidadServicio(0, ci.obtenerUltimoIncidente());
                    this.Close();
                }
            }
        }

        private void dpFechaSol_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dpFechaSol.SelectedDate != null)
            {
                dpFechaCierre.IsEnabled = true;
                dpFechaCierre.SelectedDate = dpFechaSol.SelectedDate;
            }
            else
            {
                dpFechaCierre.IsEnabled = false;
            }
        }

        private void cbHora_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(cbHora.Text))
            {
                cbHora1.IsEnabled = true;
                cbHora1.SelectedIndex = cbHora.SelectedIndex;
            }
            else
            {
                cbHora1.IsEnabled = false;
            }


        }

        private void cbMinutos_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(cbMinutos.Text))
            {
                cbMinutos1.IsEnabled = true;
                cbMinutos1.SelectedIndex = cbMinutos.SelectedIndex;
            }
            else
            {
                cbMinutos1.IsEnabled = false;
            }
        }

        private void dpFechaCierre_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dpFechaCierre.SelectedDate != null)
            {
                if (dpFechaSol.SelectedDate.Value > dpFechaCierre.SelectedDate.Value)
                {
                    dpFechaCierre.SelectedDate = dpFechaSol.SelectedDate;
                }
            }

        }

        private void cbHora1_LostFocus(object sender, RoutedEventArgs e)
        {
                if (dpFechaSol.SelectedDate.Value == dpFechaCierre.SelectedDate.Value)
                {
                    if (cbHora.SelectedIndex > cbHora1.SelectedIndex)
                    {
                        cbHora1.SelectedIndex = cbHora.SelectedIndex;
                    }                 
                }           
        }

        private void cbMinutos1_LostFocus(object sender, RoutedEventArgs e)
        {
                if (dpFechaSol.SelectedDate.Value == dpFechaCierre.SelectedDate.Value)
                {
                    if (cbHora.SelectedIndex == cbHora1.SelectedIndex)
                    {
                        if (cbMinutos.SelectedIndex > cbMinutos1.SelectedIndex)
                        {

                            cbMinutos1.SelectedIndex = cbMinutos.SelectedIndex;
                        }
                    }
                }
        }

        private void cbSoporte_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cbSoporte.SelectedIndex != -1)
            {
                cbSeguimiento.IsEnabled = true;
            }
            else
            {
                cbSeguimiento.IsEnabled = false;
            }
        }
    }
}

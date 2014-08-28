using HelpDesk.Control.Solicitudes.Incidentes;
using HelpDesk.ControlAltas;
using HelpDesk.Modelo;
using HelpDesk.Principal;
using HelpDesk.View.Control;
using HelpDesk.ViewAltas;
using HelpDesk.ViewAltas.Solicitudes;
using HelpDesk.ViewAltas.Solicitudes.Incidentes;
using HelpDesk.ViewReportes;
using HelpDesk.ViewReportes.Incidentes;
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

namespace HelpDesk.View.Solicitudes.Eventos
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewIncidentes.xaml
    /// </summary>
    public partial class hdk_ViewIncidentes : Window
    {
        hdk_ControlIncidentes ci;
        hdk_ControlAcceso Control;
        VistaIncidente item;
        string filtroTipo = "";
        private bool conexion = true;

        public hdk_ViewIncidentes(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            ci = new hdk_ControlIncidentes(Control);
            actualizarTablas();
            ci.cargarComboTipo(cbTipo);
            limite1.Text = "01/01/2014";
            limite2.SelectedDate = DateTime.Today;
            

            if (Control.item.tipoUsuario == 1)
            {
                
                this.tbAbierta.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                this.tbProceso.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                this.tbCancelada.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                this.tbCerrada.Columns[4].Visibility = System.Windows.Visibility.Hidden;
                gridVertical.Visibility = System.Windows.Visibility.Hidden;
                this.Width = 970;
                
                encuesta.Header = "Realizar encuesta";
            }
        }

        private void actualizarTablas()
        {
            if (Control.item.tipoUsuario == 0)
            {
                tbAbierta.ItemsSource = ci.cargarTablaSoporte(0, filtroTipo, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                if (tbAbierta.Items == null)
                {
                    conexion = false;
                }
                else
                {
                    tbProceso.ItemsSource = ci.cargarTablaSoporte(1, filtroTipo, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbCerrada.ItemsSource = ci.cargarTablaSoporte(2, filtroTipo, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbCancelada.ItemsSource = ci.cargarTablaSoporte(3, filtroTipo, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                }
            }
            else
            {
                tbAbierta.ItemsSource = ci.cargarTablaSolicitante(Control.item.idUsuario, filtroTipo, 0, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                if (tbAbierta.Items == null)
                {
                    conexion = false;
                }
                else
                {
                    tbProceso.ItemsSource = ci.cargarTablaSolicitante(Control.item.idUsuario, filtroTipo, 1, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbCerrada.ItemsSource = ci.cargarTablaSolicitante(Control.item.idUsuario, filtroTipo, 2, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbCancelada.ItemsSource = ci.cargarTablaSolicitante(Control.item.idUsuario, filtroTipo, 3, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                }
            
            }
        }

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            if (Control.item.tipoUsuario == 0)
            {
                hdk_ViewAltaIncidenteCompleto vaic = new hdk_ViewAltaIncidenteCompleto(Control);
                vaic.ShowDialog();
            }
            else
            {
                hdk_ViewAltaIncidentes vai = new hdk_ViewAltaIncidentes(Control);
                vai.ShowDialog();
                
            }
            this.actualizarTablas();
        }


        private void cerrar_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewCerrarIncidente cvi = new hdk_ViewCerrarIncidente(Control);
                cvi.pasarDatos(item);
                cvi.ShowDialog();
                this.actualizarTablas();
            }
            else
            {
                MessageBox.Show("Seleccione un incidente");
            }
        }

        private void cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                ci.cambiarStatus(item.numIncidente, 3);
                this.actualizarTablas();
            } 
        }


        private void asignar_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {

                    hdk_ViewAsignarSoporte vas = new hdk_ViewAsignarSoporte(Control);
                    vas.pasarDatos(item, 0);
                    vas.ShowDialog();
                    this.actualizarTablas();
                
            }
            else
            {
                MessageBox.Show("Seleccione un incidente");
            }

        }

        private void tbAbierta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item =(VistaIncidente)tbAbierta.SelectedItem;
        }

        private void busquedaPorCaso(){

            if (Control.item.tipoUsuario == 0)
            {
                    tbAbierta.ItemsSource = ci.cargarTablaSoporte(0, filtroTipo, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbProceso.ItemsSource = ci.cargarTablaSoporte(1, filtroTipo, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbCerrada.ItemsSource = ci.cargarTablaSoporte(2, filtroTipo, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbCancelada.ItemsSource = ci.cargarTablaSoporte(3, filtroTipo, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
            }
            else
            {
                    tbAbierta.ItemsSource = ci.cargarTablaSolicitante(Control.item.idUsuario, filtroTipo, 0, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbProceso.ItemsSource = ci.cargarTablaSolicitante(Control.item.idUsuario, filtroTipo, 1, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbCerrada.ItemsSource = ci.cargarTablaSolicitante(Control.item.idUsuario, filtroTipo, 2, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
                    tbCancelada.ItemsSource = ci.cargarTablaSolicitante(Control.item.idUsuario, filtroTipo, 3, filtro.Text, limite1.SelectedDate, limite2.SelectedDate);
             }
        }


        private void filtro_KeyUp(object sender, KeyEventArgs e)
        {
          this.busquedaPorCaso();
        }

        private void limite1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.busquedaPorCaso();
        }

        private void limite2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.busquedaPorCaso();
        }

        private void ItemProceso_GotFocus(object sender, RoutedEventArgs e)
        {
            item = null;
            tbProceso.SelectedIndex = -1;
            if (Control.item.tipoUsuario == 0)
            {
                Nuevo.IsEnabled = false;
                asignar.IsEnabled = true;
                cancelar.IsEnabled = false;
                cerrar.IsEnabled = true;
            }
            else
            {
                Nuevo.IsEnabled = true;
                asignar.IsEnabled = false;
                cancelar.IsEnabled = true;
                cerrar.IsEnabled = false;
            }
            btnEncuesta.IsEnabled = false;
        }

        private void ItemCerrada_GotFocus(object sender, RoutedEventArgs e)
        {
            item = null;
            tbCerrada.SelectedIndex = -1;
            if (Control.item.tipoUsuario == 0)
            {
                Nuevo.IsEnabled = true;
            }
            else
            {
                Nuevo.IsEnabled = true;
            }

            cancelar.IsEnabled = false;
            cerrar.IsEnabled = false;
            asignar.IsEnabled = false;
            btnEncuesta.IsEnabled = false;
        }

        private void ItemCancelada_GotFocus(object sender, RoutedEventArgs e)
        {
            item = null;
            tbCancelada.SelectedIndex = -1;
            if (Control.item.tipoUsuario == 0)
            {
                asignar.IsEnabled = false;
                Nuevo.IsEnabled = false;
            }
            else
            {
                Nuevo.IsEnabled = true;
            }
            cancelar.IsEnabled = false;
            cerrar.IsEnabled = false;
            asignar.IsEnabled = false; 
            btnEncuesta.IsEnabled = false;
        }

        private void ItemAbierta_GotFocus(object sender, RoutedEventArgs e)
        {
            item = null;
            tbAbierta.SelectedIndex = -1;
            cerrar.IsEnabled = false;
            cancelar.IsEnabled = true;
            if (Control.item.tipoUsuario == 0)
            {
                Nuevo.IsEnabled = false;
                cancelar.IsEnabled = false;
                asignar.IsEnabled = true;
            }
            else
            {
                asignar.IsEnabled = false;
                cancelar.IsEnabled = true;
                Nuevo.IsEnabled = true;
                cerrar.IsEnabled = false;
            }
            btnEncuesta.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cerrar.IsEnabled = false;

            if (Control.item.tipoUsuario == 0)
            {
                Nuevo.IsEnabled = false;
                cancelar.IsEnabled = false;
                cerrar.IsEnabled = false;
            }
            else
            {
                Nuevo.IsEnabled = true;
                cancelar.IsEnabled = true;
                cerrar.IsEnabled = false;
                asignar.IsEnabled = false;
                cmCancelar.Visibility = System.Windows.Visibility.Hidden;
                solDetalleAb.IsEnabled = false;
                solDetallePro.IsEnabled = false;
                solDetalleCer.IsEnabled = false;
            }

            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();           
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (conexion)
            {
                this.actualizarTablas();
            }           
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewReportIncidentes ri;

            if (Control.item.tipoUsuario == 0)
            {
                ri = new hdk_ViewReportIncidentes(ci.cargarTablaSoporte(tabCon.SelectedIndex, "", "", null, null));
            }
            else
            {
                ri = new hdk_ViewReportIncidentes(ci.cargarTablaSolicitante(Control.item.idUsuario, "", tabCon.SelectedIndex, "", null, null));
            }

            ri.ShowDialog();
        }

        private void tbProceso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = (VistaIncidente)tbProceso.SelectedItem;
            if (item != null)
            {
                if (item.seguimiento.Equals("S/A"))
                {
                    this.segDetallePro.IsEnabled = false;
                }
            }
        }

        private void tbCerrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = (VistaIncidente)tbCerrada.SelectedItem;
            if (item != null)
            {
                if (item.seguimiento.Equals("S/A"))
                {
                    this.apoDetalleCer.IsEnabled = false;
                }

                if (item.statusCal_Servicio)
                {
                    if (Control.item.tipoUsuario == 0)
                    {
                        encuesta.IsEnabled = true;
                        btnEncuesta.IsEnabled = true;
                    }
                    else
                    {
                        encuesta.IsEnabled = false;
                        btnEncuesta.IsEnabled = false;
                    }
                }
                else
                {
                    if (Control.item.tipoUsuario == 0)
                    {
                        encuesta.IsEnabled = false;
                        btnEncuesta.IsEnabled = false;
                    }
                    else
                    {
                        encuesta.IsEnabled = true;
                        btnEncuesta.IsEnabled = true;
                    }                  
                }
            }
        }

        private void tbCancelada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = (VistaIncidente)tbCancelada.SelectedItem;
        }

        private void solDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewUsuarioDetalle vud = new hdk_ViewUsuarioDetalle(item.idSolicitante, Control);
                vud.ShowDialog();
            }
        }

        private void sopDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewUsuarioDetalle vud = new hdk_ViewUsuarioDetalle(item.idSoporte, Control);
                vud.ShowDialog();
            }
        }

        private void segDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewUsuarioDetalle vud = new hdk_ViewUsuarioDetalle(item.idSeguimiento, Control);
                vud.ShowDialog();
            }
        }

        private void cbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                filtroTipo = ((((sender as ComboBox).SelectedItem) as tbltipoincidencia).nomTipoIncidente);
                this.busquedaPorCaso();
            } catch{
            }          
        }

        private void encuesta_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewEncuesta ve = new hdk_ViewEncuesta(Control);
                ve.pasarDatos(item, "Incidente");
                ve.ShowDialog();
                this.actualizarTablas();
            }
        }

        private void btnEstaditicaIn_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewEstadisticasIncidencia vei;
            if (limite1.SelectedDate != null && limite2.SelectedDate != null)
            {
                vei = new hdk_ViewEstadisticasIncidencia(limite1.SelectedDate.Value, limite2.SelectedDate.Value, Control);
                vei.ShowDialog();
            }
            else
            {
                vei = new hdk_ViewEstadisticasIncidencia(new DateTime(2014,01,01), DateTime.Today, Control);
                vei.ShowDialog();
            }
            
        }

        private void btnEstadisticaEn_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewEstadisticasCalidadIncidentes veci;
            if (limite1.SelectedDate != null && limite2.SelectedDate != null)
            {
                veci = new hdk_ViewEstadisticasCalidadIncidentes(limite1.SelectedDate.Value, limite2.SelectedDate.Value, Control);
                veci.ShowDialog();
            }
            else
            {
                veci = new hdk_ViewEstadisticasCalidadIncidentes(new DateTime(2014, 01, 01), DateTime.Today, Control);
                veci.ShowDialog();
            }
        }

        private void limite1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (limite1.SelectedDate == null)
            {
                limite1.SelectedDate = new DateTime(2014, 1, 1);
            }
        }

        private void limite2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (limite2.SelectedDate == null)
            {
                limite2.SelectedDate = DateTime.Today;
            }
        }

        private void btnEncuesta_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewEncuesta ve = new hdk_ViewEncuesta(Control);
                ve.pasarDatos(item, "Incidente");
                ve.ShowDialog();
                this.actualizarTablas();
            }
            else
            {
                MessageBox.Show("Seleccione un evento de la tabla");
            }
        }
    }
}

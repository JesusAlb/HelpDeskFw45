using HelpDesk.AsigRequerimientos;
using HelpDesk.Control.Solicitudes;
using HelpDesk.Modelo;
using HelpDesk.Principal;
using HelpDesk.View.Control;
using HelpDesk.ViewAltas;
using HelpDesk.ViewAltas.Solicitudes;
using HelpDesk.ViewReportes;
using HelpDesk.ViewReportes.Eventos;
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

namespace HelpDesk.Eventos
{
    /// <summary>
    /// Lógica de interacción para eventoVista.xaml
    /// </summary>
    public partial class hdk_ViewEvento : Window
    {
        private hdk_ControlEventos ce;
        private hdk_ViewAltaEventos vae;
        private VistaEvento item;
        private hdk_ControlAcceso Control;
        private hdk_ControlEncuestas controlEncuesta;

        public hdk_ViewEvento(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;         
            actualizarTablas();
            controlEncuesta = new hdk_ControlEncuestas(Control);
            if (Control.item.tipoUsuario == 1)
            {
                this.tbAbierta.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                this.tbProceso.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                this.tbCancelada.Columns[3].Visibility = System.Windows.Visibility.Hidden;
                this.tbCerrada.Columns[4].Visibility = System.Windows.Visibility.Hidden;
                this.gbEstadisticas.Visibility = System.Windows.Visibility.Hidden;
                this.gbRecursos.Margin = new Thickness(9, 2, 0, -200);
                this.AsignarApoyo.Visibility = System.Windows.Visibility.Hidden;        
                encuesta.Header = "Realizar encuesta";
            }
            else
            {

            }

            FechaInicio.Text = "01/01/2014";
            FechaFinal.SelectedDate = new DateTime(DateTime.Today.Year + 1, DateTime.Today.Month, DateTime.Today.Day);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            vae = new hdk_ViewAltaEventos(Control);
            vae.ShowDialog();
            this.actualizarTablas();
        }

        private void actualizarTablas()
        {
            string strfiltro = "";

            if (filtro.Foreground != Brushes.Gray)
            {
                strfiltro = filtro.Text;
            }
                ce = new hdk_ControlEventos(Control);
                if (Control.item.tipoUsuario == 0)
                {
                    tbAbierta.ItemsSource = ce.cargarTablasSoporte(0, strfiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                    tbProceso.ItemsSource = ce.cargarTablasSoporte(1, strfiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                    tbCerrada.ItemsSource = ce.cargarTablasSoporte(2, strfiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                    tbCancelada.ItemsSource = ce.cargarTablasSoporte(3, strfiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);

                }
                else
                {
                    tbAbierta.ItemsSource = ce.cargarTablasSolicitante(Control.item.idUsuario, 0, strfiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                    tbProceso.ItemsSource = ce.cargarTablasSolicitante(Control.item.idUsuario, 1, strfiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                    tbCerrada.ItemsSource = ce.cargarTablasSolicitante(Control.item.idUsuario, 2, strfiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                    tbCancelada.ItemsSource = ce.cargarTablasSolicitante(Control.item.idUsuario, 3, strfiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
            
        }

        private void Cerrar_Click(object sender, RoutedEventArgs e)
        {              
            ce.cerrarEvento(item.idEvento);
            this.actualizarTablas();
         }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                ce.cambiarStatus(item.idEvento, 3);
                this.actualizarTablas();
                MessageBox.Show("Evento cancelado exitosamente");
            }
            else
            {
                MessageBox.Show("Seleccione el evento");
            }

        }

        private void tbAbierta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item =(VistaEvento)tbAbierta.SelectedItem;
        }

        private void Asignar_Click(object sender, RoutedEventArgs e)
        {       
            if (item != null)
            {
                hdk_ViewAsigReq var = new hdk_ViewAsigReq(Control);
                var.pasarDatos(item);
                var.ShowDialog();
                this.actualizarTablas();
            }
            else
            {
                MessageBox.Show("Seleccione el evento");
            }

        }

        private void GenInfo_Click(object sender, RoutedEventArgs e)
        {

            hdk_ViewReportEventos re;

            if (Control.item.tipoUsuario == 0)
            {
                re = new hdk_ViewReportEventos(ce.cargarTablasSoporte(tabCon.SelectedIndex, "", null, null));
            }
            else
            {
                re = new hdk_ViewReportEventos(ce.cargarTablasSolicitante(Control.item.idUsuario, tabCon.SelectedIndex, "", null, null));
            }
            re.ShowDialog();
        }

        private void busquedaPorCaso()
        {
            string stFiltro = "";
            if (filtro.Foreground != Brushes.Gray)
            {
                stFiltro = filtro.Text;
            }

            if (Control.item.tipoUsuario == 0)
            {
                if (tbAbierta.IsVisible)
                {
                    tbAbierta.ItemsSource = ce.cargarTablasSoporte(0, stFiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
                else if (tbProceso.IsVisible)
                {
                    tbProceso.ItemsSource = ce.cargarTablasSoporte(1, stFiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
                else if (tbCerrada.IsVisible)
                {
                    tbCerrada.ItemsSource = ce.cargarTablasSoporte(2, stFiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
                else
                {
                    tbCancelada.ItemsSource = ce.cargarTablasSoporte(3, stFiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
            }
            else
            {
                if (tbAbierta.IsVisible)
                {
                    tbAbierta.ItemsSource = ce.cargarTablasSolicitante(Control.item.idUsuario, 0, stFiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
                else if (tbProceso.IsVisible)
                {
                    tbProceso.ItemsSource = ce.cargarTablasSolicitante(Control.item.idUsuario, 1, stFiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
                else if (tbCerrada.IsVisible)
                {
                    tbCerrada.ItemsSource = ce.cargarTablasSolicitante(Control.item.idUsuario, 2, stFiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
                else
                {
                    tbCancelada.ItemsSource = ce.cargarTablasSolicitante(Control.item.idUsuario, 3, stFiltro, FechaInicio.SelectedDate, FechaFinal.SelectedDate);
                }
            }
        }

        private void filtro_KeyUp(object sender, KeyEventArgs e)
        {
            if (filtro.Foreground != Brushes.Gray)
            {
                this.busquedaPorCaso();
            }          
        }

        private void FechaFinal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.busquedaPorCaso();
        }

        private void FechaInicio_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            this.busquedaPorCaso();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AsignarApoyo_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewAsignarSoporte vas = new hdk_ViewAsignarSoporte(Control);
                vas.pasarDatos(item, 1); 
                vas.ShowDialog();
                this.actualizarTablas();
            }
            else
            {
                MessageBox.Show("Seleccione un evento de la tabla");
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cerrar.IsEnabled = false;
            if (Control.item.tipoUsuario == 0)
            {
                    Add.IsEnabled = false;
                    Asignar.IsEnabled = false;
                    Cancelar.IsEnabled = false;
                    solDetalleAb.IsEnabled = true;
                    solDetallePro.IsEnabled = true;
                    solDetalleCer.IsEnabled = true;
                    solDetalleCan.IsEnabled = true;
                    Editar.IsEnabled = false;
            }
            else if(Control.item.tipoUsuario == 1)
            {
                AsignarApoyo.IsEnabled = false;
                solDetalleAb.IsEnabled = false;
                Editar.IsEnabled = true;
                solDetallePro.IsEnabled = false;
                solDetalleCer.IsEnabled = false;
                solDetalleCan.IsEnabled = false;
            }
        }

        private void ItemAbierta_GotFocus(object sender, RoutedEventArgs e)
        {
            tbAbierta.SelectedIndex = -1;
            item = null;
            Cerrar.IsEnabled = false;
            if (Control.item.tipoUsuario == 0)
            {
                AsignarApoyo.IsEnabled = true;
                Cancelar.IsEnabled = false;
                Editar.IsEnabled = false;
            }
            else if(Control.item.tipoUsuario == 1)
            {
                Asignar.IsEnabled = true;
                Cancelar.IsEnabled = true;
                Editar.IsEnabled = true;
            }           
            ImprimirReq.IsEnabled = true;
            btnEncuesta.IsEnabled = false;
        }

        private void ItemProceso_GotFocus(object sender, RoutedEventArgs e)
        {
            tbProceso.SelectedIndex = -1;
            item = null;
            Cerrar.IsEnabled = true;
            if (Control.item.tipoUsuario == 0)
            {
                AsignarApoyo.IsEnabled = true;
                Editar.IsEnabled = false;
            }
            else if(Control.item.tipoUsuario == 1)
            {
                Asignar.IsEnabled = true;
                Cancelar.IsEnabled = true;
                Editar.IsEnabled = true;
            }
            ImprimirReq.IsEnabled = true;
            btnEncuesta.IsEnabled = false;
        }

        private void ItemCerrada_GotFocus(object sender, RoutedEventArgs e)
        {
            tbCerrada.SelectedIndex = -1;
            item = null;
            Cerrar.IsEnabled = false;
            Asignar.IsEnabled = false;
            AsignarApoyo.IsEnabled = false;
            Cancelar.IsEnabled = false;
            ImprimirReq.IsEnabled = true;
            Editar.IsEnabled = false;
            btnEncuesta.IsEnabled = false;
        }

        private void ItemCancelada_GotFocus(object sender, RoutedEventArgs e)
        {
            tbCancelada.SelectedIndex = -1;
            item = null;
            Cerrar.IsEnabled = false;
            AsignarApoyo.IsEnabled = false;
            Asignar.IsEnabled = false;
            Cancelar.IsEnabled = false;
            ImprimirReq.IsEnabled = false;
            Editar.IsEnabled = false;
            btnEncuesta.IsEnabled = false;
        }

        private void ImprimirReq_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewReportRequerimientos vrr = new hdk_ViewReportRequerimientos(item.idEvento);
                vrr.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione el evento en la tabla");
            }
        }

        private void tbProceso_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = (VistaEvento)tbProceso.SelectedItem;
            if (item != null)
            {
                if (item.apoyo.Equals("S/A"))
                {
                    this.apoDetallePro.IsEnabled = false;
                }
            }
        }

        private void tbCerrada_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = (VistaEvento)tbCerrada.SelectedItem;
            if (item != null)
            {
                if (item.apoyo.Equals("S/A"))
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
            item = (VistaEvento)tbCancelada.SelectedItem;
        }

        private void Editar_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                vae = new hdk_ViewAltaEventos(Control);
                vae.pasarDatos(item);
                vae.ShowDialog();
                this.actualizarTablas();
            }
            else
            {
                MessageBox.Show("Selecciones el evento que desee editar");
            }
        }

        private void solicitanteDetalle_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                VistaEvento itemUs = (VistaEvento)item; 
                hdk_ViewUsuarioDetalle vud = new hdk_ViewUsuarioDetalle(itemUs.idSolicitante, Control);
                vud.ShowDialog();
            }

        }

        private void resDetallePro_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                VistaEvento itemUs = (VistaEvento)item;
                hdk_ViewUsuarioDetalle vud = new hdk_ViewUsuarioDetalle(itemUs.idResponsable, Control);
                vud.ShowDialog();
            }
        }

        private void apoDetallePro_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                VistaEvento itemUs = (VistaEvento)item;
                hdk_ViewUsuarioDetalle vud = new hdk_ViewUsuarioDetalle(itemUs.idApoyo, Control);
                vud.ShowDialog();
            }
        }

        private void encuesta_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewEncuesta ve = new hdk_ViewEncuesta(Control);
            ve.pasarDatos(item, "Evento");
            ve.ShowDialog();
            this.actualizarTablas();
        }

        private void btnEstadisticasEventos_Click(object sender, RoutedEventArgs e)
        {
            if (FechaInicio.SelectedDate != null && FechaFinal.SelectedDate != null)
            {
                hdk_ViewEstadisticasEventos vee = new hdk_ViewEstadisticasEventos(FechaInicio.SelectedDate.Value, FechaFinal.SelectedDate.Value, Control);
                vee.ShowDialog();
            }
            else
            {
                hdk_ViewEstadisticasEventos vee = new hdk_ViewEstadisticasEventos(new DateTime(2014, 01, 01), DateTime.Today, Control);
                vee.ShowDialog();
            }
        }

        private void btnEstadisticasCalidad_Click(object sender, RoutedEventArgs e)
        {
            if (FechaInicio.SelectedDate != null && FechaFinal.SelectedDate != null)
            {
                hdk_ViewEstadisticasCalidadEventos vee = new hdk_ViewEstadisticasCalidadEventos(FechaInicio.SelectedDate.Value, FechaFinal.SelectedDate.Value, Control);
                vee.ShowDialog();
            }
            else
            {
                hdk_ViewEstadisticasCalidadEventos vee = new hdk_ViewEstadisticasCalidadEventos(new DateTime(2014, 01, 01), DateTime.Today, Control);
                vee.ShowDialog();
            }
        }

        private void FechaInicio_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FechaInicio.SelectedDate == null)
            {
                FechaInicio.SelectedDate = new DateTime(2014, 1, 1);
            }
        }

        private void FechaFinal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (FechaFinal.SelectedDate == null)
            {
                FechaFinal.SelectedDate = DateTime.Today;
            }
        }

        private void filtro_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(filtro.Text))
            {
                filtro.Foreground = Brushes.Gray;
                filtro.Text = "Buscar";
            }
        }

        private void filtro_GotFocus(object sender, RoutedEventArgs e)
        {
            if (filtro.Foreground == Brushes.Gray)
            {
                filtro.Foreground = Brushes.Black;
                filtro.Text = "";
            }
        }

        private void btnEncuesta_Click(object sender, RoutedEventArgs e)
        {
            if (item != null)
            {
                hdk_ViewEncuesta ve = new hdk_ViewEncuesta(Control);
                ve.pasarDatos(item, "Evento");
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

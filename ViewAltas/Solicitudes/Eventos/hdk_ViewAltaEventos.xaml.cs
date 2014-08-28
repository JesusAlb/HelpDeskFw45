using HelpDesk.AsigRequerimientos;
using HelpDesk.Control.Catalogo;
using HelpDesk.Control.Solicitudes;
using HelpDesk.Modelo;
using HelpDesk.Principal;
using HelpDesk.ViewAltas.Catálogo;
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
    /// Lógica de interacción para hdk_ViewAltaEventos.xaml
    /// </summary>
    public partial class hdk_ViewAltaEventos : Window
    {
        hdk_ControlEventos ce;
        hdk_ControlAcceso Control;
        hdk_ControlLugar cl;
        bool modEdit;
        int idEvent;
        VistaEvento vista;
        


        public hdk_ViewAltaEventos(hdk_ControlAcceso ca)
        {

            InitializeComponent();
            Control = ca;
            ce = new hdk_ControlEventos(Control);
            cl = new hdk_ControlLugar(Control);
            cl.cargarCombo(cbLugar);
            cbTipo.Items.Add("Público");
            cbTipo.Items.Add("Privado");
            this.establecerDiasNoAdmitidos(new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day - 1));
            dpFecha.SelectedDate = DateTime.Today;
            mm.Maximum = mm2.Maximum = 59;
            mm.Minimum = mm2.Minimum = hh.Minimum = hh2.Minimum = 0;
            hh.Maximum = hh2.Maximum = 24;
        }

        private void establecerDiasNoAdmitidos(DateTime fechalimite)
        {
            dpFecha.BlackoutDates.Add(new CalendarDateRange(
                new DateTime(0001, 1, 1),
                fechalimite
                ));
        }

        public void pasarDatos(object it)
        {
            vista = (VistaEvento)it;
            modEdit = true;
            dpFecha.BlackoutDates.RemoveAt(0);
            establecerDiasNoAdmitidos(new DateTime(vista.fecha_Sol.Value.Year, vista.fecha_Sol.Value.Month, vista.fecha_Sol.Value.Day - 1));
            txtTitulo.Text = vista.titulo;
            cbLugar.Text = vista.lugar;
            txtAsistencia.Text = vista.asistencia_aprox.ToString();
            cbTipo.Text = vista.tipo_evento;
            txtAcomodo.Text = vista.acomodo;
            cbLugar.Text = vista.lugar;
            dpFecha.SelectedDate = vista.FechaInicio;
            hh.Text = vista.horaIn.Value.Hour.ToString();
            mm.Text = vista.horaIn.Value.Minute.ToString();
            hh2.Text = vista.horaFn.Value.Hour.ToString();
            mm2.Text = vista.horaFn.Value.Minute.ToString();
            txtDescripcion.Text = vista.descripcion;
            btnAñadir.Content = "Aceptar";
           // Requerimientos.Content = "Modificar requerimientos";
            idEvent = vista.idEvento;
            hh2.IsEnabled = true;
            mm2.IsEnabled = true;
            
        }

        private Boolean validarDatos(){
            
            if (!String.IsNullOrWhiteSpace(txtTitulo.Text) && !String.IsNullOrWhiteSpace(cbLugar.Text) && !String.IsNullOrWhiteSpace(hh.Text) && !String.IsNullOrWhiteSpace(mm.Text) 
           && !String.IsNullOrEmpty(txtAcomodo.Text) && !String.IsNullOrWhiteSpace(cbTipo.Text) && !String.IsNullOrWhiteSpace(hh2.Text) && !String.IsNullOrWhiteSpace(mm2.Text) 
           && !dpFecha.SelectedDate.Equals(null) && !String.IsNullOrWhiteSpace(txtAsistencia.Text) && !String.IsNullOrWhiteSpace(txtDescripcion.Text)){
                int tiempo1 = hh.Value.Value + mm.Value.Value;
                int tiempo2 = hh2.Value.Value + mm2.Value.Value;
                if (tiempo1 > tiempo2)
                {
                    MessageBox.Show("La hora de inicio del evento supera a la hora de término", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;                 
                }
                else
                {
                    return true;
                }
            }               
            else{
                MessageBox.Show("Llene todos los campos obligatorios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void verificarEdicion(string e1, string e2)
        {
                if (!e1.Equals(e2))
                {
                    btnAñadir.Content = "Modificar";
                }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!btnAñadir.Content.Equals("Aceptar"))
            {
                if (this.validarDatos())
                {
                        hdk_ControlEncuestas controlEncuesta = new hdk_ControlEncuestas(Control);
                        int idUsuario = Control.item.idUsuario;
                        int numeroEncuestas = controlEncuesta.obtenerNumeroEncuestasIn(idUsuario) + controlEncuesta.obtenerNumeroEncuestasEv(idUsuario);
                        if (numeroEncuestas == 0)
                        {
                            if (!modEdit)
                            {
                                string horaIn = hh.Text + ":" + mm.Text;
                                string horaFn = hh2.Text + ":" + mm2.Text;
                                hdk_ControlUsuario cu = new hdk_ControlUsuario(Control);
                                int uSinAsignar = cu.obtenerIdUsuario_SinAsignar();
                                if (ce.insertarEvento(Control.item.idUsuario, uSinAsignar, uSinAsignar, txtTitulo.Text, Convert.ToInt32(cbLugar.SelectedValue), DateTime.Today, txtAcomodo.Text, cbTipo.Text, Convert.ToDateTime(dpFecha.Text), Convert.ToInt32(txtAsistencia.Text), Convert.ToDateTime(horaIn), Convert.ToDateTime(horaFn), txtDescripcion.Text))
                                {                              
                                    controlEncuesta.insertarCalidadServicio(1, ce.obtenerUltimoEvento());
                                }
                            }
                            else
                            {
                                string horaIn = hh.Text + ":" + mm.Text;
                                string horaFn = hh2.Text + ":" + mm2.Text;
                                ce.editarEvento(idEvent, txtTitulo.Text, Convert.ToInt32(cbLugar.SelectedValue), txtAcomodo.Text, cbTipo.Text, dpFecha.SelectedDate.Value, Convert.ToInt32(txtAsistencia.Text), Convert.ToDateTime(horaIn), Convert.ToDateTime(horaFn), txtDescripcion.Text);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Responda la(s) encuesta antes de crear un evento", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
            
        }

      /*  private void Requerimientos_Click(object sender, RoutedEventArgs e)
        {
            if (Requerimientos.Content.Equals("Añadir requerimientos"))
            {
                if (this.validarDatos())
                {
                    string horaIn = hh.Text + ":" + mm.Text;
                    string horaFn = hh2.Text + ":" + mm2.Text;
                    hdk_ControlUsuario cu = new hdk_ControlUsuario(Control);
                    int uSinAsignar = cu.obtenerIdUsuario_SinAsignar();

                    if (ce.insertarEvento(Control.idUsuario, uSinAsignar, uSinAsignar, txtTitulo.Text, Convert.ToInt32(cbLugar.SelectedValue), DateTime.Today, txtAcomodo.Text, cbTipo.Text, Convert.ToDateTime(dpFechaIn.Text), Convert.ToInt32(txtAsistencia.Text), Convert.ToDateTime(horaIn), Convert.ToDateTime(horaFn), txtDescripcion.Text))
                    {
                        hdk_ViewAsigReq vaq = new hdk_ViewAsigReq(txtTitulo.Text);
                        vaq.ShowDialog();
                    }
                    Crear.Content = "Aceptar";
                }
            }
            else if (Requerimientos.Content.Equals("Modificar requerimientos"))
            {
                hdk_ViewAsigReq var = new hdk_ViewAsigReq("");
                var.pasarDatos(vista);
                var.ShowDialog();
            }
        }
        */
        private void titulo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void Acomodo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length -1))
            {
                e.Handled = true;
            }
        }

        private void asistencia_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
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

        private void asistencia_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void btnAddLugares_Click(object sender, RoutedEventArgs e)
        {
            int indice = cbLugar.SelectedIndex;
            int tamaño = cbLugar.Items.Count;
            hdk_ViewAltaLugar val = new hdk_ViewAltaLugar(Control);
            val.ShowDialog();           
            cl.cargarCombo(cbLugar);
            if (tamaño != cbLugar.Items.Count)
            {
                cbLugar.SelectedIndex = cbLugar.Items.Count - 1;
            }
            else
            {
                cbLugar.SelectedIndex = indice;
            }
        }

        private void hh_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(hh.Text))
            {
                hh2.IsEnabled = true;
                mm2.IsEnabled = true;
                mm.Value = 0;
                hh2.Minimum = hh.Value;
                hh2.Value = hh.Value + 1;
                mm2.Value = 0;

            }
            else if(!string.IsNullOrEmpty(hh2.Text))
            {
                hh2.IsEnabled = false;
                mm2.IsEnabled = false;
            }

            if(modEdit)
                btnAñadir.Content = "Modificar";
        }

        private void mm_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(mm.Text))
            {
                mm2.IsEnabled = true;
            }

            if (modEdit)
                btnAñadir.Content = "Modificar";
        }

        private void mm2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (hh.Value != null && hh2.Value != null)
            {
                if (mm.Value != null && mm2.Value != null)
                {
                    if (hh.Value == hh2.Value)
                    {
                        if (mm.Value > mm2.Value)
                        {
                            mm2.Value = mm.Value;
                        }
                    }
                }
            }
            if (modEdit)
                btnAñadir.Content = "Modificar";
        }

        private void txtDescripcion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtTitulo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modEdit)
            this.verificarEdicion(vista.titulo, txtTitulo.Text);
        }

        private void cbLugar_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modEdit)
            this.verificarEdicion(vista.lugar, cbLugar.Text);
        }

        private void txtAcomodo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modEdit)
            this.verificarEdicion(vista.acomodo, txtAcomodo.Text);
        }

        private void cbTipo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modEdit)
            this.verificarEdicion(vista.tipo_evento, cbTipo.Text);
        }

        private void txtAsistencia_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modEdit)
            this.verificarEdicion(vista.asistencia_aprox.ToString(), txtAsistencia.Text);
        }

        private void dpFecha_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modEdit)
            this.verificarEdicion(vista.FechaInicio.Value.ToString(), dpFecha.Text);
        }

        private void hh2_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modEdit)
                btnAñadir.Content = "Modificar";
        }

        private void txtDescripcion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modEdit)
            this.verificarEdicion(vista.descripcion, txtDescripcion.Text);
        }


    }
}

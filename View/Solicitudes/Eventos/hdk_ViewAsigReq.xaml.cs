using HelpDesk.Control.Solicitudes;
using HelpDesk.ControlAltas;
using HelpDesk.Modelo;
using HelpDesk.Principal;
using HelpDesk.ViewAltas;
using System;
using System.Collections;
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

namespace HelpDesk.AsigRequerimientos
{
    /// <summary>
    /// Lógica de interacción para asigReqVista.xaml
    /// </summary>
    public partial class hdk_ViewAsigReq : Window
    {
        private hdk_ControlAsigReq ca;
        private hdk_ControlRequerimientos cr;
        private int id;
        private bool modificar = false;
        private int tamañoOb = 0;
        private hdk_ControlAcceso Control;

        public hdk_ViewAsigReq(hdk_ControlAcceso coa)
        {
            InitializeComponent();
            Control = coa;
            ca = new hdk_ControlAsigReq(Control);
            cr = new hdk_ControlRequerimientos(Control);
            cbTipo.SelectedIndex = 0;
            modificar = false;
           // id = ca.getIdUltimoEvento();
        }

        public void pasarDatos(object it)
        {
            VistaEvento Item = (VistaEvento)it;
            txtTitulo.Text = Item.titulo;
            txtObservaciones.Text = Item.observaciones;
            id = Item.idEvento;
            modificar = true;
            this.cargarTablas("");
            this.tamañoOb = Item.observaciones.Length;
        }

        private void cargarTablas(string tipo){
            
            DataAsig.ItemsSource = ca.cargarTablaReqAsig(id);
            DataReq.ItemsSource = cr.cargarTabla(txtFiltroNombre.Text, tipo, id);
        }

        private void cbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 1){
                cargarTablas("Cuantificable");
            }
            else if ((sender as ComboBox).SelectedIndex == 2)
            {
                cargarTablas("No cuantificable");
            }
            else
            {
                cargarTablas("");
            }
             
        }

        private void nombre_KeyUp(object sender, KeyEventArgs e)
        {
            DataReq.ItemsSource = cr.cargarTabla(txtFiltroNombre.Text, cbTipo.Text, id);
        }

   /*     private void Button_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewAltaRequerimiento var = new hdk_ViewAltaRequerimiento(Control);
            var.ShowDialog();
            DataReq.ItemsSource = cr.cargarTabla(cbTipo.Text, txtFiltroNombre.Text, id);
        }*/

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            object item = DataReq.SelectedItem;
            
            if (item != null)
            {
                requerimientosSinAsignar_Result req = (requerimientosSinAsignar_Result)item;
                if (req.tipo.Equals("Cuantificable"))
                {
                    hdk_ViewAltaReqCuantificables varc = new hdk_ViewAltaReqCuantificables(Control);
                    varc.pasarDatos(id,req.idRequerimientos,null);
                    varc.ShowDialog();
                    cargarTablas(cbTipo.Text);
                }
                else
                {                  
                    if (ca.insertar(id, req.idRequerimientos, 0))
                    {
                        cargarTablas(cbTipo.Text);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione los requerimientos que desee añadir");
            }
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            object item = DataAsig.SelectedItem;
            if (item != null)
            {
                ViewEventoConRequerimiento reqAsig = (ViewEventoConRequerimiento)item;               
                if (ca.borrar(id, reqAsig.idRequerimientos))
                {
                    cargarTablas(cbTipo.Text);
                }
            }
            else
            {
                MessageBox.Show("Seleccione los requerimientos que desee borrar");
            }
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (tamañoOb != txtObservaciones.Text.Length && !String.IsNullOrWhiteSpace(txtObservaciones.Text)){
                hdk_ControlEventos ce = new hdk_ControlEventos(Control);
                ce.editarObservaciones(id, txtObservaciones.Text);
            }
            this.Close();
            
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void itEditar_Click(object sender, RoutedEventArgs e)
        {
            object item = DataAsig.SelectedItem;

            if (item != null)
            {
                    ViewEventoConRequerimiento reqAsig = (ViewEventoConRequerimiento)item;

                    hdk_ViewAltaReqCuantificables varc = new hdk_ViewAltaReqCuantificables(Control);
                    varc.pasarDatos(id, reqAsig.idRequerimientos, reqAsig.cantidad);
                    varc.ShowDialog();
                    cargarTablas(cbTipo.Text);
            }        
            else
            {
                MessageBox.Show("Seleccione los requerimientos que desee añadir");
            }
        }

        private void DataAsig_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = DataAsig.SelectedItem;
            if (item != null)
            {
                if (((ViewEventoConRequerimiento)item).cantidad == null)
                {
                    cmEditar.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    cmEditar.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

    }
}

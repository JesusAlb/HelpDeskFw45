using HelpDesk.Control.Catalogo;
using HelpDesk.Modelo;
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

namespace HelpDesk.View.Control
{

    public partial class hdk_ViewUsuarioDetalle : Window
    {

        private hdk_ControlAcceso Control;
        private ViewUsuario datos;
        private hdk_ControlEquipos ce;

        public hdk_ViewUsuarioDetalle(int user, hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            hdk_ControlUsuario cu = new hdk_ControlUsuario(Control);
            datos = cu.getUsuarioDetallado(user);
            Aceptar.Focus();
            if (Control.item.tipoUsuario == 0)
            {
                expEquipos.Visibility = System.Windows.Visibility.Visible;
                ce = new hdk_ControlEquipos(Control);
            }
            else
            {
                expEquipos.Visibility = System.Windows.Visibility.Hidden;
                this.Aceptar.Margin = new Thickness(274, 400, 264, 0);
                this.Height = 513;
            }
           

        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtArea.Text = datos.nomArea;
            txtInstitucion.Text = datos.nomInstitucion;
            txtCoordinacion.Text = datos.nomCoordinacion;
            txtDepto.Text = datos.nomDepto;
            txtExTel.Text = datos.exTel;
            txtCorreo.Text = datos.correo;
            txtNombre.Text = datos.nomCompleto;
            txtPuesto.Text = datos.nomPuesto;
            txtTipo.Text = datos.tipoUsuarioString;
            txtUs.Text = datos.nomUsuario;
            if (Control.item.tipoUsuario == 0)
            {         
                DataG.ItemsSource = ce.cargarTabla(this.txtNombre.Text);
            }
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void expEquipos_Expanded(object sender, RoutedEventArgs e)
        {

            this.Aceptar.Margin = new Thickness(274, 550, 264, 0);
            this.Height = 650;
        }

        private void expEquipos_Collapsed(object sender, RoutedEventArgs e)
        {
            this.Aceptar.Margin = new Thickness(274, 424, 264, 0);
            this.Height = 533.909;
        }
    }
}

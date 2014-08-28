using HelpDesk.ControlAltas;
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

namespace HelpDesk.ViewAltas
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewAltaTipoIncidencia.xaml
    /// </summary>
    public partial class hdk_ViewAltaTipoIncidencia : Window
    {
        private bool modificar = false;
        private tbltipoincidencia tipoIncidente;
        private hdk_ControlTipoIncidencia cti;
        private hdk_ControlAcceso Control;

        public hdk_ViewAltaTipoIncidencia(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cti = new hdk_ControlTipoIncidencia(Control);
            txtTipo.Focus();

        }

        public void pasarDatos(object item)
        {
            tipoIncidente = (tbltipoincidencia)item;
            txtTipo.Text = tipoIncidente.nomTipoIncidente;
            btnAñadir.Content = "Modificar";
            modificar = true;
        }


        private void añadir_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtTipo.Text))
            {
                if (modificar)
                {
                    if(!tipoIncidente.nomTipoIncidente.Equals(txtTipo.Text)){
                            cti.modificar(tipoIncidente.idTipoIncidente, txtTipo.Text);
                    }                    
                }
                else
                {
                    cti.insertar(txtTipo.Text);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Escriba el nombre del tipo de incidencia", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void tipo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;

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

        private void tipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.añadir_Click(sender, e);
            }
        }
    }
}

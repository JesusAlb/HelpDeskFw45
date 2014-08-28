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
    /// Lógica de interacción para hdk_ViewAltaRequerimiento.xaml
    /// </summary>
    public partial class hdk_ViewAltaRequerimiento : Window
    {

        private bool modificar = false;
        private requerimientosSinAsignar_Result requerimiento;
        private hdk_ControlRequerimientos cr;
        private hdk_ControlAcceso Control;

        public hdk_ViewAltaRequerimiento(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cr = new hdk_ControlRequerimientos(Control);
            txtRequerimiento.Focus();
        }

        public void pasarDatos(object item)
        {
            requerimiento = (requerimientosSinAsignar_Result)item;
            modificar = true;
            txtRequerimiento.Text = requerimiento.nomRequerimiento;
            cbTipo.Text = requerimiento.tipo;
            btnAñadir.Content = "Modificar";
        }

        private void añadir_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtRequerimiento.Text) && !String.IsNullOrEmpty(cbTipo.Text))
            {
                if (modificar)
                {
                    if (!(cbTipo.Text.Equals(requerimiento.tipo) && txtRequerimiento.Text.Equals(requerimiento.nomRequerimiento)))
                    {
                        cr.modificar(requerimiento.idRequerimientos, txtRequerimiento.Text, cbTipo.Text);
                    }                    
                }
                else
                {
                    cr.insertar(txtRequerimiento.Text, cbTipo.Text);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Llene todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void req_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
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

        private void req_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.cbTipo.Focus();
            }
        }

        private void cbTipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.añadir_Click(sender, e);
            }
        }

      
    }
}

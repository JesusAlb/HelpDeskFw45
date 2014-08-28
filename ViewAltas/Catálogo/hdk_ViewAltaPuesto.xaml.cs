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

namespace HelpDesk.ViewAltas.Catálogo
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewAltaPuesto.xaml
    /// </summary>
    public partial class hdk_ViewAltaPuesto : Window
    {
        private hdk_ControlPuesto cp;
        private bool modificar = false;
        private tblpuesto Puesto;
        private hdk_ControlAcceso Control;

        public hdk_ViewAltaPuesto(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cp = new hdk_ControlPuesto(Control);
            txtPuesto.Focus();
        }

        public void pasarDatos(object item)
        {
            Puesto = (tblpuesto)item;
            txtPuesto.Text = Puesto.nomPuesto;
            modificar = true;
            btnAñadir.Content = "Modificar";
        }

        private void puesto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void añadir_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtPuesto.Text))
            {
                if (modificar)
                {
                    if (!txtPuesto.Text.Equals(Puesto.nomPuesto))
                    {
                        cp.modificar(Puesto.idPuesto, txtPuesto.Text);
                    }                   
                }
                else
                {
                    cp.insertar(txtPuesto.Text);            
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor introduzca el nombre del puesto", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
    }
}

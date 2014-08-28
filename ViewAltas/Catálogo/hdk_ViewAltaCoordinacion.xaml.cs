using HelpDesk.Datos;
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
using HelpDeskApp;

namespace HelpDesk.ViewAltas
{
    /// <summary>
    /// Lógica de interacción para ViewAltaCoordinacion.xaml
    /// </summary>
    public partial class hdk_ViewAltaCoordinacion : Window
    {

        private bool modificar;
        private hdk_ControlCoordinacion cc;
        private tblcoordinacion coordinacion;
        private hdk_ControlAcceso Control;

        public hdk_ViewAltaCoordinacion(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cc = new hdk_ControlCoordinacion(Control);
            txtCord.Focus();
        }

        public void pasarDatos(object item)
        {
            coordinacion = (tblcoordinacion)item;
            txtCord.Text = coordinacion.nomCoordinacion;
            modificar = true;
            btnAñadir.Content = "Modificar";
        }

        private void añadir_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtCord.Text))
            {
                if (modificar)
                {
                    if (!txtCord.Text.Equals(coordinacion.nomCoordinacion))
                    {
                        cc.modificar(coordinacion.idCoordinacion, txtCord.Text);
                    }
                }
                else
                {
                    cc.insertar(txtCord.Text);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Escriba el nombre de la coordinación", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void cord_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
            {
                    e.Handled = true;
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HelpDesk.Principal
{
    /// <summary>
    /// Lógica de interacción para Acceso.xaml
    /// </summary>
    public partial class hdk_ViewAcceso
    {
        hdk_ControlAcceso ca = new hdk_ControlAcceso();
        hdk_ViewMenu vn;

        public hdk_ViewAcceso()
        {
            InitializeComponent();
            txtUsuario.Foreground = Brushes.Gray;
            txtContra.Foreground = Brushes.Gray;
            txtUsuario.Text = "Usuario";
            txtContra.Password = "Contraseña";
        }

        private void Ingresar_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtUsuario.Text) && !txtUsuario.Text.Equals("Usuario")
                && !String.IsNullOrWhiteSpace(txtContra.Password) && !txtContra.Password.Equals("Contraseña"))
            {
                if (ca.encontrarUsuario(txtUsuario.Text, txtContra.Password))
                {                 
                    vn = new hdk_ViewMenu(ca);
                    this.Close();         
                    vn.Show();

                }
                else
                {
                    txtUsuario.Foreground = Brushes.Gray;
                    txtContra.Foreground = Brushes.Gray;
                    txtUsuario.Text = "Usuario";
                    txtContra.Password = "Contraseña";
                    txtUsuario.Focus();
                }
            }
            else
            {
                MessageBox.Show("Ingrese los datos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private void txtContra_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtContra.Password))
            {
                txtContra.Foreground = Brushes.Gray;
                txtContra.Password = "Contraseña";
            }
        }

        private void txtUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                txtUsuario.Foreground = Brushes.Gray;
                txtUsuario.Text = "Usuario";
            }
        }

        private void txtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtUsuario.Foreground ==Brushes.Gray)
            {
                txtUsuario.Foreground = Brushes.Black;
                txtUsuario.Text = "";
            }
        }

        private void txtContra_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtContra.Foreground == Brushes.Gray)
            {
                txtContra.Foreground = Brushes.Black;
                txtContra.Password = "";
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

        private void txtUsuario_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtUsuario_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, e.Text.Length - 1)) 
            {
                e.Handled = true;
            }
        }
    }
}

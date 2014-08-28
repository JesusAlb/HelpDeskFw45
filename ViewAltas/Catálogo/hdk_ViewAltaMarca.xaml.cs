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
    /// Lógica de interacción para hdk_ViewAltaMarca.xaml
    /// </summary>
    public partial class hdk_ViewAltaMarca : Window
    {

        private hdk_ControlMarca cm;
        private bool modificar = false;
        private tblmarca ItemMarca;
        private hdk_ControlAcceso Control;

        public hdk_ViewAltaMarca(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            this.cm = new hdk_ControlMarca(Control);
            txtMarca.Focus();
        }

        public void pasarDatos(object Item)
        {
            modificar = true;
            ItemMarca = (tblmarca)Item;
            txtMarca.Text = ItemMarca.nomMarca;
            btnAñadir.Content = "Modificar";
        }

        private void marca_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void añadir_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtMarca.Text))
            {
                if (modificar)
                {
                    if (!txtMarca.Text.Equals(ItemMarca.nomMarca))
                    {
                        this.cm.modificar(ItemMarca.idMarca, txtMarca.Text);
                    }                   
                }
                else
                {
                    this.cm.insertar(txtMarca.Text);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor introduzca el nombre de la marca", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
  
    }
}

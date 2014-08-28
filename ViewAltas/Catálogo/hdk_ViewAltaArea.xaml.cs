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
    /// Lógica de interacción para hdk_ViewAltaArea.xaml
    /// </summary>
    public partial class hdk_ViewAltaArea : Window
    {

        hdk_ControlArea ca;
        hdk_ControlAcceso Control;
        private bool modificar = false;
        private tblarea Area;

        public hdk_ViewAltaArea(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            this.ca = new hdk_ControlArea(Control);
            txtArea.Focus();
        }

        public void pasarDatos(object item)
        {
            this.Area = (tblarea)item;
            this.txtArea.Text = Area.nomArea;
            this.modificar = true;
            this.btnAñadir.Content = "Modificar";
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewAltaCoordinacion vac = new hdk_ViewAltaCoordinacion(Control);
            vac.ShowDialog();
        }

        private void añadir_Click(object sender, RoutedEventArgs e)
        {

            if (!String.IsNullOrWhiteSpace(txtArea.Text))
            {
                if (this.modificar)
                {
                    if(!txtArea.Text.Equals(Area.nomArea))
                    {
                        this.ca.modificar(Area.idArea, txtArea.Text);
                    }
                }
                else
                {
                    this.ca.insertar(txtArea.Text);
                }
                this.Close();

            } else{
                MessageBox.Show("Por favor introduzca el nombre del área", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void area_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if(!char.IsLetter(e.Text, e.Text.Length -1)){
                e.Handled = true;
            }
        }
    }
}

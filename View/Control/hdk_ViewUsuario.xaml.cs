using HelpDesk.Control.Catalogo;
using HelpDesk.ControlAltas;
using HelpDesk.Principal;
using HelpDesk.ViewAltas;
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

namespace HelpDesk.Usuarios
{
    /// <summary>
    /// Lógica de interacción para usuarioVista.xaml
    /// </summary>
    public partial class hdk_ViewUsuario : Window
    {
        hdk_ControlUsuario cu;
        hdk_ControlAcceso control;

        public hdk_ViewUsuario(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            control = ca;
            cu = new hdk_ControlUsuario(control);
            DataG.ItemsSource = cu.cargarTabla("");
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewAltaUsuario vau = new hdk_ViewAltaUsuario(control);
            vau.ShowDialog();
            DataG.ItemsSource = cu.cargarTabla(filtro.Text);
        }

        private void Actualizar_Click(object sender, RoutedEventArgs e)
        {
            object item = DataG.SelectedItem;
            if (item != null)
            {
                hdk_ControlDepartamento cd = new hdk_ControlDepartamento(control);
                hdk_ViewAltaUsuario vau = new hdk_ViewAltaUsuario(control);
                vau.pasarDatos(item);
                vau.ShowDialog();
                DataG.ItemsSource = cu.cargarTabla(filtro.Text);
            }
            else
            {
                MessageBox.Show("Seleccione una fila");
            }
        }

  /*      private void Borrar_Click(object sender, RoutedEventArgs e)
        {
            object item = DataG.SelectedItem;
            if (item != null)
            {
                //Se obtiene el valor del campo id
                string str = ((DataG.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                int id = Convert.ToInt32(str);
                MessageBox.Show(str);
                if (MessageBox.Show("¿Estas seguro que desas eliminar?", "AVISO", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (cu.borrarRegistro(id))
                    {
                        MessageBox.Show("Se borro el registro");
                        DataG.ItemsSource = cu.cargarTabla(filtro.Text);
                    }
                    else
                    {
                        MessageBox.Show("Usuario asociado a un evento o incidente");
                    }
                    
                }
            }
        }*/


        private void filtro_KeyUp(object sender, KeyEventArgs e)
        {
            DataG.ItemsSource = cu.cargarTabla(filtro.Text);
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
    }
}

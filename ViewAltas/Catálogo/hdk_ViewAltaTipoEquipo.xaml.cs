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
    /// Lógica de interacción para hdk_ViewAltaTipoEquipo.xaml
    /// </summary>
    public partial class hdk_ViewAltaTipoEquipo : Window
    {

        private hdk_ControlTipoEquipo cte;
        private bool modificar = false;
        private tbltipoequipo itemTipoEquipo;
        hdk_ControlAcceso Control;

        public hdk_ViewAltaTipoEquipo(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            this.cte = new hdk_ControlTipoEquipo(Control);
            txtTipoEquipo.Focus();
        }

        public void pasarDatos(object Item)
        {
            itemTipoEquipo = (tbltipoequipo)Item;
            modificar = true;
            txtTipoEquipo.Text = itemTipoEquipo.nomTipoEquipo;
            equipo.IsChecked = itemTipoEquipo.siEquipo;
            teclado.IsChecked = itemTipoEquipo.siTeclado;
            mouse.IsChecked = itemTipoEquipo.siMouse;
            monitor.IsChecked = itemTipoEquipo.siMonitor;
            dd.IsChecked = itemTipoEquipo.siDiscoDuro;
            red.IsChecked = itemTipoEquipo.siRed;
            ram.IsChecked = itemTipoEquipo.siRAM;
            procesador.IsChecked = itemTipoEquipo.siProcesador;
            btnAñadir.Content = "Modificar";
        }

        private void tipoEq_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void añadir_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtTipoEquipo.Text))
            {
                if (modificar)
                {
                    this.cte.modificar(itemTipoEquipo.idTipoEquipo, txtTipoEquipo.Text, equipo.IsChecked.Value, dd.IsChecked.Value, red.IsChecked.Value, monitor.IsChecked.Value, mouse.IsChecked.Value, teclado.IsChecked.Value, ram.IsChecked.Value, procesador.IsChecked.Value);
                }
                else
                {
                    this.cte.insertar(txtTipoEquipo.Text, equipo.IsChecked.Value, dd.IsChecked.Value, red.IsChecked.Value, monitor.IsChecked.Value, mouse.IsChecked.Value, teclado.IsChecked.Value, ram.IsChecked.Value, procesador.IsChecked.Value);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor introduzca el nombre del tipo de equipo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void chSeleccionar_Checked(object sender, RoutedEventArgs e)
        {
                dd.IsChecked = true;
                red.IsChecked = true;
                ram.IsChecked = true;
                monitor.IsChecked = true;
                procesador.IsChecked = true;
                mouse.IsChecked = true;
                teclado.IsChecked = true;

        }

        private void chSeleccionar_Unchecked(object sender, RoutedEventArgs e)
        {
            dd.IsChecked = false;
            red.IsChecked = false;
            ram.IsChecked = false;
            monitor.IsChecked = false;
            procesador.IsChecked = false;
            mouse.IsChecked = false;
            teclado.IsChecked = false;
        }
    }
}

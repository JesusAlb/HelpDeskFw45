using HelpDesk.Control.Catalogo;
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
using HelpDesk.View.Control;
using HelpDesk.ViewAltas.Control;
using HelpDesk.Principal;
using HelpDesk.ViewReportes;
using System.Collections;

namespace HelpDesk.View.Catalogos
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewEquipos.xaml
    /// </summary>
    public partial class hdk_ViewEquipos : Window
    {
        private hdk_ControlEquipos ce;
        private hdk_ControlAcceso Control;

        public hdk_ViewEquipos(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            ce = new hdk_ControlEquipos(Control);
            DataG.ItemsSource = ce.cargarTabla("");
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

        private void Imp_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewReportEquipos vre = new hdk_ViewReportEquipos(DataG.ItemsSource as IList);
            vre.ShowDialog();
        }

        private void Mod_Click(object sender, RoutedEventArgs e)
        {
            object item = DataG.SelectedItem;
            if (item != null)
            {
                hdk_ViewAltaEquipo vae = new hdk_ViewAltaEquipo(Control);
                vae.pasarDatos(item);
                vae.ShowDialog();
                DataG.ItemsSource = ce.cargarTabla("");
            }
            else
            {
                MessageBox.Show("Seleccione el renglon del elemento a modificar");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewAltaEquipo vae = new hdk_ViewAltaEquipo(Control);
            vae.ShowDialog();
            DataG.ItemsSource = ce.cargarTabla("");

        }

        private void txtFiltro_KeyUp(object sender, KeyEventArgs e)
        {
            DataG.ItemsSource = ce.cargarTabla(txtFiltro.Text);
        }
    }
}

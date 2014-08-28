using HelpDesk.Control.Catalogo;
using HelpDesk.Principal;
using HelpDesk.ViewAltas.Catálogo;
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

namespace HelpDesk.View.CatálogosConjuntados
{
    /// <summary>
    /// Lógica de interacción para hdk_VistaCatalogosEquipos.xaml
    /// </summary>
    public partial class hdk_VistaCatalogosEquipos : Window
    {
        private hdk_ControlTipoEquipo cte;
        private hdk_ControlMarca cm;
        private hdk_ControlAcceso Control;

        public hdk_VistaCatalogosEquipos(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cte = new hdk_ControlTipoEquipo(Control);
            cm = new hdk_ControlMarca(Control);
            dataGTipoEquipo.ItemsSource = cte.cargarTabla("");
            dataGMarca.ItemsSource = cm.cargarTabla("");
        }

        private void cargarTablas(string filtro)
        {
            if (tabTipos.IsSelected)
            {
                dataGTipoEquipo.ItemsSource = cte.cargarTabla(filtro);
            }
            else
            {
                dataGMarca.ItemsSource = cm.cargarTabla(filtro);
            }    
            
        }

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            if (tabTipos.IsSelected)
            {
                hdk_ViewAltaTipoEquipo vate = new hdk_ViewAltaTipoEquipo(Control);
                vate.ShowDialog();
            }
            else if(tabMarcas.IsSelected)
            {
                hdk_ViewAltaMarca vam = new hdk_ViewAltaMarca(Control);
                vam.ShowDialog();               
            }
            cargarTablas("");
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            

            if (tabTipos.IsSelected)
            {
                object Item = dataGTipoEquipo.SelectedItem;
                if (Item != null)
                {
                    hdk_ViewAltaTipoEquipo vate = new hdk_ViewAltaTipoEquipo(Control);
                    vate.pasarDatos(Item);
                    vate.ShowDialog();
                    cargarTablas("");
                }
                else
                {
                    MessageBox.Show("Seleccione el tipo de equipo a modificar");
                }
            }
            else if (tabMarcas.IsSelected)
            {
                object Item = dataGMarca.SelectedItem;
                if (Item != null)
                {
                    hdk_ViewAltaMarca vam = new hdk_ViewAltaMarca(Control);
                    vam.pasarDatos(Item);
                    vam.ShowDialog();
                    cargarTablas("");
                }
                else
                {
                    MessageBox.Show("Seleccione la marca que desee modificar");
                }
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

        private void txtFiltroMarca_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroMarca.Text);
        }

        private void txtFiltroTipos_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroTipos.Text);
        }

        private void textoIntermitente(TextBox txt)
        {
            if (String.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Foreground = Brushes.Gray;
                txt.Text = "Buscar";
            }
            else if(txt.Text.Equals("Buscar")){
                txt.Foreground = Brushes.Black;
                txt.Text = "";
            }
        }

        private void txtFiltroTipos_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroTipos);
        }

        private void txtFiltroTipos_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroTipos);
        }

        private void txtFiltroMarca_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroMarca);
        }

        private void txtFiltroMarca_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroMarca);
        }

    }
}

using HelpDesk.Control.Catalogo;
using HelpDesk.ControlAltas;
using HelpDesk.Principal;
using HelpDesk.ViewAltas;
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
    /// Lógica de interacción para hdk_VistaCatalogosSolicitudes.xaml
    /// </summary>
    public partial class hdk_VistaCatalogosSolicitudes : Window
    {
        private hdk_ControlAcceso Control;
        private hdk_ControlRequerimientos cr;
        private hdk_ControlTipoIncidencia cti;
        private hdk_ControlLugar cl;
        private string filtroTipo = "";

        public hdk_VistaCatalogosSolicitudes(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cr = new hdk_ControlRequerimientos(Control);
            cti = new hdk_ControlTipoIncidencia(Control);
            cl = new hdk_ControlLugar(Control);
            dataGRequerimientos.ItemsSource = cr.cargarTabla("","",null);
            dataGLugar.ItemsSource = cl.cargarTabla("");
            dataGIncidente.ItemsSource = cti.cargarTabla("");
            ChCuantificable.IsChecked = true;
            ChNoCuantificable.IsChecked = true;
        }

        private void cargarTablas(string filtro)
        { 
            if (tabTipoIncidencia.IsSelected)
            {
                this.dataGIncidente.ItemsSource = cti.cargarTabla(filtro);
            }
            else if(tabLugar.IsSelected)
            {
                dataGLugar.ItemsSource = cl.cargarTabla(filtro);
            }
            else if(tabRequerimiento.IsSelected)
            {
                dataGRequerimientos.ItemsSource = cr.cargarTabla(filtro, filtroTipo, null);
            }               
        }


        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            if (tabTipoIncidencia.IsSelected)
            {
                hdk_ViewAltaTipoIncidencia vati = new hdk_ViewAltaTipoIncidencia(Control);
                vati.ShowDialog();
            }
            else if (tabLugar.IsSelected)
            {
                hdk_ViewAltaLugar val = new hdk_ViewAltaLugar(Control);
                val.ShowDialog();
            }
            else if (tabRequerimiento.IsSelected)
            {
                hdk_ViewAltaRequerimiento var = new hdk_ViewAltaRequerimiento(Control);
                var.ShowDialog();
            }
            this.cargarTablas("");
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (tabTipoIncidencia.IsSelected)
            {
                object item = dataGIncidente.SelectedItem;
                if (item != null)
                {
                    hdk_ViewAltaTipoIncidencia vati = new hdk_ViewAltaTipoIncidencia(Control);
                    vati.pasarDatos(item);
                    vati.ShowDialog();
                    cargarTablas("");
                }
                else
                {
                    MessageBox.Show("Seleccione el tipo de incidente a modificar");
                }              
            }
            else if (tabLugar.IsSelected)
            {
                 object item = dataGLugar.SelectedItem;
                 if (item != null)
                 {
                     hdk_ViewAltaLugar val = new hdk_ViewAltaLugar(Control);
                     val.pasarDatos(item);
                     val.ShowDialog();
                     cargarTablas("");
                 }
                 else
                 {
                     MessageBox.Show("Seleccione el lugar a modificar");
                 }
            }
            else if (tabRequerimiento.IsSelected)
            {
                 object item = dataGRequerimientos.SelectedItem;
                 if (item != null)
                 {
                     hdk_ViewAltaRequerimiento var = new hdk_ViewAltaRequerimiento(Control);
                     var.pasarDatos(item);
                     var.ShowDialog();
                     cargarTablas("");
                 }
                 else
                 {
                     MessageBox.Show("Seleccione el requerimiento a modificar");
                 }
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

        private void ChCuantificable_Checked(object sender, RoutedEventArgs e)
        {
            if (ChNoCuantificable.IsChecked.Value)
            {
                filtroTipo = "";
            }
            else
            {
                filtroTipo = "Cuantificable";
            }
            txtFiltroRequerimiento.Focus();
            this.cargarTablas(txtFiltroRequerimiento.Text);
        }

        private void ChNoCuantificable_Checked(object sender, RoutedEventArgs e)
        {
            if (ChCuantificable.IsChecked.Value)
            {
                filtroTipo = "";
            }
            else
            {
                filtroTipo = "No cuantificable";
            }           
            txtFiltroRequerimiento.Focus();
            this.cargarTablas(txtFiltroRequerimiento.Text);
        }

        private void txtFiltroRequerimiento_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroRequerimiento.Text);
        }

        private void txtFiltroTipoIncidente_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroTipoIncidente.Text);
        }

        private void txtFiltroLugar_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroLugar.Text);
        }

        private void textoIntermitente(TextBox txt)
        {
            if (String.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Foreground = Brushes.Gray;
                txt.Text = "Buscar";
            }
            else if (txt.Text.Equals("Buscar"))
            {
                txt.Foreground = Brushes.Black;
                txt.Text = "";
            }
        }

        private void txtFiltroTipoIncidente_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroTipoIncidente);
        }

        private void txtFiltroTipoIncidente_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroTipoIncidente);
        }

        private void txtFiltroLugar_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroLugar);
        }

        private void txtFiltroLugar_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroLugar);
        }

        private void txtFiltroRequerimiento_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroRequerimiento);
        }

        private void txtFiltroRequerimiento_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroRequerimiento);
        }

        private void ChCuantificable_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ChNoCuantificable.IsChecked.Value)
            {
                filtroTipo = "No cuantificable";
            }
            else
            {
                filtroTipo = "Todos";
            }
            txtFiltroRequerimiento.Focus();
            this.cargarTablas(txtFiltroRequerimiento.Text);
        }

        private void ChNoCuantificable_Unchecked(object sender, RoutedEventArgs e)
        {
            if (ChCuantificable.IsChecked.Value)
            {
                filtroTipo = "Cuantificable";
            }
            else
            {
                filtroTipo = "Todos";
            }
            txtFiltroRequerimiento.Focus();
            this.cargarTablas(txtFiltroRequerimiento.Text);
        }
    }
}

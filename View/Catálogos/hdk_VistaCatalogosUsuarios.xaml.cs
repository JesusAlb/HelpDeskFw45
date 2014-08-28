using HelpDesk.Control.Catalogo;
using HelpDesk.ControlAltas;
using HelpDesk.Datos;
using HelpDesk.Modelo;
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
    /// Lógica de interacción para hdk_VistaCatalogosUsuarios.xaml
    /// </summary>
    public partial class hdk_VistaCatalogosUsuarios : Window
    {
        private hdk_ControlAcceso Control;
        private hdk_ControlCoordinacion cc;
        private hdk_ControlDepartamento cd;
        private hdk_ControlArea car;
        private hdk_ControlPuesto cp;
        private string filtroCoord = "";


        public hdk_VistaCatalogosUsuarios(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cc = new hdk_ControlCoordinacion(Control);
            cd = new hdk_ControlDepartamento(Control);
            car = new hdk_ControlArea(Control);
            cp = new hdk_ControlPuesto(Control);
            cd.cargarComboCord(cbFiltroCord);
            dataGArea.ItemsSource = car.cargarTabla("");
            dataGCoord.ItemsSource = cc.cargarTabla("");
            dataGDepto.ItemsSource = cd.cargarTabla("", "");
            dataGPuesto.ItemsSource = cp.cargarTabla("");
        }

        private void cargarTablas(string filtro)
        {
            if (tabArea.IsSelected)
            {
                dataGArea.ItemsSource = car.cargarTabla(filtro);
            }
            else if (tabCoordinacion.IsSelected)
            {
                dataGCoord.ItemsSource = cc.cargarTabla(filtro);
            }
            else if (tabDepto.IsSelected)
            {
                dataGDepto.ItemsSource = cd.cargarTabla(filtro, filtroCoord);
            }
            else if (tabPuesto.IsSelected)
            {
                dataGPuesto.ItemsSource = cp.cargarTabla(filtro);
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

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            if (tabArea.IsSelected)
            {
                hdk_ViewAltaArea vaa = new hdk_ViewAltaArea(Control);
                vaa.ShowDialog();
            }
            else if (tabCoordinacion.IsSelected)
            {
                hdk_ViewAltaCoordinacion vac = new hdk_ViewAltaCoordinacion(Control);
                vac.ShowDialog();
                cd.cargarComboCord(cbFiltroCord);
            }
            else if (tabDepto.IsSelected)
            {
                hdk_ViewAltaDepartamento vad = new hdk_ViewAltaDepartamento(Control);
                vad.ShowDialog();
            }
            else if (tabPuesto.IsSelected)
            {
                hdk_ViewAltaPuesto vap = new hdk_ViewAltaPuesto(Control);
                vap.ShowDialog();
            }
            cargarTablas("");
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (tabArea.IsSelected)
            {
                object item = dataGArea.SelectedItem;
                if(item != null){
                    hdk_ViewAltaArea vaa = new hdk_ViewAltaArea(Control);
                    vaa.pasarDatos(item);
                    vaa.ShowDialog();
                    cargarTablas("");
                }
                else
                {
                    MessageBox.Show("Seleccione la área a modificar");
                }

            }
            else if (tabCoordinacion.IsSelected)
            {
                object item = dataGCoord.SelectedItem;
                if (item != null)
                {
                    hdk_ViewAltaCoordinacion vac = new hdk_ViewAltaCoordinacion(Control);
                    vac.pasarDatos(item);
                    vac.ShowDialog();
                    cargarTablas("");
                    cd.cargarComboCord(cbFiltroCord);
                }
                else
                {
                    MessageBox.Show("Seleccione la área a modificar");
                }
            }
            else if (tabDepto.IsSelected)
            {
                object item = dataGDepto.SelectedItem;
                if (item != null)
                {
                    hdk_ViewAltaDepartamento vad = new hdk_ViewAltaDepartamento(Control);
                    vad.pasarDatos(item);
                    vad.ShowDialog();
                    cargarTablas("");
                }
                else
                {
                    MessageBox.Show("Seleccione la área a modificar");
                }
            }
            else if (tabPuesto.IsSelected)
            {
                object item = dataGPuesto.SelectedItem;
                if (item != null)
                {
                    hdk_ViewAltaPuesto vap = new hdk_ViewAltaPuesto(Control);
                    vap.pasarDatos(item);
                    vap.ShowDialog();
                    cargarTablas("");
                }
                else
                {
                    MessageBox.Show("Seleccione la área a modificar");
                }
            }
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void txtFiltroPuesto_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroPuesto.Text);
        }

        private void txtFiltroArea_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroArea.Text);
        }

        private void txtFiltroDepto_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroDepto.Text);
        }

        private void cbFiltroCord_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.filtroCoord = (((sender as ComboBox).SelectedItem as tblcoordinacion).nomCoordinacion);
            txtFiltroDepto.Focus();
            this.cargarTablas(txtFiltroDepto.Text);
        }

        private void txtFiltroCord_KeyUp(object sender, KeyEventArgs e)
        {
            this.cargarTablas(txtFiltroCoord.Text);
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

        private void txtFiltroCoord_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroCoord);
        }

        private void txtFiltroCoord_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroCoord);
        }

        private void txtFiltroDepto_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroDepto);
        }

        private void txtFiltroDepto_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroDepto);
        }

        private void txtFiltroArea_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroArea);
        }

        private void txtFiltroArea_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroArea);
        }

        private void txtFiltroPuesto_GotFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroPuesto);
        }

        private void txtFiltroPuesto_LostFocus(object sender, RoutedEventArgs e)
        {
            this.textoIntermitente(txtFiltroPuesto);
        }

    }
}

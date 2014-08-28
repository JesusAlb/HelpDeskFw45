using HelpDesk.Control.Catalogo;
using HelpDesk.Control.Solicitudes;
using HelpDesk.Control.Solicitudes.Incidentes;
using HelpDesk.Modelo;
using HelpDesk.Principal;
using HelpDesk.View.Control;
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

namespace HelpDesk.ViewAltas
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewAsignarSoporte.xaml
    /// </summary>
    public partial class hdk_ViewAsignarSoporte : Window
    {
        private hdk_ControlIncidentes ci;
        private hdk_ControlEventos ce;
        private hdk_ControlAcceso Control;
        private hdk_ControlUsuario cu;
        private int fuente;
        private int id;
        private bool modificar = false;
        private int soporte = 0;
        private int seguimiento = 0;

        public hdk_ViewAsignarSoporte(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cu = new hdk_ControlUsuario(Control);
            ci = new hdk_ControlIncidentes(Control);
            cu.cargarComboUsuarios(cbSoporte, 0);
            cu.cargarComboUsuarios(cbApoyo, 0);
        }

        public void pasarDatos(object item, int origen)
        {
            fuente = origen;

            if (fuente == 0)
            {
                VistaIncidente vistaIncidente = (VistaIncidente)item;
                cbApoyo.SelectedValue = seguimiento = vistaIncidente.idSeguimiento;
                cbSoporte.SelectedValue = soporte = vistaIncidente.idSoporte;
                id = vistaIncidente.numIncidente;
            }
            else{
                ce = new hdk_ControlEventos(Control);
                Soporte.Content = "Responsable";
                Seguimiento.Content = "Apoyo";
               VistaEvento vistaEvento = (VistaEvento)item;
                cbApoyo.SelectedValue = seguimiento = vistaEvento.idApoyo;
                cbSoporte.SelectedValue = seguimiento = vistaEvento.idResponsable;
                id = vistaEvento.idEvento;
            }
        }


        private void Asig_Click(object sender, RoutedEventArgs e)
        {
            if(seguimiento != (int)cbApoyo.SelectedValue || soporte != (int)cbSoporte.SelectedValue){
                bool asigacion = false;

                if (!cbSoporte.Text.Equals("S/A"))
                {
                    int sopo = Convert.ToInt32(cbSoporte.SelectedValue);
                    int seg = Convert.ToInt32(cbApoyo.SelectedValue);
                    if (fuente == 0)
                    {
                        if (ci.asignarSoporte(id, sopo, seg))
                            asigacion = true;
                    }
                    else
                    {
                        if (ce.asignarResponsable(id, sopo, seg))
                            asigacion = true;
                    }
                    if (asigacion)
                    {
                            MessageBox.Show("Asignación exitosa");
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Especifique al menos al usuario que brindará el soporte");
                }
            }
            else
            {
                this.Close();
            }
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

        private void cbSoporte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.cbApoyo.Focus();
            }
        }

        private void cbApoyo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Asig_Click(sender, e);
            }
        }

        private void DetalleSop_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewUsuarioDetalle vud = new hdk_ViewUsuarioDetalle(Convert.ToInt32(cbSoporte.SelectedValue), Control);
            vud.ShowDialog();
        }

        private void DetalleSeg_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewUsuarioDetalle vud = new hdk_ViewUsuarioDetalle(Convert.ToInt32(cbApoyo.SelectedValue), Control);
            vud.ShowDialog();
        }

        private void cbSoporte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewUsuario usuario = (ViewUsuario)(sender as ComboBox).SelectedItem;
            if (!usuario.nomUsuario.Equals("S/A"))
            {
                DetalleSop.IsEnabled = true;
            }
            else
            {
                DetalleSop.IsEnabled = false;
            }
        }

        private void cbApoyo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ViewUsuario usuario = (ViewUsuario)(sender as ComboBox).SelectedItem;
            if (!usuario.nomUsuario.Equals("S/A"))
            {
                DetalleSeg.IsEnabled = true;
            }
            else
            {
                DetalleSeg.IsEnabled = false;
            }
        }

    }
}

using HelpDesk.Control.Solicitudes;
using HelpDesk.Control.Solicitudes.Incidentes;
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

namespace HelpDesk.ViewReportes
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewMenuReporteEvento.xaml
    /// </summary>
    public partial class hdk_ViewMenuReporte : Window
    {
        int fuente;
        hdk_ControlAcceso Control;
        hdk_ControlIncidentes ci;
        hdk_ControlEventos ce;

        public hdk_ViewMenuReporte(int f, hdk_ControlAcceso ca)
        {
            InitializeComponent();
            fuente = f;
            Control = ca;
            if (fuente == 0)
            {
                this.Title = "Help Desk - Imprimir incidentes";
                ci = new hdk_ControlIncidentes(Control);
            }
            else
            {
                ce = new hdk_ControlEventos(Control);
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

        private void Imprimir_Click(object sender, RoutedEventArgs e)
        {

            if (status.SelectedIndex != -1)
            {

                if (fuente == 0)
                {
                    hdk_ViewReportIncidentes ri;

                    if (Control.item.tipoUsuario == 0)
                    {
                        ri = new hdk_ViewReportIncidentes(ci.cargarTablaSoporte(status.SelectedIndex, "","",null,null));
                    }
                    else
                    {
                        ri = new hdk_ViewReportIncidentes(ci.cargarTablaSolicitante(Control.item.idUsuario,"",status.SelectedIndex, "", null, null));
                    }
                    
                    ri.ShowDialog();
                }
                else
                {
                    hdk_ViewReportEventos re;

                    if (Control.item.tipoUsuario == 0)
                    {
                        re = new hdk_ViewReportEventos(ce.cargarTablasSoporte(status.SelectedIndex,"",null, null));
                    }
                    else
                    {
                        re = new hdk_ViewReportEventos(ce.cargarTablasSolicitante(Control.item.idUsuario,status.SelectedIndex, "", null, null));
                    }
                    re.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione el estatus");
            }
        }

        private void status_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Imprimir_Click(sender, e);
            }
        }
    }
}

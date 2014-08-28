using HelpDesk.Principal;
using HelpDesk.ViewReportes.Eventos;
using HelpDesk.ViewReportes.Incidentes;
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
    /// Lógica de interacción para hdk_ViewMenuEstadisticas.xaml
    /// </summary>
    public partial class hdk_ViewMenuEstadisticas : Window
    {

        private hdk_ControlAcceso Control;

        public hdk_ViewMenuEstadisticas(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;

        }

        private void btnGenerar_Click(object sender, RoutedEventArgs e)
        {
            if (cbTema.SelectedIndex != -1)
            {
                switch (cbTema.SelectedIndex)
                {
                    case 0:
                        {
                            hdk_ViewEstadisticasIncidencia vei = new hdk_ViewEstadisticasIncidencia(new DateTime(2014,1,1), DateTime.Today,Control);
                            vei.ShowDialog();
                        } break;
                    case 1:
                        {
                            hdk_ViewEstadisticasEventos vei = new hdk_ViewEstadisticasEventos(new DateTime(2014, 1, 1), DateTime.Today, Control);
                            vei.ShowDialog();
                        }break;
                    case 2:
                        {
                            hdk_ViewEstadisticasCalidadIncidentes vei = new hdk_ViewEstadisticasCalidadIncidentes(new DateTime(2014, 1, 1), DateTime.Today, Control);
                            vei.ShowDialog();
                        }break;
                    case 3:
                        {
                            hdk_ViewEstadisticasCalidadEventos vei = new hdk_ViewEstadisticasCalidadEventos(new DateTime(2014, 1, 1), DateTime.Today, Control);
                            vei.ShowDialog();
                        }break;

                }
            }else{
                MessageBox.Show("Seleccione el informe grafico que desea generar");
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
    }
}

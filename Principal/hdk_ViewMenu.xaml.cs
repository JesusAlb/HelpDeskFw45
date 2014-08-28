
using HelpDesk.Control.Catalogo;
using HelpDesk.Control.Solicitudes;
using HelpDesk.Control.Solicitudes.Incidentes;
using HelpDesk.Eventos;
using HelpDesk.Usuarios;
using HelpDesk.View.Catalogos;
using HelpDesk.View.CatálogosConjuntados;
using HelpDesk.View.Solicitudes.Eventos;
using HelpDesk.ViewAltas;
using HelpDesk.ViewReportes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HelpDeskApp;

namespace HelpDesk.Principal
{
    /// <summary>
    /// Lógica de interacción para VentanaPrincipal.xaml
    /// </summary>
    public partial class hdk_ViewMenu : Window
    {

    public int tipoUsuario=0;
    private hdk_ControlAcceso Control;
    private hdk_ControlIncidentes ci;
    private hdk_ControlEncuestas ce;
    private bool conexion = true;
    private NotifyIcon nIcon;
    private int contador = 0;

        public hdk_ViewMenu(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            NomUs.Content = Control.item.nomCompleto;

            if (Control.item.tipoUsuario == 0)
            {
                ci = new hdk_ControlIncidentes(Control);

            }
            else
            {
                ce = new hdk_ControlEncuestas(Control);
                Cat.IsEnabled = false;
                Equipos.IsEnabled = false;
                Rep.Items.RemoveAt(3);
                Rep.Items.RemoveAt(2);
                Con.Items.RemoveAt(1);
                this.Title = "Help Desk - Menú principal - Solicitante";
                Usuarios.Header = "Perfil de usuario";
                lbelIncidentesEspera.Content = "Encuestas en espera:";
                lbelNumeroIncidentes.Content = "0";
                lbelTipoUsuario.Content = "Solicitante";
            }

            this.manejadorNotifyIcon(); 

        }

        private void manejadorNotifyIcon()
        {
            try
            {
                this.nIcon = new System.Windows.Forms.NotifyIcon();
                this.nIcon.BalloonTipText = "La Aplicación se encuentra ejecutando";
                this.nIcon.BalloonTipTitle = "Help desk - Esperando solicitudes";
                this.nIcon.Text = "Presione para Mostrar";
                // this.nIcon.Icon = new Icon("../../Imagenes/imca.ico");
                this.nIcon.Icon = new Icon("./imca.ico");
                this.nIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
                this.nIcon.Click += new EventHandler(iconoNotificacion_Click);
                System.Windows.Forms.ContextMenu menu = new System.Windows.Forms.ContextMenu();
                System.Windows.Forms.MenuItem item1 = new System.Windows.Forms.MenuItem("Mostrar aplicación");
                item1.Click += new EventHandler(item1_Click);
                System.Windows.Forms.MenuItem item2 = new System.Windows.Forms.MenuItem("Mostrar incidentes");
                item2.Click += new EventHandler(item2_Click);
                System.Windows.Forms.MenuItem item3 = new System.Windows.Forms.MenuItem("Mostrar eventos");
                item3.Click += new EventHandler(item3_Click);
                System.Windows.Forms.MenuItem item4 = new System.Windows.Forms.MenuItem("Cerrar");
                item4.Click += new EventHandler(item4_Click);
                menu.MenuItems.Add(item1);
                menu.MenuItems.Add(item2);
                menu.MenuItems.Add(item3);
                menu.MenuItems.Add(item4);
                this.nIcon.ContextMenu = menu;
            }
            catch
            {

            }
        }

        private void iconoNotificacion_Click(object sender, EventArgs e)
        {
            this.Show();
            WindowState = System.Windows.WindowState.Normal;
        }

        void item4_Click(object sender, EventArgs e)
        {
            this.nIcon = null;
            this.Close();
        }

        void item3_Click(object sender, EventArgs e)
        {
            hdk_ViewEvento ve = new hdk_ViewEvento(Control);
            ve.ShowDialog();
        }

        void item2_Click(object sender, EventArgs e)
        {
            hdk_ViewIncidentes vi = new hdk_ViewIncidentes(Control);
            vi.ShowDialog();
        }

        void item1_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = System.Windows.WindowState.Normal;
        }

        private void Usu_Click(object sender, RoutedEventArgs e)
        {
            if (Control.item.tipoUsuario == 0)
            {
                hdk_ViewUsuario uv = new hdk_ViewUsuario(Control);
                uv.ShowDialog();
            }
            else if(Control.item.tipoUsuario == 1)
            {
                hdk_ViewAltaUsuario uv = new hdk_ViewAltaUsuario(Control);
                uv.pasarDatos(Control.item);
                uv.ShowDialog();
            }
        }

        private void iMenuEvento_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewEvento ve = new hdk_ViewEvento(Control);
            ve.ShowDialog();
        }

        private void incidentes_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewIncidentes vi = new hdk_ViewIncidentes(Control);
            vi.ShowDialog();
        }

        private void atEventos_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewMenuReporte vmr = new hdk_ViewMenuReporte(1, Control);
            vmr.ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            hdk_ViewMenuReporte vmr = new hdk_ViewMenuReporte(0, Control);
            vmr.ShowDialog();
        }

        private void CambiarUs_Click(object sender, RoutedEventArgs e)
        {
            this.nIcon = null;
            Control = null;
            hdk_ViewAcceso va = new hdk_ViewAcceso();
            NomUs.Content = "Sesión no iniciada";
            Cat.IsEnabled = false;
            Con.IsEnabled = false;
            Sol.IsEnabled = false;
            Rep.IsEnabled = false;
            va.ShowDialog();
            this.Close();
            
        }

        private void minButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            if(System.Windows.MessageBox.Show("¿Desea mantener la aplicación en ejecución?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                this.nIcon = null;
                this.Close();
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Minimized;
            }
        }

        private void titleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void rEquipos_Click(object sender, RoutedEventArgs e)
        {
            hdk_ControlEquipos ce = new hdk_ControlEquipos(Control);
            hdk_ViewReportEquipos vre = new hdk_ViewReportEquipos(ce.cargarTabla(""));
            vre.ShowDialog();
        }

        private void Equipos_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewEquipos ve = new hdk_ViewEquipos(Control);
            ve.ShowDialog();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (Control != null && conexion == true)
            {
                Control.actualizarModelo();
                if (Control.item.tipoUsuario == 0)
                {
                    int num = ci.obtenerNumeroIncidentes();
                    if (num == -1)
                    {
                        conexion = false;
                        num = contador;
                    }
                    if (num != contador)
                    {
                        contador = num;
                        if (num > 0)
                        {
                            lbelNumeroIncidentes.Foreground = new SolidColorBrush(Colors.Red);
                            lbelNumeroIncidentes.Content = "" + num;
                            if (WindowState == WindowState.Minimized)
                            {
                                this.Show();
                            }
                                System.Windows.MessageBox.Show("Hay " + num + " incidente(s) sin responder", "ATENCIÓN", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.btnIncidentes.Focus();                                                                                
                        }
                        else
                        {
                            lbelNumeroIncidentes.Foreground = new SolidColorBrush(Colors.Green);
                            lbelNumeroIncidentes.Content = "" + num;
                        }
                    }
                }
                else if (Control.item.tipoUsuario == 1)
                {
                    int numEnIn = ce.obtenerNumeroEncuestasIn(Control.item.idUsuario);
                    int encuestasTotales;
                    int numEnEv;
                    if (numEnIn == -1)
                    {
                        conexion = false;
                        encuestasTotales = contador;
                    }
                    else
                    {
                        numEnEv = ce.obtenerNumeroEncuestasEv(Control.item.idUsuario);
                        encuestasTotales = numEnEv + numEnIn;
                    }
                    if (encuestasTotales != contador)
                    {
                        contador = numEnIn;
                        if (encuestasTotales > 0)
                        {
                            lbelNumeroIncidentes.Foreground = new SolidColorBrush(Colors.Red);
                            lbelNumeroIncidentes.Content = "" + encuestasTotales;
                        }
                        else
                        {
                            lbelNumeroIncidentes.Foreground = new SolidColorBrush(Colors.Green);
                            lbelNumeroIncidentes.Content = "" + numEnIn;
                        }
                    }
                }
            }
        }

        private void btnIncidentes_Click(object sender, RoutedEventArgs e)
        {
            if (Control.item.tipoUsuario == 0)
            {
                hdk_ViewIncidentes ci = new hdk_ViewIncidentes(Control);
                ci.ShowDialog();
            }
        }

        private void itParaUsuario_Click(object sender, RoutedEventArgs e)
        {
            hdk_VistaCatalogosUsuarios vcu = new hdk_VistaCatalogosUsuarios(Control);
            vcu.ShowDialog();
        }

        private void itParaSolicitudes_Click(object sender, RoutedEventArgs e)
        {
            hdk_VistaCatalogosSolicitudes vcs = new hdk_VistaCatalogosSolicitudes(Control);
            vcs.ShowDialog();
        }

        private void itParaEquipos_Click(object sender, RoutedEventArgs e)
        {
            hdk_VistaCatalogosEquipos  vce = new hdk_VistaCatalogosEquipos(Control);
            vce.ShowDialog();
        }

        private void itEstadisticas_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewMenuEstadisticas vme = new hdk_ViewMenuEstadisticas(Control);
            vme.ShowDialog();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                if (this.nIcon != null)
                    this.nIcon.ShowBalloonTip(2000);
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            VerificarIcono();
        }

        private void VerificarIcono()
        {
            MostrarIcono(!IsVisible);
        }

        private void MostrarIcono(bool mostrar)
        {
            if (this.nIcon != null)
                this.nIcon.Visible = mostrar;
        }
    }
}

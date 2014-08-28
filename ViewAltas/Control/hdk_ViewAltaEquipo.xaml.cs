using HelpDesk.Control.Catalogo;
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

namespace HelpDesk.ViewAltas.Control
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewAltaEquipo.xaml
    /// </summary>
    public partial class hdk_ViewAltaEquipo : Window
    {
        private bool modificar = false;
        private int idEquipo;
        private string mac;
        private string ip;
        private string RAM = "S/A";
        private string DD = "S/A";
        private string Pro = "S/A";
        hdk_ControlEquipos ce;
        hdk_ControlTipoEquipo cte;
        hdk_ControlMarca cm;
        hdk_ControlAcceso control;
        ComboBox[] Marcas;
        TextBox[] txtStrings;
        TextBox[] txtIP;
        TextBox[] txtMAC;
        VistaEquipoReporte itEquipos = null;


        public hdk_ViewAltaEquipo(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            control = ca;
            cargarCombos();

        }


        private void cargarCombos()
        {
            ce = new hdk_ControlEquipos(control);
            cm = new hdk_ControlMarca(control);
            hdk_ControlUsuario cu = new hdk_ControlUsuario(control);
            cte = new hdk_ControlTipoEquipo(control);
            cm.cargarCombo(cbEquipo, 1);
            cm.cargarCombo(cbMonitor, 1);
            cm.cargarCombo(cbMouse, 1);
            cm.cargarCombo(cbTeclado, 1);
            cte.cargarCombo(cbTipo);
            cu.cargarComboUsuarios(cbResponsable,2);
            cbMedida.Items.Add("GB");
            cbMedida.Items.Add("TB");
            cbMedida.SelectedIndex = 0;
        }

        public void pasarDatos(object item)
        {
            modificar = true;
            btnAñadir.Content = "Aceptar";
            itEquipos = (VistaEquipoReporte)item;
            idEquipo = itEquipos.idResponEq;
            cbTipo.Text = itEquipos.nomTipoEquipo;
            cbEquipo.Text = itEquipos.nomMarcaEquipo;
            cbMonitor.Text = itEquipos.nomMarcaMonitor;
            cbMouse.Text = itEquipos.nomMarcaMouse;
            cbResponsable.Text = itEquipos.nomCompleto;
            cbTeclado.Text = itEquipos.nomMarcaTeclado;

            string[] Partes = itEquipos.discoDuro.Split(' ');
            txtDD.Text = Partes[0];
            if (txtDD.IsEnabled)
            {
                cbMedida.Text = Partes[1];
            }

            Partes = itEquipos.memoriaRam.Split(' ');
            txtRAM.Text = Partes[0];

            Partes = itEquipos.procesador.Split(' ');
            txtProcesador.Text = Partes[0];

            txtEquipo.Text = itEquipos.serieEquipo;
            txtMonitor.Text = itEquipos.serieMonitor;
            txtMouse.Text = itEquipos.serieMouse;
            txtTeclado.Text = itEquipos.serieTeclado;
            String[] redPartes = itEquipos.ip.Split('.');
            txtIp1.Text = redPartes[0];
            txtIp2.Text = redPartes[1];
            txtIp3.Text = redPartes[2];
            txtIp4.Text = redPartes[3];
            redPartes = itEquipos.mac.Split('-');
            txtMac1.Text = redPartes[0];
            txtMac2.Text = redPartes[1];
            txtMac3.Text = redPartes[2];
            txtMac4.Text = redPartes[3];
            txtMac5.Text = redPartes[4];
            txtMac6.Text = redPartes[5];
            

        }

        public void pasarResponsable(string nombre)
        {
            cbResponsable.Text = nombre;
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

        private void tipoEquipoSeleccionado()
        {
            tbltipoequipo tipoEquipo = (tbltipoequipo)cbTipo.SelectedItem;
            cbEquipo.IsEnabled = tipoEquipo.siEquipo;
            txtEquipo.IsEnabled = tipoEquipo.siEquipo;
            txtDD.IsEnabled = tipoEquipo.siDiscoDuro;
            cbMonitor.IsEnabled = tipoEquipo.siMonitor;
            txtMonitor.IsEnabled = tipoEquipo.siMonitor;
            cbMouse.IsEnabled = tipoEquipo.siMouse;
            txtMouse.IsEnabled = tipoEquipo.siMouse;
            txtIp1.IsEnabled = tipoEquipo.siRed;
            txtIp2.IsEnabled = tipoEquipo.siRed;
            txtIp3.IsEnabled = tipoEquipo.siRed;
            txtIp4.IsEnabled = tipoEquipo.siRed;
            txtMac1.IsEnabled = tipoEquipo.siRed;
            txtMac2.IsEnabled = tipoEquipo.siRed;
            txtMac3.IsEnabled = tipoEquipo.siRed;
            txtMac4.IsEnabled = tipoEquipo.siRed;
            txtMac5.IsEnabled = tipoEquipo.siRed;
            txtMac6.IsEnabled = tipoEquipo.siRed;
            cbTeclado.IsEnabled = tipoEquipo.siTeclado;
            txtTeclado.IsEnabled = tipoEquipo.siTeclado;
            txtProcesador.IsEnabled = tipoEquipo.siProcesador;
            txtRAM.IsEnabled = tipoEquipo.siRAM;

            ComboBox[] AMar = { cbMonitor, cbMouse, cbTeclado };
            TextBox[] AtxtStr = { txtDD, txtRAM, txtProcesador, txtMonitor, txtMouse, txtTeclado };
            TextBox[] Atxtip = { txtIp1, txtIp2, txtIp3, txtIp4 };
            TextBox[] Atxtmac = { txtMac1, txtMac2, txtMac3, txtMac4, txtMac5, txtMac6 };

            Marcas = AMar;
            txtStrings = AtxtStr;
            txtIP = Atxtip;
            txtMAC = Atxtmac;

            for (int x = 0; x < Marcas.Length; x++)
            {
                if (!Marcas[x].IsEnabled)
                {
                    cm.cargarCombo(Marcas[x], 0);
                    Marcas[x].Text = "N/A";
                }
                else
                {
                    cm.cargarCombo(Marcas[x], 1);
                }
            }

            for (int x = 0; x < txtStrings.Length; x++)
            {
                if (!txtStrings[x].IsEnabled)
                {
                    txtStrings[x].Text = "N/A";
                }
                else
                {
                    txtStrings[x].Text = "";
                }
            }
        }

        private void cbTipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.tipoEquipoSeleccionado();
        }

        private bool validarDatos()
        {
            if (!String.IsNullOrEmpty(cbResponsable.Text) && !String.IsNullOrEmpty(cbTipo.Text) && !String.IsNullOrEmpty(cbEquipo.Text) && !String.IsNullOrWhiteSpace(txtEquipo.Text))
            {
                if (txtMAC[0].IsEnabled)
                {
                    for (int x = 0; x < txtMAC.Length; x++)
                    {
                        if (!String.IsNullOrWhiteSpace(txtMAC[x].Text))
                        {
                            if (x == txtMAC.Length - 1)
                            {
                                mac = mac + txtMAC[x].Text;
                            }
                            else
                            {
                                mac = mac + txtMAC[x].Text + "-";
                            }
                        }
                        else
                        {
                            ip = "";
                            mac = "";
                            MessageBox.Show("Llene correctamente la dirección MAC", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return false;
                        }
                    }
                    //Fin del ciclo de validación MAC

                    for (int x = 0; x < txtIP.Length; x++)
                    {
                        if (!String.IsNullOrWhiteSpace(txtIP[x].Text))
                        {
                            if (x == txtIP.Length - 1)
                            {
                                ip = ip + txtIP[x].Text;
                            }
                            else
                            {
                                ip = ip + txtIP[x].Text + ".";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Llene correctamente la dirección IP", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            ip = "";
                            mac = "";
                            return false;
                        }
                    }

                    if (ip.Equals("0.0.0.0") || mac.Equals("00-00-00-00-00-00"))
                    {
                        MessageBox.Show("Llene correctamente las direcciones de red", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    //Fin del ciclo de validación IP
                }
                else {
                    ip = "0.0.0.0";
                    mac = "00-00-00-00-00-00";
                }
                    

                    for (int x = 0; x < txtStrings.Length; x++)
                    {
                        if (txtStrings[x].IsEnabled)
                        {
                            if (String.IsNullOrWhiteSpace(txtStrings[x].Text))
                            {
                                MessageBox.Show("Llene todos los campos disponibles o escriba el valor por defecto S/A", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                            else
                            {
                                if (x==0)
                                {
                                    DD = txtStrings[x].Text + " " + cbMedida.Text;
                                }
                                else if (x == 1)
                                {
                                    RAM = txtStrings[x].Text + " GB";
                                }
                                else if (x == 2)
                                {
                                   Pro = txtStrings[x].Text + " GHz";
                                }

                            }
                        }
                    }

                    for (int x = 0; x < Marcas.Length; x++)
                    {
                        if (Marcas[x].IsEnabled)
                        {
                            if (Marcas[x].SelectedIndex == -1)
                            {
                                MessageBox.Show("Llene todos los campos de marcas disponibles", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return false;
                            }
                            
                        }
                    }
                    return true;
                }
            
            else
            {
                MessageBox.Show("Llene los campos obligatorios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Añadir_Click(object sender, RoutedEventArgs e)
        {
            if (this.validarDatos())
            {   
                if (modificar && btnAñadir.Content.Equals("Modificar"))
                {
                    ce.modificar(idEquipo, Convert.ToInt32(cbResponsable.SelectedValue), DD, ip, mac, Convert.ToInt32(cbEquipo.SelectedValue), Convert.ToInt32(cbMonitor.SelectedValue), Convert.ToInt32(cbMouse.SelectedValue), Convert.ToInt32(cbTeclado.SelectedValue), RAM, Pro, txtEquipo.Text, txtMonitor.Text, txtMouse.Text, txtTeclado.Text, Convert.ToInt32(cbTipo.SelectedValue));
                }
                else if (btnAñadir.Content.Equals("Añadir"))
                {
                    ce.insertar(Convert.ToInt32(cbResponsable.SelectedValue), DD, ip, mac, Convert.ToInt32(cbEquipo.SelectedValue), Convert.ToInt32(cbMonitor.SelectedValue), Convert.ToInt32(cbMouse.SelectedValue), Convert.ToInt32(cbTeclado.SelectedValue), RAM, Pro, txtEquipo.Text, txtMonitor.Text, txtMouse.Text, txtTeclado.Text, Convert.ToInt32(cbTipo.SelectedValue));
                }
                
                this.Close();
            }
        }

        private void seleccionarTexto(TextBox txt)
        {
            txt.SelectAll();
        }

        private void txtRed_GotFocus(object sender, RoutedEventArgs e)
        {
            this.seleccionarTexto(sender as TextBox);
        }

        private void txtEquipo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtMonitor_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtTeclado_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtMouse_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtDD_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtRAM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtIp1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void txtIp1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtMac1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }

        }

        private void txtMac1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }

        }

        private void txtDD_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtRAM_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void txtProcesador_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }

        }

        private void verificarEdicion(string e1, string e2)
        {           
                if (!e1.Equals(e2))
                {
                    btnAñadir.Content = "Modificar";
                }           
        }

        private void cbResponsable_LostFocus(object sender, RoutedEventArgs e)
        {
            if (itEquipos != null)      
                this.verificarEdicion(itEquipos.nomCompleto, cbResponsable.Text);   
        }

        private void cbTipo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (itEquipos != null)    
            this.verificarEdicion(itEquipos.nomTipoEquipo, cbTipo.Text);
        }

        private void cbEquipo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (itEquipos != null)    
            this.verificarEdicion(itEquipos.nomMarcaEquipo, cbEquipo.Text);
        }

        private void cbMonitor_LostFocus(object sender, RoutedEventArgs e)
        {
            if (itEquipos != null)    
            this.verificarEdicion(itEquipos.nomMarcaMonitor, cbMonitor.Text);
        }

        private void cbTeclado_LostFocus(object sender, RoutedEventArgs e)
        {
            if (itEquipos != null)    
            this.verificarEdicion(itEquipos.nomMarcaTeclado, cbTeclado.Text);
        }

        private void cbMouse_LostFocus(object sender, RoutedEventArgs e)
        {
            if (itEquipos != null)    
            this.verificarEdicion(itEquipos.nomMarcaMouse, cbMouse.Text);
        }

        private void txtProcesador_KeyUp(object sender, KeyEventArgs e)
        {
            if (itEquipos != null)
                this.verificarEdicion(itEquipos.procesador, txtProcesador.Text);
        }

        private void txtRAM_KeyUp(object sender, KeyEventArgs e)
        {
            if (itEquipos != null)
                this.verificarEdicion(itEquipos.memoriaRam, txtRAM.Text);
        }

        private void txtDD_KeyUp(object sender, KeyEventArgs e)
        {
            if (itEquipos != null)
                this.verificarEdicion(itEquipos.discoDuro, txtDD.Text);
        }

        private void txtEquipo_KeyUp(object sender, KeyEventArgs e)
        {
            if (itEquipos != null)
                this.verificarEdicion(itEquipos.serieEquipo, txtEquipo.Text);
        }

        private void txtMonitor_KeyUp(object sender, KeyEventArgs e)
        {
            if (itEquipos != null)
                this.verificarEdicion(itEquipos.serieMonitor, txtMonitor.Text);
        }

        private void txtTeclado_KeyUp(object sender, KeyEventArgs e)
        {
            if (itEquipos != null)
                this.verificarEdicion(itEquipos.serieTeclado, txtTeclado.Text);
        }

        private void txtMouse_KeyUp(object sender, KeyEventArgs e)
        {
            if (itEquipos != null)
                this.verificarEdicion(itEquipos.serieMouse, txtMouse.Text);
        }

        private void txtIp1_KeyUp(object sender, KeyEventArgs e)
        {
            if (itEquipos != null)
                btnAñadir.Content = "Modificar";
        }

    }

}

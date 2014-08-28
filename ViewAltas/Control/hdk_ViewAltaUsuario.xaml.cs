using HelpDesk.Control.Catalogo;
using HelpDesk.ControlAltas;
using HelpDesk.Datos;
using HelpDesk.Modelo;
using HelpDesk.Principal;
using HelpDesk.ViewAltas.Catálogo;
using HelpDesk.ViewAltas.Control;
using System;
using System.Collections;
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

    public partial class hdk_ViewAltaUsuario : Window
    {
        private bool modificar = false;
        private int idUsuario;
        private hdk_ControlDepartamento cd;
        private hdk_ControlUsuario cu;
        private hdk_ControlArea ca;
        private hdk_ControlPuesto cp;
        private hdk_ControlAcceso control;
        private ViewUsuario usuario;

        public hdk_ViewAltaUsuario(hdk_ControlAcceso cac)
        {
            InitializeComponent();
            control = cac;
            cd = new hdk_ControlDepartamento(control);
            ca = new hdk_ControlArea(control);
            cp = new hdk_ControlPuesto(control);
            cargarCBTipo();          
            cd.cargarComboCord(cbCoordinacion);
            ca.cargarCombo(cbArea);
            cp.cargarCombo(cbPuesto);
            cu = new hdk_ControlUsuario(control);
            cu.cargarComboInstitucion(cbInstitucion);
            cbInstitucion.SelectedIndex = 0;
            if (control.item.tipoUsuario == 1)
            {
                AddArea.Visibility = System.Windows.Visibility.Hidden;
                AddCord.Visibility = System.Windows.Visibility.Hidden;
                AddDep.Visibility = System.Windows.Visibility.Hidden;
                AddPuesto.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void cargarCBTipo()
        {
            cbTipo.Items.Add("Soporte");
            cbTipo.Items.Add("Solicitante");
        }

        public void pasarDatos(object Item)
        {
            modificar = true;
            chEquipo.Visibility = System.Windows.Visibility.Hidden;
            usuario = (ViewUsuario)Item;
            idUsuario = usuario.idUsuario;
            txtUsuario.Text = usuario.nomUsuario;
            txtNombre.Text = usuario.nomCompleto;
            txtPassword.Password = usuario.password;
            cbTipo.Text = usuario.tipoUsuarioString;
            cbCoordinacion.Text = usuario.nomCoordinacion;
            txtExTel.Text = usuario.exTel;
            string[] split = usuario.correo.Split('@');
            txtCorreo.Text = split[0];
            cbInstitucion.Text = split[1];
            cd.cargarComboDep(cbDepto, Convert.ToInt32(cbCoordinacion.SelectedValue));
            cbDepto.Text = usuario.nomDepto;
            cbArea.Text = usuario.nomArea;
            cbPuesto.Text = usuario.nomPuesto;
            btnAñadir.Content = "Aceptar";
        }

        private void AddCord_Click(object sender, RoutedEventArgs e)
        {
            int indice = cbCoordinacion.SelectedIndex;
            int tamaño = cbCoordinacion.Items.Count;
            hdk_ViewAltaCoordinacion vac = new hdk_ViewAltaCoordinacion(control);
            vac.ShowDialog();
            cd.cargarComboCord(cbCoordinacion);
            if (tamaño != cbCoordinacion.Items.Count)
            {
                cbCoordinacion.SelectedIndex = cbCoordinacion.Items.Count - 1;
            }
            else
            {
                cbCoordinacion.SelectedIndex = indice;
            }
        }

        private void AddDep_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewAltaDepartamento hvad = new hdk_ViewAltaDepartamento(control);
            hvad.addPar(cbCoordinacion.SelectedIndex);
            hvad.ShowDialog();
            if (hvad.añadio)
            {
                int co = Convert.ToInt32(cbCoordinacion.SelectedValue);
                cd.cargarComboDep(cbDepto, co);
                cbDepto.SelectedIndex = cbDepto.Items.Count - 1;
            }
        }

        private void AddUs_Click(object sender, RoutedEventArgs e)
        {
            if (!btnAñadir.Content.Equals("Aceptar"))
            {
                if (!String.IsNullOrWhiteSpace(txtUsuario.Text) && !String.IsNullOrWhiteSpace(txtNombre.Text) && !String.IsNullOrWhiteSpace(cbArea.Text)
                    && !String.IsNullOrEmpty(cbTipo.Text) && !String.IsNullOrWhiteSpace(cbDepto.Text) && !String.IsNullOrWhiteSpace(cbInstitucion.Text)
                    && !String.IsNullOrWhiteSpace(txtPassword.Password) && !String.IsNullOrWhiteSpace(cbPuesto.Text) && !String.IsNullOrWhiteSpace(cbArea.Text))
                {
                    int dep = Convert.ToInt32(cbDepto.SelectedValue);
                    int are = Convert.ToInt32(cbArea.SelectedValue);
                    int pue = Convert.ToInt32(cbPuesto.SelectedValue);
                    string correo = txtCorreo.Text + "@" + cbInstitucion.Text;
                    if (txtPassword.Password.Equals(txtPasswordRep.Password))
                    {
                        if (modificar)
                        {
                            cu.modificar(idUsuario, txtUsuario.Text, txtNombre.Text, cbTipo.SelectedIndex, dep, txtExTel.Text, correo, txtPassword.Password, are, pue, Convert.ToInt32(cbInstitucion.SelectedValue));
                        }
                        else
                        {
                            if (cu.insertar(txtUsuario.Text, txtNombre.Text, cbTipo.SelectedIndex, dep, txtExTel.Text, correo, txtPassword.Password, are, pue, Convert.ToInt32(cbInstitucion.SelectedValue)))
                            {
                                if (chEquipo.IsChecked.Value)
                                {
                                    this.Close();
                                    hdk_ViewAltaEquipo vae = new hdk_ViewAltaEquipo(control);
                                    vae.pasarResponsable(txtNombre.Text);
                                    vae.ShowDialog();
                                }
                            }
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Verifique que las contraseñas sea iguales", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Llene todos los campos obligatorios", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                this.Close();
            }
                    
        }

        private void Cord_LostFocus(object sender, RoutedEventArgs e)
        {
            int co = Convert.ToInt32(cbCoordinacion.SelectedValue);
            cd.cargarComboDep(cbDepto, co);
            if (btnAñadir.Content.Equals("Aceptar"))
            {
                this.verificarEdicion(usuario.nomCoordinacion, cbCoordinacion.Text);
            }
        }

        private void Us_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                e.Handled = true;
            }
        }

        private void Nom_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!char.IsLetter(e.Text, e.Text.Length - 1))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                e.Handled = true;
            }
        }

        private void exTel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
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

        private void Us_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void exTel_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void AddArea_Click(object sender, RoutedEventArgs e)
        {
            int indice = cbArea.SelectedIndex;
            int tamaño = cbArea.Items.Count;
            hdk_ViewAltaArea vaa = new hdk_ViewAltaArea(control);
            vaa.ShowDialog();
            ca.cargarCombo(cbArea);
            if (tamaño != cbArea.Items.Count)
            {
                cbArea.SelectedIndex = cbArea.Items.Count - 1;
            }
            else
            {
                cbArea.SelectedIndex = indice;
            }
            
        }

        private void AddPuesto_Click(object sender, RoutedEventArgs e)
        {
            int indice = cbPuesto.SelectedIndex;
            int tamaño = cbPuesto.Items.Count;
            hdk_ViewAltaPuesto vap = new hdk_ViewAltaPuesto(control);
            vap.ShowDialog();
            cp.cargarCombo(cbPuesto);
            if (tamaño != cbPuesto.Items.Count)
            {
                cbPuesto.SelectedIndex = cbPuesto.Items.Count - 1;
            }
            else
            {
                cbPuesto.SelectedIndex = indice;
            }
            
        }

        private void verificarEdicion(string e1, string e2)
        {
                if (!e1.Equals(e2))
                {
                    btnAñadir.Content = "Modificar";
                }
        }

        private void cbTipo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modificar)   
                this.verificarEdicion(usuario.tipoUsuarioString, cbTipo.Text);
        }

        private void cbCoordinacion_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modificar)   
                this.verificarEdicion(usuario.nomCoordinacion, cbCoordinacion.Text);
        }

        private void cbDepto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modificar)   
                this.verificarEdicion(usuario.nomDepto, cbDepto.Text);
        }

        private void cbArea_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modificar)   
                this.verificarEdicion(usuario.nomArea, cbArea.Text);
        }

        private void cbPuesto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (modificar)   
                this.verificarEdicion(usuario.nomPuesto, cbPuesto.Text);
        }

        private void txtUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (modificar)
                this.verificarEdicion(usuario.nomUsuario, txtUsuario.Text);
        }

        private void txtNombre_KeyUp(object sender, KeyEventArgs e)
        {
            if (modificar)
                this.verificarEdicion(usuario.nomCompleto, txtNombre.Text);
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (modificar)
                this.verificarEdicion(usuario.password, txtPassword.Password);
        }

        private void txtExTel_KeyUp(object sender, KeyEventArgs e)
        {
            if (modificar)
                this.verificarEdicion(usuario.exTel, txtExTel.Text);
        }

        private void txtCorreo_KeyUp(object sender, KeyEventArgs e)
        {
            if (modificar)
                btnAñadir.Content = "Modificar";
        }
    }
}

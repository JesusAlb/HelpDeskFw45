using HelpDesk.ControlAltas;
using HelpDesk.Modelo;
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

namespace HelpDesk.ViewAltas
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewAltaDepartamento.xaml
    /// </summary>
    public partial class hdk_ViewAltaDepartamento : Window
    {

        private bool modificar;
        public bool añadio;
        private ViewDepartamento departamento;
        private hdk_ControlDepartamento cd;
        private hdk_ControlAcceso Control;

        public hdk_ViewAltaDepartamento(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cd = new hdk_ControlDepartamento(Control);
            cd.cargarComboCord(cbCoord);
            txtDepto.Focus();
        }

        public void pasarDatos(object item)
        {
            departamento = (ViewDepartamento)item;
            txtDepto.Text = departamento.nomDepto;
            cbCoord.SelectedValue = departamento.coordinacion;
            modificar = true;
            btnAñadir.Content = "Modificar";
        }

        private void añadir_Click(object sender, RoutedEventArgs e)
        {
            añadio = true;
            if (!String.IsNullOrEmpty(cbCoord.Text) && !String.IsNullOrWhiteSpace(txtDepto.Text))
            {
                int co = Convert.ToInt32(cbCoord.SelectedValue);
                if (modificar)
                {
                    if (!(txtDepto.Text.Equals(departamento.nomDepto) && cbCoord.SelectedValue.Equals(departamento.coordinacion)))
                    {
                        cd.modificar(departamento.idDepto, txtDepto.Text, co);
                    }                 
                }
                else
                {
                    cd.insertar(txtDepto.Text, co);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Llene todos los campos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void addPar(int p)
        {
            cbCoord.SelectedIndex = p;
        }

        private void dep_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetter(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            hdk_ViewAltaCoordinacion vac = new hdk_ViewAltaCoordinacion(Control);
            vac.ShowDialog();
            cd.cargarComboCord(cbCoord);
            cbCoord.SelectedIndex = cbCoord.Items.Count - 1;
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

    }
}

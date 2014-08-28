using HelpDesk.Control.Catalogo;
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

namespace HelpDesk.ViewAltas.Catálogo
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewAltaLugar.xaml
    /// </summary>
    public partial class hdk_ViewAltaLugar : Window
    {
        hdk_ControlAcceso Control;
        hdk_ControlLugar cl;
        tbllugar Lugar;
        bool Modificar = false;

        public hdk_ViewAltaLugar(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            cl = new hdk_ControlLugar(Control);
            txtLugar.Focus();
        }

        public void pasarDatos(object item)
        {
            Lugar = (tbllugar)item;
            txtLugar.Text = Lugar.nomLugar;
            Modificar = true;
            btnAñadir.Content = "Modificar";
        }

        private void txtLugar_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, e.Text.Length - 1))
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

        private void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtLugar.Text))
            {
                if (Modificar)
                {
                    if (!txtLugar.Text.Equals(Lugar.nomLugar))
                    {
                        cl.modificar(Lugar.idLugar, txtLugar.Text);
                    }                    
                }
                else
                {
                    cl.insertar(txtLugar.Text);
                }
                this.Close();
            }
        }
    }
}

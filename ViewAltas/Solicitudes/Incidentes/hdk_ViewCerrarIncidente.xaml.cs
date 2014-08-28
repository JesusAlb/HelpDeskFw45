using HelpDesk.Control.Solicitudes;
using HelpDesk.Control.Solicitudes.Incidentes;
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
    /// Lógica de interacción para hdk_ViewCerrarIncidente.xaml
    /// </summary>
    public partial class hdk_ViewCerrarIncidente : Window
    {

        hdk_ControlIncidentes ci;
        hdk_ControlAcceso Control;
        VistaIncidente incidente;

        public hdk_ViewCerrarIncidente(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            ci = new hdk_ControlIncidentes(Control);
            ci.cargarComboTipo(cbTipo);
        }

        public void pasarDatos(object item)
        {
            incidente = (VistaIncidente)item;
            cbTipo.Text = incidente.tipo;
        }

        private void terminar_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(solucion.Text) && !String.IsNullOrWhiteSpace(acciones.Text))
            {
                ci.cerrarIncidente(incidente.numIncidente, solucion.Text, acciones.Text, Convert.ToInt32(cbTipo.SelectedValue));
                Close();
            }
        }

        private void solucion_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }

        private void acciones_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

          /*  if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }*/
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


        private void checkEditar_Checked(object sender, RoutedEventArgs e)
        {
            cbTipo.IsEnabled = true;
        }

        private void checkEditar_Unchecked(object sender, RoutedEventArgs e)
        {
            cbTipo.Text = incidente.tipo;
            cbTipo.IsEnabled = false;
        }

        private void acciones_PreviewKeyDown(object sender, KeyEventArgs e)
        {
             System.Windows.Input.Key k = e.Key;

        if (Key.A <= k && k <= Key.Z ||
            k == Key.Back || k == Key.Enter ||
            k == Key.Space || k == Key.Tab || 
            k == Key.Subtract || k == Key.LeftShift ||
            k == Key.RightShift || k == Key.OemComma ||
            k == Key.OemPeriod || k == Key.OemTilde ||
            k == Key.OemMinus || k == Key.OemSemicolon || 
            k == Key.Delete || k == Key.Capital ||
            k == Key.Left || k == Key.Right || k == Key.Decimal)
        {

        }
        else
        {
            e.Handled = true;
            System.Media.SystemSound ss = System.Media.SystemSounds.Beep;
            ss.Play();
        }
        }
    }
}

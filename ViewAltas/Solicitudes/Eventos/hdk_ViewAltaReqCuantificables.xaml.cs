using HelpDesk.Control.Solicitudes;
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
    /// Lógica de interacción para hdk_ViewAltaReqCuantificables.xaml
    /// </summary>
    public partial class hdk_ViewAltaReqCuantificables : Window
    {
        private hdk_ControlAsigReq car;
        private hdk_ControlAcceso Control;
        private int idEvento;
        private int idReq;
        private bool modificar = false;
        private int cantidadInicial;

        public hdk_ViewAltaReqCuantificables(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            Control = ca;
            car = new hdk_ControlAsigReq(Control);
        }

        public void pasarDatos(int evento, int req, int? cantidad)
        {
            idEvento = evento;
            idReq = req;            
           if (cantidad != null)
           {
               modificar = true;
               cantidadInicial = cantidad.Value;
               txtCantidad.Text = cantidad.Value.ToString();
           }
           
        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                if (!modificar)
                {                
                        if (car.insertar(idEvento, idReq, Convert.ToInt32(txtCantidad.Text)))
                        {
                           this.Close();
                        }
                }
                else
                {
                    if (cantidadInicial != Convert.ToInt32(txtCantidad.Text))
                    {
                        if (car.editar(idEvento, idReq, Convert.ToInt32(txtCantidad.Text)))
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
        }

        private void can_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
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

        private void can_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
    }
}

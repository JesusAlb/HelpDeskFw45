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

namespace HelpDesk.ViewAltas.Solicitudes
{
    /// <summary>
    /// Lógica de interacción para hdk_ViewEncuesta.xaml
    /// </summary>
    public partial class hdk_ViewEncuesta : Window
    {
        hdk_ControlEncuestas ce;
        hdk_ControlAcceso Control;
        VistaIncidente incidente;
        VistaEvento evento;
        int idCalidadServicio;

        public hdk_ViewEncuesta(hdk_ControlAcceso ca)
        {
            InitializeComponent();
            this.Control = ca;
            ce = new hdk_ControlEncuestas(Control);
            if (this.Control.item.tipoUsuario == 0)
            {
                this.DataG.Columns[2].Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                this.DataG.Columns[3].Visibility = System.Windows.Visibility.Hidden;

                gbResultado.Visibility = System.Windows.Visibility.Hidden;
                lbelObs.Margin = new Thickness(58, 290, 0, 0);
                txtObservaciones.Margin = new Thickness(58, 322, 0, 0);
                btnRegistrar.Margin = new Thickness(283, 397, 283, 0);
                this.Height = 495;
            }             
        }

        public void pasarDatos(Object it, string fuente)
        {
            
            if (Control.item.tipoUsuario == 0)
            {
                if (fuente.Equals("Evento"))
                {
                    evento = (VistaEvento)it;
                    idCalidadServicio = evento.idCalidad_Servicio;                   
                    txtObservaciones.Text = evento.observacionesServicio;
                    txtSolicitante.Text = evento.solicitante;
                    this.asignarValorImagen(evento.promedioCalidad);
                    
                }
                else if (fuente.Equals("Incidente"))
                {
                    incidente = (VistaIncidente)it;
                    idCalidadServicio = incidente.idCalidad_Servicio;
                    txtObservaciones.Text = incidente.observacionesServicio;
                    txtSolicitante.Text = incidente.solicitante;
                    this.asignarValorImagen(incidente.promedioCalidad);
                }

                DataG.ItemsSource = ce.cargarTablaEncuesta(idCalidadServicio);
                txtObservaciones.IsReadOnly = true;
                btnRegistrar.Content = "Aceptar";
                
            }
            else
            {
                if (fuente.Equals("Evento"))
                {
                    evento = (VistaEvento)it;
                    idCalidadServicio = evento.idCalidad_Servicio;
                    txtSolicitante.Text = evento.solicitante;
                    DataG.ItemsSource = ce.cargarTablaPreguntas(1);
                }
                else if (fuente.Equals("Incidente"))
                {
                    incidente = (VistaIncidente)it;
                    idCalidadServicio = incidente.idCalidad_Servicio;
                    txtSolicitante.Text = incidente.solicitante;
                    DataG.ItemsSource = ce.cargarTablaPreguntas(0);
                }
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

        private static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent is valid.  
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child 
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree 
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child.  
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search 
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name 
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found. 
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        private bool validar()
        {
           bool regreso = true;
           int tamaño = DataG.Items.Count;
           for (int x = 0; x < tamaño; x++)
                {
                    DataG.SelectedIndex = x;
                    DataGridRow row = (DataGridRow)DataG.ItemContainerGenerator.ContainerFromIndex(x);
                    ComboBox cbRespuestas = FindChild<ComboBox>(row, "cbRespuesta");
                    if (String.IsNullOrEmpty(cbRespuestas.Text))
                    {
                        regreso = false;
                        x = tamaño;
                    }
                }
           return regreso;
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            if (btnRegistrar.Content.Equals("Registrar"))
            {
                if (this.validar())
                {
                    VistaPregunta pregunta;
                    int tamañoDG = DataG.Items.Count;
                    int valorTotal = 0;
                    for (int x = 0; x < DataG.Items.Count; x++)
                    {
                        DataG.SelectedIndex = x;
                        pregunta = (VistaPregunta)DataG.SelectedItem;
                        DataGridRow row = (DataGridRow)DataG.ItemContainerGenerator.ContainerFromIndex(x);
                        ComboBox cbRespuestas = FindChild<ComboBox>(row, "cbRespuesta");
                        valorTotal = valorTotal + Convert.ToInt32(cbRespuestas.Text);
                        ce.insertarRespuesta(idCalidadServicio, pregunta.idPregunta, Convert.ToInt32(cbRespuestas.Text));

                    }
                    float promedio = (float)valorTotal / tamañoDG;
                    string observaciones;
                    if (String.IsNullOrWhiteSpace(txtObservaciones.Text))
                    {
                        observaciones = "Sin observaciones";
                    }
                    else
                    {
                        observaciones = txtObservaciones.Text;
                    }
                    ce.modificarCalidadServicio(idCalidadServicio, observaciones, promedio);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Por favor responda a todos las preguntas", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                this.Close();
            }          
        }

   /*     private void obtenerResultado()
        {
            int tamañoDG = DataG.Items.Count;
            int valorTotal = 0;
            for (int x = 0; x < tamañoDG; x++)
            {
                DataG.SelectedIndex = x;
                valorTotal = valorTotal + ((VistaEncuesta)DataG.SelectedItem).valorRespuesta;
            }
            this.asignarValorImagen(valorTotal, tamañoDG);
        }*/

        private void asignarValorImagen(double? promedioEn)
        {
            txtValorPromedio.Text = promedioEn.ToString();
            string packUri = null;
            if (promedioEn >= 1 && promedioEn < 2.5)
            {
                packUri = "pack://application:,,,/HelpDesk;component/Imagenes/iconos/nivel0.png"; 
            }
            else if (promedioEn >= 2.5 && promedioEn < 4)
            {
                packUri = "pack://application:,,,/HelpDesk;component/Imagenes/iconos/nivel1.png"; 
            }
            else if (promedioEn >= 4 && promedioEn < 5.5)
            {
                packUri = "pack://application:,,,/HelpDesk;component/Imagenes/iconos/nivel2.png"; 
            }
            else if (promedioEn >= 5.5 && promedioEn <= 7)
            {
                packUri = "pack://application:,,,/HelpDesk;component/Imagenes/iconos/nivel3.png"; 
            }
            else if (promedioEn >= 7 && promedioEn <= 8.5)
            {
                packUri = "pack://application:,,,/HelpDesk;component/Imagenes/iconos/nivel4.png";
            }
            else if (promedioEn >= 8.5 && promedioEn <= 10)
            {
                packUri = "pack://application:,,,/HelpDesk;component/Imagenes/iconos/nivel5.png";
            }
            imEstado.Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
            }

        private void txtObservaciones_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
        }
        }


    }



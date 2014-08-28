using HelpDesk.Modelo;
using HelpDesk.Principal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HelpDesk.ControlAltas
{
    public class hdk_ControlDepartamento
    {
        hdk_ControlAcceso dbHelp;

        public hdk_ControlDepartamento(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }


       public IList cargarTabla(string filtro, string cord)
        {
            try
            {
                var query = dbHelp.DB.ViewDepartamentos.Where(x => x.nomDepto.Contains(filtro) && x.nomCoordinacion.Contains(cord));

                return query.ToList();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            
        }

       public bool borrarRegistro(int id)
        {
            try
            {
                var ItemARemover = dbHelp.DB.tbldepartamentos.SingleOrDefault(x => x.idDepto == id);
                if (ItemARemover != null)
                {
                    dbHelp.DB.tbldepartamentos.Remove(ItemARemover);
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

       public bool insertar(string nombre, int idCord)
        {
            try
            {
                var cord = new tbldepartamento { nomDepto = nombre, coordinacion = idCord };
                if (cord != null)
                {
                    dbHelp.DB.tbldepartamentos.Attach(cord);
                    dbHelp.DB.tbldepartamentos.Add(cord);
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Se añadió exitosamente");
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

       public bool modificar(int id, string nombre, int cord)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tbldepartamentos.SingleOrDefault(x => x.idDepto == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.nomDepto = nombre;
                    ItemAmodificar.coordinacion = cord;
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Se actualizó exitosamente");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

       public bool cargarComboCord(ComboBox cb)
        {
            try
            {
                var items = dbHelp.DB.tblcoordinacions.OrderBy(a => a.idCoordinacion).ToList();
                items.Insert(0, new tblcoordinacion { idCoordinacion = 0, nomCoordinacion = "" });
                cb.ItemsSource = items;
                cb.DisplayMemberPath = "nomCoordinacion";
                cb.SelectedValuePath = "idCoordinacion";              
                return true;
            }catch{
                cb.Items.Add("Error");
                return false;
            }
        }

        public int getIdCoordinacion(int id)
        {
            try
            {
                var registro = dbHelp.DB.tbldepartamentos.SingleOrDefault(x => x.idDepto == id);
                if (registro != null)
                {
                    return registro.coordinacion;
                }
                else
                {
                    return -1;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return -1;
            }

        }

        public bool cargarComboDep(ComboBox cb, int cord)
        {
            try
            {
                if (cord != -1)
                {
                    cb.ItemsSource = dbHelp.DB.tbldepartamentos.Where(a => a.coordinacion == cord).ToList();
                }
                else
                {
                    cb.ItemsSource = dbHelp.DB.tbldepartamentos.ToList();
                }
                cb.DisplayMemberPath = "nomDepto";
                cb.SelectedValuePath = "idDepto";
                return true;
            } catch{
                cb.Items.Add("Error");
                return false;
            }
            
            
        }  
    }
}

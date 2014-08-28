using HelpDesk.Modelo;
using HelpDesk.Principal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelpDesk.ControlAltas
{
   public class hdk_ControlTipoIncidencia
    {
       hdk_ControlAcceso dbHelp;

       public hdk_ControlTipoIncidencia(hdk_ControlAcceso ca)
       {
           dbHelp = ca;
       }

       public IList cargarTabla(string filtro)
       {
           try
           {
               return dbHelp.DB.tbltipoincidencias.Where(a => a.nomTipoIncidente.Contains(filtro)).ToList();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               return null;
           }
       }

       public bool borrarRegistro(int id)
       {
           try
           {
               var ItemARemover = dbHelp.DB.tbltipoincidencias.SingleOrDefault(x => x.idTipoIncidente == id);
               if (ItemARemover != null)
               {
                   dbHelp.DB.tbltipoincidencias.Remove(ItemARemover);
                   dbHelp.DB.SaveChanges();
                   dbHelp.actualizarModelo();
               }
               return true;
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               return false;
           }

       }

       public bool insertar(string nombre)
       {
           try
           {
               var cord = new tbltipoincidencia { nomTipoIncidente = nombre };
               if (cord != null)
               {
                   dbHelp.DB.tbltipoincidencias.Attach(cord);
                   dbHelp.DB.tbltipoincidencias.Add(cord);
                   dbHelp.DB.SaveChanges();
                   dbHelp.actualizarModelo();
                   MessageBox.Show("Se añadió exitosamente");
               }
               return true;
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               return false;
           }

       }

       public bool modificar(int id, string nombre)
       {
           try
           {
               var ItemAmodificar = dbHelp.DB.tbltipoincidencias.SingleOrDefault(x => x.idTipoIncidente == id);
               if (ItemAmodificar != null)
               {
                   ItemAmodificar.nomTipoIncidente = nombre;
                   dbHelp.DB.SaveChanges();
                   dbHelp.actualizarModelo();
                   MessageBox.Show("Se actualizó exitosamente");
               }
               return true;
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
               return false;
           }
       }
    }

}

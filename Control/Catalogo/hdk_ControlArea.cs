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

namespace HelpDesk.Control.Catalogo
{
    class hdk_ControlArea
    {
        hdk_ControlAcceso dbHelp;

        public hdk_ControlArea(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public IList cargarTabla(string area)
        {
            try
            {
                return dbHelp.DB.tblareas.Where(a => a.nomArea.Contains(area)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void cargarCombo(ComboBox cbArea)
        {
            cbArea.ItemsSource = dbHelp.DB.tblareas.OrderBy(a=>a.idArea).ToList();
            cbArea.DisplayMemberPath = "nomArea";
            cbArea.SelectedValuePath = "idArea";
        }

        public bool insertar(string nombre)
        {
            try
            {
                var area = new tblarea { nomArea = nombre };
                if (area != null)
                {
                    dbHelp.DB.tblareas.Attach(area);
                    dbHelp.DB.tblareas.Add(area);
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

                var ItemAmodificar = dbHelp.DB.tblareas.SingleOrDefault(x => x.idArea == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.nomArea = nombre;
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

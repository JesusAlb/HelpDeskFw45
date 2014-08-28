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
    class hdk_ControlLugar
    {
        hdk_ControlAcceso dbHelp;

        public hdk_ControlLugar(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public IList cargarTabla(string filtro)
        {
            try
            {
                return dbHelp.DB.tbllugars.Where(a => a.nomLugar.Contains(filtro)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void cargarCombo(ComboBox cb)
        {
            cb.ItemsSource = dbHelp.DB.tbllugars.OrderBy( a => a.idLugar).ToList();
            cb.DisplayMemberPath = "nomLugar";
            cb.SelectedValuePath = "idLugar";
        }

        public bool insertar(string lugar){
            try
            {
                tbllugar Lugar = new tbllugar { nomLugar = lugar };
                if (Lugar != null)
                {
                    dbHelp.DB.tbllugars.Attach(Lugar);
                    dbHelp.DB.tbllugars.Add(Lugar);
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

                var ItemAmodificar = dbHelp.DB.tbllugars.SingleOrDefault(x => x.idLugar == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.nomLugar = nombre;
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

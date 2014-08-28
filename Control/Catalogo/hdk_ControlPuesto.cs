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
    class hdk_ControlPuesto
    {
        hdk_ControlAcceso dbHelp;

        public hdk_ControlPuesto(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public IList cargarTabla(string filtro)
        {
            return dbHelp.DB.tblpuestoes.Where(a => a.nomPuesto.Contains(filtro)).ToList();
        }

        public void cargarCombo(ComboBox cbPuestos)
        {
            cbPuestos.ItemsSource = dbHelp.DB.tblpuestoes.OrderBy(a=>a.idPuesto).ToList();
            cbPuestos.DisplayMemberPath = "nomPuesto";
            cbPuestos.SelectedValuePath = "idPuesto";
        }

        public bool insertar(string nombre)
        {
            try
            {
                var puesto = new tblpuesto { nomPuesto = nombre };
                if (puesto != null)
                {
                    dbHelp.DB.tblpuestoes.Attach(puesto);
                    dbHelp.DB.tblpuestoes.Add(puesto);
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
                var ItemAmodificar = dbHelp.DB.tblpuestoes.SingleOrDefault(x => x.idPuesto == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.nomPuesto = nombre;
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

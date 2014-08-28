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
    class hdk_ControlMarca
    {
        hdk_ControlAcceso dbHelp;

        public hdk_ControlMarca(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public IList cargarTabla(string ma)
        {
            try
            {
                return dbHelp.DB.tblmarcas.Where(a => a.nomMarca.Contains(ma) && !a.nomMarca.Equals("N/A")).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public int obtenerMarcaNoAplica()
        {
            return dbHelp.DB.tblmarcas.Where(a => a.nomMarca.Equals("N/A")).SingleOrDefault().idMarca;
        }

        public void cargarCombo(ComboBox cb, int tipo)
        {
            if (tipo == 0)
            {
                cb.ItemsSource = dbHelp.DB.tblmarcas.ToList();
            }
            else
            {
                cb.ItemsSource = this.cargarTabla("");
            }

            cb.DisplayMemberPath = "nomMarca";
            cb.SelectedValuePath = "idMarca";
        }

        public bool insertar(string ma)
        {
            try
            {
                var marca = new tblmarca { nomMarca = ma };
                if (marca!= null)
                {
                    dbHelp.DB.tblmarcas.Attach(marca);
                    dbHelp.DB.tblmarcas.Add(marca);
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

        public bool modificar(int idMa, string nomMa)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tblmarcas.SingleOrDefault(x => x.idMarca == idMa);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.nomMarca = nomMa;
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

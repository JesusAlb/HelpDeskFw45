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
    class hdk_ControlTipoEquipo
    {
        hdk_ControlAcceso dbHelp;

        public hdk_ControlTipoEquipo(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public IList cargarTabla(string filtro)
        {
            try
            {
                return dbHelp.DB.tbltipoequipoes.Where(a => a.nomTipoEquipo.Contains(filtro)).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }          
        }

        public bool insertar(string nombre, bool cpu, bool dd, bool red, bool monitor, bool mouse, bool teclado, bool ram, bool procesador)
        {
            try
            {
                var tipoEquipos = new tbltipoequipo { nomTipoEquipo = nombre, siEquipo = cpu, siDiscoDuro = dd, siRed = red, siMonitor = monitor, siMouse = mouse, siTeclado = teclado, siRAM = ram, siProcesador = procesador };
                if (tipoEquipos != null)
                {
                    dbHelp.DB.tbltipoequipoes.Attach(tipoEquipos);
                    dbHelp.DB.tbltipoequipoes.Add(tipoEquipos);
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

        public bool modificar(int idTipoEq, string nombre, bool cpu, bool dd, bool red, bool monitor, bool mouse, bool teclado, bool ram, bool procesador)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tbltipoequipoes.SingleOrDefault(x => x.idTipoEquipo == idTipoEq);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.nomTipoEquipo = nombre;
                    ItemAmodificar.siEquipo = cpu;
                    ItemAmodificar.siDiscoDuro = dd;
                    ItemAmodificar.siMonitor = monitor;
                    ItemAmodificar.siMouse = mouse;
                    ItemAmodificar.siTeclado = teclado;
                    ItemAmodificar.siRed = red;
                    ItemAmodificar.siRAM = ram;
                    ItemAmodificar.siProcesador = procesador;
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

        public void cargarCombo(ComboBox cb)
        {
            cb.ItemsSource = dbHelp.DB.tbltipoequipoes.ToList();
            cb.DisplayMemberPath = "nomTipoEquipo";
            cb.SelectedValuePath = "idTipoEquipo";
        }
    }
}

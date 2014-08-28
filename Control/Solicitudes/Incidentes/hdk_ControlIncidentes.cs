using HelpDesk.Modelo;
using HelpDesk.Principal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HelpDesk.Control.Solicitudes.Incidentes
{
    public class hdk_ControlIncidentes
    {

        hdk_ControlAcceso dbHelp;

        public hdk_ControlIncidentes(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public bool cargarComboTipo(ComboBox cb)
        {
            try
            {
                var items = dbHelp.DB.tbltipoincidencias.ToList();
                items.Insert(0, new tbltipoincidencia { idTipoIncidente = 0, nomTipoIncidente = "" });
                cb.ItemsSource = items;
                cb.DisplayMemberPath = "nomTipoIncidente";
                cb.SelectedValuePath = "idTipoIncidente";
                return true;
            }
            catch 
            {
                cb.Items.Add("Error");
                return false;
            }
        }

        public IList cargarTablaSolicitante(int idSol, string cbtipo, int status, string filtro, DateTime? limiteIn, DateTime? limiteSp)
        {

            DateTime busquedaFecha;
            int numero;
            DateTime.TryParse(filtro, out busquedaFecha);
            if (busquedaFecha < new DateTime(1753, 1, 1))
            {
                busquedaFecha = new DateTime(1753, 1, 1);
            }
            int.TryParse(filtro, out numero);

            try
            {
                if (limiteIn == null || limiteSp == null)
                {
                    return dbHelp.DB.VistaIncidentes.Where(a => (a.acciones.Contains(filtro) || a.descripcion.Contains(filtro) ||
                        a.seguimiento.Contains(filtro) || a.solicitante.Contains(filtro) || a.tipo.Contains(filtro) ||
                        a.soporte.Contains(filtro) || a.numIncidente == numero || a.prioridad.Contains(filtro) ||
                        a.fecha_Cierre.Value.Equals(busquedaFecha) || a.fecha_Sol.Value.Equals(busquedaFecha) ||
                       /* a.fecha_Cierre.Value.Day == numero || a.fecha_Cierre.Value.Month == numero || a.fecha_Cierre.Value.Year == numero ||
                        a.fecha_Sol.Value.Day == numero || a.fecha_Sol.Value.Month == numero || a.fecha_Sol.Value.Year == numero ||
                        a.horaIn.Value.Minute == numero || a.horaIn.Value.Hour == numero ||
                        a.horaFn.Value.Minute == numero || a.horaFn.Value.Hour == numero || */
                        a.solucion.Contains(filtro)) && (a.status == status && a.idSolicitante == idSol && a.tipo.Contains(cbtipo))).ToList();
                }
                else
                {
                    return dbHelp.DB.VistaIncidentes.Where(a => (a.acciones.Contains(filtro) || a.descripcion.Contains(filtro) ||
                        a.seguimiento.Contains(filtro) || a.solicitante.Contains(filtro) || a.tipo.Contains(filtro) ||
                        a.soporte.Contains(filtro) || a.numIncidente == numero || a.prioridad.Contains(filtro) ||
                        a.fecha_Cierre.Value.Equals(busquedaFecha) || a.fecha_Sol.Value.Equals(busquedaFecha) ||
                      /*  a.fecha_Cierre.Value.Day == numero || a.fecha_Cierre.Value.Month == numero || a.fecha_Cierre.Value.Year == numero ||
                        a.fecha_Sol.Value.Day == numero || a.fecha_Sol.Value.Month == numero || a.fecha_Sol.Value.Year == numero ||
                        a.horaIn.Value.Minute == numero || a.horaIn.Value.Hour == numero ||
                        a.horaFn.Value.Minute == numero || a.horaFn.Value.Hour == numero ||*/
                        a.solucion.Contains(filtro)) && (a.status == status && a.idSolicitante == idSol && a.tipo.Contains(cbtipo) && (a.fecha_Sol >= limiteIn && a.fecha_Sol <= limiteSp))).ToList();
                }
            }
            catch 
            {
                return null;
            }
        }

        public IList cargarTablaSoporte(int status, string cbtipo, string filtro, DateTime? limiteIn, DateTime? limiteSp){

            DateTime busquedaFecha;
            int numero;
            DateTime.TryParse(filtro, out busquedaFecha);
            int.TryParse(filtro, out numero);
            if (busquedaFecha < new DateTime(1753, 1, 1))
            {
                busquedaFecha = new DateTime(1753, 1, 1);
            }
            try
            {
                if (limiteIn == null || limiteSp == null)
                {
                    return dbHelp.DB.VistaIncidentes.Where(a => (a.acciones.Contains(filtro) || a.descripcion.Contains(filtro) ||
                        a.seguimiento.Contains(filtro) || a.solicitante.Contains(filtro) ||
                        a.soporte.Contains(filtro) || a.numIncidente == numero || a.prioridad.Contains(filtro) ||
                        a.fecha_Cierre.Value.Equals(busquedaFecha) || a.fecha_Sol.Value.Equals(busquedaFecha) ||
                        /*a.fecha_Cierre.Value.Day == numero || a.fecha_Cierre.Value.Month == numero || a.fecha_Cierre.Value.Year == numero ||
                          a.fecha_Sol.Value.Day == numero || a.fecha_Sol.Value.Month == numero || a.fecha_Sol.Value.Year == numero ||
                          a.horaIn.Value.Minute == numero || a.horaIn.Value.Hour == numero ||
                          a.horaFn.Value.Minute == numero || a.horaFn.Value.Hour == numero ||*/
                        a.solucion.Contains(filtro)) && (a.status == status && a.tipo.Contains(cbtipo))).ToList();
                }
                else
                {
                    return dbHelp.DB.VistaIncidentes.Where(a => (a.acciones.Contains(filtro) || a.descripcion.Contains(filtro) ||
                        a.seguimiento.Contains(filtro) || a.solicitante.Contains(filtro) || 
                        a.soporte.Contains(filtro) || a.numIncidente == numero || a.prioridad.Contains(filtro) ||
                        a.fecha_Cierre.Value.Equals(busquedaFecha) || a.fecha_Sol.Value.Equals(busquedaFecha) ||
                        /*  a.fecha_Cierre.Value.Day == numero || a.fecha_Cierre.Value.Month == numero || a.fecha_Cierre.Value.Year == numero ||
                          a.fecha_Sol.Value.Day == numero || a.fecha_Sol.Value.Month == numero || a.fecha_Sol.Value.Year == numero ||
                          a.horaIn.Value.Minute == numero || a.horaIn.Value.Hour == numero ||
                          a.horaFn.Value.Minute == numero || a.horaFn.Value.Hour == numero ||*/
                        a.solucion.Contains(filtro)) && (a.status == status && a.tipo.Contains(cbtipo)) && (a.fecha_Sol >= limiteIn && a.fecha_Sol <= limiteSp)).ToList();
                }
            }
            catch 
            {
                return null;
            }
        }

        public bool insertarIncidente(int sol, int sop, int seg, string desc, string ac, string solucion, int tipo, DateTime feIn, DateTime? feFn, string priory, DateTime In, DateTime? Fn)
        {

            try
            {
                int st = 0;
                if (dbHelp.item.tipoUsuario == 0)
                {
                    st = 2;
                }

                var incidente = new tblincidente
                {
                    descripcion = desc,
                    solicitante = sol,
                    acciones = ac,
                    solucion = solucion,
                    tipo = tipo,
                    fecha_Sol = feIn,
                    fecha_Cierre = feFn,
                    status = st,
                    prioridad = priory,
                    horaIn = In,
                    horaFn = Fn,
                    soporte = sop,
                    seguimiento = seg

                };
                if (incidente != null)
                {
                    dbHelp.DB.tblincidentes.Attach(incidente);
                    dbHelp.DB.tblincidentes.Add(incidente);
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Incidente creado exitosamente");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                try
                {
                    using (StreamWriter w = File.AppendText("../../altaIncidentes.txt"))
                    {
                        string registro = feIn.ToShortDateString() + "    " + tipo + "    " + desc + "    " + sol + "    " + priory + "    " + In.ToShortTimeString() + "    " + "0";
                        w.WriteLine(registro);
                    }
                }
                catch
                {

                }
                return false;
                }
                
                
        }

        public bool cambiarStatus(int id, int status)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tblincidentes.SingleOrDefault(x => x.numIncidente == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.status = status;
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

        public bool cerrarIncidente(int id, string sol, string ac, int tipo)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tblincidentes.SingleOrDefault(x => x.numIncidente == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.status = 2;
                    ItemAmodificar.solucion = sol;
                    ItemAmodificar.acciones = ac;
                    ItemAmodificar.tipo = tipo;
                    ItemAmodificar.fecha_Cierre = DateTime.Today;
                    ItemAmodificar.horaFn = DateTime.Now;
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Incidente finalizado exitosamente");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool asignarSoporte(int id, int sop, int seg)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tblincidentes.SingleOrDefault(x => x.numIncidente == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.status = 1;
                    ItemAmodificar.soporte = sop;
                    ItemAmodificar.seguimiento = seg;
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

        public int obtenerNumeroIncidentes()
        {
            try
            {
                return dbHelp.DB.tblincidentes.Where(a => a.status == 0).Count();
            }
            catch
            {
                return -1;
            }
        }
        
        public int obtenerUltimoIncidente()
        {
            try
            {
                return dbHelp.DB.tblincidentes.Max(a => a.numIncidente);
            }
            catch
            {
                return 1;
            }
        }
    }
}

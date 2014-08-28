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

namespace HelpDesk.Control.Solicitudes
{
    class hdk_ControlEventos
    {
        hdk_ControlAcceso dbHelp;

        public hdk_ControlEventos(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public IList cargarTablasSolicitante(int idSol, int status, string filtro, DateTime? FechaInicio, DateTime? FechaFinal)
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
                if (FechaInicio == null || FechaFinal == null)
                {
                    return dbHelp.DB.VistaEventos.Where(a => (a.status == status && a.idSolicitante == idSol) && (a.acomodo.Contains(filtro) || a.apoyo.Contains(filtro) ||
                                                     a.fecha_Sol.Value.Equals(busquedaFecha) || a.fecha_cierre.Value.Equals(busquedaFecha) || a.FechaInicio.Value.Equals(busquedaFecha) ||
                                                     /*a.horaIn.Value.Hour == numero || a.horaIn.Value.Minute == numero || a.horaFn.Value.Hour == numero || 
                                                     a.horaFn.Value.Minute == numero || a.fecha_Sol.Value.Day.Equals(numero) || a.fecha_Sol.Value.Month.Equals(numero) || a.fecha_Sol.Value.Year.Equals(numero) ||
                                                     a.fecha_cierre.Value.Day.Equals(numero) || a.fecha_cierre.Value.Month.Equals(numero) || a.fecha_cierre.Value.Year.Equals(numero) ||
                                                     a.FechaInicio.Value.Day.Equals(numero) || a.FechaInicio.Value.Month.Equals(numero) || a.FechaInicio.Value.Year.Equals(numero) ||         */                 
                                                     a.descripcion.Contains(filtro) || a.lugar.Contains(filtro) || a.responsable.Contains(filtro) ||
                                                     a.tipo_evento.Contains(filtro))).ToList();
                }
                else
                {
                    return dbHelp.DB.VistaEventos.Where(a => (a.status == status && a.idSolicitante == idSol && (a.FechaInicio >= FechaInicio && a.FechaInicio <= FechaFinal)) && (a.acomodo.Contains(filtro) || a.apoyo.Contains(filtro) ||
                                       a.fecha_Sol.Value.Equals(busquedaFecha) || a.fecha_cierre.Value.Equals(busquedaFecha) ||a.FechaInicio.Value.Equals(busquedaFecha) ||
                        /*a.horaIn.Value.Hour == numero || a.horaIn.Value.Minute == numero || a.horaFn.Value.Hour == numero || 
                        a.horaFn.Value.Minute == numero || a.fecha_Sol.Value.Day.Equals(numero) || a.fecha_Sol.Value.Month.Equals(numero) || a.fecha_Sol.Value.Year.Equals(numero) ||
                        a.fecha_cierre.Value.Day.Equals(numero) || a.fecha_cierre.Value.Month.Equals(numero) || a.fecha_cierre.Value.Year.Equals(numero) ||
                        a.FechaInicio.Value.Day.Equals(numero) || a.FechaInicio.Value.Month.Equals(numero) || a.FechaInicio.Value.Year.Equals(numero) ||*/
                                       a.descripcion.Contains(filtro) || a.lugar.Contains(filtro) || a.responsable.Contains(filtro)||a.tipo_evento.Contains(filtro))).ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public tblevento cargarEvento(int evento)
        {
            return dbHelp.DB.tbleventoes.SingleOrDefault(a => a.idEvento == evento);
        }

        public IList cargarTablasSoporte(int status, string filtro, DateTime? FechaInicio, DateTime? FechaFinal)
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
                if (FechaInicio == null || FechaFinal == null)
                {
                   // MessageBox.Show("FILTRO: " + filtro + "; NUMERO: " + numero + "; BUSQUEDA FECHA: " + busquedaFecha);
                    return dbHelp.DB.VistaEventos.Where(a => (a.descripcion.Contains(filtro) || a.titulo.Contains(filtro) ||
                                                     a.fecha_Sol.Value.Equals(busquedaFecha) || a.fecha_cierre.Value.Equals(busquedaFecha) || 
                                                     a.FechaInicio.Value.Equals(busquedaFecha) ||
                                                     a.titulo.Contains(filtro) || a.solicitante.Contains(filtro) ||
                                                     a.acomodo.Contains(filtro) || a.apoyo.Contains(filtro) ||
                                                     a.asistencia_aprox == numero || a.descripcion.Contains(filtro) ||
                                                     a.lugar.Contains(filtro) || a.observaciones.Contains(filtro) ||
                                                     a.responsable.Contains(filtro) || a.tipo_evento.Contains(filtro) 
                                                     ) && a.status == status).ToList();

                 /*   return dbHelp.VistaEventos.Where(a =>(a.acomodo.Contains(filtro) || a.apoyo.Contains(filtro) || 
                                                     a.fecha_Sol.Value.Equals(busquedaFecha) || a.fecha_cierre.Value.Equals(busquedaFecha) || 
                                                     a.FechaInicio.Value.Equals(busquedaFecha) || a.horaIn.Value.Hour == numero || a.horaIn.Value.Minute == numero || a.horaFn.Value.Hour == numero || 
                                                     a.horaFn.Value.Minute == numero || a.fecha_Sol.Value.Day == numero || a.fecha_Sol.Value.Month == numero || a.fecha_Sol.Value.Year == numero ||
                                                     a.fecha_cierre.Value.Day == numero || a.fecha_cierre.Value.Month == numero || a.fecha_cierre.Value.Year == numero ||
                                                     a.FechaInicio.Value.Day == numero || a.FechaInicio.Value.Month == numero || a.FechaInicio.Value.Year == numero ||
                                                     a.descripcion.Contains(filtro) || a.lugar.Contains(filtro) || a.responsable.Contains(filtro) ||
                                                     a.tipo_evento.Contains(filtro)) && (a.status == status) ).ToList();
               
                  */ }
                else
                {
                    return dbHelp.DB.VistaEventos.Where(a => (a.descripcion.Contains(filtro) || a.titulo.Contains(filtro) ||
                                                     a.fecha_Sol.Value.Equals(busquedaFecha) || a.fecha_cierre.Value.Equals(busquedaFecha) ||
                                                     a.FechaInicio.Value.Equals(busquedaFecha) ||
                                                     a.titulo.Contains(filtro) || a.solicitante.Contains(filtro) ||
                                                     a.acomodo.Contains(filtro) || a.apoyo.Contains(filtro) ||
                                                     a.asistencia_aprox == numero || a.descripcion.Contains(filtro) ||
                                                     a.lugar.Contains(filtro) || a.observaciones.Contains(filtro) ||
                                                     a.responsable.Contains(filtro) || a.tipo_evento.Contains(filtro)
                                                     ) && (a.status == status) && (a.FechaInicio >= FechaInicio && a.FechaInicio <= FechaFinal)).ToList();

 /*                   return dbHelp.VistaEventos.Where(a => (a.acomodo.Contains(filtro) || a.apoyo.Contains(filtro) ||
                                 a.fecha_Sol.Value.Equals(busquedaFecha) || a.fecha_cierre.Value.Equals(busquedaFecha) ||
                                 a.FechaInicio.Value.Equals(busquedaFecha) || a.horaIn.Value.Hour == numero || a.horaIn.Value.Minute == numero || a.horaFn.Value.Hour == numero || 
                                 a.horaFn.Value.Minute == numero || a.fecha_Sol.Value.Day.Equals(numero) || a.fecha_Sol.Value.Month.Equals(numero) || a.fecha_Sol.Value.Year.Equals(numero) ||
                                 a.fecha_cierre.Value.Day.Equals(numero) || a.fecha_cierre.Value.Month.Equals(numero) || a.fecha_cierre.Value.Year.Equals(numero) ||
                                 a.FechaInicio.Value.Day.Equals(numero) || a.FechaInicio.Value.Month.Equals(numero) || a.FechaInicio.Value.Year.Equals(numero) ||                                 
                                 a.descripcion.Contains(filtro) || a.lugar.Contains(filtro) || a.responsable.Contains(filtro) ||
                                 (a.FechaInicio >= FechaInicio) || (a.fecha_Sol >= FechaInicio && a.fecha_cierre <= FechaFinal) ||
                                 a.tipo_evento.Contains(filtro)) && (a.status == status)).ToList();*/

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public bool insertarEvento(int id, int res, int apo, string tit, int lug, DateTime feSol, string ac, string tipo, DateTime feIn, int asis, DateTime In, DateTime Fn, string desc)
        {       
            try
            {
                var evento = new tblevento
                {
                    titulo = tit,
                    descripcion = desc,
                    fecha_Sol = feSol,
                    solicitante = id,
                    responsable = res,
                    apoyo = apo,
                    lugar = lug,
                    acomodo = ac,
                    tipo_evento = tipo,
                    FechaInicio = feIn,
                    asistencia_aprox = asis,
                    horaIn = In,
                    horaFn = Fn,
                    status = 0,
                    observaciones = "Sin observaciones",

                };
                if (evento != null)
                {
                    dbHelp.DB.tbleventoes.Attach(evento);
                    dbHelp.DB.tbleventoes.Add(evento);
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Evento creado exitosamente");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                try
                {
                    using (StreamWriter w = File.AppendText("../../altaEventos.txt"))
                    {
                        string registro = feSol.Date + "    " + tit + "    " + desc + "    " + id + "    " + lug + "    " + ac + "    " + tipo + "    " + feIn.Date + "    " + In.TimeOfDay + "    " + Fn.TimeOfDay + "    " + asis + "    " + "0";
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
                var ItemAmodificar = dbHelp.DB.tbleventoes.SingleOrDefault(x => x.idEvento == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.status = status;
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

        public bool cerrarEvento(int id)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tbleventoes.SingleOrDefault(x => x.idEvento == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.status = 2;
                    ItemAmodificar.fecha_cierre = DateTime.Today;
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Evento cerrado exitosamente");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool asignarResponsable(int id, int res, int apo)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tbleventoes.SingleOrDefault(x => x.idEvento == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.status = 1;
                    ItemAmodificar.responsable = res;
                    ItemAmodificar.apoyo = apo;
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

        public bool editarEvento(int id, string tit, int lug, string aco, string tipo, DateTime feIn, int asis, DateTime In, DateTime Fn, string desc)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tbleventoes.SingleOrDefault(x => x.idEvento == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.titulo = tit;
                    ItemAmodificar.descripcion = desc;
                    ItemAmodificar.lugar = lug;
                    ItemAmodificar.acomodo = aco;
                    ItemAmodificar.tipo_evento = tipo;
                    ItemAmodificar.asistencia_aprox = asis;
                    ItemAmodificar.FechaInicio = feIn;
                    ItemAmodificar.horaIn = In;
                    ItemAmodificar.horaFn = Fn;
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

        public bool editarObservaciones(int id, String ob)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tbleventoes.SingleOrDefault(x => x.idEvento == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.observaciones = ob;
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

        public int obtenerUltimoEvento()
        {
            try
            {
                return dbHelp.DB.tbleventoes.Max(a => a.idEvento);
            }
            catch
            {
                return 1;
            }
        }
    }
}

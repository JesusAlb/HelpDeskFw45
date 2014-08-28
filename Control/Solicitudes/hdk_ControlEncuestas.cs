using HelpDesk.Modelo;
using HelpDesk.Principal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelpDesk.Control.Solicitudes
{
    class hdk_ControlEncuestas
    {

        hdk_ControlAcceso dbHelp;

        public hdk_ControlEncuestas(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public int obtenerNumeroEncuestasIn(int us){
            try
            {
                return dbHelp.DB.VistaIncidentes.Where(a => a.idSolicitante == us && a.statusCal_Servicio == false && a.status == 2).Count();
            }
            catch
            {
                return -1;
            }

        }

        public int obtenerNumeroEncuestasEv(int us)
        {
            try
            {
                return dbHelp.DB.VistaEventos.Where(a => a.idSolicitante == us && a.statusCal_Servicio == false && a.status == 2).Count();
            }
            catch
            {
                return 0;
            }
        }

        public IList cargarTablaPreguntas(int tema)
        {
            try
            {
                return dbHelp.DB.VistaPreguntas.Where(a => a.afinidad == tema || a.afinidad == 2).ToList();
            }
            catch 
            {
                return null;
            }
        }

        public IList cargarTablaEncuesta(int calidad)
        {
            try
            {
                return dbHelp.DB.VistaEncuestas.Where(a => a.idCalidad_Servicio == calidad).ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool insertarCalidadServicio(int fuente, int inev)
        {
            try
            {
                tblcalidadservicio calidad;

                if (fuente == 0)
                {
                    calidad = new tblcalidadservicio
                    {
                        incidente = inev,
                        observacionesServicio = "S/A",
                        statusCal_Servicio = false,
                        promedioCalidad = 0
                    };
                }
                else 
                {
                    calidad = new tblcalidadservicio
                    {
                        evento = inev,
                        observacionesServicio = "S/A",
                        statusCal_Servicio = false,
                        promedioCalidad = 0
                    };
                }
                
                if (calidad != null)
                {
                    dbHelp.DB.tblcalidadservicios.Attach(calidad);
                    dbHelp.DB.tblcalidadservicios.Add(calidad);
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool modificarCalidadServicio(int idCalidad, string ob, float promedio)
        {
            try
            {
                var calidadAModificar = dbHelp.DB.tblcalidadservicios.SingleOrDefault(a => a.idCalidad_Servicio == idCalidad);
                if (calidadAModificar != null)
                {
                    calidadAModificar.observacionesServicio = ob;
                    calidadAModificar.statusCal_Servicio = true;
                    calidadAModificar.promedioCalidad = promedio; 
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Gracias por brindarnos su opinión");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool insertarRespuesta(int calidad, int preg, int resp){
            
            try{
            var respuesta =  new tblrespuesta {

                calidadServicio = calidad,
                pregunta = preg,
                valorRespuesta = resp
                };
            if (respuesta != null)
            {
                dbHelp.DB.tblrespuestas.Attach(respuesta);
                dbHelp.DB.tblrespuestas.Add(respuesta);
                dbHelp.DB.SaveChanges();
                dbHelp.actualizarModelo();
                
                
            }
            return true;
            } catch(Exception ex){

                MessageBox.Show(ex.InnerException.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }
 
    }
}

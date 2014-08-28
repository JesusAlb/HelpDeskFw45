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
    class hdk_ControlUsuario
    {
        private hdk_ControlAcceso dbHelp;
        
        public hdk_ControlUsuario(hdk_ControlAcceso ca)
        {
            dbHelp = ca;
        }

        public int obtenerIdUsuario_SinAsignar(){
            try
            {
                return dbHelp.DB.tblusuarios.Where(a => a.nomUsuario.Equals("S/A")).SingleOrDefault().idUsuario;
            }
            catch
            {
                return 0;
            }
        }

        public ViewUsuario getUsuarioDetallado(int id)
        {
            try
            {
                return dbHelp.DB.ViewUsuarios.Where(a => a.idUsuario == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }

        public void cargarComboUsuarios(ComboBox cb, int tip)
        {
            try{
                if (tip == 0)
                {
                    cb.ItemsSource = this.dbHelp.DB.ViewUsuarios.Where(a => a.tipoUsuario == 0).ToList();
                }
                else if (tip == 1)
                {
                    cb.ItemsSource = this.dbHelp.DB.ViewUsuarios.Where(a => a.tipoUsuario == 1 && !a.nomUsuario.Equals("S/A")).ToList();
                }
                else if(tip == 2)
                {
                    cb.ItemsSource = cargarTabla("");
                }

                cb.DisplayMemberPath = "nomCompleto";
                cb.SelectedValuePath = "idUsuario";
            }
            catch
            {
                cb.Items.Add("Error");
            }
        }

        public void cargarComboInstitucion(ComboBox cb)
        {
            try
            {
                cb.ItemsSource = dbHelp.DB.tblinstitucions.Where(a => a.status == true).ToList();
                cb.DisplayMemberPath = "correoInstitucion";
                cb.SelectedValuePath = "idInstitucion";
            }catch{

            }
        }


        public IList cargarTabla(string filtro)
        {
            try
            {
                var query = dbHelp.DB.ViewUsuarios.Where(a => ((a.nomCompleto.Contains(filtro) || a.nomUsuario.Contains(filtro) ||
                                                               a.nomCoordinacion.Contains(filtro) || a.nomDepto.Contains(filtro) ||
                                                               a.nomPuesto.Contains(filtro) || a.nomArea.Contains(filtro) || a.nomInstitucion.Contains(filtro)
                                                               || a.tipoUsuarioString.Contains(filtro) || a.correo.Contains(filtro)) && !a.nomUsuario.Equals("S/A")));
                return query.ToList();
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
                var itemRemover = dbHelp.DB.tblusuarios.Single(x => x.idUsuario == id);
                if (itemRemover != null)
                {
                    dbHelp.DB.tblusuarios.Remove(itemRemover);
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

        public bool modificar(int id, string us, string nom, int niv, int dep, string ex, string correo, string con, int are, int pues, int inst)
        {
            try
            {
                var ItemAmodificar = dbHelp.DB.tblusuarios.SingleOrDefault(x => x.idUsuario == id);
                if (ItemAmodificar != null)
                {
                    ItemAmodificar.nomUsuario = us;
                    ItemAmodificar.nomCompleto = nom;
                    ItemAmodificar.password = con;
                    ItemAmodificar.tipoUsuario = niv;
                    ItemAmodificar.exTel = ex;
                    ItemAmodificar.correo = correo;
                    ItemAmodificar.depto = dep;
                    ItemAmodificar.area = are;
                    ItemAmodificar.puesto = pues;
                    ItemAmodificar.institucion = inst;
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Se actualizó exitosamente");
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool insertar(string us, string nom, int niv, int dep, string ex, string email, string con, int are, int pues, int inst)
        {
            try
            {
                var user = new tblusuario
                {
                    nomUsuario = us,
                    nomCompleto = nom,
                    tipoUsuario = niv,
                    depto = dep,
                    exTel = ex,
                    correo = email,
                    password = con,
                    area = are,
                    puesto = pues,
                    institucion = inst
                };
                if (user != null)
                {
                    dbHelp.DB.tblusuarios.Attach(user);
                    dbHelp.DB.tblusuarios.Add(user);
                    dbHelp.DB.SaveChanges();
                    dbHelp.actualizarModelo();
                    MessageBox.Show("Se añadió exitosamente");
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.InnerException.InnerException.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

        }

    }
}

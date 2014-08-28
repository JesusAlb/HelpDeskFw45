using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows;
using HelpDesk.Modelo;
using System.Globalization;
using System.Threading;
using System.IO;

namespace HelpDesk.Principal
{
  public class hdk_ControlAcceso
    {
        private dbhelpdeskEntities db;
        private ViewUsuario Item;


        public hdk_ControlAcceso()
        {
            this.actualizarModelo();
        }

        public void actualizarModelo()
        {
            try
            {
                DB = new dbhelpdeskEntities();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se puede conectar al servidor. Cierre el sistema y vuelva a intentarlo mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } 

        public dbhelpdeskEntities DB
        {
            get { return db; }
            set { db = value; }
        }

        public ViewUsuario item
      {
          get { return Item; }
          set { Item = value; }
      }

        public bool encontrarUsuario(String nombre, string password){
            bool regreso = true;
            try
            {
                
                var UsuarioItem = DB.ViewUsuarios.Where(x => x.nomUsuario == nombre && x.password == password).SingleOrDefault();
                if (UsuarioItem != null)
                {
                    item = UsuarioItem;
                }
                else
                {
                    MessageBox.Show("Contraseña o usuario inválidos", "Error",MessageBoxButton.OK, MessageBoxImage.Error);
                    regreso = false;
                }
            }
            catch (Exception ex)
          {
                MessageBox.Show("No se pudo conectar al servidor", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                regreso = false;
           }

            return regreso;

        }

    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HelpDesk.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblinstitucion
    {
        public tblinstitucion()
        {
            this.tblusuarios = new HashSet<tblusuario>();
        }
    
        public int idInstitucion { get; set; }
        public string nomInstitucion { get; set; }
        public string correoInstitucion { get; set; }
        public bool status { get; set; }
    
        public virtual ICollection<tblusuario> tblusuarios { get; set; }
    }
}

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
    
    public partial class tblpuesto
    {
        public tblpuesto()
        {
            this.tblusuarios = new HashSet<tblusuario>();
        }
    
        public int idPuesto { get; set; }
        public string nomPuesto { get; set; }
    
        public virtual ICollection<tblusuario> tblusuarios { get; set; }
    }
}

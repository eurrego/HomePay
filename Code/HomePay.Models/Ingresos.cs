//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HomePay.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ingresos
    {
        public int IdIngresos { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public System.DateTime Fecha { get; set; }
        public string Persona { get; set; }
        public Nullable<int> IdCategoria { get; set; }
    
        public virtual Categoria Categoria { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//    Dieser Code wurde aus einer Vorlage generiert.
//
//    Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten Ihrer Anwendung.
//    Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZYClkETicaret.WEBUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MusteriAdres
    {
        public int Id { get; set; }
        public Nullable<System.Guid> MusteriID { get; set; }
        public string Adi { get; set; }
        public string Adres { get; set; }
    
        public virtual Musteri Musteri { get; set; }
    }
}

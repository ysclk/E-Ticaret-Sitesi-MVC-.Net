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
    
    public partial class Kargo
    {
        public Kargo()
        {
            this.Satis = new HashSet<Satis>();
        }
    
        public int Id { get; set; }
        public string SirketAdi { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string WebSite { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<Satis> Satis { get; set; }
    }
}

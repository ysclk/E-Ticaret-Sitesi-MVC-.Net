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
    
    public partial class Musteri
    {
        public Musteri()
        {
            this.MusteriAdres = new HashSet<MusteriAdres>();
            this.Satis = new HashSet<Satis>();
        }
    
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string KullaniciAdi { get; set; }
        public string EMail { get; set; }
        public string Telefon { get; set; }
        public System.Guid Id { get; set; }
    
        public virtual ICollection<MusteriAdres> MusteriAdres { get; set; }
        public virtual ICollection<Satis> Satis { get; set; }
    }
}

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
    
    public partial class OzellikDeger
    {
        public OzellikDeger()
        {
            this.UrunOzellik = new HashSet<UrunOzellik>();
        }
    
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Aciklama { get; set; }
        public int OzellikTipID { get; set; }
    
        public virtual ICollection<UrunOzellik> UrunOzellik { get; set; }
        public virtual OzellikTip OzellikTip { get; set; }
    }
}

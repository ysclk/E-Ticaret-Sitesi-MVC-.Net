using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZYClkETicaret.WEBUI.Models;

namespace ZYClkETicaret.WEBUI.App_Classes
{
    public class Context
    {
		private static ETicaretEntities2 baglanti;

		public static ETicaretEntities2 Baglanti
		{
			get
			{ 
				if(baglanti == null)
				 baglanti = new ETicaretEntities2();
				return baglanti;
			}
			set { baglanti = value; }
		}

	}
}
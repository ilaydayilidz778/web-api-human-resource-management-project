using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
	public class Harcama
	{
        public int Id { get; set; }

        public string KullaniciId { get; set; } = null!;
        public Kullanici Kullanici { get; set; } = null!;
        public int HarcamaTuruId { get; set; }
        public HarcamaTuru HarcamaTuru { get; set; } = null!;

        [Precision(18, 2)]
        public decimal Tutar { get; set; }

        public int ParaBirimiId { get; set; }
        public ParaBirimi ParaBirimi { get; set; }=null!;

        public bool? OnayDurumu { get; set; }
		public bool AktiflikDurumu { get; set; }


		public DateOnly TalepTarihi { get; set; }
        public DateOnly? CevaplanmaTarihi { get; set; }

        public string? Dosya { get; set; }

    }
}

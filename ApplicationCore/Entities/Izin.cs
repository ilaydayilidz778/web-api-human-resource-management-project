using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
	public class Izin
	{
        public int Id { get; set; }
        public string KullaniciId { get; set; } = null!;
        public Kullanici Kullanici { get; set; } = null!;

        public DateOnly TalepTarihi { get; set; }

        public DateOnly IzinBaslangicTarihi { get; set; }
        public DateOnly IzinBitisTarihi { get; set; }

        public int GunSayisi { get; set; }

        public int IzinTuruId { get; set; }

        public IzinTuru IzinTuru { get; set; } = null!;

        public bool? OnayDurumu { get; set; }
        public bool AktiflikDurumu { get; set; }

        public string?  Belge { get; set; }


    }
}

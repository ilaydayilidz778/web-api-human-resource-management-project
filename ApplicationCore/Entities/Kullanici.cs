using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
	public class Kullanici : IdentityUser
	{
        // özet bilgi bölümü ---------------------

        public string? PhotoByte { get; set; }
        public string Ad { get; set; } = null!;
		public string? IkinciAd { get; set; }

		public string Soyad { get; set; } = null!;
		public string? IkinciSoyad { get; set; }

		public string Cinsiyet { get; set; } = null!;

        public string? Meslek { get; set; }

		public int? DepartmanId { get; set; }

		public Departman? Departman { get; set; }


		//Profil detay bölümü --------------------------

		public string? TelefonNumarasi { get; set; }
		public DateOnly DogumTarihi { get; set; }
		public string? DogumYeri { get; set; }
		public string TCKimlikNumarasi { get; set; } = null!;
		public DateOnly IseGirisTarihi { get; set; }
		public DateOnly? IstenCikistarihi { get; set; }
		public bool AktiflikDurumu { get; set; }

		public string? Adres { get; set; }

		[Precision(18, 2)]
		public decimal? Maas { get; set; }

		public int? MaasTipiId { get; set; }

		public MaasTipi? MaasTipi { get; set; } = null!;

		public int FirmaId { get; set; }
		public Firma Firma { get; set; } = null!;

		public string? ResetToken { get; set; }

        public string? LoginToken { get; set; }

    }
}

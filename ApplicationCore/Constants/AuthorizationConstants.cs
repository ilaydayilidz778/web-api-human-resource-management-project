using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Constants
{
	public class AuthorizationConstants
	{
		public const string DEFAULT_SISTEM_PAROLA = "Admin1234.";

		public const string DEFAULT_MUSTERI_PAROLA = "Abc1234.";

		public const int DEFAULT_SISTEM_FIRMA_ID = 1;
		public const string DEFAULT_SISTEM_FIRMA = "Human Resources Management Web Service";
		public const string DEFAULT_SISTEM_FIRMA_EMAILEKLENTISI = "hrm.com";


		public const int DEFAULT_MUSTERI_FIRMA_ID = 2; 
		public const string DEFAULT_MUSTERI_FIRMA = "ABC Yazılım A.Ş.";
		public const string DEFAULT_MUSTERI_FIRMA_EMAILEKLENTISI = "abcyazilim.com";

		public const int FIRMA_SAHIP_DEPARTMAN_ID = 1;
		public const string FIRMA_SAHIP_DEPARTMAN = "Patron";
		public class DemoSistemYoneticisi
		{
			public const string EMAIL = "admin@example.com";
			public const string AD = "Admin";
			public const string SOYAD = "Admin"; 
			public const string TCKIMLIKNUMARASI = "12345678901";
			public const int FIRMAID=DEFAULT_SISTEM_FIRMA_ID; 
		}
		public class DemoFirmaSahibi 
		{
			public const string EMAIL = "owner@example.com";

			public const string FOTOGRAFURL = "";
			public const string AD = "Ahmet";
			public const string IKINCIAD = "Mehmet";
			public const string SOYAD = "Yılmaz";
			public const string IKINCISOYAD = "Kara";
			public const string MESLEK = "Yazılım Mühendisi";
			public static readonly DateOnly DOGUMTARIHI = new DateOnly(1973,08,11);
			public const string DOGUMYERI = "İstanbul";
			public const string TCKIMLIKNUMARASI = "12345678901";
			public static readonly DateOnly ISEGIRISTARIHI = new DateOnly(1999, 12, 5);
			public const bool AKTIFLIKDURUMU = true;
			public const string ADRES = "İstanbul, Türkiye";
			public const decimal MAAS = 80001.50m;
			public const int FIRMAID =DEFAULT_MUSTERI_FIRMA_ID;
			public const int DEPARTMANID =FIRMA_SAHIP_DEPARTMAN_ID;
			public const string TELEFONNUMARASI = "05554443322";
		}

		public static class Roller
		{
			public const string SISTEM_YONETICI = "Admin"; 
			public const string FIRMA_SAHIP = "Firma Sahibi"; 
			public const string FIRMA_CALISAN = "Personel"; 
		}
	}
}

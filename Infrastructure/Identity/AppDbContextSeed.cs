using ApplicationCore.Constants;
using ApplicationCore.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
	public class AppDbContextSeed
	{
		public static async Task SeedAsync(AppDbContext db, RoleManager<IdentityRole> rolemanager, UserManager<Kullanici> userManager)
		{
			await db.Database.MigrateAsync();



			if (await db.Departmanlar.AnyAsync() || await db.Firmalar.AnyAsync() || await db.Firmalar.AnyAsync())
			{
				return;
			}

			var d1 = new Departman() { Ad = AuthorizationConstants.FIRMA_SAHIP_DEPARTMAN, AktiflikDurumu = true };
			var d2 = new Departman() { Ad = "Planlama", AktiflikDurumu = true };
			var d3 = new Departman() { Ad = "Üretim", AktiflikDurumu = true };

			var f1 = new Firma() { Ad = AuthorizationConstants.DEFAULT_SISTEM_FIRMA, AktiflikDurumu = true, EmailEklentisi = AuthorizationConstants.DEFAULT_SISTEM_FIRMA_EMAILEKLENTISI ,MaximumIsAvansi=50000};

			var f2 = new Firma() { Ad = AuthorizationConstants.DEFAULT_MUSTERI_FIRMA, AktiflikDurumu = true, EmailEklentisi = AuthorizationConstants.DEFAULT_MUSTERI_FIRMA_EMAILEKLENTISI, MaximumIsAvansi = 50000 };

			var mt1 = new MaasTipi() { Ad = "Bordro" };
			var mt2 = new MaasTipi() { Ad = "Sözleşme" };

			await db.AddRangeAsync(mt1, mt2, d1, d2, d3, f1, f2);
			await db.SaveChangesAsync();

			//İzin Türleri

			var izinTuru1 = new IzinTuru() {  Ad = "Doğum", IzinSuresi = 80 };
			var izinTuru2 = new IzinTuru() {  Ad = "Babalık", IzinSuresi = 5 };
			var izinTuru3 = new IzinTuru() {  Ad = "Evlilik", IzinSuresi = 3 };
			var izinTuru4 = new IzinTuru() {  Ad = "Ölüm", IzinSuresi = 3 };
			var izinTuru5 = new IzinTuru() {  Ad = "Hastalık" };
			var izinTuru6 = new IzinTuru() {  Ad = "Ücretsiz" };
			var izinTuru7 = new IzinTuru() {  Ad = "Yıllık" };

			 await db.AddRangeAsync(
				izinTuru1, izinTuru2, izinTuru3, izinTuru4, izinTuru5, izinTuru6, izinTuru7
				);
			 await db.SaveChangesAsync();

			// para birimleri

			var pb1 = new ParaBirimi() { Ad = "Türk Lirası", Simge = "₺" ,Kod="TRY"};
			var pb2 = new ParaBirimi() { Ad = "Dolar", Simge = "$", Kod = "ABD DOLARI" };
			var pb3 = new ParaBirimi() { Ad = "Euro", Simge = "€", Kod = "EURO" };

			await db.AddRangeAsync(pb1, pb2, pb3);
			await db.SaveChangesAsync();

			// avans turleri

			var at1 = new AvansTuru() { Ad = "Ücret" };
			var at2 = new AvansTuru() { Ad = "İş" };

			await db.AddRangeAsync(at1, at2);
			await db.SaveChangesAsync();

			// harcama türleri

			var ht1 = new HarcamaTuru() { Ad = "Eğitim" };
			var ht2 = new HarcamaTuru() { Ad = "Yol" };
			var ht3 = new HarcamaTuru() { Ad = "Ekipman" };

			await db.AddRangeAsync(ht1, ht2, ht3);
			await db.SaveChangesAsync();






			//roller

			if (!await rolemanager.RoleExistsAsync(AuthorizationConstants.Roller.SISTEM_YONETICI))
			{
				await rolemanager.CreateAsync(new IdentityRole(AuthorizationConstants.Roller.SISTEM_YONETICI));
			}
			if (!await rolemanager.RoleExistsAsync(AuthorizationConstants.Roller.FIRMA_SAHIP))
			{
				await rolemanager.CreateAsync(new IdentityRole(AuthorizationConstants.Roller.FIRMA_SAHIP));
			}
			if (!await rolemanager.RoleExistsAsync(AuthorizationConstants.Roller.FIRMA_CALISAN))
			{
				await rolemanager.CreateAsync(new IdentityRole(AuthorizationConstants.Roller.FIRMA_CALISAN));
			}

			//demo kullanıcılar
			if (!await userManager.Users.AnyAsync(x => x.Email == AuthorizationConstants.DemoSistemYoneticisi.EMAIL))
			{
				var sistemYoneticisi = new Kullanici()
				{
					Email = AuthorizationConstants.DemoSistemYoneticisi.EMAIL,
					UserName = "admin@hrm.com",
					EmailConfirmed = true,
					Ad = "Admin",
					Soyad = "Admin",
					TCKimlikNumarasi = AuthorizationConstants.DemoSistemYoneticisi.TCKIMLIKNUMARASI,

					Firma = f1,

					Cinsiyet="Erkek"
				};
				await userManager.CreateAsync(sistemYoneticisi, AuthorizationConstants.DEFAULT_SISTEM_PAROLA);
				await userManager.AddToRoleAsync(sistemYoneticisi, AuthorizationConstants.Roller.SISTEM_YONETICI);
			}
			if (!await userManager.Users.AnyAsync(x => x.Email == AuthorizationConstants.DemoFirmaSahibi.EMAIL))
			{
				var firmaSahibi = new Kullanici()
				{
					Email = AuthorizationConstants.DemoFirmaSahibi.EMAIL,
					UserName = "ahmetyilmaz@abcyazilim.com",
					EmailConfirmed = true,

					Ad = AuthorizationConstants.DemoFirmaSahibi.AD,
					IkinciAd = AuthorizationConstants.DemoFirmaSahibi.IKINCIAD,
					Soyad = AuthorizationConstants.DemoFirmaSahibi.SOYAD,
					IkinciSoyad = AuthorizationConstants.DemoFirmaSahibi.IKINCISOYAD,
					DepartmanId = AuthorizationConstants.DemoFirmaSahibi.DEPARTMANID,
					Meslek = AuthorizationConstants.DemoFirmaSahibi.MESLEK,
					DogumTarihi = AuthorizationConstants.DemoFirmaSahibi.DOGUMTARIHI,
					DogumYeri = AuthorizationConstants.DemoFirmaSahibi.DOGUMYERI,
					TCKimlikNumarasi = AuthorizationConstants.DemoFirmaSahibi.TCKIMLIKNUMARASI,
					IseGirisTarihi = AuthorizationConstants.DemoFirmaSahibi.ISEGIRISTARIHI,
					AktiflikDurumu = AuthorizationConstants.DemoFirmaSahibi.AKTIFLIKDURUMU,
					Adres = AuthorizationConstants.DemoFirmaSahibi.ADRES,
					Maas = AuthorizationConstants.DemoFirmaSahibi.MAAS,
					MaasTipi = mt1,
					Firma = f2,
					TelefonNumarasi = AuthorizationConstants.DemoFirmaSahibi.TELEFONNUMARASI,

					Cinsiyet = "Erkek"

				};
				await userManager.CreateAsync(firmaSahibi, AuthorizationConstants.DEFAULT_MUSTERI_PAROLA);
				await userManager.AddToRoleAsync(firmaSahibi, AuthorizationConstants.Roller.FIRMA_SAHIP);

				//İzinler

				var izin1 = new Izin()
				{
					Kullanici = firmaSahibi,
					TalepTarihi = new DateOnly(2024, 4, 1),
					IzinBaslangicTarihi = new DateOnly(2024, 4, 5 ),
					GunSayisi=5,
					IzinTuru=izinTuru2,
					AktiflikDurumu=true,
				};
				var izin2 = new Izin()
				{
					Kullanici = firmaSahibi,
					TalepTarihi = new DateOnly(2024, 3, 5),
					IzinBaslangicTarihi = new DateOnly(2024, 3, 15 ),
					GunSayisi=3,
					IzinTuru=izinTuru3,
					OnayDurumu=false,
					AktiflikDurumu=true,
				};
				var izin3 = new Izin()
				{
					Kullanici = firmaSahibi,
					TalepTarihi = new DateOnly(2024, 4, 2),
					IzinBaslangicTarihi = new DateOnly(2024, 5, 10 ),
					GunSayisi=80,
					IzinTuru=izinTuru5,
					OnayDurumu=true,
					AktiflikDurumu=true,
				};

				await db.AddRangeAsync(izin1, izin2, izin3);
				await db.SaveChangesAsync();
			}








		}
	}
}


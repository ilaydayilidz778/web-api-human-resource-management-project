using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Controllers;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
	public class KullaniciModelService : IKullaniciModelService
	{
		private readonly UserManager<Kullanici> _userManager;
		private readonly IIzinRepository _izinRepository;
		private readonly AppDbContext _context;
		private readonly RoleManager<IdentityRole> _roleManager;

		public KullaniciModelService(UserManager<Kullanici> userManager, IIzinRepository izinRepository,AppDbContext context,RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_izinRepository = izinRepository;
			_context = context;
			_roleManager = roleManager;
		}


		public async Task<KullaniciModel> KullaniciGetir(string id)
		{
			var kullanici = await _userManager.Users.Include(x => x.Departman).Include(x => x.Firma).Include(x => x.MaasTipi).FirstOrDefaultAsync(x => x.Id == id);

			if (kullanici == null)
			{
				return null!;
			}

			var roles = await _userManager.GetRolesAsync(kullanici);

			KullaniciModel model = new KullaniciModel()
			{
				Id = kullanici.Id,
				PhotoByte = kullanici.PhotoByte,
				FirstName = kullanici.Ad,
				SecondName = kullanici.IkinciAd,
				LastName = kullanici.Soyad,
				SecondLastName = kullanici.IkinciSoyad,
				Gender=kullanici.Cinsiyet,

				Job = kullanici.Meslek,
				Department = kullanici.Departman?.Ad,

				Email = kullanici.Email!,
				Contact = kullanici.TelefonNumarasi,
				BirthDate = kullanici.DogumTarihi,
				BirthPlace = kullanici.DogumYeri,
				IdentityNumber = kullanici.TCKimlikNumarasi,
				DateOfRecruitment = kullanici.IseGirisTarihi,
				DateOfDismissal = kullanici.IstenCikistarihi,

				Address = kullanici.Adres,
				Salary = kullanici.Maas,
				SalaryType = kullanici.MaasTipi?.Ad,
				CompanyName = kullanici.Firma.Ad,
				Role = roles[0],
				LoginToken = kullanici.LoginToken
			};
			model.KalanYillikIzin = await KalanYillikIzin(kullanici.Id);
			model.ToplamYillikIzin = await ToplamYillikIzin(kullanici.Id);

			return model;
		}

		public async Task<KullaniciModel> KullaniciyiTokenlaGetir(string token)
		{
			var kullanici = await _userManager.Users.Include(x => x.Departman).Include(x => x.Firma).Include(x => x.MaasTipi).FirstOrDefaultAsync(x => x.LoginToken == token);

			if (kullanici == null)
			{
				return null!;
			}

			var roles = await _userManager.GetRolesAsync(kullanici);

			KullaniciModel model = new KullaniciModel()
			{
				Id = kullanici.Id,
				PhotoByte = kullanici.PhotoByte,
				FirstName = kullanici.Ad,
				SecondName = kullanici.IkinciAd,
				LastName = kullanici.Soyad,
				SecondLastName = kullanici.IkinciSoyad,

				Gender = kullanici.Cinsiyet,

				Job = kullanici.Meslek,
				Department = kullanici.Departman?.Ad,

				Email = kullanici.Email!,
				Contact = kullanici.TelefonNumarasi,
				BirthDate = kullanici.DogumTarihi,
				BirthPlace = kullanici.DogumYeri,
				IdentityNumber = kullanici.TCKimlikNumarasi,
				DateOfRecruitment = kullanici.IseGirisTarihi,
				DateOfDismissal = kullanici.IstenCikistarihi,

				Address = kullanici.Adres,
				Salary = kullanici.Maas,
				SalaryType = kullanici.MaasTipi?.Ad,
				CompanyName = kullanici.Firma.Ad,
				Role = roles[0],
				LoginToken = kullanici.LoginToken,
			};
			model.KalanYillikIzin = await KalanYillikIzin(kullanici.Id);
			model.ToplamYillikIzin = await ToplamYillikIzin(kullanici.Id);

			return model;
		}

		public async Task<List<KullaniciModel>> TumKullanicilar()
		{
			var userRoles = await _context.UserRoles.ToListAsync();
			var roles = await _roleManager.Roles.ToListAsync();


			var tumKullanicilar= await _userManager.Users.Include(x => x.Departman).Include(x => x.Firma).Select(x =>

			 new KullaniciModel
			 {
				 Id = x.Id,
				 PhotoByte = x.PhotoByte,
				 FirstName = x.Ad,
				 SecondName = x.IkinciAd,
				 LastName = x.Soyad,
				 SecondLastName = x.IkinciSoyad,

				 Gender = x.Cinsiyet,

				 Job = x.Meslek,
				 Department = x.Departman != null ? x.Departman.Ad : null,

				 Email = x.Email!,
				 Contact = x.TelefonNumarasi,
				 BirthDate = x.DogumTarihi,
				 BirthPlace = x.DogumYeri,
				 IdentityNumber = x.TCKimlikNumarasi,
				 DateOfRecruitment = x.IseGirisTarihi,
				 DateOfDismissal = x.IstenCikistarihi,

				 Address = x.Adres,
				 Salary = x.Maas,
				 SalaryType = x.MaasTipi != null ? x.MaasTipi.Ad : null,
				 CompanyName = x.Firma.Ad,

				 LoginToken = x.LoginToken
			 }

			 ).ToListAsync();

			foreach(var kullanici in tumKullanicilar)
			{
				string id=userRoles.FirstOrDefault(x=>x.UserId==kullanici.Id).RoleId;
				kullanici.Role = roles.FirstOrDefault(x => x.Id == id).Name;
			}

			return tumKullanicilar;
		}
		public async Task<int> KalanYillikIzin(string id)
		{

			var toplamYillikIzin = await ToplamYillikIzin(id);

			var izinGunleri= await _izinRepository.KullaniciToplamIzin(id);

			return toplamYillikIzin -izinGunleri;
		}

		public async Task<int> ToplamYillikIzin(string id)
		{
			var kullanici = await _userManager.FindByIdAsync(id);

			if (kullanici.IseGirisTarihi > DateOnly.FromDateTime(DateTime.Today.AddYears(-1)))
			{
				return 0;
			}

			int gun;

			if (kullanici.IseGirisTarihi > DateOnly.FromDateTime(DateTime.Today.AddYears(-5)))
			{
				gun = 14;
			}

			else if (kullanici.IseGirisTarihi > DateOnly.FromDateTime(DateTime.Today.AddYears(-15)))
			{
				gun = 20;
			}
			else
			{
				gun = 26;
			}
			return gun;
		}



	}
}


// bitiş tarihi ekle - toplam izin günü

using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
	public class IzinRepository : IIzinRepository
	{
		private readonly AppDbContext _context;
		private readonly IBaseRepository<Firma> _firmaRepository;
		private readonly UserManager<Kullanici> _userManager;

		public IzinRepository(AppDbContext context, IBaseRepository<Firma> firmaRepository,UserManager<Kullanici> userManager)
		{
			_context = context;
			_firmaRepository = firmaRepository;
			_userManager = userManager;
		}


		public async Task IzinEkle(Izin izin)
		{
			await _context.AddAsync(izin);
			await _context.SaveChangesAsync();
		}

		public async Task IzinIptalEt(int id)
		{
			Izin izin = await _context.Izinler.FindAsync(id);

			if (izin == null)
			{
				throw new Exception();

			}

			izin.AktiflikDurumu = false;
			_context.Update(izin);
			await _context.SaveChangesAsync();

		}

		public async Task IzinOnayla(int id)
		{
			await OnayDurumu(id, true);
		}

		public async Task IzinReddet(int id)
		{
			await OnayDurumu(id, false);

		}

		public async Task<List<Izin>> TumIzinler()
		{
			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).ThenInclude(x => x.Departman).ToListAsync();
		}

		// Firma

		public async Task<List<Izin>> TumIzinlerFirmayaGore(string firmaAd)
		{
			Firma firma = await FirmaIdGetir(firmaAd);

			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id).ToListAsync();
		}
		public async Task<List<Izin>> TumIzinlerFirmayaGoreOnayli(string firmaAd)
		{
			Firma firma = await FirmaIdGetir(firmaAd);

			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu == true).ToListAsync();
		}
		public async Task<List<Izin>> TumIzinlerFirmayaGoreRed(string firmaAd)
		{
			Firma firma = await FirmaIdGetir(firmaAd);

			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu == false).ToListAsync();
		}
		public async Task<List<Izin>> TumIzinlerFirmayaGoreBekleme(string firmaAd)
		{
			Firma firma = await FirmaIdGetir(firmaAd);

			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu == null).ToListAsync();
		}
		public async Task<List<Izin>> TumIzinlerFirmayaGoreIptal(string firmaAd)
		{
			Firma firma = await FirmaIdGetir(firmaAd);

			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && !x.AktiflikDurumu).ToListAsync();
		}

		// Kullanıcı

		public async Task<List<Izin>> TumIzinlerKullaniciyaGore(string id)
		{
			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).Where(x => x.Kullanici.Id == id).ToListAsync();

		}

		public async Task<List<Izin>> TumIzinlerKullaniciyaGoreOnayli(string id)
		{
			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).Where(x => x.Kullanici.Id == id && x.AktiflikDurumu && x.OnayDurumu==true).ToListAsync();
		}

		public async Task<List<Izin>> TumIzinlerKullaniciyaGoreRed(string id)
		{
			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).Where(x => x.Kullanici.Id == id && x.AktiflikDurumu && x.OnayDurumu == false).ToListAsync();

		}

		public async Task<List<Izin>> TumIzinlerKullaniciyaGoreBekleme(string id)
		{
			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).Where(x => x.Kullanici.Id == id && x.AktiflikDurumu && x.OnayDurumu == null).ToListAsync();
		}
		public async Task<List<Izin>> TumIzinlerKullaniciyaGoreIptal(string id)
		{
			return await _context.Izinler.Include(x => x.IzinTuru).Include(x => x.Kullanici).Where(x => x.Kullanici.Id == id && !x.AktiflikDurumu).ToListAsync();
		}

		// İşlemler

		private async Task OnayDurumu(int id, bool durum)
		{
			Izin izin = await _context.Izinler.FindAsync(id);

			if (izin == null)
			{
				throw new Exception();
			}

			izin.OnayDurumu = durum;
			_context.Update(izin);
			await _context.SaveChangesAsync();
		}

		private async Task<Firma> FirmaIdGetir(string FirmaAd)
		{
			return await _firmaRepository.AdaGoreGetir(FirmaAd);
		}

		public async Task<int> KullaniciToplamIzin(string id)
		{
			Kullanici kullanici= await _userManager.FindByIdAsync(id);

			if (kullanici == null)
			{
				throw new Exception();
			}

			DateOnly tarih=new DateOnly(DateTime.Today.Year,kullanici.IseGirisTarihi.Month,kullanici.IseGirisTarihi.Day);

			if (tarih > DateOnly.FromDateTime(DateTime.Today))
			{
				tarih = tarih.AddYears(-1);
			}

			var izinGunleri = await _context.Izinler.Where(x => x.KullaniciId == id && x.IzinTuru.Ad == "Yıllık"&& x.OnayDurumu==true && x.AktiflikDurumu==true && x.IzinBaslangicTarihi>tarih).SumAsync(x => x.GunSayisi);
			return izinGunleri;
		}


	}
}

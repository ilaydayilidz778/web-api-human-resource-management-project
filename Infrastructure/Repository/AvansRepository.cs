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
	public class AvansRepository : IAvansRepository
	{
		private readonly AppDbContext _context;
		private readonly IBaseRepository<Firma> _firmaRepository;

		public AvansRepository(AppDbContext context,IBaseRepository<Firma> firmaRepository)
        {
			_context = context;
			_firmaRepository = firmaRepository;
		}

		public async Task<List<Avans>> TumAvanslar()
		{
			return await _context.Avanslar.Include(x=>x.AvansTuru).Include(x => x.ParaBirimi).Include(x=>x.Kullanici).ThenInclude(x=>x.Departman).ToListAsync();
		}

		// Firma

		public async Task<List<Avans>> TumAvanslarFirmayaGore(string firmaAd)
		{

			var firma = await FirmaAdaGoreGetir(firmaAd);

			return await _context.Avanslar.Include(x => x.AvansTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x=>x.Kullanici.FirmaId==firma.Id).ToListAsync();
		}

		public async Task<List<Avans>> TumAvanslarFirmayaGoreBekleme(string firmaAd)
		{

			var firma = await FirmaAdaGoreGetir(firmaAd);

			return await _context.Avanslar.Include(x => x.AvansTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu==null).ToListAsync();
		}

		public async Task<List<Avans>> TumAvanslarFirmayaGoreOnayli(string firmaAd)
		{
			var firma = await FirmaAdaGoreGetir(firmaAd);

			return await _context.Avanslar.Include(x => x.AvansTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu == true).ToListAsync();
		}

		public async Task<List<Avans>> TumAvanslarFirmayaGoreRed(string firmaAd)
		{
			var firma = await FirmaAdaGoreGetir(firmaAd);

			return await _context.Avanslar.Include(x => x.AvansTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu == false).ToListAsync();
		}

		// Kullanici

		public async Task<List<Avans>> TumAvanslarKullaniciyaGore(string id)
		{
			return await _context.Avanslar.Include(x => x.AvansTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).Where(x => x.KullaniciId == id).ToListAsync();
		}

		public async Task<List<Avans>> TumAvanslarKullaniciyaGoreBekleme(string id)
		{
			return await _context.Avanslar.Include(x => x.AvansTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).Where(x => x.KullaniciId == id && x.AktiflikDurumu && x.OnayDurumu==null).ToListAsync();
		}

		public async Task<List<Avans>> TumAvanslarKullaniciyaGoreOnayli(string id)
		{
			return await _context.Avanslar.Include(x => x.AvansTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).Where(x => x.KullaniciId == id && x.AktiflikDurumu && x.OnayDurumu == true).ToListAsync();
		}

		public async Task<List<Avans>> TumAvanslarKullaniciyaGoreRed(string id)
		{
			return await _context.Avanslar.Include(x => x.AvansTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).Where(x => x.KullaniciId == id && x.AktiflikDurumu && x.OnayDurumu == false).ToListAsync();
		}
        public async Task AvansEkle(Avans avans)
		{
			await _context.AddAsync(avans);
			await _context.SaveChangesAsync();
		}

		public async Task AvansIptalEt(int id)
		{
			Avans avans = await _context.Avanslar.FindAsync(id);

			if (avans == null)
			{
				throw new Exception();
			}
			avans.AktiflikDurumu = false;

			_context.Update(avans);
			await _context.SaveChangesAsync();
		}

		public async Task AvansOnayla(int id)
		{
			await OnayDurumu(id, true);
		}

		public async Task AvansReddet(int id)
		{
			await OnayDurumu(id, false);

		}

		public async Task<Firma> FirmaAdaGoreGetir(string firmaAd)
		{
			Firma firma = await _firmaRepository.AdaGoreGetir(firmaAd);
			if (firma == null)
			{
				throw new Exception();
			}
			return firma;
		}

		public async Task OnayDurumu(int id, bool durum)
		{
			Avans avans = await _context.Avanslar.FindAsync(id);

			if(avans == null)
			{
				throw new Exception();
			}
			avans.OnayDurumu = durum;
			avans.CevaplanmaTarihi = DateOnly.FromDateTime(DateTime.Today);
			_context.Update(avans);
			await _context.SaveChangesAsync();
		}
	}
}

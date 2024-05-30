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
	public class HarcamaRepository : IHarcamaRepository
	{
		private readonly AppDbContext _context;
		private readonly IBaseRepository<Firma> _firmaRepository;

		public HarcamaRepository(AppDbContext context,  IBaseRepository<Firma> firmaRepository)
        {
			_context = context;
			_firmaRepository = firmaRepository;
		}

        public async Task<List<Harcama>> TumHarcamalar()
		{
			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).ToListAsync();
		}

		// Firma

		public async Task<List<Harcama>> TumHarcamalarFirmayaGore(string firmaAd)
		{
			var firma = await FirmaAdaGoreGetir(firmaAd);

			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id).ToListAsync();
		}

		public async Task<List<Harcama>> TumHarcamalarFirmayaGoreBekleme(string firmaAd)
		{
			var firma = await FirmaAdaGoreGetir(firmaAd);

			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu == null).ToListAsync();
		}

		public async Task<List<Harcama>> TumHarcamalarFirmayaGoreOnayli(string firmaAd)
		{
			var firma = await FirmaAdaGoreGetir(firmaAd);

			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu == true).ToListAsync();
		}

		public async Task<List<Harcama>> TumHarcamalarFirmayaGoreRed(string firmaAd)
		{
			var firma = await FirmaAdaGoreGetir(firmaAd);

			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).ThenInclude(x => x.Departman).Where(x => x.Kullanici.FirmaId == firma.Id && x.AktiflikDurumu && x.OnayDurumu == false).ToListAsync();
		}

		// Kullanıcı

		public async Task<List<Harcama>> TumHarcamalarKullaniciyaGore(string id)
		{
			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).Where(x => x.KullaniciId == id).ToListAsync();
		}

		public async Task<List<Harcama>> TumHarcamalarKullaniciyaGoreBekleme(string id)
		{
			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).Where(x => x.KullaniciId == id && x.AktiflikDurumu && x.OnayDurumu == null).ToListAsync();
		}

		public async Task<List<Harcama>> TumHarcamalarKullaniciyaGoreOnayli(string id)
		{
			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).Where(x => x.KullaniciId == id && x.AktiflikDurumu && x.OnayDurumu == true).ToListAsync();
		}

		public async Task<List<Harcama>> TumHarcamalarKullaniciyaGoreRed(string id)
		{
			return await _context.Harcamalar.Include(x => x.HarcamaTuru).Include(x => x.ParaBirimi).Include(x => x.Kullanici).Where(x => x.KullaniciId == id && x.AktiflikDurumu && x.OnayDurumu == false).ToListAsync();
		}

		// İşlemler

		public async Task HarcamaEkle(Harcama harcama)
		{
			await _context.AddAsync(harcama);
			await _context.SaveChangesAsync();
		}

		public async Task HarcamaIptalEt(int id)
		{
			Harcama harcama = await _context.Harcamalar.FindAsync(id);

			if (harcama == null)
			{
				throw new Exception();
			}
			harcama.AktiflikDurumu = false;
			_context.Update(harcama);
			await _context.SaveChangesAsync();
		}

		public async Task HarcamaOnayla(int id)
		{
			await OnayDurumu(id, true);
		}

		public async Task HarcamaReddet(int id)
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
			Harcama harcama = await _context.Harcamalar.FindAsync(id);

			if (harcama == null)
			{
				throw new Exception();
			}
			harcama.OnayDurumu = durum;
			harcama.CevaplanmaTarihi = DateOnly.FromDateTime(DateTime.Today);
			_context.Update(harcama);
			await _context.SaveChangesAsync();
		}
	}
}

using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IHarcamaRepository
	{
		Task<List<Harcama>> TumHarcamalar();

		// Firma
		Task<List<Harcama>> TumHarcamalarFirmayaGore(string firmaAd);
		Task<List<Harcama>> TumHarcamalarFirmayaGoreOnayli(string firmaAd);
		Task<List<Harcama>> TumHarcamalarFirmayaGoreRed(string firmaAd);
		Task<List<Harcama>> TumHarcamalarFirmayaGoreBekleme(string firmaAd);

		// Kullanici

		Task<List<Harcama>> TumHarcamalarKullaniciyaGore(string id);
		Task<List<Harcama>> TumHarcamalarKullaniciyaGoreOnayli(string id);
		Task<List<Harcama>> TumHarcamalarKullaniciyaGoreRed(string id);
		Task<List<Harcama>> TumHarcamalarKullaniciyaGoreBekleme(string id);

		//İşlemler

		Task HarcamaEkle(Harcama harcama);
		Task HarcamaIptalEt(int id);
		Task HarcamaOnayla(int id);
		Task HarcamaReddet(int id);

		Task<Firma> FirmaAdaGoreGetir(string firmaAd);
		Task OnayDurumu(int id, bool durum);
	}
}

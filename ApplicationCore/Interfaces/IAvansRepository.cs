using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IAvansRepository
	{
		Task<List<Avans>> TumAvanslar();

		// Firma
		Task<List<Avans>> TumAvanslarFirmayaGore(string firmaAd);
		Task<List<Avans>> TumAvanslarFirmayaGoreOnayli(string firmaAd);
		Task<List<Avans>> TumAvanslarFirmayaGoreRed(string firmaAd);
		Task<List<Avans>> TumAvanslarFirmayaGoreBekleme(string firmaAd);

		// Kullanici

		Task<List<Avans>> TumAvanslarKullaniciyaGore(string id);
		Task<List<Avans>> TumAvanslarKullaniciyaGoreOnayli(string id);
		Task<List<Avans>> TumAvanslarKullaniciyaGoreRed(string id);
		Task<List<Avans>> TumAvanslarKullaniciyaGoreBekleme(string id);

		//İşlemler

		Task AvansEkle(Avans avans);
		Task AvansIptalEt(int id);
		Task AvansOnayla(int id);
		Task AvansReddet(int id);

		Task<Firma> FirmaAdaGoreGetir(string firmaAd);

		Task OnayDurumu(int id, bool durum);



	}
}

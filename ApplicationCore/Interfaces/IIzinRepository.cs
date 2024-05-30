using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IIzinRepository
	{
		public Task<List<Izin>> TumIzinler();

		// Firma
		public Task<List<Izin>> TumIzinlerFirmayaGore(string firmaAd); 
		public Task<List<Izin>> TumIzinlerFirmayaGoreOnayli(string firmaAd);
		public Task<List<Izin>> TumIzinlerFirmayaGoreRed(string firmaAd);
		public Task<List<Izin>> TumIzinlerFirmayaGoreBekleme(string firmaAd);

		// Kullanici
		public Task<List<Izin>> TumIzinlerKullaniciyaGore(string id); 
		public Task<List<Izin>> TumIzinlerKullaniciyaGoreOnayli(string id); 
		public Task<List<Izin>> TumIzinlerKullaniciyaGoreRed(string id); 
		public Task<List<Izin>> TumIzinlerKullaniciyaGoreBekleme(string id); 

		// İşlemler

		public Task IzinEkle(Izin izin);				
		public Task IzinIptalEt(int id);      // Kullanicinin kendi talebini iptal etmesi

		public Task IzinOnayla(int id);		//Firma sahibinin onayı
		public Task IzinReddet(int id);     //Firma sahibinin reddetmesi

		public Task<int> KullaniciToplamIzin(string id);

	}
}

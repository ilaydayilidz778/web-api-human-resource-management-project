using ApplicationCore.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
	public interface IYeniKullaniciModelService
	{
		Task YeniKullaniciEkleAsync(YeniKullaniciModel yeniKullaniciModel);

		Task KullaniciAdiAtaAsync(Kullanici kullanici,Firma firma);
	}
}

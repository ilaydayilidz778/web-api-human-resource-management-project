using ApplicationCore.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
	public interface IIzinModelService
	{
		Task<Izin> IzinOluştur(IzinModel izin);
		int GunSayisi(DateOnly baslangicTarihi,DateOnly bitisTarihi);
	}
}

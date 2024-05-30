using ApplicationCore.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
	public interface IKullaniciModelService
	{
		Task<List<KullaniciModel>> TumKullanicilar();

		Task<KullaniciModel> KullaniciGetir(string id);

        Task<KullaniciModel> KullaniciyiTokenlaGetir(string token);

		Task<int> KalanYillikIzin(string id);

    }
}

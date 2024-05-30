using ApplicationCore.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
	public interface IParolaService
	{
		Task<Kullanici> KullaniciGetirAsync(string email);

		//Task<bool> ParolaSifirlamaMailGonderAsync(Kullanici kullanici);
		Task ParolaSifirlamaMailGonderAsync(Kullanici kullanici);

		//Task<bool> ParolaSifirlaAsync(ParolaSifirlaModel parolaSifirlamaModel);
		Task ParolaSifirlaAsync(ParolaSifirlaModel parolaSifirlamaModel);
	}
}

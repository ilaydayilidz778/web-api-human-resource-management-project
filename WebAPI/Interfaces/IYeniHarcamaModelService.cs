using ApplicationCore.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
	public interface IYeniHarcamaModelService
	{
		Task<Harcama> HarcamaOlustur(YeniHarcamaModel model);
	}
}

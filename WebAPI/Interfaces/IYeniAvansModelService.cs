using ApplicationCore.Entities;
using WebAPI.Models;

namespace WebAPI.Interfaces
{
	public interface IYeniAvansModelService
	{
		Task<Avans> AvansOlustur(YeniAvansModel avans);
	}
}

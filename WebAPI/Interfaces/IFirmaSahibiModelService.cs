using WebAPI.Models;

namespace WebAPI.Interfaces
{
	public interface IFirmaSahibiModelService
	{
		Task<bool> ProfilGuncelleAsync(string id,FirmaSahibiModel model);
	}
}

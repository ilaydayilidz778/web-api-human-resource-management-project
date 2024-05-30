using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FirmaSahibiController : ControllerBase
	{
		private readonly IFirmaSahibiModelService _firmaSahibiModelService;

		public FirmaSahibiController(IFirmaSahibiModelService firmaSahibiModelService)
		{
			_firmaSahibiModelService = firmaSahibiModelService;
		}
		[HttpPut("id")]
		public async Task<ActionResult> FirmaSahibiGuncelle(string id, FirmaSahibiModel model)
		{
			if (await _firmaSahibiModelService.ProfilGuncelleAsync(id, model))
				return Ok();
			return BadRequest();
		}
	}
}

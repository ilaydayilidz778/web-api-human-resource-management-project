using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ParolaController : ControllerBase
	{
		private readonly IParolaService _parolaService;

		public ParolaController(IParolaService parolaService)
		{
			_parolaService = parolaService;
		}
		[HttpPost]
		public async Task<ActionResult> SifremiUnuttum(string username)
		{
			var kullanici = await _parolaService.KullaniciGetirAsync(username);

			if (kullanici == null)
			{
				return NotFound();
			}

			//if (await _parolaService.ParolaSifirlamaMailGonderAsync(kullanici))
			//	return Ok();
			//return BadRequest();
			try
			{
				await _parolaService.ParolaSifirlamaMailGonderAsync(kullanici);
				return Ok();
			}
			catch (Exception)
			{
				return BadRequest();
			}
		}
		[HttpPut]
		public async Task<ActionResult> ParolaDegistir(ParolaSifirlaModel parolaSifirlaModel)
		{
			//if(await _parolaService.ParolaSifirlaAsync(parolaSifirlaModel))
			//	return Ok();
			//return BadRequest();
			try
			{
				await _parolaService.ParolaSifirlaAsync(parolaSifirlaModel);
				return Ok();
			}
			catch (Exception)
			{

				return BadRequest();
			}


		}

	}
}

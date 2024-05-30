using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IzinTuruController : ControllerBase
	{
		private readonly IBaseRepository<IzinTuru> _repository;

		public IzinTuruController(IBaseRepository<IzinTuru> repository)
		{
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<IzinTuru>> IzinTurleriniGetir()
		{
			var izinTurleri = await _repository.HepsiniGetir();
			return izinTurleri;
		}
	}
}

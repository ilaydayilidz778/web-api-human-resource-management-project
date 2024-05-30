using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HarcamaTuruController : ControllerBase
	{
		private readonly IBaseRepository<HarcamaTuru> _repository;

		public HarcamaTuruController(IBaseRepository<HarcamaTuru> repository)
        {
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<HarcamaTuru>> HarcamaTurleriniGetir()
		{
			var harcamaTurleri = await _repository.HepsiniGetir();
			return harcamaTurleri;
		}
	}
}

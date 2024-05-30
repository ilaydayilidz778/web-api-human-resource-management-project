using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AvansTuruController : ControllerBase
	{
		private readonly IBaseRepository<AvansTuru> _repository;

		public AvansTuruController(IBaseRepository<AvansTuru> repository)
        {
			_repository = repository;
		}
		[HttpGet]
		public async Task<IEnumerable<AvansTuru>> AvansTurleriniGetir()
		{
			var avansTurleri = await _repository.HepsiniGetir();
			return avansTurleri;
		}
    }
}

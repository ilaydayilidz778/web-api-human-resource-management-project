using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DepartmanController : ControllerBase
	{
		private readonly IBaseRepository<Departman> _repository;

		public DepartmanController(IBaseRepository<Departman> repository)
        {
			_repository = repository;
		}

		[HttpGet]
		public async Task<IEnumerable<string>> DepartmanlariGetir()
		{
			var departmanlar = await _repository.HepsiniGetirString();
			return departmanlar;
		}

	}
}


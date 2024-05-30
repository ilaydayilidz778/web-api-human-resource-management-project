using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParaBirimiController : ControllerBase
    {
        private readonly IBaseRepository<ParaBirimi> _repository;

        public ParaBirimiController(IBaseRepository<ParaBirimi> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ParaBirimi>> ParaBirimleriniGetir()
        {
            var paraBirimleri = await _repository.HepsiniGetir();
            return paraBirimleri;
        }
    }
}

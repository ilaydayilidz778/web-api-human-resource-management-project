using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaasTipiController : ControllerBase
    {
        private readonly IBaseRepository<MaasTipi> _repository;

        public MaasTipiController(IBaseRepository<MaasTipi> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> MaasTipleriniGetir()
        {
            var maasTipleri = await _repository.HepsiniGetirString();
            return maasTipleri;
        }
    }
}

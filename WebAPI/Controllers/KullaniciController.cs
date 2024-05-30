using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciController : ControllerBase
    {
        private readonly IKullaniciModelService _kullaniciModelService;
        private readonly IYeniKullaniciModelService _yeniKullaniciModelService;
        private readonly ILoginModelService _loginModelService;

        public KullaniciController(IKullaniciModelService kullaniciModelService
            , IYeniKullaniciModelService yeniKullaniciModelService, ILoginModelService loginModelService
            )
        {
            _kullaniciModelService = kullaniciModelService;
            _yeniKullaniciModelService = yeniKullaniciModelService;
            _loginModelService = loginModelService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<KullaniciModel>>> GetUsers()
        {
            return await _kullaniciModelService.TumKullanicilar();
        }

        [HttpGet("id")]
        public async Task<ActionResult<KullaniciModel>> GetUserById(string id)
        {

            return await _kullaniciModelService.KullaniciGetir(id);
        }

        [HttpGet("token")]
        public async Task<ActionResult<KullaniciModel>> GetUserByToken(string token)
        {

            return await _kullaniciModelService.KullaniciyiTokenlaGetir(token);
        }

        [HttpPost]
        public async Task<ActionResult> YeniKullaniciEkle(YeniKullaniciModel yeniKullaniciModel)
        {
            try
            {
                await _yeniKullaniciModelService.YeniKullaniciEkleAsync(yeniKullaniciModel);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginOnay(LoginModel loginModel)
        {
            try
            {
                var kullaniciModel = await _loginModelService.LoginAsync(loginModel);
                return Ok(kullaniciModel);

            }
            catch (Exception)
            {
                return NotFound();  
            }

        }

    }
}

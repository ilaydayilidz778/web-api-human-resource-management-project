using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HarcamaController : ControllerBase
	{
		private readonly IHarcamaRepository _harcamaRepository;
		private readonly IYeniHarcamaModelService _yeniHarcamaModelService;

		public HarcamaController( IHarcamaRepository harcamaRepository,IYeniHarcamaModelService yeniHarcamaModelService)
        {
			_harcamaRepository = harcamaRepository;
			_yeniHarcamaModelService = yeniHarcamaModelService;
		}

		[HttpGet]
		public async Task<ActionResult<List<Harcama>>> TumHarcamalar()
		{
			var harcamalar = await _harcamaRepository.TumHarcamalar();
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}

		// Firma

		[HttpGet("Firma")]
		public async Task<ActionResult<List<Harcama>>> TumHarcamalarFirma(string firmaAd)
		{
			var harcamalar = await _harcamaRepository.TumHarcamalarFirmayaGore(firmaAd);
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}

		[HttpGet("FirmaOnayli")]
		public async Task<ActionResult<List<Harcama>>> TumAvanslarFirmaOnayli(string firmaAd)
		{
			var harcamalar = await _harcamaRepository.TumHarcamalarFirmayaGoreOnayli(firmaAd);
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}

		[HttpGet("FirmaRed")]
		public async Task<ActionResult<List<Harcama>>> TumAvanslarFirmaRed(string firmaAd)
		{
			var harcamalar = await _harcamaRepository.TumHarcamalarFirmayaGoreRed(firmaAd);
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}

		[HttpGet("FirmaBekleme")]
		public async Task<ActionResult<List<Harcama>>> TumAvanslarFirmaBekleme(string firmaAd)
		{
			var harcamalar = await _harcamaRepository.TumHarcamalarFirmayaGoreBekleme(firmaAd);
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}

		//Kullanici

		[HttpGet("Kullanici")]
		public async Task<ActionResult<List<Harcama>>> TumHarcamalarKullanici(string id)
		{
			var harcamalar = await _harcamaRepository.TumHarcamalarKullaniciyaGore(id);
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}
		[HttpGet("KullaniciOnayli")]
		public async Task<ActionResult<List<Harcama>>> TumHarcamalarKullaniciOnayli(string id)
		{
			var harcamalar = await _harcamaRepository.TumHarcamalarKullaniciyaGoreOnayli(id);
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}
		[HttpGet("KullaniciRed")]
		public async Task<ActionResult<List<Harcama>>> TumHarcamalarKullaniciRed(string id)
		{
			var harcamalar = await _harcamaRepository.TumHarcamalarKullaniciyaGoreRed(id);
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}
		[HttpGet("KullaniciBekleme")]
		public async Task<ActionResult<List<Harcama>>> TumHarcamalarKullaniciBekleme(string id)
		{
			var harcamalar = await _harcamaRepository.TumHarcamalarKullaniciyaGoreBekleme(id);
			if (harcamalar == null)
			{
				return NotFound();
			}
			return Ok(harcamalar);
		}

		// İşlemler -----------------------------------------------------

		// Kullanici işlemleri

		[HttpPost("Ekle")]
		public async Task<ActionResult> HarcamaEkle(YeniHarcamaModel model)
		{
			var harcama = await _yeniHarcamaModelService.HarcamaOlustur(model);

			try
			{
				await _harcamaRepository.HarcamaEkle(harcama);
				return Ok(harcama);

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		[HttpPut("Iptal")]
		public async Task<ActionResult> HarcamaIptal(int id)
		{

			try
			{
				await _harcamaRepository.HarcamaIptalEt(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		// Yönetici işlemleri

		[HttpPut("Onay")]
		public async Task<ActionResult> HarcamaOnay(int id)
		{

			try
			{
				await _harcamaRepository.HarcamaOnayla(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		[HttpPut("Red")]
		public async Task<ActionResult> HarcamaRed(int id)
		{

			try
			{
				await _harcamaRepository.HarcamaReddet(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}





	}
}

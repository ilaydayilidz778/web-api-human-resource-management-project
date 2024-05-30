using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IzinController : ControllerBase
	{
		private readonly IIzinRepository _izinRepository;
		private readonly IIzinModelService _izinModelService;

		public IzinController(IIzinRepository izinRepository, IIzinModelService izinModelService)
		{
			_izinRepository = izinRepository;
			_izinModelService = izinModelService;
		}
		[HttpGet]
		public async Task<ActionResult<List<Izin>>> TumIzinler()
		{
			var izinler = await _izinRepository.TumIzinler();
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}

		// Firma

		[HttpGet("Firma")]
		public async Task<ActionResult<List<Izin>>> TumIzinlerFirma(string firmaAd)
		{
			var izinler = await _izinRepository.TumIzinlerFirmayaGore(firmaAd);
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}

		[HttpGet("FirmaOnayli")]
		public async Task<ActionResult<List<Izin>>> TumIzinlerFirmaOnayli(string firmaAd)
		{
			var izinler = await _izinRepository.TumIzinlerFirmayaGoreOnayli(firmaAd);
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}

		[HttpGet("FirmaRed")]
		public async Task<ActionResult<List<Izin>>> TumIzinlerFirmaRed(string firmaAd)
		{
			var izinler = await _izinRepository.TumIzinlerFirmayaGoreRed(firmaAd);
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}

		[HttpGet("FirmaBekleme")]
		public async Task<ActionResult<List<Izin>>> TumIzinlerFirmaBekleme(string firmaAd)
		{
			var izinler = await _izinRepository.TumIzinlerFirmayaGoreBekleme(firmaAd);
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}

		// Kullanıcı

		[HttpGet("Kullanici")]
		public async Task<ActionResult<List<Izin>>> TumIzinlerKullanici(string id)
		{
			var izinler = await _izinRepository.TumIzinlerKullaniciyaGore(id);
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}
		[HttpGet("KullaniciOnayli")]
		public async Task<ActionResult<List<Izin>>> TumIzinlerKullaniciOnayli(string id)
		{
			var izinler = await _izinRepository.TumIzinlerKullaniciyaGoreOnayli(id);
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}
		[HttpGet("KullaniciRed")]
		public async Task<ActionResult<List<Izin>>> TumIzinlerKullaniciRed(string id)
		{
			var izinler = await _izinRepository.TumIzinlerKullaniciyaGoreRed(id);
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}
		[HttpGet("KullaniciBekleme")]
		public async Task<ActionResult<List<Izin>>> TumIzinlerKullaniciBekleme(string id)
		{
			var izinler = await _izinRepository.TumIzinlerKullaniciyaGoreBekleme(id);
			if (izinler == null)
			{
				return NotFound();
			}
			return Ok(izinler);
		}

		//  işlemler ------------------------------------------------------------------

		//  Kullanici işlemleri

		[HttpPost("Ekle")]
		public async Task<ActionResult> IzinEkle(IzinModel model)
		{
			var izin = await _izinModelService.IzinOluştur(model);

			try
			{
				await _izinRepository.IzinEkle(izin);
				return Ok(izin);

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		[HttpPut("Iptal")]
		public async Task<ActionResult> IzinIptal(int id)
		{

			try
			{
				await _izinRepository.IzinIptalEt(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		// Yönetici işlemleri

		[HttpPut("Onay")]
		public async Task<ActionResult> IzinOnay(int id)
		{

			try
			{
				await _izinRepository.IzinOnayla(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}
		[HttpPut("Red")]
		public async Task<ActionResult> IzinRed(int id)
		{

			try
			{
				await _izinRepository.IzinReddet(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}




	}
}

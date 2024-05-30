using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AvansController : ControllerBase
	{
		private readonly IAvansRepository _avansRepository;
		private readonly IYeniAvansModelService _yeniAvansModelService;
		private readonly IAvansKisitlariModelService _avansKisitlariModelService;

		public AvansController(IAvansRepository avansRepository,IYeniAvansModelService yeniAvansModelService,IAvansKisitlariModelService avansKisitlariModelService)
        {
			_avansRepository = avansRepository;
			_yeniAvansModelService = yeniAvansModelService;
			_avansKisitlariModelService = avansKisitlariModelService;
		}

		[HttpGet]

		public async Task<ActionResult<List<Avans>>> TumAvanslar()
		{
			var avanslar = await _avansRepository.TumAvanslar();
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}

		// Firma

		[HttpGet("Firma")]
		public async Task<ActionResult<List<Avans>>> TumAvanslarFirma(string firmaAd)
		{
			var avanslar = await _avansRepository.TumAvanslarFirmayaGore(firmaAd);
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}		
		
		[HttpGet("FirmaOnayli")]
		public async Task<ActionResult<List<Avans>>> TumAvanslarFirmaOnayli(string firmaAd)
		{
			var avanslar = await _avansRepository.TumAvanslarFirmayaGoreOnayli(firmaAd);
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}		
		
		[HttpGet("FirmaRed")]
		public async Task<ActionResult<List<Avans>>> TumAvanslarFirmaRed(string firmaAd)
		{
			var avanslar = await _avansRepository.TumAvanslarFirmayaGoreRed(firmaAd);
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}

		[HttpGet("FirmaBekleme")]
		public async Task<ActionResult<List<Avans>>> TumAvanslarFirmaBekleme(string firmaAd)
		{
			var avanslar = await _avansRepository.TumAvanslarFirmayaGoreBekleme(firmaAd);
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}

		// Kullanici 

		[HttpGet("Kullanici")]
		public async Task<ActionResult<List<Avans>>> TumAvanslarKullanici(string id)
		{
			var avanslar = await _avansRepository.TumAvanslarKullaniciyaGore(id);
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}

		[HttpGet("KullaniciOnayli")]
		public async Task<ActionResult<List<Avans>>> TumAvanslarKullaniciOnayli(string id)
		{
			var avanslar = await _avansRepository.TumAvanslarKullaniciyaGoreOnayli(id);
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}

		[HttpGet("KullaniciRed")]
		public async Task<ActionResult<List<Avans>>> TumAvanslarKullaniciRed(string id)
		{
			var avanslar = await _avansRepository.TumAvanslarKullaniciyaGoreRed(id);
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}

		[HttpGet("KullaniciBekleme")]
		public async Task<ActionResult<List<Avans>>> TumAvanslarKullaniciBekleme(string id)
		{
			var avanslar = await _avansRepository.TumAvanslarKullaniciyaGoreBekleme(id);
			if (avanslar == null)
			{
				return NotFound();
			}
			return Ok(avanslar);
		}

		// İşlemler --------------------------------------------------

		// Kullanici işlemleri

		[HttpPost("Ekle")]
		public async Task<ActionResult> AvansEkle(YeniAvansModel model)
		{
			var avans = await _yeniAvansModelService.AvansOlustur(model);

			try
			{
				await _avansRepository.AvansEkle(avans);
				return Ok(avans);

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		[HttpPut("Iptal")]
		public async Task<ActionResult> AvansIptal(int id)
		{

			try
			{
				await _avansRepository.AvansIptalEt(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		// Yönetici işlemleri

		[HttpPut("Onay")]
		public async Task<ActionResult> AvansOnay(int id)
		{

			try
			{
				await _avansRepository.AvansOnayla(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		[HttpPut("Red")]
		public async Task<ActionResult> AvansRed(int id)
		{

			try
			{
				await _avansRepository.AvansReddet(id);
				return Ok();

			}
			catch (Exception)
			{
				return BadRequest();
			}

		}

		// Avans Kısıtları

		[HttpGet("AvansKisitlari")]
		public async Task<ActionResult<AvansKisitlariModel>> AvansKisitlari(string id)
		{
			var maksimumIsAvansi = await _avansKisitlariModelService.MaksimumIsAvansı(id);
			var aylikAvans = await _avansKisitlariModelService.AylıkAvans(id);
			var yillikAvansSayisi = await _avansKisitlariModelService.YillikAvansSayisi(id);

			AvansKisitlariModel model=new AvansKisitlariModel() 
			{
				MaximumWorkAdvance = maksimumIsAvansi,
				InMonth=aylikAvans,
				CountInYear=yillikAvansSayisi,
			};
			return Ok(model);
		}

	}
}

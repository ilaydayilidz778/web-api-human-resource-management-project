using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
	public class YeniAvansModelService : IYeniAvansModelService
	{
		private readonly IBaseRepository<AvansTuru> _avansTuruRepository;
		private readonly IBaseRepository<ParaBirimi> _paraBirimiRepository;

		public YeniAvansModelService(IBaseRepository<AvansTuru> avansTuruRepository,IBaseRepository<ParaBirimi> paraBirimiRepository)
        {
			_avansTuruRepository = avansTuruRepository;
			_paraBirimiRepository = paraBirimiRepository;
		}
        public async Task<Avans> AvansOlustur(YeniAvansModel model)
		{
			var avansTuru = await _avansTuruRepository.AdaGoreGetir(model.AdvanceType);
			var paraBirimi = await _paraBirimiRepository.AdaGoreGetir(model.AdvanceCurrency);

			if (avansTuru == null || paraBirimi==null)
			{
				throw new Exception();
			}

			Avans avans = new Avans()
			{

				KullaniciId = model.UserId,
				Tutar = model.AdvanceAmount,
				Aciklama = model.AdvanceDescription,

				ParaBirimi = paraBirimi,
				AvansTuru = avansTuru,

				AktiflikDurumu = true,
				OnayDurumu = null

			};
			avans.TalepTarihi = DateOnly.FromDateTime(DateTime.Today);

			return avans;

		}
	}
}

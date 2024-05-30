using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
	public class YeniHarcamaModelService : IYeniHarcamaModelService
	{
		private readonly IBaseRepository<HarcamaTuru> _harcamaTuruRepository;
		private readonly IBaseRepository<ParaBirimi> _paraBirimiRepository;

		public YeniHarcamaModelService( IBaseRepository<HarcamaTuru> harcamaTuruRepository,IBaseRepository<ParaBirimi> paraBirimiRepository)
        {
			_harcamaTuruRepository = harcamaTuruRepository;
			_paraBirimiRepository = paraBirimiRepository;
		}
        public async Task<Harcama> HarcamaOlustur(YeniHarcamaModel model)
		{
			var harcamaTuru = await _harcamaTuruRepository.AdaGoreGetir(model.ExpenseType);
			var parabirimi = await _paraBirimiRepository.AdaGoreGetir(model.ExpenseCurrency);

			if(harcamaTuru==null || parabirimi == null)
			{
				throw new Exception();
			}

			Harcama harcama = new Harcama()
			{

				KullaniciId = model.UserId,
				Tutar = model.ExpenseAmount,
				Dosya = model.ExpenseFiles,

				ParaBirimi=parabirimi,
				HarcamaTuru=harcamaTuru,

				AktiflikDurumu=true,
				OnayDurumu=null

			};

			harcama.TalepTarihi = DateOnly.FromDateTime(DateTime.Today);
			return harcama;

		}
	}
}

using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
	public class IzinModelService : IIzinModelService
	{
		private readonly IBaseRepository<IzinTuru> _izinTuruRepository;

		public IzinModelService(IBaseRepository<IzinTuru> izinTuruRepository)
        {
			_izinTuruRepository = izinTuruRepository;
		}


		public async Task<Izin> IzinOluştur(IzinModel model)
		{
			var izinTuru = await _izinTuruRepository.AdaGoreGetir(model.PermissionType);

			if (izinTuru == null) 
			{
				throw new Exception();
			}



			Izin izin = new Izin() {

				Id = model.Id,
				KullaniciId = model.UserId,
				IzinBaslangicTarihi = model.PermissionStartDate,
				IzinBitisTarihi = model.PermissionEndDate,
				IzinTuru = izinTuru,
				GunSayisi = GunSayisi(model.PermissionStartDate,model.PermissionEndDate),
				Belge=model.DocumentName,

			};

			izin.OnayDurumu=null;
			izin.AktiflikDurumu=true;
			izin.TalepTarihi = DateOnly.FromDateTime(DateTime.Today);

			return await Task.FromResult(izin);
		}

		public int GunSayisi(DateOnly baslangicTarihi, DateOnly bitisTarihi)
		{
			int isGunSayisi = 0;

			for (DateOnly date = baslangicTarihi; date <= bitisTarihi; date = date.AddDays(1))
			{
				if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
				{
					isGunSayisi++;
				}
			}

			return isGunSayisi;
		}


	}
}

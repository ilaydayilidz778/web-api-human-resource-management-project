using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class AvansKisitlariModelService : IAvansKisitlariModelService
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly IBaseRepository<Firma> _firmaRepository;
        private readonly IAvansRepository _avansRepository;

        public AvansKisitlariModelService(UserManager<Kullanici> userManager, IBaseRepository<Firma> firmaRepository, IAvansRepository avansRepository)
        {
            _userManager = userManager;
            _firmaRepository = firmaRepository;
            _avansRepository = avansRepository;
        }
        public async Task<bool> AylıkAvans(string id)
        {

            var avanslar = await _avansRepository.TumAvanslarKullaniciyaGore(id);

            if (avanslar.Any(x => x.AktiflikDurumu && !(x.OnayDurumu == false) && x.AvansTuru.Ad == "Ücret" && x.TalepTarihi.Month == DateTime.Today.Month && x.TalepTarihi.Year == DateTime.Today.Year))
            {
                return true;
            }
            return false;
        }


        public async Task<int> YillikAvansSayisi(string id)
        {
            var avanslar = await _avansRepository.TumAvanslarKullaniciyaGoreOnayli(id);

            return avanslar.Count(x => x.TalepTarihi.Year == DateTime.Today.Year);

        }
        public async Task<decimal> MaksimumIsAvansı(string id)
        {
            var kullanici = await _userManager.FindByIdAsync(id);

            if (kullanici == null)
            {
                throw new Exception();
            }

            var firma = await _firmaRepository.IdyeGoreGetir(kullanici.FirmaId);

            if (firma == null)
            {
                throw new Exception();
            }

            return firma.MaximumIsAvansi;
        }
    }
}

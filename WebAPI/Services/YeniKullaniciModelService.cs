using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class YeniKullaniciModelService : IYeniKullaniciModelService
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly IParolaService _parolaService;
        private readonly IBaseRepository<Departman> _departmanRepository;
        private readonly IBaseRepository<Firma> _firmaRepository;
        private readonly IBaseRepository<MaasTipi> _maasTipiRepository;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public YeniKullaniciModelService(UserManager<Kullanici> userManager, IParolaService parolaService,
            IBaseRepository<Departman> departmanRepository, IBaseRepository<Firma> firmaRepository,
            IBaseRepository<MaasTipi> maasTipiRepository, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _parolaService = parolaService;
            _departmanRepository = departmanRepository;
            _firmaRepository = firmaRepository;
            _maasTipiRepository = maasTipiRepository;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task YeniKullaniciEkleAsync(YeniKullaniciModel yeniKullaniciModel)
        {
            var departman = await _departmanRepository.AdaGoreGetir(yeniKullaniciModel.Department!);
            var firma = await _firmaRepository.AdaGoreGetir(yeniKullaniciModel.CompanyName);
            var maasTipi = await _maasTipiRepository.AdaGoreGetir(yeniKullaniciModel.SalaryType!);


            Kullanici kullanici = new Kullanici()
            {
                PhotoByte =yeniKullaniciModel.PhotoByte,
                Ad = yeniKullaniciModel.FirstName,
                IkinciAd = yeniKullaniciModel.SecondName,
                Soyad = yeniKullaniciModel.LastName,
                IkinciSoyad = yeniKullaniciModel.SecondLastName,
                Cinsiyet=yeniKullaniciModel.Gender,

                Meslek = yeniKullaniciModel.Job,

                Email = yeniKullaniciModel.Email,
                UserName = yeniKullaniciModel.Email,
                DogumTarihi = yeniKullaniciModel.BirthDate,
                DogumYeri = yeniKullaniciModel.BirthPlace,

                TCKimlikNumarasi = yeniKullaniciModel.IdentityNumber,
                IseGirisTarihi = yeniKullaniciModel.DateOfRecruitment,
                IstenCikistarihi = yeniKullaniciModel.DateOfDismissal,
                Adres = yeniKullaniciModel.Address,
                Maas = yeniKullaniciModel.Salary,

                TelefonNumarasi = yeniKullaniciModel.Contact,
                AktiflikDurumu = true,
                EmailConfirmed = true,

                Departman = departman,
                Firma = firma!,
                MaasTipi = maasTipi,

            };

            await KullaniciAdiAtaAsync(kullanici, firma!);
            await _userManager.CreateAsync(kullanici);

            await _userManager.AddToRoleAsync(kullanici, AuthorizationConstants.Roller.FIRMA_CALISAN);
            
            await _parolaService.ParolaSifirlamaMailGonderAsync(kullanici);
        }

        public async Task KullaniciAdiAtaAsync(Kullanici kullanici, Firma firma)
        {
            string adSoyad = (kullanici.Ad + kullanici.Soyad).ToLower()
                .Replace('ç', 'c')
                .Replace('ğ', 'g')
                .Replace('ı', 'i')
                .Replace('ö', 'o')
                .Replace('ş', 's')
                .Replace('ü', 'u');

            var kullanicilar = await _userManager.Users.Where(x => x.UserName.StartsWith(adSoyad)).ToListAsync();

            for (int i = 1; i <= kullanicilar.Count + 1; i++)
            {
                if (kullanicilar.Any(x => x.UserName == $"{adSoyad + (i != 1 ? i : "")}@{firma.EmailEklentisi}"))
                    continue;
                adSoyad += (i != 1 ? i : "");
                break;
            };
            kullanici.UserName = $"{adSoyad}@{firma.EmailEklentisi}";
        }



    }
}

using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class LoginModelService : ILoginModelService
    {
        private readonly UserManager<Kullanici> _userManager;
        private readonly IKullaniciModelService _kullaniciModelService;
        private readonly SignInManager<Kullanici> _signInManager;

        public LoginModelService(UserManager<Kullanici> userManager, IKullaniciModelService kullaniciModelService, SignInManager<Kullanici> signInManager)
        {
            _userManager = userManager;
            _kullaniciModelService = kullaniciModelService;
            _signInManager = signInManager;
        }

        public async Task<KullaniciModel> LoginAsync(LoginModel loginModel)
        {
            // Kullanıcıyı doğrula
            var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, lockoutOnFailure: false);

            // Doğrulama sonuçlarını kontrol et
            if (result.Succeeded)
            {
                var kullanici = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginModel.Username);
                if (kullanici == null)
                {
                    throw new Exception();
                }

                kullanici.LoginToken = Guid.NewGuid().ToString(); ;
                await _userManager.UpdateAsync(kullanici); 
                var kullaniciModel = await _kullaniciModelService.KullaniciGetir(kullanici.Id);
                kullaniciModel.LoginToken = kullanici.LoginToken;
                return kullaniciModel;
            }
            throw new Exception();
        }
    }
}
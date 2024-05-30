using WebAPI.Models;

namespace WebAPI.Interfaces
{
    public interface ILoginModelService
    {
        Task<KullaniciModel> LoginAsync(LoginModel loginModel);
    }
}

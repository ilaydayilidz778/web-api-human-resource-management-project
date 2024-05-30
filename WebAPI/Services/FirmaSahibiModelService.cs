using ApplicationCore.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.IO;
using System.Threading.Tasks;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
	public class FirmaSahibiModelService : IFirmaSahibiModelService
	{
		private readonly UserManager<Kullanici> _userManager;
		private readonly IWebHostEnvironment _hostEnvironment;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public FirmaSahibiModelService(UserManager<Kullanici> userManager, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
		{
			_userManager = userManager;
			_hostEnvironment = hostEnvironment;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<bool> ProfilGuncelleAsync(string id, FirmaSahibiModel model)
		{
			if (id != model.Id)
			{
				return false;
			}

			var kullanici = await _userManager.FindByIdAsync(model.Id);
			if (kullanici == null)
			{
				return false;
			}

			if(model.PhotoByte!="")
				kullanici.PhotoByte =model.PhotoByte;

			kullanici.TelefonNumarasi = model.Contact;
			kullanici.Adres = model.Address;

			try
			{
				await _userManager.UpdateAsync(kullanici);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}
	}
}

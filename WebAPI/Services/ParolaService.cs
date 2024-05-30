using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using WebAPI.Interfaces;
using WebAPI.Models;
using NuGet.Common;

namespace WebAPI.Services
{
	public class ParolaService : IParolaService
	{
		private readonly UserManager<Kullanici> _userManager;

		public ParolaService(UserManager<Kullanici> userManager)
		{
			_userManager = userManager;
		}
		public async Task<Kullanici> KullaniciGetirAsync(string username)
		{
			return (await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username))!;
		}

		public async Task<Kullanici> TokenKullaniciGetirAsync(string token)
		{
			return (await _userManager.Users.FirstOrDefaultAsync(x => x.ResetToken == token))!;
		}


		//public async Task<bool> ParolaSifirlamaMailGonderAsync(Kullanici kullanici)
		public async Task ParolaSifirlamaMailGonderAsync(Kullanici kullanici)
		{
			if (!await _userManager.Users.AnyAsync(x => x.Email == kullanici.Email))
				throw new Exception();

			string resetToken = Guid.NewGuid().ToString();
			kullanici.ResetToken = resetToken;
			await _userManager.UpdateAsync(kullanici);

			string url = "https://hrmhub.azurewebsites.net/passwordsetting";
			MailMessage message = new MailMessage();
			SmtpClient smtpClient = new SmtpClient();

			string sender = "mehmetyigit.mail@gmail.com";
			string reciever = kullanici.Email!;

			message.From = new MailAddress(sender);
			message.To.Add(new MailAddress(reciever));
			message.Subject = "Parola Belirleme";
			message.Body = $"Kullanıcı adınız {kullanici.UserName} olarak belirlenmiştir. Lütfen parolanızı belirlemek için bağlantıya {url}?token={resetToken} tıklayınız.";


			smtpClient.Port = 587;
			smtpClient.Host = "smtp.gmail.com";
			smtpClient.EnableSsl = true;
			smtpClient.Credentials = new NetworkCredential(sender, "ceyvhdnanhgicdlf");
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			smtpClient.Send(message);
		}

		public async Task ParolaSifirlaAsync(ParolaSifirlaModel parolaSifirlaModel)
		{
			Kullanici kullanici = await TokenKullaniciGetirAsync(parolaSifirlaModel.Token);

			if (kullanici == null)
			{
				throw new Exception();
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(kullanici);
			await _userManager.ResetPasswordAsync(kullanici, token, parolaSifirlaModel.NewPassword);
			kullanici.ResetToken = "";

		}
	}
}


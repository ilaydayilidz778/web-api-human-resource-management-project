using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
	public interface IBaseRepository<T> where T : BaseEntity
	{
		public Task<List<T>> HepsiniGetir();
		public Task<T?> IdyeGoreGetir(int id);
		public Task<T?> AdaGoreGetir(string ad);
		public Task<List<string>> HepsiniGetirString();
	}
}

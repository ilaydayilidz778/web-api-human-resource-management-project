using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
	public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
	{
		private readonly AppDbContext _context;

		public BaseRepository(AppDbContext context)
        {
			_context = context;
		}
        public async Task<T?> AdaGoreGetir(string ad)
		{
			return await _context.Set<T>().FirstOrDefaultAsync(x=>x.Ad==ad);

		}

		public async Task<List<T>> HepsiniGetir()
		{
			return await _context.Set<T>().ToListAsync();
		}
		public async Task<List<string>> HepsiniGetirString()
		{
			return await _context.Set<T>().Select(x => x.Ad).ToListAsync();
		}

		public async Task<T?> IdyeGoreGetir(int id)
		{
			return await _context.Set<T>().FindAsync(id);
		}
	}
}

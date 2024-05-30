using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
	public class AppDbContext:IdentityDbContext<Kullanici>
	{
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<Firma> Firmalar { get; set; }
        public DbSet<MaasTipi> MaasTipleri { get; set; }
        public DbSet<Izin> Izinler { get; set; }
        public DbSet<IzinTuru> IzinTurleri { get; set; }
        public DbSet<ParaBirimi> ParaBirimleri { get; set; }
        public DbSet<AvansTuru> AvansTurleri { get; set; }
        public DbSet<HarcamaTuru> HarcamaTurleri { get; set; }
        public DbSet<Avans> Avanslar { get; set; }
        public DbSet<Harcama> Harcamalar { get; set; }
    }
}

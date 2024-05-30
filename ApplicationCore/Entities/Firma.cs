using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
	public class Firma:BaseEntity
	{

		public bool AktiflikDurumu { get; set; }
		public string EmailEklentisi { get; set; } = null!;

		[Precision(18,2)]
        public decimal MaximumIsAvansi { get; set; }
    }
}

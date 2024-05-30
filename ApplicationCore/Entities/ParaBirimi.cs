using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
	public class ParaBirimi:BaseEntity
	{
		public string Simge { get; set; } = null!;

		public string Kod { get; set; }=null!;

    }
}

// mevcut kodların olduğu api

//  https://api.genelpara.com/embed/para-birimleri.json 

// alternatif

//  https://hasanadiguzel.com.tr/api/kurgetir

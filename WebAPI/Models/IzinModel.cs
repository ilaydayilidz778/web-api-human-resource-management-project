namespace WebAPI.Models
{
	public class IzinModel
	{ //id - kullanıcıid - başlangıç tarihi - bitiş tarihi - izin türü -belge adı
		public int Id { get; set; }
		public string UserId { get; set; } = null!;
		public DateOnly PermissionStartDate { get; set; }
		public DateOnly PermissionEndDate { get; set; }
		public string PermissionType { get; set; } = null!;
		public string? DocumentName { get; set; }



	}
}

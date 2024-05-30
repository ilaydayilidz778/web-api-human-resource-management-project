namespace WebAPI.Models
{
	public class YeniAvansModel
	{
		public string UserId { get; set; } = null!;

		public string AdvanceType { get; set; } = null!;
        public string AdvanceCurrency { get; set; } = null!;
        public string AdvanceDescription { get; set; } = null!;
        public decimal AdvanceAmount { get; set; } 
    }
}


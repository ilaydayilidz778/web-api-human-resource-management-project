namespace WebAPI.Models
{
	public class YeniHarcamaModel
	{
		public string UserId { get; set; } = null!;
		public string ExpenseType { get; set; } = null!;
        public string  ExpenseCurrency { get; set; } = null!;
        public string? ExpenseDescription { get; set; }
        public decimal ExpenseAmount { get; set; }
        public string? ExpenseFiles { get; set; }
    }
}








// iş avansı max ayrı api yaz
namespace WebAPI.Interfaces
{
	public interface IAvansKisitlariModelService
	{
		Task<decimal> MaksimumIsAvansı(string id);

		Task<bool> AylıkAvans(string id); 

		Task<int> YillikAvansSayisi(string id);
	}
}

namespace DanceStudio.Models
{
	public class PaymentAccount
	{
		public int IdSubscription { get; set; }
		public int IdClient { get; set; }
		public string? Month { get; set; }
		public bool isPayment { get; set; }
	}
}

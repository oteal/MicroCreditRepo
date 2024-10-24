namespace CreditWebApi.Models {
	public class User {
		public int Id { get; set; }
		public Guid CMDToken { get; set; }
		public string Phonenumber { get; set; }
		public string Email { get; set; }
		public decimal Salary { get; set; }
		public decimal Debts { get; set; }
		public int HistoryCreditsCount { get; set; }

	}
}

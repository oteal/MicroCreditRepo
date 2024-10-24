using CreditWebApi.Models;

namespace CreditWebApi.Persitence {
	public static class Storage {
		public static Dictionary<int, User> Users = new Dictionary<int, User> {
			{ 1, new User {
					Id = 1,
					Phonenumber = "123456789",
					Email = "test1@gmail.com",
					Salary = 1500,
					Debts = 10000,
					HistoryCreditsCount = 1,
				}
			},
			{ 2, new User {
					Id = 2,
					Phonenumber = "987654321",
					Email = "test2@gmail.com",
					Salary = 3000,
					Debts = 20000,
					HistoryCreditsCount = 5,
				}
			},
		};
	}
}

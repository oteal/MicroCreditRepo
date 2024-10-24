using CreditWebApi.Models;
using CreditWebApi.Persitence;

namespace CreditWebApi.Services {
	public class UserService {
		public static User GetUser(Guid token) {
			return Storage.Users.FirstOrDefault(m => m.Value.CMDToken == token).Value;
		}
	}
}

namespace CreditWebApi.Models {

	public interface IChaveMovelDigital {
		Guid? Login(string phoneNumber, string password);
		bool Logout(Guid token);
		bool IsValidToken(Guid token);
	}
}
using CreditWebApi.Models;

namespace CreditWebApi.Services {
	public class CMDService : IChaveMovelDigital {


    private static Dictionary<string, Guid> UserAuthTokens = new Dictionary<string, Guid> {
      { "123456789", new Guid("31BC8DB6-AB43-4B17-ACDF-40A1D21BE9E9") },
      { "987654321", new Guid("2C546F52-3133-43B2-91A0-83EBFA02A7C1") },
    };

    public Guid? Login(string phoneNumber, string password) {

      if(UserAuthTokens.ContainsKey(phoneNumber) && IsValidPassword(phoneNumber, password)) {
        return UserAuthTokens[phoneNumber];
      }
      return null;
    }
    public bool Logout(Guid token) {
      return true;
    }
    public bool IsValidToken(Guid token) {
      return UserAuthTokens.Any(m => m.Value == token);
    }


		private bool IsValidPassword(string phoneNumber, string password) {
      return true;
		}
	}

  public static class CMDStorage {
    private static Dictionary<string, int> UserAuthTokens = new Dictionary<string, int>();
    public static string GenerateToken(int userId) {
      var token = Guid.NewGuid().ToString();
      UserAuthTokens[token] = userId;
      return token;
    }
  }
}

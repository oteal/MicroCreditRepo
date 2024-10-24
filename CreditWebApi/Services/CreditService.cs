using CreditWebApi.Models;

namespace CreditWebApi.Services {
	public class CreditService {
		private const decimal InflationRate = 2.4m;
		private const decimal UnemployeeRate = 6.1m;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <param name="amount">Amount to subscribe</param>
    /// <returns></returns>
		public RiskLevel CalculateRisk(User user, decimal amount) {
      decimal finalScore = 0;

      //unemployee score
      if(UnemployeeRate > 5) {
        finalScore += 5;
      }

      //Inflation score
      if(InflationRate > 5) {
        finalScore += 5;
      }
      else if(InflationRate > 2) {
        finalScore += 2;
			}
			else {
        finalScore -= 1;
			}

      //total credits client had. if high number is a good historical client
      if(user.HistoryCreditsCount >= 5) {
        finalScore -= 5;
      }
      else {
        finalScore += 5;
      }

      //Debts amount client has
      if(user.Debts >= 20000) {
        finalScore += 5;
      }
      else if(user.Debts > 0) {
        finalScore += 2;
      }

      if(finalScore <= 15) {
        return RiskLevel.LOW;
      }
      else if(finalScore <= 25) {
        return RiskLevel.MEDIUM;
      }
      else {
        return RiskLevel.HIGH;
      }
    }

    internal bool IsValidAmount(User user, decimal amount, out decimal maxAmountAllowed) {
      maxAmountAllowed = GetMaxAmountBasedOnSalary(user.Salary);

      return amount <= maxAmountAllowed;
    }

    private decimal GetMaxAmountBasedOnSalary(decimal salary) {
      /*
      -- Stored Procedure:
      CREATE PROCEDURE spGetMaxAmount(DECIMAL(10,2) @salary, @maxAmount DECIMAL(10, 2) OUTPUT) 
      AS
      BEGIN
       IF @salary <= 1000
          BEGIN
              SET @maxAmount = 1000;
          END
          ELSE IF @salary > 1000 AND @salary <= 2000
          BEGIN
              SET @maxAmount = 2000;
          END
          ELSE
          BEGIN
              SET @maxAmount = 5000;
          END
      END

      */
      if(salary <= 1000) {
        return 1000;
			}else if(salary <= 2000) {
        return 2000;
			}else {
        return 5000;
			}
		}

		internal bool Subscribe(User user, decimal amount) {
      //TODO: subscription code
      return true;
		}
	}
}

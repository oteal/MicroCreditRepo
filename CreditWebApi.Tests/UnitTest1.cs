using CreditWebApi.Controllers;
using CreditWebApi.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;

namespace CreditWebApi.Tests {
	[TestClass]
	public class UnitTest1 {
		private readonly Mock<IChaveMovelDigital> _mockCMDService;
		private readonly AuthController _authController;
		private readonly MicroCreditController _microCreditController;

		public UnitTest1() {
			_mockCMDService = new Mock<IChaveMovelDigital>();
			_authController = new AuthController(_mockCMDService.Object);
			_microCreditController = new MicroCreditController(_mockCMDService.Object);
		}

		[TestMethod]
		public void TestLogin1() {
			Guid expectedToken = Guid.NewGuid();
			_mockCMDService.Setup(s => s.Login("123456789", "1234")).Returns(expectedToken);

			// Act
			var result = _authController.Login(new CMDRequest { PhoneNumber = "123456789", Password = "1234" }) as OkObjectResult;

			// Assert
			result.Should().NotBeNull();
			result.Value.Should().BeEquivalentTo(new { Token = expectedToken });
		}

		[TestMethod]
		public void TestLogin2() {
			Guid? expectedToken = null;
			_mockCMDService.Setup(s => s.Login("123456789", "12345")).Returns(expectedToken);

			// Act
			var result = _authController.Login(new CMDRequest { PhoneNumber = "123456789", Password = "12345" }) as UnauthorizedObjectResult;

			// Assert
			result.Should().NotBeNull();
		}


		[TestMethod]
		public void TestSubscribe1() {
			decimal amount = 1000;
			Guid authToken = Guid.NewGuid();

			_mockCMDService.Setup(s => s.Login("123456789", "1234")).Returns(authToken);
			_authController.Login(new CMDRequest { PhoneNumber = "123456789", Password = "1234" });
			_mockCMDService.Setup(s => s.IsValidToken(authToken)).Returns(true);

			// Act
			var result = _microCreditController.Subscribe(amount, authToken) as OkObjectResult;

			// Assert
			result.Should().NotBeNull();
		}
		[TestMethod]
		public void TestSubscribe2() {
			decimal amount = 6000;
			Guid authToken = Guid.NewGuid();

			_mockCMDService.Setup(s => s.Login("123456789", "1234")).Returns(authToken);
			_authController.Login(new CMDRequest { PhoneNumber = "123456789", Password = "1234" });
			_mockCMDService.Setup(s => s.IsValidToken(authToken)).Returns(true);

			// Act
			var result = _microCreditController.Subscribe(6000, authToken) as UnauthorizedObjectResult;

			// Assert
			result.Should().NotBeNull();
			result.Value.Should().BeOfType<string>()
					.Which.Should().Contain("Credit amount is not allowed");
		}
	}
}

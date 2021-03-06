using Moq;
using NUnit.Framework;
using ThoughtWorks.CruiseControl.CCTrayLib.X10;
using ThoughtWorks.CruiseControl.CCTrayLib.Configuration;

namespace ThoughtWorks.CruiseControl.UnitTests.CCTrayLib.X10
{
	[TestFixture]
	public class LampControllerTest
	{
		private LampController lampController;
		private Mock<IX10LowLevelDriver> x10LowLevelDriverMock;
        private const int GREEN_LAMP_DEVICE_CODE = 1;
        private const int RED_LAMP_DEVICE_CODE = 2;
        private const int YELLOW_LAMP_DEVICE_CODE = 3;

		[SetUp]
		public void SetUp()
		{
			x10LowLevelDriverMock = new Mock<IX10LowLevelDriver>(MockBehavior.Strict);

			X10Configuration configuration = new X10Configuration();
			configuration.SuccessUnitCode = GREEN_LAMP_DEVICE_CODE;
			configuration.BuildingUnitCode = YELLOW_LAMP_DEVICE_CODE;
			configuration.FailureUnitCode = RED_LAMP_DEVICE_CODE;
			IX10LowLevelDriver x10LowLevelDriver = x10LowLevelDriverMock.Object as IX10LowLevelDriver;
			lampController = new LampController(configuration,x10LowLevelDriver);
		}
		
		[TearDown]
		public void TearDown()
		{
			x10LowLevelDriverMock.Verify();
		}
		
		[Test]
		public void ShouldSendGreenControlCodeWhenTurningGreenLampOn()
		{
			x10LowLevelDriverMock.Setup(driver => driver.ControlDevice(GREEN_LAMP_DEVICE_CODE, Function.On, 0)).Verifiable();
			lampController.GreenLightOn = true;
		}
		
		[Test]
		public void ShouldSendRedControlCodeWhenTurningRedLampOn()
		{
			x10LowLevelDriverMock.Setup(driver => driver.ControlDevice(RED_LAMP_DEVICE_CODE, Function.On, 0)).Verifiable();
			lampController.RedLightOn = true;
		}

		[Test]
		public void ShouldSendGreenControlCodeWhenTurningGreenLampOff()
		{
			x10LowLevelDriverMock.Setup(driver => driver.ControlDevice(GREEN_LAMP_DEVICE_CODE, Function.Off, 0)).Verifiable();
			lampController.GreenLightOn = false;
		}

		[Test]
		public void ShouldSendRedControlCodeWhenTurningRedLampOff()
		{
			x10LowLevelDriverMock.Setup(driver => driver.ControlDevice(RED_LAMP_DEVICE_CODE, Function.Off, 0)).Verifiable();
			lampController.RedLightOn = false;
		}
		
		[Test]
		public void OnceTheLampHasBeenTurnedOnTurningItOnAgainDoesNotSendTheCommandAgain()
		{
			x10LowLevelDriverMock.Setup(driver => driver.ControlDevice(RED_LAMP_DEVICE_CODE, Function.Off, 0)).Verifiable();
			lampController.RedLightOn = false;
			lampController.RedLightOn = false;
			lampController.RedLightOn = false;
			x10LowLevelDriverMock.Setup(driver => driver.ControlDevice(RED_LAMP_DEVICE_CODE, Function.On, 0)).Verifiable();
			lampController.RedLightOn = true;
			lampController.RedLightOn = true;
			lampController.RedLightOn = true;

			x10LowLevelDriverMock.Setup(driver => driver.ControlDevice(GREEN_LAMP_DEVICE_CODE, Function.On, 0)).Verifiable();
			lampController.GreenLightOn = true;
			lampController.GreenLightOn = true;
			lampController.GreenLightOn = true;
			x10LowLevelDriverMock.Setup(driver => driver.ControlDevice(GREEN_LAMP_DEVICE_CODE, Function.Off, 0)).Verifiable();
			lampController.GreenLightOn = false;
			lampController.GreenLightOn = false;
			lampController.GreenLightOn = false;

		}

	}
}

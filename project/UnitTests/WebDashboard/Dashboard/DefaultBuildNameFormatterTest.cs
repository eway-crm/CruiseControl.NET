using NUnit.Framework;
using ThoughtWorks.CruiseControl.WebDashboard.Dashboard;

namespace ThoughtWorks.CruiseControl.UnitTests.WebDashboard.Dashboard
{
	[TestFixture]
	public class DefaultBuildNameFormatterTest
	{
		[Test]
		public void ShouldFormatPassedBuildCorrectly()
		{
			string formattedBuildName = new DefaultBuildNameFormatter().GetPrettyBuildName("log20020830164057Lbuild.6.xml");
			Assert.AreEqual("30 Aug 2002 16:40 (6)", formattedBuildName);
		}

		[Test]
		public void ShouldFormatFailedBuildCorrectly()
		{
			string formattedBuildName = new DefaultBuildNameFormatter().GetPrettyBuildName("log20020507042535.xml");
			Assert.AreEqual("07 May 2002 04:25 (Failed)", formattedBuildName);
		}

		[Test]
		public void ShouldGetCorrectCssLinkForPassedBuild()
		{
			string className = new DefaultBuildNameFormatter().GetCssClassForBuildLink("log20020830164057Lbuild.6.xml");
			Assert.AreEqual("build-passed-link", className);
		}

		[Test]
		public void ShouldGetCorrectCssLinkForFailedBuild()
		{
			string className = new DefaultBuildNameFormatter().GetCssClassForBuildLink("log20020507042535.xml");
			Assert.AreEqual("build-failed-link", className);
		}
	}
}

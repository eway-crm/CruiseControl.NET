using ThoughtWorks.CruiseControl.Core;

namespace ThoughtWorks.CruiseControl.WebDashboard.Dashboard
{
	public class DefaultBuildNameFormatter : IBuildNameFormatter
	{
		public string GetPrettyBuildName(string originalBuildName)
		{
			bool isSuccessful = LogFileUtil.IsSuccessful(originalBuildName);
			return string.Format("{0} ({1})", LogFileUtil.GetFormattedDateString(originalBuildName), isSuccessful ?  LogFileUtil.ParseBuildNumber(originalBuildName) : "Failed");	
		}

		public string GetCssClassForBuildLink(string originalBuildName)
		{
			return LogFileUtil.IsSuccessful(originalBuildName) ? "build-passed-link" : "build-failed-link";
		}
	}
}

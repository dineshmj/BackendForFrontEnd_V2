namespace FW.Common.WebUtilties.Helpers
{
	public static class NextJSBFFHelper
	{
		public static bool DoesNextJsRelativePathExist(this string relativePath, string clientAppFolderName = "client-app")
		{
			relativePath = relativePath.Trim ('/');
			var segments = relativePath.Split ('/', StringSplitOptions.RemoveEmptyEntries);

			var root = Path.Combine (Directory.GetCurrentDirectory (), clientAppFolderName, "out");
			var targetPagePath = Path.Combine (root, Path.Combine (segments), "index.html");

			return File.Exists (targetPagePath);
		}	
	}
}
namespace FW.Landscape.Common
{
	public static class CookieNames
	{
		public const string PAS_SHELL_HOST_BFF = "__Host-PAS-Shell-bff";								// Cookie names must start with __Host- so that OIDC logout can work correctly.
		public const string MICROSERVICE_PRODUCTS_HOST_BFF = "__Host-Microservice-Products-bff";
	}
}
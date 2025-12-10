namespace FW.Landscape.Common.Microservices
{
	public static class OrdersMicroservice
	{
		public const string CLIENT_NAME_FOR_IDP = "Orders Microservice BFF Client";
		public const string CLIENT_ID_FOR_IDP = "Orders.Microservice.BFF.ClientID";
		public const string CLIENT_SECRET_FOR_IDP = "3ac91ab3-7ba0-4727-b4f7-36120bec10c5";     // Random GUID for demo purposes only.

		public const string BFF_CLIENT_BASE_URL = "https://localhost:33800";
		public const string MICROSERVICE_API_BASE_URL = "https://localhost:44305";
	}
}
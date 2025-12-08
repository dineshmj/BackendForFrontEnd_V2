namespace FW.Landscape.Common
{
	public static class PASShellBFF
	{
		public const string CLIENT_BASE_URL = "https://localhost:44367";
		public const string CLIENT_NAME = "PAS Shell BFF Client";
		public const string CLIENT_ID = "PAS.Shell.BFF.ClientID";
		public const string CLIENT_SECRET = "50485e4d-183f-465a-8234-fb4fb4316d09";         // Random GUID as secret; for demo purposes only.

        // FIXME: Are these needed?
        public const string SHELL_BFF_TOKEN_ISSUER = "PAS Shell BFF Client";
		public const string SHELL_BFF_TOKEN_SIGNING_KEY = "990fdac7-be84-4ebe-85ff-038368d7ca19";       // Random GUID as signing key; for demo purposes only.

        // FIXME: Are these needed?
        // For each microservice, define audience constants. Comment out later.
        public const string SHELL_BFF_TOKEN_AUDIENCE_FOR_PRODUCTS = "Products Microservice BFF Client";
	}
}
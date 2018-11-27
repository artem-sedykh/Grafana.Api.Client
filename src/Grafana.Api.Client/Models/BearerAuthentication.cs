namespace Grafana.Models
{
    internal class BearerAuthentication : IAuthenticationData
    {
        public BearerAuthentication(string token)
        {
            Parameter = token;
        }

        public string Scheme => "Bearer";

        public string Parameter { get; }
    }
}

namespace Grafana.Models
{
    public interface IAuthenticationData
    {
        string Scheme { get; }

        string Parameter { get; }
    }
}

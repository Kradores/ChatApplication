namespace Chat.Domain.Options;

public class AppConfiguration
{
    public string Secret { get; set; } = null!;
    public bool BehindSSLProxy { get; set; }
    public string ProxyIP { get; set; } = null!;
    public string ApplicationUrl { get; set; } = null!;
}

namespace Blog;

public static class Configuration
{
    // TOKEN - JWT - Json Web Token
    public static string JwtKey = "Z3VpbGhlcm1lbWVpcmFsaW5kb2Vnb3N0b3Nv=";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "GuilhermeMeiraSilvaLindo";
    public static SmtpConfiguration Smtp = new();
    
    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

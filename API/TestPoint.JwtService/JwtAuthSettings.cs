namespace TestPoint.JwtService;

public class JwtAuthSettings
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string TokenSecurityKey { get; set; }
    public int TokenExp { get; set; }
    public int ShortTokenExp { get; set; }
}

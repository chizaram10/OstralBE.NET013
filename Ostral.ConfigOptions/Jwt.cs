namespace Ostral.ConfigOptions;

public class Jwt
{
    public string? Token { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public double LifeTime { get; set; }
}
namespace Infrastructure.Configuration;


public class CorsOptions
{
    public const string SectionName = "Cors";
    
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
    
    public string[] AllowedMethods { get; set; } = { "GET", "POST", "PUT", "DELETE", "PATCH", "OPTIONS" };
    
    public string[] AllowedHeaders { get; set; } = { "Authorization", "Content-Type", "X-Requested-With" };
   
    public bool AllowCredentials { get; set; } = true;

    public int PreflightCacheDuration { get; set; } = 3600;
    
    public string[] ExposedHeaders { get; set; } = Array.Empty<string>();
}
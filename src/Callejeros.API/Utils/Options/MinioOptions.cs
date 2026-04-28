namespace Adoption.API.Utils.Options;

public class MinioOptions
{
    public string InternalEndpoint { get; set; } = null!;
    public string PublicEndpoint { get; set; } = null!;
    public string AccessKey { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public bool WithSsl { get; set; }
}

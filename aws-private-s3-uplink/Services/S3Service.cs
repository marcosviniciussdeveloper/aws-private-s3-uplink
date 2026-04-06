using Amazon.S3;
using Amazon.S3.Model;

namespace aws_private_s3_uplink.Services;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;

    public S3Service(IAmazonS3 s3Client)
    {
        _s3Client = s3Client;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string bucketName)
    {
        var key = $"{Guid.NewGuid()}_{file.FileName}";
        
        using var stream = file.OpenReadStream();
        
        var request = new PutObjectRequest
        {
            BucketName = bucketName,
            Key = key,
            InputStream = stream,
            ContentType = file.ContentType
        };

        await _s3Client.PutObjectAsync(request);

        return key;
    }
}

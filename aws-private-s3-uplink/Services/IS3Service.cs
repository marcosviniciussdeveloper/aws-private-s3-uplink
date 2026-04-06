using Microsoft.AspNetCore.Http;

namespace aws_private_s3_uplink.Services;

public interface IS3Service
{
    Task<string> UploadFileAsync(IFormFile file, string bucketName);
}

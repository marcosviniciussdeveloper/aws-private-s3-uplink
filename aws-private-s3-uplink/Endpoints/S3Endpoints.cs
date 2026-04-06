using Amazon.S3;
using aws_private_s3_uplink.Services;
using Microsoft.AspNetCore.Mvc;

namespace aws_private_s3_uplink.Endpoints;

public static class S3Endpoints
{
    public static void MapS3Endpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/upload", async ([FromForm] UploadRequest request, [FromServices] IS3Service s3Service) =>
        {
            if (request.File == null || request.File.Length == 0)
            {
                return Results.BadRequest("Nenhum arquivo enviado ou o arquivo está vazio.");
            }

            if (string.IsNullOrWhiteSpace(request.BucketName))
            {
                return Results.BadRequest("O nome do bucket é obrigatório.");
            }

            try
            {
                var fileKey = await s3Service.UploadFileAsync(request.File, request.BucketName);
                return Results.Ok(new { Message = "Upload concluído com sucesso", FileKey = fileKey });
            }
            catch (AmazonS3Exception e)
            {
                return Results.Problem(
                    detail: $"Erro de comunicação com S3: {e.Message}",
                    statusCode: 500,
                    title: "Erro na AWS"
                );
            }
            catch (Exception e)
            {
                return Results.Problem(
                    detail: $"Erro inesperado: {e.Message}",
                    statusCode: 500,
                    title: "Erro Interno do Servidor"
                );
            }
        })
        .DisableAntiforgery()
        .WithName("UploadFile")
        .WithOpenApi();
    }
}

public class UploadRequest
{
    public IFormFile? File { get; set; }
    public string? BucketName { get; set; }
}

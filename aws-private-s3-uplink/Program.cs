using Amazon.S3;
using aws_private_s3_uplink.Endpoints;
using aws_private_s3_uplink.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionando política global de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<IS3Service, S3Service>();

var app = builder.Build();

// Habilitando o uso da política de CORS
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapS3Endpoints();

app.Run();

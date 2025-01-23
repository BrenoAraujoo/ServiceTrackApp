using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.Interfaces;

namespace ServiceTrackApp.Api.Controllers;

[ApiController]
public class UploadController (IBlobStorageService blobStorageService, IConfiguration configuration) : ApiControllerBase
{

    
    [HttpPost("v1/upload")]
    public async Task<IActionResult> Upload(IFormFile? file)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest("Arquivo inválido.");
        }

        // Obter o stream do arquivo
        using var stream = file.OpenReadStream();

        // Enviar para o Blob Storage
        
        var blobUrl = await blobStorageService.UploadImageAsync(stream, file.FileName);

        return Ok(new { Url = blobUrl });
    }
    [HttpGet("v1/image")]
    public IActionResult GenereateSasUrl(string blobName)
    {
        var connectionString = configuration["Azure:ConnectionString"];
        var containerName = configuration["Azure:ContainerName"];
        
        var blobServiceClient = new BlobServiceClient(connectionString);
        var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);
        
        
        var blobClient = blobContainerClient.GetBlobClient(blobName);

        if (!blobClient.Exists())
        {
            return NotFound("Arquivo não encontrado.");
        }

        // Defina o tempo de expiração da URL SAS
        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerName,
            BlobName = blobName,
            Resource = "b",
            ExpiresOn = DateTime.UtcNow.AddMinutes(30) // Expira em 30 minutos
        };

        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var sasUrl = blobClient.GenerateSasUri(sasBuilder);

        return Ok(sasUrl.ToString());
    }
}
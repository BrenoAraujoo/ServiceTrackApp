using Microsoft.AspNetCore.Mvc;
using ServiceTrackApp.Application.Interfaces;

namespace ServiceTrackApp.Api.Controllers;

[ApiController]
public class UploadController (IBlobStorageService blobStorageService) : ApiControllerBase
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
}
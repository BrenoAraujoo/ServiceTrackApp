using System.Text;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using ServiceTrackApp.Application.Interfaces;
using ServiceTrackApp.Domain.Common.Erros;
using ServiceTrackApp.Domain.Common.Result;

namespace ServiceTrackApp.Application.Services;

public class BlobStorageService (IConfiguration configuration) : IBlobStorageService
{

    public async Task<Result> UploadImageAsync(Stream imageStream, string fileName)
    {
        try
        {
            var connectionString = configuration["Azure:ConnectionString"];
            var containerName = configuration["Azure:ContainerName"];

            // Cria um cliente para o contêiner
            var blobServiceClient = new BlobServiceClient(connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            
            // Cria o cliente para o blob (arquivo)
            StringBuilder sb = new StringBuilder();
            sb.Append(Guid.NewGuid());
            sb.Append("-");
            sb.Append(fileName);
            
            var blobClient = containerClient.GetBlobClient(sb.ToString());

            // Faz o upload
            await blobClient.UploadAsync(imageStream, overwrite: true);

            // Retorna a URL do blob
            return Result<string>.Success(blobClient.Uri.ToString());
            
        }
        catch (Exception e)
        {
            return Result.Failure(CustomError.ValidationError(ErrorMessage.UploadImageError));
        }

    }
}
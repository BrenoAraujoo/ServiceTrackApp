using Microsoft.AspNetCore.Http;
using ServiceTrackApp.Domain.Common.Result;

namespace ServiceTrackApp.Application.Interfaces;

public interface IBlobStorageService
{
    public Task<Result> UploadImageAsync(Stream imageStream, string fileName);
}
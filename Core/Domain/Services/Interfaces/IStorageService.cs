using Dto.Responses;
using Dto.UploadData;

namespace MarketplaceSI.Core.Domain.Services.Interfaces;
public interface IStorageService
{
    public Task DeleteObjectAsync(string image, string path = null);
    public Task DeleteObjectsAsync(List<string> images);
    List<S3UploadFiles> GetMultiPresignedUrl(List<string> fileNames, string path);
    List<string> GetImagesLinkPublic(List<string> images, string? path = null);
    string GetImageLinkPublic(string image, string path);
    public Task<List<string>> UploadImagesAsync(List<InputFileData> images, string path);
    public Task<string> UploadImageAsync(Stream image, string imageName, string path = null);
    Task InvalidateFileLink(string image, string? path = null);
}
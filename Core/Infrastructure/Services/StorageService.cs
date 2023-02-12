using Amazon.CloudFront;
using Amazon.S3.Model;
using Amazon.S3;
using MarketplaceSI.Core.Domain.Services.Interfaces;
using Dto.Responses;
using Dto.UploadData;
using MarketplaceSI.Core.Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using System.Web;
using Domain.Settings;

namespace MarketplaceSI.Core.Infrastructure.Services;
public class StorageService : IStorageService
{
    protected readonly AmazonS3Client _client;
    protected readonly AmazonSettings _options;
    private readonly string _envName;
    private FileInfo _fileInfo;
    public StorageService(IOptions<AmazonSettings> options, IHostEnvironment environment)
    {
        _options = options.Value;
        _client = new AmazonS3Client(_options.KeyId, _options.KeySecret, Amazon.RegionEndpoint.GetBySystemName(_options.Region));
        _envName = environment.EnvironmentName;
    }
    private string PathToEnv(string path)
    {
        return $"{_envName.ToLower()}/{path}";
    }

    public string CreatePreSignedUrl(string imageName, string path)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _options.BacketName,
            Key = PathToEnv(path) + $"{imageName}",
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddHours(1)
        };

        return _client.GetPreSignedURL(request);
    }

    public async Task DeleteObjectAsync(string image, string path = null)
    {
        try
        {
            await _client.DeleteObjectAsync(new DeleteObjectRequest()
            {
                BucketName = _options.BacketName,
                Key = PathToEnv(path ?? "") + image
            });
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"An exception has occurred while deleting object from backet{Environment.NewLine} {ex.Message}");
        }
    }

    public async Task DeleteObjectsAsync(List<string> images)
    {
        if (images.Count != 0)
        {
            List<KeyVersion> kv = images.Select(x => new KeyVersion() { Key = PathToEnv(x) }).ToList();
            await _client.DeleteObjectsAsync(new DeleteObjectsRequest()
            {
                BucketName = _options.BacketName,
                Objects = kv
            });
        }
    }
    public List<S3UploadFiles> GetMultiPresignedUrl(List<string> fileNames, string path)
    {
        var listUrls = new List<S3UploadFiles>();
        foreach (var file in fileNames)
        {
            listUrls.Add(new S3UploadFiles() { S3UploadUrl = CreatePreSignedUrl(file, path), FileName = file });
        }
        return listUrls;
    }
    public List<string> GetImagesLinkPublic(List<string> images, string? path = null)
    {
        List<string> list = new List<string>();
        foreach (string image in images)
        {
            list.Add(GetImageLinkPublic(image, path));
        }
        return list;
    }
    public string GetImageLinkPublic(string image, string path)
    {
        return $"{_options.CloudFrontDomain}/{PathToEnv(path ?? "")}{image}";
    }

    public async Task<List<string>> UploadImagesAsync(List<InputFileData> images, string path)
    {
        var files = new List<string>();
        try
        {
            foreach (var image in images)
            {
                files.Add(await UploadImageAsync(image.FileStream, Guid.NewGuid().ToString(), path));
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"An exception has occurred while uploading objects to backet{Environment.NewLine} {ex.Message}");
        }
        return files;
    }

    public async Task<string> UploadImageAsync(Stream image, string imageName, string path = null)
    {
        try
        {
            await _client.PutObjectAsync(new PutObjectRequest()
            {
                InputStream = image,
                BucketName = _options.BacketName,
                Key = PathToEnv(path ?? "") + imageName,
                CannedACL = S3CannedACL.PublicRead,
            });
            return $"{_options.CloudFrontDomain}/{PathToEnv(path ?? "")}{imageName}";
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"An exception has occurred while uploading object to backet{Environment.NewLine} {ex.Message}");
            return string.Empty;
        }
    }
    public async Task InvalidateFileLink(string image, string? path = null)
    {
        try
        {
            if (!string.IsNullOrEmpty(image))
            {
                var client = new AmazonCloudFrontClient(_options.KeyId, _options.KeySecret, Amazon.RegionEndpoint.GetBySystemName(_options.Region));
                var filePath = PathToEnv(path ?? "") + HttpUtility.UrlEncode(image);
                await client.CreateInvalidationAsync(new Amazon.CloudFront.Model.CreateInvalidationRequest()
                {
                    DistributionId = _options.CloudFrontStorageDistributor,
                    InvalidationBatch = new Amazon.CloudFront.Model.InvalidationBatch()
                    {
                        CallerReference = RandomExtension.GenerateActivationKey(),
                        Paths = new Amazon.CloudFront.Model.Paths()
                        {
                            Quantity = 1,
                            Items = new List<string>() {
                                "/" + filePath
                            }
                        }
                    }
                });
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"An exception has occurred while invalidating cloudfront images link{Environment.NewLine} {ex.Message}");
        }
    }
}

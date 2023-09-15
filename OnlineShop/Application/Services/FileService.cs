using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using OnlineShop.Application.Configurations;
using OnlineShop.Application.Models;

namespace OnlineShop.Application.Services;

public class FileService
{
    private readonly BlobContainerClient _filesContainer;
    
    public FileService(IOptions<BlobStorageConfiguration> configuration)
    {
        var credential = new StorageSharedKeyCredential(configuration.Value.StorageAccount, configuration.Value.Key);
        var blobServiceClient = new BlobServiceClient(new Uri(configuration.Value.BlobUri), credential);
        _filesContainer = blobServiceClient.GetBlobContainerClient(configuration.Value.StorageAccount);
    }

    public async Task<List<BlobModel>> ListAsync()
    {
        var files = new List<BlobModel>();

        await foreach (var file in _filesContainer.GetBlobsAsync())
        {
            string uri = _filesContainer.Uri.ToString();
            var name = file.Name;
            var fullUri = $"{uri}/{name}";
            
            files.Add(new BlobModel
            {
                Uri = fullUri,
                Name = name,
                ContentType = file.Properties.ContentType,
            });
        }

        return files;
    }

    public async Task<BlobResponseModel> UploadAsync(IFormFile blob)
    {
        var client = _filesContainer.GetBlobClient(blob.FileName);

        await using var data = blob.OpenReadStream();
        await client.UploadAsync(data, new BlobHttpHeaders { ContentType = blob.ContentType });

        BlobResponseModel response = new()
        {
            Status = $"File {blob.FileName} Uploaded Successfully",
            Error = false,
            Blob =
            {
                Uri = client.Uri.AbsoluteUri,
                Name = client.Name
            }
        };

        return response;
    }

    public async Task<BlobModel?> DownloadAsync(string blobFileName)
    {
        var file = _filesContainer.GetBlobClient(blobFileName);

        if (!await file.ExistsAsync()) 
            return null;
        
        var content = await file.DownloadContentAsync();
        
        var blobContent = await file.OpenReadAsync();
        blobContent.Position = 0;
        
        return new BlobModel
        {
            Content = blobContent, 
            Name = blobFileName,
            ContentType = content.Value.Details.ContentType
        };
    }
}
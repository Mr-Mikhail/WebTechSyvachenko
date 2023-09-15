namespace OnlineShop.Application.Configurations;

public class BlobStorageConfiguration
{
    public string Key { get; set; } = default!;

    public string BlobUri { get; set; } = default!;

    public string StorageAccount { get; set; } = default!;
}
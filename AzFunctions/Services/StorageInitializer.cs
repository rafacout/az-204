using Azure.Storage.Blobs;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AzFunctions.Services;

public class StorageInitializer(IConfiguration config, ILogger<StorageInitializer> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var conn = config["AzureWebJobsStorage"]!;

        var blobService = new BlobServiceClient(conn);
        await blobService.GetBlobContainerClient("my-blobs")
            .CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        logger.LogInformation("Blob container 'my-blobs' ready.");

        var queueService = new QueueServiceClient(conn);
        await queueService.GetQueueClient("myqueue-items")
            .CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        logger.LogInformation("Queue 'myqueue-items' ready.");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

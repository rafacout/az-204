using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzFunctions.Functions;

public class BlobFunction
{
    private readonly ILogger<BlobFunction> _logger;

    public BlobFunction(ILogger<BlobFunction> logger)
    {
        _logger = logger;
    }

    [Function(nameof(BlobFunction))]
    public async Task Run([BlobTrigger("my-blobs/{name}", Connection = "AzureWebJobsStorage")] Stream stream, string name)
    {
        using var blobStreamReader = new StreamReader(stream);
        var content = await blobStreamReader.ReadToEndAsync();
        _logger.LogInformation("C# Blob trigger function Processed blob\n Name: {name} \n Data: {content}", name, content);
    }
}
using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace FunctionApp
{
    public class Function1
    {
        /**
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        } **/

        [Function("BlobTriggerFunction")]
        public static async Task Run(
            [BlobTrigger("test2/{name}")] Stream stream, string name,
            FunctionContext context)
        {
            var logger = context.GetLogger("BlobTriggerFunction");
            logger.LogInformation("Hello World - Blob Trigger Function Initiated");

            try
            {
                using var blobStreamReader = new StreamReader(stream);
                var content = await blobStreamReader.ReadToEndAsync();
                logger.LogInformation($"Blob Trigger Function Processed blob\n Name: {name} \n Data: {content}");

            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occurred while processing Blob: {name}");
                throw;
            }
        }

    }

}

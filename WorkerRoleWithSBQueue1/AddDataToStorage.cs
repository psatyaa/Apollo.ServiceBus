using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using ServiceBus.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRoleWithSBQueue1
{
    public class AddDataToStorage
    {

        //public AddDataToStorage(SampleClass sampleClass)
        //{
        //    string storageConnectionString = "DefaultEndpointsProtocol=https;"
        //                                + "AccountName=satyargdiag"
        //                                + ";AccountKey=+isZSKGLHG5CP8//KGHqsHnp/9bRPwXHpIchd3xa38MWb+yfv+LpfuiSegtNkJZXAplRaGvg/e1oyF68pRIMVA=="
        //                                + ";EndpointSuffix=core.windows.net";

        //    CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
        //    CloudBlobClient serviceClient = account.CreateCloudBlobClient();

        //    // Create container. Name must be lower case.
        //    Console.WriteLine("Creating container...");
        //    var container = serviceClient.GetContainerReference("QueueProcessedContainer");
        //    container.CreateIfNotExistsAsync().Wait();

        //    var json = Newtonsoft.Json.JsonConvert.SerializeObject(sampleClass);
        //    // write a blob to the container
        //    CloudBlockBlob blob = container.GetBlockBlobReference("helloworld.txt");
        //    blob.UploadTextAsync(json).Wait();
        //}

        public void AddDatatoLocalFile(SampleClass sampleClass)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(sampleClass);
            File.AppendAllText(Directory.GetCurrentDirectory() + @"\myFile.txt", json);
        }
        
    }
}

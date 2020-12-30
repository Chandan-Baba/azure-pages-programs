using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Azure_Blob_File
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Get a connection string to our Azure Storage account.
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=demostoragelbaccount;AccountKey=q5t0r4dwT0UZ9oj9s0oYQQCrUsHayZn3pNNGYeDGbpuqHfdcmMguHMnKrXLRp+YNwu0bqw78Rj/qLNeGiMNgJA==;EndpointSuffix=core.windows.net";
            string containerName = "lbdemocontainers";

            Console.WriteLine($"Recursivly listing blobs and virtual directories for container '{containerName}'");

            BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            await ListBlobsForPrefixRecursive(container, "", 0);
            //BlobClient blobClient = container.GetBlobClient(blobItem[0].Name.ToString());

            //string localFilePath = @"D:\VS2019 projects\storage";
            ////string downloadFilePath = localFilePath..Replace(".txt", "DOWNLOAD.txt");
            //string downloadFilePath = localFilePath + @"\" + blobClient.Name;
            //Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

            //// Download the blob's contents and save it to a file
            //BlobDownloadInfo download = blobClient.Download();

            //FileStream downloadFileStream = File.OpenWrite(downloadFilePath);
            //download.Content.CopyToAsync(downloadFileStream);
            //downloadFileStream.Close();
            Console.ReadLine();
        }

        public static async Task ListBlobsForPrefixRecursive(BlobContainerClient container, string prefix, int level)
        {
            string spaces = new string(' ', level);
            Console.WriteLine($"{spaces}- {prefix}");
             foreach (Page<BlobHierarchyItem> page in container.GetBlobsByHierarchy(prefix: prefix, delimiter: "/").AsPages())
            {
                foreach (var blob in page.Values.Where(item => item.IsBlob).Select(item => item.Blob))
                {
                    Console.WriteLine($"{spaces} {blob.Name}");
                }
                var prefixes = page.Values.Where(item => item.IsPrefix).Select(item => item.Prefix);
                foreach (var p in prefixes)
                {
                    await ListBlobsForPrefixRecursive(container, p, level + 1);
                }
            }

        }
    }
}

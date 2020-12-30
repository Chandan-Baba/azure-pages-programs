using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
          {
            string storageAccount_connectionString = "DefaultEndpointsProtocol=https;AccountName=demolbsa;AccountKey=7K5vcUQJxNuCvAhuGumw1+MQDib1T/gLsaZfa6jzDgoB8sLPw3hnes+KswkzhAxYktlG6LwcU3ykULXiIApCjA==;EndpointSuffix=core.windows.net";

            BlobServiceClient blobServiceClient = new BlobServiceClient(storageAccount_connectionString);

            BlobContainerClient b = blobServiceClient.GetBlobContainerClient("demolbcontainer");

            List<BlobItem> blobItem = b.GetBlobs().ToList();
            
            BlobClient blobClient = b.GetBlobClient(blobItem[2].Name.ToString());

            string localFilePath = @"D:\VS2019 projects\storage";
            //string downloadFilePath = localFilePath..Replace(".txt", "DOWNLOAD.txt");
            string downloadFilePath = localFilePath + @"\" + blobClient.Name;

            string strDirectoryName = Path.GetDirectoryName(blobClient.Name);
            string strDirectoryFullPath = @"D:\VS2019 projects\storage" + @"\" + strDirectoryName;
            //string strFileName = Path.GetFileName(blobClient.Name);

            Console.WriteLine("\nDownloading blob to\n\t{0}\n", downloadFilePath);

            // Download the blob's contents and save it to a file
            BlobDownloadInfo download = blobClient.Download();
            if(!Directory.Exists(strDirectoryFullPath))
            {
                Directory.CreateDirectory(strDirectoryFullPath);
            }

            
            FileStream downloadFileStream = File.OpenWrite(downloadFilePath);
            download.Content.CopyToAsync(downloadFileStream);
            downloadFileStream.Close();

            //try
            //{
            //    //root directory
            //    CloudBlobDirectory dira = container.GetDirectoryReference(string.Empty);
            //    //true for all sub directories else false 
            //    var rootDirFolders = dira.ListBlobsSegmentedAsync(true, BlobListingDetails.Metadata, null, null, null, null).Result;

            //    foreach (var blob in rootDirFolders.Results)
            //    {
            //        Console.WriteLine(blob.Container.Name.ToString());
            //        Console.WriteLine(blob.StorageUri.ToString());
            //        Console.WriteLine(blob.Uri.ToString());
            //    }
            //    string text = new WebClient().DownloadString("https://demostoragelbaccount.blob.core.windows.net/lbdemocontainers/sample.txt");
            //    Console.ReadLine();
            //}
            //catch (Exception e)
            //{
            //    //  Block of code to handle errors
            //    Console.WriteLine("Error", e);

            //}
        }
    }
}

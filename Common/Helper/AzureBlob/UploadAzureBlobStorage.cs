using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper.AzureBlob
{
    public class UploadAzureBlobStorage
    {
        public static async Task<string> UploadFileAzure(IFormFile file)
        {
            string response = $"{AzureConfigurations.RootPath}/{AzureConfigurations.Container}/";

            try
            {
                var blobContainerClient = new BlobContainerClient(AzureConfigurations.StorageConnectionString, AzureConfigurations.Container);
                string UploadFileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.UtcNow.ToString("yyyy-MM-ddTHHmmss") + Path.GetExtension(file.FileName);
                string UploadFilePath = $"{AzureConfigurations.Directory}/{UploadFileName}";
                BlobClient Uploadblob = blobContainerClient.GetBlobClient(UploadFilePath);


                using (var stream = file.OpenReadStream())
                {
                    var result = await Uploadblob.UploadAsync(stream);
                    response = response + UploadFilePath;
                }
                return response;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
    }
}

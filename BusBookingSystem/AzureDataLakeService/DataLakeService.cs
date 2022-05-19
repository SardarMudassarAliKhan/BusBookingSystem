using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using CVBank.Dto.ConfigurationModel;
using CVBank.Dto.ResponseModel;
using CVBank.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.Management.DataLake.Store;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Rest;
using Microsoft.Azure.Management.DataFactory.Models;
using CVBank.DataLakeExtensions;
using Common.Helper;

namespace CVBank.AzureDataLakeService
{
    public class DataLakeService : IDataLakeService
    {
        private MySettings MySettings { get; set; }

        public DataLakeService(IOptions<MySettings> settings)
        {
            MySettings = settings.Value;
        }
        public async Task<string> GetAccessToken()
        {
            string tennant = MySettings.TenantId;
            string clientId = MySettings.ClientId;
            string clientSecret = MySettings.ClientSecret;

            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
                new KeyValuePair<string, string>("resource", "https://management.azure.com/")
            };

            var url = $"https://login.microsoftonline.com/{tennant}/oauth2/token";

            using var client = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new FormUrlEncodedContent(nvc)
            };

            using var res = await client.SendAsync(req);
            var jsonString = await res.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<dynamic>(jsonString);
            return json.access_token;
        }
        public async Task<PipelineResponseModel> GetDataPipelines()
        {
            string subscriptionId = MySettings.SubscriptionId;
            string bearer = await GetAccessToken();
            string resourceGroup = MySettings.ResourceGroup;
            string factoryName = MySettings.DataFactory;
            var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.DataFactory/factories/{factoryName}/pipelines?api-version=2018-06-01";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            using var res = await client.SendAsync(req);
            var jsonString = await res.Content.ReadAsStringAsync();
            var myDeserializedClass = JsonConvert.DeserializeObject<PipelineResponseModel>(jsonString);
            return myDeserializedClass;
        }

        public async Task<string> GetPipelineName(string pipelineId)
        {
            string subscriptionId = MySettings.SubscriptionId;
            string bearer = await GetAccessToken();
            string resourceGroup = MySettings.ResourceGroup;
            string factoryName = MySettings.DataFactory;
            var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.DataFactory/factories/{factoryName}/pipelines?api-version=2018-06-01";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
            var req = new HttpRequestMessage(HttpMethod.Get, url);

            using var res = await client.SendAsync(req);
            var jsonString = await res.Content.ReadAsStringAsync();
            var myDeserializedClass = JsonConvert.DeserializeObject<PipelineResponseModel>(jsonString);
            if (myDeserializedClass.value.HasData())
            {
                foreach (var pipe in myDeserializedClass.value)
                {
                    if (pipe.etag.Equals(pipelineId))
                    {
                        return pipe.name;
                    }
                }
            }
            return "";
        }

        public async Task<SingleFileUploadResponse> UploadFile(SingleFileUpload file)
        {
            var response = new SingleFileUploadResponse();
            try
            {
                if (file.FormFile.HasData())
                {
                    response.FileName = System.IO.Path.GetFileNameWithoutExtension(file.FormFile.FileName);
                    response.Extension = System.IO.Path.GetExtension(file.FormFile.FileName);
                    response.FilePath = file.FormFile.FileName.AppendTimeStamp();
                    var path = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot",
                                response.FilePath);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.FormFile.CopyToAsync(stream);
                    }
                    // Data Lake Store File System Management Client
                    DataLakeStoreFileSystemManagementClient adlsFileSystemClient;

                    // Portal > Azure AD > App Registrations > App > Application ID (aka Client ID)
                    string clientId = MySettings.ClientId;

                    // Portal > Azure AD > App Registrations > App > Settings > Keys (aka Client Secret)
                    string clientSecret = MySettings.ClientSecret;

                    // Portal > Azure AD > Properties > Directory ID (aka Tenant ID)
                    string tenantId = MySettings.TenantId;

                    // Name of the Azure Data Lake Store
                    string adlsAccountName = MySettings.AccountName;

                    // 2. Create credentials to authenticate requests as an Active Directory application
                    var clientCredential = new ClientCredential(clientId, clientSecret);
                    var creds = ApplicationTokenProvider.LoginSilentAsync(tenantId, clientCredential).Result;

                    // 2. Initialise Data Lake Store File System Client
                    adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(creds);

                    // 3. Upload a file to the Data Lake Store
                    response.FilePath = $"Input/{response.FilePath}";

                    adlsFileSystemClient.FileSystem.UploadFile(adlsAccountName, path, response.FilePath, 1, false, true);

                    if ((System.IO.File.Exists(path)))
                    {
                        System.IO.File.Delete(path);
                    }
                    return response;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<CreateRunResponse> StartPipeline(string pipelineName, string fileName)
        {
            try
            {
                string resourceGroup = MySettings.ResourceGroup;
                string dataFactoryName = MySettings.DataFactory;
                string subscriptionId = MySettings.SubscriptionId;
                string accessToken = await GetAccessToken();
                DataFactoryManagementClient client;
                TokenCredentials cred = new TokenCredentials(accessToken);
                client = new DataFactoryManagementClient(cred)
                {
                    SubscriptionId = subscriptionId
                };
                var parameters = new Dictionary<string, object>()
                    {
                       {"FileName", fileName }
                    };
                var runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName, parameters: parameters).Result.Body;
                return runResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<string> PipelineStatus(string runId)
        {
            try
            {
                string resourceGroup = MySettings.ResourceGroup;
                string dataFactoryName = MySettings.DataFactory;
                string subscriptionId = MySettings.SubscriptionId;
                string accessToken = await GetAccessToken();
                DataFactoryManagementClient client;
                TokenCredentials cred = new TokenCredentials(accessToken);

                client = new DataFactoryManagementClient(cred)
                {
                    SubscriptionId = subscriptionId
                };
                
                var runResponse = await client.PipelineRuns.GetAsync(resourceGroupName: resourceGroup, factoryName: dataFactoryName, runId: runId);
                return runResponse.HasData() ? runResponse.Status : "";
            }
            catch (Exception ex)
            {
                return "";
            }

        }
        public async Task<SingleFileDownloadResponse> DownloadFile(SingleFileDownload file)
        {
            
            var response = new SingleFileDownloadResponse();

            try
            {
                DataLakeStoreFileSystemManagementClient adlsFileSystemClient;

                string clientId = MySettings.ClientId;
                string clientSecret = MySettings.ClientSecret;
                string tenantId = MySettings.TenantId;
                string adlsAccountName = MySettings.AccountName;

                var clientCredential = new ClientCredential(clientId, clientSecret);
                var creds = await ApplicationTokenProvider.LoginSilentAsync(tenantId, clientCredential);

                adlsFileSystemClient = new DataLakeStoreFileSystemManagementClient(creds);

                var source = file.SourcePath;
                response.FileName = source.GetFileName();
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", $"temp\\{response.FileName}");
                var destination = path;

                adlsFileSystemClient.FileSystem.DownloadFile(adlsAccountName, source, destination, 1, false, true);
                var bytes = System.IO.File.ReadAllBytes($"wwwroot\\temp\\{response.FileName}");
                if(bytes.Length > 0)
                {
                    response.bytes = bytes;
                }
                System.IO.File.Delete($"wwwroot\\temp\\{response.FileName}");
            }
            catch (Exception ex)
            {
                return response;                
            }

            return response;
        }
    }
}

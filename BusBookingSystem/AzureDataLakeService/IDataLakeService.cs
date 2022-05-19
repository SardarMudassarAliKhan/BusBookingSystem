using Microsoft.Azure.Management.DataFactory.Models;
using CVBank.Dto.ResponseModel;
using CVBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVBank.AzureDataLakeService
{
    public interface IDataLakeService
    {
        Task<string> GetAccessToken();
        Task<PipelineResponseModel> GetDataPipelines();
        Task<string> GetPipelineName(string pipelineId);
        Task<SingleFileUploadResponse> UploadFile(SingleFileUpload file);
        Task<SingleFileDownloadResponse> DownloadFile(SingleFileDownload file);
        Task<CreateRunResponse> StartPipeline(string pipelineName, string fileName);
        Task<string> PipelineStatus(string runId);
    }
}

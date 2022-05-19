using CVBank.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.InfraStructure.Service.File
{
    public interface IFilesService
    {
        Task<ResponseSaveFileDto> SaveUserFile(SaveFileDto request);
        Task<SavePipelineRunResponse> SavePipelineRun(SavePipeLineRunRequest request);
        Task<bool> UpdatePipelineStatus(long userFileId,string status);
        Task<List<FileDto>> GetUserFiles(DownloadFilterDto filter);
        Task<bool> DeleteFile(long userFileId);
    }
}

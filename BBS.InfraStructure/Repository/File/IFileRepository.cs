using CVBank.Domain.Data.Domain;
using CVBank.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.InfraStructure.Repository.File
{
    public interface IFileRepository
    {
        Task<UserFile> SaveUserFile(UserFile request);
        Task<List<UserFile>> GetUserFiles(DownloadFilterDto filter);
        Task<SavePipelineRunResponse> SavePipelineRun(SavePipeLineRunRequest request);
        Task<bool> UpdatePipelineStatus(long userFileId, string status);
        Task<bool> DeleteFile(long userFileId);
    }
}

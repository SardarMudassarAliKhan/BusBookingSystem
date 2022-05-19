using Common.Helper;
using CVBank.Domain.Data.Domain;
using CVBank.Dto.Dtos;
using CVBank.InfraStructure.Repository.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.InfraStructure.Service.File
{
    public class FilesService : IFilesService
    {
        private readonly IFileRepository _userFileRepository;
        public FilesService(IFileRepository userFileRepository)
        {
            _userFileRepository = userFileRepository;
        }

        public async Task<bool> DeleteFile(long userFileId)
        {
            return await _userFileRepository.DeleteFile(userFileId);
        }

        public async Task<List<FileDto>> GetUserFiles(DownloadFilterDto filter)
        {
            var userFiles = new List<FileDto>();
            var files = await _userFileRepository.GetUserFiles(filter);
            if(files.HasData())
            {
                foreach(var file in files)
                {
                    userFiles.Add(new FileDto()
                    {
                        UserFileId = file.UserFileId,
                        UserId = file.UserId,
                        FilePath = file.FilePath,
                        PipelineName = file.PipelineName,
                        FileName = file.FileName,
                        PipelineTag = file.PipelineTag,
                        FileDirectory = file.FileDirectory,
                        CreatedDate = file.CreatedDate,
                        RunId = file.RunId,
                        Status = file.Status
                    }) ;
                }
            }
            return userFiles; 
        }

        public async Task<SavePipelineRunResponse> SavePipelineRun(SavePipeLineRunRequest request)
        {
            return await _userFileRepository.SavePipelineRun(request);
        }

        public async Task<ResponseSaveFileDto> SaveUserFile(SaveFileDto request)
        {
            var result = new ResponseSaveFileDto();
            string OutputUrl = string.Empty;
            if (request.FilePath.ToLower().Contains(".xml")) OutputUrl = "OutputXML/";
            else OutputUrl = "Output/";

            // Add Complete Url
            OutputUrl += request.TempFileName;

            // Set CSV Extension for downloading
            OutputUrl = Path.ChangeExtension(OutputUrl, ".csv");

            var userFile = new UserFile() {
                UserId = request.UserId,
                PipelineTag = request.PipelineTag,
                FilePath = request.FilePath,
                FileName = request.FileName,
                FileDirectory = OutputUrl,
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            var response = await _userFileRepository.SaveUserFile(userFile);
            result.UserFileId = response.UserFileId;
            result.FilePath = response.FilePath;
            result.RunId = response.RunId;
            result.CreatedDate = response.CreatedDate;
            return result;
        }

        public async Task<bool> UpdatePipelineStatus(long userFileId, string status)
        {
            return await _userFileRepository.UpdatePipelineStatus(userFileId, status);
        }
    }
}

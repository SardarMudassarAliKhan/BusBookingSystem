using Microsoft.EntityFrameworkCore;
using CVBank.Data;
using CVBank.Domain.Data.Domain;
using CVBank.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CVBank.InfraStructure.Repository.File
{
    public class FileRepository : IFileRepository
    {
        private readonly ApplicationDbContext _context;
        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteFile(long userFileId)
        {
            var userFile = await _context.UserFiles.FirstOrDefaultAsync(x => x.UserFileId == userFileId);
            if (userFile != null)
            {
                userFile.IsActive = false;
                _context.Entry(userFile).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<UserFile>> GetUserFiles(DownloadFilterDto filter)
        {
            var query = _context.UserFiles.Where(x => x.UserId == filter.UserId && x.IsActive);
            if (filter.StartDate.HasValue)
            {
                query = query.Where(x => x.CreatedDate.Date >= filter.StartDate.Value.Date);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(x => x.CreatedDate.Date <= filter.EndDate.Value.Date);
            }
            if (!string.IsNullOrEmpty(filter.Pipeline))
            {
                query = query.Where(x => x.PipelineTag.Contains(filter.Pipeline));
            }
            if (!string.IsNullOrEmpty(filter.SearchBox))
            {
                query = query.Where(x => x.FileName.Contains(filter.SearchBox));
            }
            return await query.ToListAsync();
        }

        public async Task<SavePipelineRunResponse> SavePipelineRun(SavePipeLineRunRequest request)
        {
            var response = new SavePipelineRunResponse();
            var userFile = await _context.UserFiles.FirstOrDefaultAsync(x => x.UserFileId == request.UserFileId);
            if(userFile != null)
            {
                userFile.RunId = request.RunId;
                userFile.PipelineName = request.PipelineName;
                _context.Entry(userFile).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                response.Succeeded = true;
            }
            return response;
        }

        public async Task<UserFile> SaveUserFile(UserFile request)
        {
            _context.UserFiles.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> UpdatePipelineStatus(long userFileId, string status)
        {
            var userFile = await _context.UserFiles.FirstOrDefaultAsync(x => x.UserFileId == userFileId);
            if (userFile != null)
            {
                userFile.Status = status;
                _context.Entry(userFile).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

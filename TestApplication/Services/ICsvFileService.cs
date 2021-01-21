using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace TestApplication.Services
{
    public interface ICsvFileService
    {
        public Task DownloadFileToServerAsync(IFormFile uploadFile);
        public Task DeleteFileRecordFromDatabaseAndFileAsync(int Id);
    }
}

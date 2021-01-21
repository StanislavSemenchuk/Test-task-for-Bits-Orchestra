using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Models;

namespace TestApplication.Services
{
    public class CsvFileService : ICsvFileService
    {
        private MyDBContext _context;
        private IWebHostEnvironment _appEnvironment;

        public CsvFileService(MyDBContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task DownloadFileToServerAsync(IFormFile uploadFile)
        {
            string permittedExtension = ".csv";
            string ext = Path.GetExtension(uploadFile.FileName).ToLowerInvariant();

            if (!string.IsNullOrEmpty(ext) || ext == permittedExtension)
            {
                string path = "/files/" + uploadFile.FileName;

                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fileStream);
                }
                CsvFile file = new CsvFile { Name = uploadFile.FileName, Path = path };
                _context.CsvFiles.Add(file);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("File with wrong extension");
            }
        }

        public async Task DeleteFileRecordFromDatabaseAndFileAsync(int Id)
        {
            var fileRecord = await _context.CsvFiles.FirstOrDefaultAsync(f => f.Id == Id);
            if (fileRecord != null)
            {
                File.Delete(_appEnvironment.WebRootPath + fileRecord.Path);
                _context.CsvFiles.Remove(fileRecord);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}

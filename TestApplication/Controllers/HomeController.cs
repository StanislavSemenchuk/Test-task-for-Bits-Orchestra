using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using TestApplication.Models;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    public class HomeController : Controller
    {
        private ICsvFileService _csvFileService;
        private MyDBContext _context;

        public HomeController(ICsvFileService csvFileService, MyDBContext context)
        {
            _context = context;
            _csvFileService = csvFileService;
        }

        public async Task<IActionResult> Index()
        {
            return View( await _context.CsvFiles.ToListAsync());
        }

        public IActionResult AddFile() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadFile) 
        {
            if (uploadFile != null) 
            {
                await _csvFileService.DownloadFileToServerAsync(uploadFile);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Delete(int Id) 
        {
            await _csvFileService.DeleteFileRecordFromDatabaseAndFileAsync(Id);
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

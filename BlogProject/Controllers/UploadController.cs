using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BlogProject.Controllers
{
    [Route("api/[controller]")]
    public class UploadController : Controller
    {
        public IConfiguration _configuration;

        public UploadController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult UploadImage([FromForm] IFormFile imageData)
        {
            string uniqueName = null;
            if (imageData is not null)
            {
                string uploadFolder = Path.Combine(_configuration["ImgFolderPath"]);
                var extensionName = Path.GetExtension(imageData.FileName);
                var imageName = Path.GetFileNameWithoutExtension(imageData.FileName);
                uniqueName = $"{imageName}_{DateTime.Now.ToString("dd-MMM-yyyyTHH.mm.ss")}_{Guid.NewGuid()}{extensionName}";
                string filePath = Path.Combine(uploadFolder, uniqueName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                imageData.CopyTo(fileStream);
                fileStream.Flush();
            }
            return Json(new { uploadedUrl = $"{_configuration["ImageLinkURL"]}{_configuration["ImgFolderPathString"]}/{uniqueName}" });
        }
    }
}

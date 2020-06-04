using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace catch_the_trash.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public class FileUploadAPI
        {
            public IFormFile files { get; set; }
        }

        private string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };

        [HttpPost]
        public async Task<string> Post([FromForm]FileUploadAPI objFile)
        {
            

            try
            {
                if (objFile.files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }

                    var ext = Path.GetExtension(objFile.files.FileName).ToLowerInvariant();

                    if (permittedExtensions.Contains(ext))
                    {                
                            using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + Guid.NewGuid() + ext))
                        {
                            objFile.files.CopyTo(fileStream);
                            fileStream.Flush();
                            return "\\Upload\\" + objFile.files.FileName;
                        }
                    }
                    else
                    {
                        return "Wrong extension";
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }
    }
}

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
            public List<IFormFile> files { get; set; }
        }

        private string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]FileUploadAPI objFile)
        {
            

            try
            {
                if (objFile.files.Count > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }

                    List<string> fileNames = new List<string>();

                    foreach (var file in objFile.files)
                    {
                        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

                        if (permittedExtensions.Contains(ext))
                        {
                            string newFileName = Guid.NewGuid() + ext;
                            using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + newFileName))
                            {
                                file.CopyTo(fileStream);
                                fileStream.Flush();
                                fileNames.Add(newFileName);
                            }
                        }
                       
                    }

                    return Ok(fileNames);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}

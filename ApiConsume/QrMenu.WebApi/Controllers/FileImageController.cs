using System.Globalization;
using System.Text;
using BusinessLayer.Concrete;

using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SixLabors.ImageSharp.Formats.Jpeg;
using Ude;

namespace QrMenu.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FileImageController : ControllerBase
{

    [HttpDelete]
    public async Task<IActionResult> DeleteImage(string path)
    {
        var checkForPhoto = path.Split('/');
        var fileName = @"images/" + checkForPhoto[6];
        var file = new FileInfo(fileName);
        //  var photoArray = checkForPhoto.Split('/');
        if (file.Exists) //check file exsit or not
            file.Delete();
        return Ok(fileName);
    }


    [HttpPost]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
       

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var path = Path.Combine(Directory.GetCurrentDirectory(), "images/" + fileName);
       /* var stream = new FileStream(path, FileMode.Create);
        await file.CopyToAsync(stream);
        stream.Close();
       */
        using (var image = Image.Load(file.OpenReadStream()))
        {
            // Resize the image to a maximum of 800 pixels wide or high, maintaining the aspect ratio
            var size = new Size(1270, 720);
            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(size.Width, size.Height),
                Mode = ResizeMode.Max
            }));

            // Optimize the image and save it to disk
            var encoder = new JpegEncoder { Quality = 60 };
             
           
            image.Save(path, encoder);
        }
      
        return Created("", new { FileName = fileName });
    }

    [HttpGet("images/{imageName}")]
    public IActionResult GetImage(string imageName)
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "images", imageName);

        if (!System.IO.File.Exists(imagePath)) return NotFound();

        // Read the image file as bytes
        var imageBytes = System.IO.File.ReadAllBytes(imagePath);

        // Determine the content type based on the image file extension
        var contentType = GetContentType(imagePath);

        // Return the image file as the response with the appropriate content type
        return File(imageBytes, contentType);
    }

    private string GetContentType(string path)
    {
        var provider = new FileExtensionContentTypeProvider();
        string contentType;
        if (!provider.TryGetContentType(path, out contentType)) contentType = "application/octet-stream";
        return contentType;
    }
}
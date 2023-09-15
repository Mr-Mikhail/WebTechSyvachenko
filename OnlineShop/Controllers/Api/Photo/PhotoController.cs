using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application.Services;

namespace OnlineShop.Controllers.Api.Photo;

[Route(Routes.PhotoManagementSystem)]
public class PhotoController : ControllerBase
{
    private readonly FileService _fileService;

    public PhotoController(FileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet(Routes.All)]
    public async Task<IActionResult> GetAllBlobs()
    {
        return Ok(await _fileService.ListAsync());
    }

    [HttpPut(Routes.Add)]
    public async Task<IActionResult> UploadBlob(IFormFile blob)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        if (!blob.ContentType.Contains("image"))
            return BadRequest("Only images are supported");
        
        var response = await _fileService.UploadAsync(blob);
        return Ok(response);
    }
    
    [HttpGet(Routes.Filtered)]
    public async Task<IActionResult> UploadBlob([FromQuery] string blobName)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var response = await _fileService.DownloadAsync(blobName);
        return File(response.Content, response.ContentType, response.Name);
    }
}
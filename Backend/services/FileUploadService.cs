namespace Backend.Services;

public class FileUploadService
{
    private readonly string _uploadFolder;
    private readonly ILogger<FileUploadService> _logger;

    public FileUploadService(IWebHostEnvironment environment, ILogger<FileUploadService> logger)
    {
        _uploadFolder = Path.Combine(environment.WebRootPath, "uploads", "products");
        _logger = logger;
        
        if (!Directory.Exists(_uploadFolder))
        {
            Directory.CreateDirectory(_uploadFolder);
        }
    }

    public async Task<(bool Success, string? FilePath, string? Error)> UploadImageAsync(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return (false, null, "File is empty");

            if (file.Length > 5 * 1024 * 1024)
                return (false, null, "File exceeds 5MB");

            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" }.Contains(ext))
                return (false, null, "Invalid file type");

            var fileName = Guid.NewGuid().ToString() + ext;
            var fullPath = Path.Combine(_uploadFolder, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return (true, "/uploads/products/" + fileName, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Upload failed");
            return (false, null, ex.Message);
        }
    }

    public bool DeleteImage(string? imagePath)
    {
        try
        {
            if (string.IsNullOrEmpty(imagePath)) return true;

            var fileName = Path.GetFileName(imagePath);
            var fullPath = Path.Combine(_uploadFolder, fileName);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }
}

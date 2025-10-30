using Microsoft.AspNetCore.Http;

namespace Backend.Services;

public class FileUploadService
{
    private readonly string _uploadFolder;
    private readonly long _maxFileSize = 5 * 1024 * 1024; // 5MB
    private readonly string[] _allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

    public FileUploadService(IWebHostEnvironment environment)
    {
        _uploadFolder = Path.Combine(environment.WebRootPath, "uploads", "products");
        
        // Tạo thư mục nếu chưa tồn tại
        if (!Directory.Exists(_uploadFolder))
        {
            Directory.CreateDirectory(_uploadFolder);
        }
    }


    public async Task<(bool success, string? filePath, string? error)> UploadImageAsync(IFormFile file)
    {
        try
        {
            // Validate file
            if (file == null || file.Length == 0)
                return (false, null, "File is empty");

            // Kiểm tra kích thước
            if (file.Length > _maxFileSize)
                return (false, null, $"File size exceeds {_maxFileSize / 1024 / 1024}MB");

            // Kiểm tra extension
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_allowedExtensions.Contains(extension))
                return (false, null, $"Only {string.Join(", ", _allowedExtensions)} files are allowed");

            // Tạo tên file unique
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_uploadFolder, fileName);

            // Lưu file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Trả về đường dẫn tương đối
            var relativePath = $"/uploads/products/{fileName}";
            return (true, relativePath, null);
        }
        catch (Exception ex)
        {
            return (false, null, $"Error uploading file: {ex.Message}");
        }
    }


    public bool DeleteImage(string? imagePath)
    {
        try
        {
            if (string.IsNullOrEmpty(imagePath))
                return true;

            // Chuyển từ đường dẫn tương đối sang đường dẫn tuyệt đối
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
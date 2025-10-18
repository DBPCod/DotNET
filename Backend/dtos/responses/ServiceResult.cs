namespace Backend.Dtos.Responses;

public class ServiceResult
{
    // Cloudinary
    public string PublicId { get; set; } = "";
    public string Url { get; set; } = "";
    public string Format { get; set; } = "";

    // General
    public string Error { get; set; } = "";
}
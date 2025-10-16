namespace Backend.Dtos.Responses;

public class Response
{
    public string Message { get; set; } = "Internal Server Error";
    public int StatusCode { get; set; } = 500;
    public Data Data { get; set; } = new Data();
}
namespace Backend.Dtos.Responses;

public class Response
{
    public string Message { get; set; } = "Internal Server Error";
    public int StatusCode { get; set; } = 200;
    public ResponseData Data { get; set; } = new ResponseData();
}
namespace Backend.Utils.Customs;

public class ExceptionCustom : Exception
{
    public int StatusCode { get; }

    public ExceptionCustom(string message) : base(message)
    {
        StatusCode = 500;
    }

    public ExceptionCustom(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }

    public ExceptionCustom(int statusCode, string message, Exception innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}

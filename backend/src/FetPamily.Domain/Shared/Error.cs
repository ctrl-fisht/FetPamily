namespace FetPamily.Domain.Shared;

public record Error
{
    public string Code { get;}
    public string Message { get;}
    public ErrorType ErrorType { get;}

    private Error(string code, string message, ErrorType errorType)
    {
        Code = code;
        Message = message;
        ErrorType = errorType;
    }

    public static Error ValidationError(string code, string message)
    {
        return new Error(code, message, ErrorType.ValidationError);
    }

    public static Error NotFound(string code, string message)
    {
        return new Error(code, message, ErrorType.NotFound);
    }

    public static Error AlreadyExist(string code, string message)
    {
        return new Error(code, message, ErrorType.Conflict);
    }

    public static Error InternalError(string code, string message)
    {
        return new Error(code, message, ErrorType.InternalError);
    }
}
namespace FetPamily.Domain.Shared;

public static class Errors
{
    public static class General
    {
        public static Error ValidationInvalid(string name) =>
            Error.ValidationError("value.is.invalid", $"parameter '{name}' is invalid");
        
        public static Error ValidationMaxLength(string name, int maxLength) =>
            Error.ValidationError("value.is.invalid", $"parameter {name} max length is {maxLength}");

        public static Error ValidationNotNull(string name) => 
            Error.ValidationError("value.is.invalid", $"parameter '{name}' cannot be empty");
        
        public static Error ValidationInvalidFormat(string name, string format) =>
            Error.ValidationError("value.is.invalid", $"parameter '{name}' format is invalid (example '{format}')");
        
        public static Error ValidationGreaterThan(string name, int min) =>
            Error.ValidationError("value.is.invalid", $"parameter '{name}' must be greater than {min}");
        
        public static Error ValidationNumberNegative(string name) =>
            Error.ValidationError("value.is.invalid", $"parameter '{name} can't be negative");
        
        public static Error ValidationDateInFuture(string name) =>
            Error.ValidationError("value.is.invalid", $"date '{name}' in future");
        
        
        public static Error NotFound(string name) =>
            Error.NotFound("record.not.found", $"record '{name}' not found");
        
        
        
        
        public static Error AlreadyExist(string id) =>
            Error.AlreadyExist("record.already.exist", $"record '{id}' already exists"); 
    }
}
using PersonDirectory.Domain.Errors;

namespace PersonDirectory.Domain.Abstractions;

public record Error(ErrorList Code, string Name)
{
    public static readonly Error None = new(ErrorList.None, string.Empty);

    public static readonly Error NullValue = new(ErrorList.Null, "Null value was provided");
}

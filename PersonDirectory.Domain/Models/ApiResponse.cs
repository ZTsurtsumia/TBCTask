namespace PersonDirectory.Domain.Models
{
    public abstract record ApiResponse
    {
        public string? Message { get; protected set; }
    }
    public sealed record ApiResponse<T> : ApiResponse
    {
        public T? Payload { get; set; }
        public ApiResponse(T? payload, string? message = null)
        {
            Payload = payload;
            Message = message;
        }
    }
}

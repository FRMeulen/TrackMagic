namespace TrackMagic.Infrastructure.Middleware
{
    public class ErrorResult
    {
        public List<string>? Errors { get; set; }
        public string? Source { get; set; }
        public string? Exception { get; set; }
        public string? ErrorId { get; set; }
        public string? SupportMessage { get; set; }
        public int StatusCode { get; set; }
    }
}

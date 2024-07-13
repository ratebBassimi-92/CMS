namespace CmsApi.ModelsDto
{
    public class ResponseDto
    {
        public string? Status { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Object? Data { get; set; }
    }
}

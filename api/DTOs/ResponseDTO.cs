namespace api.DTOs
{
    public class ResponseDTO
    {
        public bool? sucesso { get; set; }
        public string? mensagem { get; set; }
        public object? dados { get; set; }
    }
}

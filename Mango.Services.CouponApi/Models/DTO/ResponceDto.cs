namespace Mango.Services.CouponApi.Models.DTO
{
    public class ResponceDto
    {
        public object? Result { get; set; }
        public bool isSuccess { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}

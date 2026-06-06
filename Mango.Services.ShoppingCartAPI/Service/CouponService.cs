using ango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Service.IService;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Mango.Services.ShoppingCartAPI.Service
{
    public class CouponService :ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/CouponApi/GetByCode/{couponCode}");
            var apiContet = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponceDto>(apiContet);
            if (resp != null && resp.isSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();
        }
    }
}

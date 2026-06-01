using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService baseService;

        public CouponService(IBaseService baseService)
        {
            this.baseService = baseService;
        }
        public async Task<ResponceDto?> CreateCouponsAsync(CouponDto couponDto)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/CouponApi"
            });
        }

        public async Task<ResponceDto?> DeleteCouponsAsync(int id)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CouponAPIBase + "/api/CouponApi/" + id
            });
        }

        public async Task<ResponceDto?> GetAllCouponAsync()
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/CouponApi"
            });
        }

        public async Task<ResponceDto?> GetCouponByCodeAsync(string couponCode)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/CouponApi/GetByCode/" + couponCode
            });
        }

        public async Task<ResponceDto?> GetCouponByIdAsync(int id)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CouponAPIBase + "/api/CouponApi/" + id
            });
        }

        public async Task<ResponceDto?> UpdateCouponsAsync(CouponDto couponDto)
        {
            return await baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = couponDto,
                Url = SD.CouponAPIBase + "/api/CouponApi"
            });
        }
    }
}

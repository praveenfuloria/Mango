using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponceDto?> GetCouponByCodeAsync(string couponCode);
        Task<ResponceDto?> GetCouponByIdAsync(int id);
        Task<ResponceDto?> GetAllCouponAsync();
        Task<ResponceDto?> CreateCouponsAsync(CouponDto couponDto);
        Task<ResponceDto?> UpdateCouponsAsync(CouponDto couponDto);
        Task<ResponceDto?> DeleteCouponsAsync(int id);
    }
}

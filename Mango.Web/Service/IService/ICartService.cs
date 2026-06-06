using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICartService
    {
        Task<ResponceDto?> GetCartByUserIdAsnyc(string userId);
        Task<ResponceDto?> UpsertCartAsync(CartDto cartDto);
        Task<ResponceDto?> RemoveFromCartAsync(int cartDetailsId);
        Task<ResponceDto?> ApplyCouponAsync(CartDto cartDto);
        Task<ResponceDto?> EmailCart(CartDto cartDto);
    }
}

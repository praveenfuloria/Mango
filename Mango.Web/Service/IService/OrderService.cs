using Mango.Web.Models;
using Mango.Web.Utility;

namespace Mango.Web.Service.IService
{
    public class OrderService :IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }



        public async Task<ResponceDto?> CreateOrder(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = cartDto,
                Url = SD.OrderAPIBase + "/api/order/CreateOrder"
            });
        }

        //public async Task<ResponceDto?> CreateStripeSession(StripeRequestDto stripeRequestDto)
        //{
        //    return await _baseService.SendAsync(new RequestDto()
        //    {
        //        ApiType = SD.ApiType.POST,
        //        Data = stripeRequestDto,
        //        Url = SD.OrderAPIBase + "/api/order/CreateStripeSession"
        //    });
        //}

        public async Task<ResponceDto?> GetAllOrder(string? userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetOrders?userId=" + userId
            });
        }

        public async Task<ResponceDto?> GetOrder(int orderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/api/order/GetOrder/" + orderId
            });
        }

        public async Task<ResponceDto?> UpdateOrderStatus(int orderId, string newStatus)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = newStatus,
                Url = SD.OrderAPIBase + "/api/order/UpdateOrderStatus/" + orderId
            });
        }

        public async Task<ResponceDto?> ValidateStripeSession(int orderHeaderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = orderHeaderId,
                Url = SD.OrderAPIBase + "/api/order/ValidateStripeSession"
            });
        }
    }
}

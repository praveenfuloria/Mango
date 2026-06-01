using AutoMapper;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.DTO;
using Microsoft.Extensions.Logging.Abstractions;

namespace Mango.Services.CouponApi
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<Coupon, CouponDto>().ReverseMap();
        }
        //public static MapperConfiguration RegisterMaps()
        //{
        //    var mapperConfig = new MapperConfiguration(config =>
        //    {
        //        config.CreateMap<Coupon, CouponDto>().ReverseMap();
        //    }, new NullLoggerFactory());
        //    return mapperConfig;
        //}
    }
}

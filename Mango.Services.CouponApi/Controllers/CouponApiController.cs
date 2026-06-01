using AutoMapper;
using Mango.Services.CouponApi.Data;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ResponceDto _responceDto;

        public CouponApiController(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            _responceDto = new ResponceDto();
        }

        //Get api/couponapi
        [HttpGet]
        public ResponceDto Get()
        {
            try
            {
                //Get Data using Domain Model
                IEnumerable<Coupon> CouponsDomain = dbContext.Coupons.ToList();
                //Map Domain Model to DTO
                var Coupondto = mapper.Map<CouponDto>(CouponsDomain);
                //Return DTO as responce
                _responceDto.Result = Coupondto;
            }
            catch (Exception ex)
            {
                _responceDto.isSuccess = false;
                _responceDto.Message = ex.Message;

            }
            return _responceDto;
        }

        //Get api/couponapi
        [HttpGet]
        [Route("{id:int}")]
        public ResponceDto Get([FromRoute] int id)
        {
            try
            {
                //Get Data using Domain Model
                Coupon CouponsDomain = dbContext.Coupons.FirstOrDefault(x => x.CouponId == id);
                //Map Domain Model to DTO
                var Coupondto = mapper.Map<CouponDto>(CouponsDomain);
                //Return DTO as responce
                _responceDto.Result = CouponsDomain;
            }
            catch (Exception ex)
            {
                _responceDto.isSuccess = false;
                _responceDto.Message = ex.Message;

            }
            return _responceDto;
        }

        //Get api/couponapi
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponceDto GetByCode([FromRoute] string code)
        {
            try
            {
                //Get Data using Domain Model
                Coupon CouponsDomain = dbContext.Coupons.FirstOrDefault(x => x.CouponCode.ToLower() == code.ToLower());
                //Map Domain Model to DTO
                var Coupondto = mapper.Map<CouponDto>(CouponsDomain);
                //Return DTO as responce
                if (Coupondto == null)
                {
                    _responceDto.isSuccess = false;
                    _responceDto.Message = "Invalid Coupon Code";
                }
                _responceDto.Result = CouponsDomain;
            }
            catch (Exception ex)
            {
                _responceDto.isSuccess = false;
                _responceDto.Message = ex.Message;

            }
            return _responceDto;
        }
        //Post api/couponapi
        [HttpPost]
        public ResponceDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                //Map DTO to Domain Model
                var couponDomain = mapper.Map<Coupon>(couponDto);
                //Add to database
                dbContext.Coupons.Add(couponDomain);
                dbContext.SaveChanges();
                //Map Domain Model to DTO
                 couponDto = mapper.Map<CouponDto>(couponDomain);
                //Return DTO as responce
                _responceDto.Result = couponDto;
            }
            catch (Exception ex)
            {
                _responceDto.isSuccess = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }

        //Put api/couponapi
        [HttpPut]
        public ResponceDto Put([FromBody] CouponDto couponDto)
        {
            try
            {

                //Map DTO to Domain Model
                var couponDomain = mapper.Map<Coupon>(couponDto);
                var couponFromDb = dbContext.Coupons.FirstOrDefault(x => x.CouponId == couponDomain.CouponId);

                if(couponFromDb ==null)
                {
                    _responceDto.isSuccess = false;
                    _responceDto.Message = "Coupon Not Found";
                    return _responceDto;
                }
                //Add to database;
                dbContext.Update(couponDomain);
                dbContext.SaveChanges();
                //Map Domain Model to DTO
                couponDto = mapper.Map<CouponDto>(couponFromDb);
                //Return DTO as responce
                _responceDto.Result = couponDto;
            }
            catch (Exception ex)
            {
                _responceDto.isSuccess = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }

        //Delete api/couponapi
        [HttpDelete]
        public ResponceDto Delete(int id)
        {
            try
            {
                var couponFromDb = dbContext.Coupons.FirstOrDefault(x => x.CouponId == id);

                if (couponFromDb == null)
                {
                    _responceDto.isSuccess = false;
                    _responceDto.Message = "Coupon Not Found";
                    return _responceDto;
                }
                //Add to database;
                dbContext.Coupons.Remove(couponFromDb);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                _responceDto.isSuccess = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }
    }
}

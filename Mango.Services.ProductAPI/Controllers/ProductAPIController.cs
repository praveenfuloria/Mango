using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Mango.Services.ProductAPI.Model;
using Mango.Services.ProductAPI.Model.DTO;
using Mango.Services.ProductAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IMapper mapper;
        private readonly ResponceDto _responceDto;

        public ProductAPIController(AppDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            _responceDto = new ResponceDto();
        }

        //Get api/product
        [HttpGet]
        public ResponceDto Get()
        {
            try
            {
                //Get Data using Domain Model
                IEnumerable<Product> CouponsDomain = dbContext.Products.ToList();
                //Map Domain Model to DTO
                var Coupondto = mapper.Map<List<ProductDto>>(CouponsDomain);
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

        //Get api/product
        [HttpGet]
        [Route("{id:int}")]
        public ResponceDto Get([FromRoute] int id)
        {
            try
            {
                //Get Data using Domain Model
                Product ProductDomain = dbContext.Products.FirstOrDefault(x => x.ProductId == id);
                //Map Domain Model to DTO
                var Coupondto = mapper.Map<ProductDto>(ProductDomain);
                //Return DTO as responce
                _responceDto.Result = ProductDomain;
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
        public ResponceDto Post([FromBody] ProductDto producutDto)
        {
            try
            {
                //Map DTO to Domain Model
                var couponDomain = mapper.Map<Product>(producutDto);
                //Add to database
                dbContext.Products.Add(couponDomain);
                dbContext.SaveChanges();
                //Map Domain Model to DTO
                producutDto = mapper.Map<ProductDto>(couponDomain);
                //Return DTO as responce
                _responceDto.Result = producutDto;
            }
            catch (Exception ex)
            {
                _responceDto.isSuccess = false;
                _responceDto.Message = ex.Message;
            }
            return _responceDto;
        }

        //Put api/product
        [HttpPut]
        public ResponceDto Put([FromBody] ProductDto productDto)
        {
            try
            {

                //Map DTO to Domain Model
                var productDomain = mapper.Map<Product>(productDto);
                var productFromDb = dbContext.Products.FirstOrDefault(x => x.ProductId == productDomain.ProductId);

                if (productFromDb == null)
                {
                    _responceDto.isSuccess = false;
                    _responceDto.Message = "Product Not Found";
                    return _responceDto;
                }
                //Add to database;
                dbContext.Update(productDomain);
                dbContext.SaveChanges();
                //Map Domain Model to DTO
                productDto = mapper.Map<ProductDto>(productFromDb);
                //Return DTO as responce
                _responceDto.Result = productDto;
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
                var couponFromDb = dbContext.Products.FirstOrDefault(x => x.ProductId == id);

                if (couponFromDb == null)
                {
                    _responceDto.isSuccess = false;
                    _responceDto.Message = "Product Not Found";
                    return _responceDto;
                }
                //Add to database;
                dbContext.Products.Remove(couponFromDb);
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

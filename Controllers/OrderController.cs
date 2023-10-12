using AutoMapper;
using Azure;
using BharatMedicsV2.Models;
using BharatMedicsV2.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BharatMedicsV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        protected APIs reply;
        private readonly IOrder _orderRepository;
        private readonly IMapper mapper;
         
        public OrderController(IOrder orderRepository, IMapper _mapper)
        {
            _orderRepository = orderRepository;
            reply = new();
            mapper = _mapper;


        }

        #region Add Order
        [HttpPost("Add Orders")]
        public async Task<ActionResult> AddDrugs([FromBody] Order order)
        {

            try
            {
                if (order == null)
                {
                    return BadRequest();
                }

                if (order.OrderId > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }

                await _orderRepository.CreateAsync(order);
                await _orderRepository.SaveAsync();

                reply.Response = order;
                reply.StatusCode = HttpStatusCode.Created;
                return Ok();
                
            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest();
            }

            return Ok();


        }
        #endregion

        #region Get All Orders
        [HttpGet("Get All Orders")] 
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {

            try
            {
               
                reply.Response = await _orderRepository.GetAllOrders();
                reply.StatusCode = HttpStatusCode.OK;
                return Ok(reply.Response);

            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
                return BadRequest();
            }

            return Ok(reply.Response);


        }
        #endregion

        #region Get Specific Orders
        [HttpGet("{Id}", Name = "GetOrder")] 
        public async Task<ActionResult<APIs>> GetSpecificOrders(int Id)
        {
            try
            {

                if (Id == 0)
                {
                    return BadRequest();
                }
                var temp = await _orderRepository.GetAsync(u => u.OrderId == Id);
                if (temp == null)
                {
                    return NotFound();
                }


                reply.Response = await _orderRepository.GetAsync(u => u.OrderId == Id);
                reply.StatusCode = HttpStatusCode.OK;
                return Ok(reply);
            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return reply;

        }
        #endregion

        #region Update Order Details
        [HttpPut("{Id}", Name = "UpdateOrderDetails")] // This method updates the order
        public async Task<ActionResult> UpdateOrder(int Id, Order order)
        {

            try
            {

                if (Id == null || Id == 0 || Id != order.OrderId) return BadRequest();
                var drugg = await _orderRepository.GetAsync(u => u.OrderId == Id);


                drugg.IsVerified = order.IsVerified;

                await _orderRepository.UpdateOrderAsync(drugg);
                await _orderRepository.SaveAsync();

                reply.StatusCode = HttpStatusCode.NoContent;
                reply.IsSuccess = true;
                return Ok(reply.Response);
            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return BadRequest(reply.Response);


        }
        #endregion

        #region Delete Orders

        [HttpDelete("{Id}", Name = "DeleteOrders")] // This method deletes existince order from database
        public async Task<ActionResult<APIs>> DeleteOrder(int Id)
        {

            try
            {
                if (Id == null)
                {
                    return BadRequest();
                }

                var druggs = await _orderRepository.GetAsync(u => u.OrderId == Id);

                if (druggs == null)
                {
                    return BadRequest();
                }

                
                await _orderRepository.DeleteOrderAsync(druggs.OrderId);
                await _orderRepository.SaveAsync();

                reply.StatusCode = HttpStatusCode.NoContent;
                reply.IsSuccess = true;
                return Ok(reply);
            }
            catch (Exception ex)
            {
                reply.IsSuccess = false;
                reply.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return reply;
        }
        #endregion






    }
}

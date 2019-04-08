using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourAgency.BLL.DTOs;
using TourAgency.BLL.Interfaces;
using TourAgency.WebApi.ViewModel;

namespace TourAgency.WebApi.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
   
        [HttpGet]
        public IEnumerable<OrderViewModel> GetAllOrders()
        {
            var ordersDTO = _orderService.GetAllOrders();
            var mapperList = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>()).CreateMapper();
            return mapperList.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(ordersDTO);
        }

        [HttpGet("my/{email}")]
        public IEnumerable<OrderViewModel> GetAllOrdersByUserEmail(string email)
        {
            var ordersDTO = _orderService.GetAllOrdersByEmail(email);
            var mapperList = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, OrderViewModel>()).CreateMapper();
            return mapperList.Map<IEnumerable<OrderDTO>, List<OrderViewModel>>(ordersDTO);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int? id)
        {
            var orderDTO = _orderService.GetOrder(id);
            var order = _mapper.Map<OrderViewModel>(orderDTO);
            return Ok(order);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderViewModel model)
        {
            var orderDTO = _mapper.Map<OrderDTO>(model);
            bool isSaved = _orderService.CreateOrder(orderDTO);
            if (isSaved)
            {
                return Ok();
            }
            return BadRequest("Could not add the dailyProgram");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int? id)
        {
            var result = _orderService.DeleteOrder(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest("order wasn't deleted");
        }
    }
}

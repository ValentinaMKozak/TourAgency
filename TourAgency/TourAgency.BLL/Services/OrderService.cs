using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;
using TourAgency.BLL.Infrastructure;
using TourAgency.BLL.Interfaces;
using TourAgency.DAL.Entities;
using TourAgency.DAL.Interfaces;
using TourAgency.DAL.Repositories;

namespace TourAgency.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

        public OrderService()
        {
            Database = new EFUnitOfWork();
        }

        public IEnumerable<OrderDTO> GetAllOrders()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            var ordersDb = Database.Orders.GetAll();
            var ordersDTO = mapper.Map<IEnumerable<Order>, List<OrderDTO>>(ordersDb);

            return ordersDTO;
        }

        public IEnumerable<OrderDTO> GetAllOrdersByEmail(string email)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            var ordersDb = Database.Orders.Find(o => o.Email == email);
            var ordersDTO = mapper.Map<IEnumerable<Order>, List<OrderDTO>>(ordersDb);

            return ordersDTO;
        }
       
        public IEnumerable<OrderDTO> GetAllOrdersByTourId(int? tourId)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            var ordersDb = Database.Orders.Find(o => o.TourId == tourId);
            var ordersDTO = mapper.Map<IEnumerable<Order>, List<OrderDTO>>(ordersDb);

            return ordersDTO;
        }
       
        public OrderDTO GetOrder(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var orderDb = Database.Orders.Get(id.Value);
            if (orderDb == null)
                throw new ValidationException("Order wasn't found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>()).CreateMapper();
            var orderDTO = mapper.Map<Order, OrderDTO>(orderDb);

            return orderDTO;
        }
        public bool CreateOrder(OrderDTO orderDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>()).CreateMapper();
            var orderDb = mapper.Map<Order>(orderDTO);
            Database.Orders.Create(orderDb);

            return Database.Save();
        }

        public bool DeleteOrder(int? id)
        {
            if (id == null)
                throw new ValidationException("id was not passed", "");
            var orderDb = Database.Orders.Get(id.Value);
            if (orderDb == null)
                throw new ValidationException("Order wasn't found", "");
            Database.Orders.Delete(orderDb.OrderId);

            return Database.Save();
        }

        public bool UpdateOrder(int? id, OrderDTO OrderDTO)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}


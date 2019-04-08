using System;
using System.Collections.Generic;
using System.Text;
using TourAgency.BLL.DTOs;

namespace TourAgency.BLL.Interfaces
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetAllOrders();
        IEnumerable<OrderDTO> GetAllOrdersByEmail(string email);
        IEnumerable<OrderDTO> GetAllOrdersByTourId(int? tourId);
        OrderDTO GetOrder(int? id);
        bool CreateOrder(OrderDTO orderDTO);
        bool UpdateOrder(int? id, OrderDTO OrderDTO);
        bool DeleteOrder(int? id);
        void Dispose();
    }
}



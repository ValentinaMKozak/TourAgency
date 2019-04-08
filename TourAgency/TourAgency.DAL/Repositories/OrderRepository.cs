using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourAgency.DAL.EF;
using TourAgency.DAL.Entities;
using TourAgency.DAL.Interfaces;

namespace TourAgency.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly TourAgencyContext db;

        public OrderRepository(TourAgencyContext context)
        {
            db = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return db.Orders;
        }

        public Order Get(int? id)
        {
            return db.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public IEnumerable<Order> Find(Func<Order, bool> predicate)
        {
            return db.Orders.Where(predicate).ToList();
        }

        public void Create(Order order)
        {
            db.Orders.Add(order);
        }

        public void Update(Order order)
        {
            db.Entry(order).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
                db.Orders.Remove(order);
        }
    }
}

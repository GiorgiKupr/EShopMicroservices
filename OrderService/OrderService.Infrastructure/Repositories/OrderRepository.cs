using Microsoft.EntityFrameworkCore;
using OrderService.Application.Abstraction;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderServiceDbContext context) : base(context)
        {
        }
    }
}

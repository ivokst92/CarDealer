namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Services.Enums;
    using CarDealer.Services.Interfaces;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Customer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CustomerService : ICustomerService
    {
        private readonly CarDealerDbContext db;

        public CustomerService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public TotalCustomerSalesModel CustomerSales(int id)
        {
            var customerData = this.db.Customers
                .Include(d => d.Sales)
                .ThenInclude(s => s.Car)
                .ThenInclude(x => x.Parts)
                .ThenInclude(p => p.Part)
                .Where(x => x.Id == id).First();

            return new TotalCustomerSalesModel()
            {
                Name = customerData.Name,
                CarsCount = customerData.Sales.Count,
                TotalSpendMoney = customerData.Sales.Sum(x => x.Car.Parts.Sum(p => p.Part.Price)).GetValueOrDefault()
            };
        }

        public IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order)
        {
            var customers = this.db.Customers.AsQueryable();
            switch (order)
            {
                case OrderDirection.Ascending:
                    customers = customers
                        .OrderBy(c => c.BirthDate)
                        .ThenBy(t => t.Name);
                    break;
                case OrderDirection.Descending:
                    customers = customers
                        .OrderByDescending(c => c.BirthDate)
                        .ThenBy(t => t.Name);
                    break;
                default:
                    throw new InvalidOperationException($"Invalid order direction {order}.");
            }

            return customers
                .Select(x =>
                new CustomerModel
                {
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    IsYoungDriver = x.IsYoungDriver
                })
                .ToList();
        }
    }
}

namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Data.Models;
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

        public CustomerModel ById(int id)
            => this.db.Customers
            .Where(x => x.Id == id)
            .Select(x => new CustomerModel
            {
                Id = x.Id,
                Name = x.Name,
                BirthDate = x.BirthDate,
                IsYoungDriver = x.IsYoungDriver
            })
            .FirstOrDefault();

        public void CreateCustomer(string name, DateTime birthdate, bool isYoungDriver)
        {
            var customer = new Customer
            {
                Name = name,
                BirthDate = birthdate,
                IsYoungDriver = isYoungDriver
            };

            this.db.Add(customer);
            this.db.SaveChanges();
        }

        public bool CustomerExist(int id)
       => this.db.Customers.Any(x => x.Id == id);

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

        public void EditCustomer(int id, string name, DateTime birthdate, bool isYoungDriver)
        {
            var existingCustomer = this.db.Customers.Find(id);

            if (existingCustomer == null)
            {
                return;
            }

            existingCustomer.Name = name;
            existingCustomer.BirthDate = birthdate;
            existingCustomer.IsYoungDriver = isYoungDriver;
            this.db.SaveChanges();
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
                    Id = x.Id,
                    Name = x.Name,
                    BirthDate = x.BirthDate,
                    IsYoungDriver = x.IsYoungDriver
                })
                .ToList();
        }
    }
}

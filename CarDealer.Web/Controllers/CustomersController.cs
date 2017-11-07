namespace CarDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using CarDealer.Services.Interfaces;
    using CarDealer.Services.Enums;
    using CarDealer.Web.Models.Customers;

    public class CustomersController : Controller
    {
        private ICustomerService customerService;
        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        public IActionResult All(string order)
        {
            var orderDirection = order.ToLower() == "ascending"
                ? OrderDirection.Ascending
                : OrderDirection.Descending;
            var customers = customerService.OrderedCustomers(orderDirection);

            return View(new AllCustomersModel()
            {
                Customers = customers,
                OrderDirection = orderDirection
            });
        }

        public IActionResult TotalSales(int Id)
            => View(customerService.CustomerSales(Id));
    }
}
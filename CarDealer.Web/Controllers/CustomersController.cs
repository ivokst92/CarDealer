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

        [Route(nameof(Create))]
        public IActionResult Create()
        => View();

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CustomerFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            this.customerService.CreateCustomer(model.Name,
                                                model.Birthdate,
                                                model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending.ToString() });
        }

        [Route("customers/Edit/{Id}")]
        public IActionResult Edit(int Id)
        {
            var customer = this.customerService.ById(Id);

            if (customer == null)
            {
                NotFound();
            }

            return View(new CustomerFormModel()
            {
                Name = customer.Name,
                Birthdate = customer.BirthDate,
                IsYoungDriver = customer.IsYoungDriver
            });
        }

        [HttpPost]
        [Route("customers/Edit/{Id}")]
        public IActionResult Edit(int Id, CustomerFormModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool customerExist = this.customerService.CustomerExist(Id);
            if (!customerExist)
            {
                NotFound();
            }
            this.customerService.EditCustomer(Id,
                                              model.Name,
                                              model.Birthdate,
                                              model.IsYoungDriver);

            return RedirectToAction(nameof(All), new { order = OrderDirection.Ascending.ToString() });
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
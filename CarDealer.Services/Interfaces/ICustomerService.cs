namespace CarDealer.Services.Interfaces
{
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Customer;
    using System;
    using System.Collections.Generic;

    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);
        TotalCustomerSalesModel CustomerSales(int id);
        void CreateCustomer(string name, DateTime birthdate, bool IsYoungDriver);
        CustomerModel ById(int id);
        void EditCustomer(int id, string name, DateTime birthdate, bool isYoungDriver);
        bool CustomerExist(int id);
    }
}

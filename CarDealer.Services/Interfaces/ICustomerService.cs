namespace CarDealer.Services.Interfaces
{
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Customer;
    using System.Collections.Generic;
    public interface ICustomerService
    {
        IEnumerable<CustomerModel> OrderedCustomers(OrderDirection order);
        TotalCustomerSalesModel CustomerSales(int id);
    }
}

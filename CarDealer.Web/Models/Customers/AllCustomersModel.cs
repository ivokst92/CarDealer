﻿namespace CarDealer.Web.Models.Customers
{
    using CarDealer.Services.Enums;
    using CarDealer.Services.Models;
    using System.Collections.Generic;
    public class AllCustomersModel
    {
        public IEnumerable<CustomerModel> Customers { get; set; }
        public OrderDirection OrderDirection { get; set; }
    }
}

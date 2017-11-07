namespace CarDealer.Services.Interfaces
{
    using CarDealer.Services.Models;
    using System.Collections.Generic;

    public interface ISaleService
    {
        SaleDetailBaseModel SaleById(int Id);
        IEnumerable<SaleDetailModel> AllSales();
        IEnumerable<SaleDetailModel> AllSalesWithDiscount();
        IEnumerable<SaleDetailModel> AllSalesByDiscountPercentage(double percentage);
    }
}

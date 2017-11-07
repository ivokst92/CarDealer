namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Services.Interfaces;
    using CarDealer.Services.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class SaleService : ISaleService
    {
        private readonly CarDealerDbContext db;

        public SaleService(CarDealerDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<SaleDetailModel> AllSales()
        {
            return this.db.Sales
                 .Include(d => d.Car)
                 .ThenInclude(a => a.Parts)
                 .ThenInclude(a => a.Part)
                 .Include(a => a.Customer)
                 .Select(x => new SaleDetailModel
                 {
                     Customer = x.Customer.Name,
                     Make = x.Car.Make,
                     Model = x.Car.Model,
                     TravelledDistance = x.Car.TravelledDistance,
                     DiscountPercentage = x.Discount,
                     Price = x.Car.Parts.Sum(p => p.Part.Price).GetValueOrDefault(),
                     PriceWithDiscount = x.Car.Parts.Sum(p => p.Part.Price * (1 - x.Discount)).GetValueOrDefault(),
                 });
        }

        public IEnumerable<SaleDetailModel> AllSalesByDiscountPercentage(double percentage)
        {
            return this.AllSales().Where(x => x.DiscountPercentage == percentage / 100);
        }

        public IEnumerable<SaleDetailModel> AllSalesWithDiscount()
        {
            return this.AllSales().Where(x => x.DiscountPercentage != 0);
        }

        public SaleDetailBaseModel SaleById(int Id)
        {
            return this.db.Sales
                 .Include(d => d.Car)
                 .ThenInclude(a => a.Parts)
                 .ThenInclude(a => a.Part)
                 .Include(a => a.Customer)
                 .Where(x => x.Id == Id)
                 .Select(x => new SaleDetailBaseModel
                 {
                     Id = x.Id,
                     Customer = x.Customer.Name,
                     Make = x.Car.Make,
                     Model = x.Car.Model
                 }).First();
        }
    }
}

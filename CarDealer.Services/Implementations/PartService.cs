namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Services.Interfaces;
    using CarDealer.Services.Models.Parts;
    using System.Collections.Generic;
    using System.Linq;

    public class PartService : IPartService
    {
        private readonly CarDealerDbContext db;

        public PartService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<PartModel> All(int page, int size = 25)
        => this.db
                .Parts
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .Select(p =>
                new PartModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price.GetValueOrDefault(),
                    Quantity = p.Quantity,
                    Supplier = p.Supplier.Name,
                }).ToList();

        public void Create(string name, double price, int quantity, int supplierId)
        {
            this.db.Parts.Add(
                new Data.Models.Part()
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    SupplierId = supplierId
                });
            this.db.SaveChanges();
        }

        public int Total()
            => this.db.Parts.Count();

    }
}

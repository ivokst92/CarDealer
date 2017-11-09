namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Services.Interfaces;
    using System.Linq;
    using CarDealer.Services.Models;
    using System.Collections.Generic;
    using CarDealer.Services.Models.Supplier;

    public class SupplierService : ISupplierService
    {
        private readonly CarDealerDbContext db;
        public SupplierService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<SupplierModel> All(bool isImporter)
        {
            return db.Suppliers
                .Where(s => s.IsImporter == isImporter)
                .Select(s => new SupplierModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    TotalParts = s.Parts.Count
                }).ToList();
        }

        public IEnumerable<SupplierBaseModel> All()
        => this.db
            .Suppliers
            .OrderBy(s => s.Name)
            .Select(s => new SupplierBaseModel
            {
                Id = s.Id,
                Name = s.Name
            })
            .ToList();
    }
}

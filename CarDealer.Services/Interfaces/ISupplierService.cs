namespace CarDealer.Services.Interfaces
{
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Supplier;
    using System.Collections.Generic;

    public interface ISupplierService
    {
        IEnumerable<SupplierModel> All(bool isImporter);
        IEnumerable<SupplierBaseModel> All();
    }
}

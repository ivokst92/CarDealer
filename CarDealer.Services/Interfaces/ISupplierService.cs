namespace CarDealer.Services.Interfaces
{
    using CarDealer.Services.Models;
    using System.Collections.Generic;
    public interface ISupplierService
    {
        IEnumerable<SupplierModel> All(bool isImporter);
    }
}

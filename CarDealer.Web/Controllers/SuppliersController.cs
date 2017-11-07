namespace CarDealer.Web.Controllers
{
    using CarDealer.Services.Interfaces;
    using CarDealer.Web.Models.Suppliers;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    public class SuppliersController : Controller
    {
        private const string SuppliersView = "Suppliers";
        private readonly ISupplierService service;
        public SuppliersController(ISupplierService service)
        {
            this.service = service;
        }
        public IActionResult Local()
            => View(SuppliersView, GetSuppliersModel(false));

        public IActionResult Importers()
            => View(SuppliersView, GetSuppliersModel(true));

        private SuppliersModel GetSuppliersModel(bool importers)
        {
            var type = importers ? "Importeter" : "Local";
            var suppliers = this.service.All(importers);
            return new SuppliersModel()
            {
                Type = type,
                Suppliers = suppliers
            };
        }
    }
}
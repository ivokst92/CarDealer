namespace CarDealer.Web.Controllers
{
    using CarDealer.Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    [Route("sales")]
    public class SalesController : Controller
    {
        private readonly ISaleService service;
        public SalesController(ISaleService service)
        {
            this.service = service;
        }

        [Route("", Order =4)]
        public IActionResult All()
           => View(service.AllSales());

        [Route("{Id}", Order = 3)]
        public IActionResult SaleDetail(int Id)
          => View(service.SaleById(Id));

        [Route("discounted/{percent}", Order = 1)]
        public IActionResult Discounted(double percent)
           => View("All", service.AllSalesByDiscountPercentage(percent));

        [Route("discounted", Order = 2)]
        public IActionResult Discounted()
           => View("All", service.AllSalesWithDiscount());
    }
}
namespace CarDealer.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using CarDealer.Services.Interfaces;
    using CarDealer.Web.Models.Cars;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService service;
        public CarsController(ICarService service)
        {
            this.service = service;
        }

        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {
            var cars = this.service.ByMake(make);

            return View(new CarsByMakeModel()
            {
                Cars = cars,
                Make = make
            });
        }

        [Route("parts", Order = 1)]
        public IActionResult Parts()
            => View(this.service.WithParts());

        [Route("All")]
        public IActionResult All()
            => View(this.service.AllCars());
    }
}
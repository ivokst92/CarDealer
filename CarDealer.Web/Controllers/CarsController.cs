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
        private readonly ICarService serviceParts;
        public CarsController(ICarService service)
        {
            this.serviceParts = service;
        }

        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {
            var cars = this.serviceParts.ByMake(make);

            return View(new CarsByMakeModel()
            {
                Cars = cars,
                Make = make
            });
        }

        [Route(nameof(Create))]
        public IActionResult Create()
            => View();

        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carModel);
            }

            this.serviceParts.Create(carModel.Make,
                                     carModel.Model,
                                     carModel.TravelledDistance);

            return RedirectToAction(nameof(All));
        }

        [Route("parts", Order = 1)]
        public IActionResult Parts()
            => View(this.serviceParts.WithParts());

        [Route("All")]
        public IActionResult All()
            => View(this.serviceParts.AllCars());
    }
}
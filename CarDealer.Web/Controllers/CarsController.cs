namespace CarDealer.Web.Controllers
{
    using CarDealer.Services.Interfaces;
    using CarDealer.Web.Models.Cars;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [Route("cars")]
    public class CarsController : Controller
    {
        private readonly ICarService carService;
        private readonly IPartService partsService;

        public CarsController(ICarService carService, IPartService partsService)
        {
            this.carService = carService;
            this.partsService = partsService;
        }

        [Route("{make}", Order = 2)]
        public IActionResult ByMake(string make)
        {
            var cars = this.carService.ByMake(make);

            return View(new CarsByMakeModel()
            {
                Cars = cars,
                Make = make
            });
        }

        [Authorize]
        [Route(nameof(Create))]
        public IActionResult Create()
            => View(new CarFormModel
            {
                Parts = GetSelectListParts()
            });

        [Authorize]
        [HttpPost]
        [Route(nameof(Create))]
        public IActionResult Create(CarFormModel carModel)
        {
            if (!ModelState.IsValid)
            {
                carModel.Parts = GetSelectListParts();
                return View(carModel);
            }

            this.carService.Create(carModel.Make,
                                     carModel.Model,
                                     carModel.TravelledDistance,
                                     carModel.SelectedPats);

            return RedirectToAction(nameof(Parts));
        }

        [Route("parts", Order = 1)]
        public IActionResult Parts()
            => View(this.carService.WithParts());

        [Route("All")]
        public IActionResult All()
            => View(this.carService.AllCars());

        private IEnumerable<SelectListItem> GetSelectListParts()
        {
            return this.partsService
                 .All()
                 .Select(x =>
                 new SelectListItem
                 {
                     Text = x.Name,
                     Value = x.Id.ToString()
                 });
        }
    }
}
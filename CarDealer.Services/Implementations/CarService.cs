﻿namespace CarDealer.Services.Implementations
{
    using CarDealer.Data;
    using CarDealer.Services.Interfaces;
    using CarDealer.Services.Models;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Services.Models.Cars;

    public class CarService : ICarService
    {
        private readonly CarDealerDbContext db;
        public CarService(CarDealerDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<CarModel> AllCars()
        {
            return this.db.Cars.Select(x => new CarModel
            {
                Make = x.Make,
                Model = x.Model,
                TravelledDistance = x.TravelledDistance
            });
        }

        public IEnumerable<CarModel> ByMake(string make)
        {
            return this.db.Cars
                 .Where(x => x.Make.ToLower() == make.ToLower())
                 .OrderBy(x => x.Model)
                 .ThenBy(x => x.TravelledDistance)
                 .Select(x =>
                 new CarModel
                 {
                     Make = x.Make,
                     Model = x.Model,
                     TravelledDistance = x.TravelledDistance
                 })
                 .ToList();
        }

        public IEnumerable<CarWithPartsModel> WithParts()
            => this.db.Cars
            .Select(c => new CarWithPartsModel()
            {
                Make = c.Make,
                Model = c.Model,
                TravelledDistance = c.TravelledDistance,
                Parts = c.Parts.Select(p => new PartModel()
                {
                    Name = p.Part.Name,
                    Price = p.Part.Price.GetValueOrDefault(),
                })
            })
            .ToList();

    }
}

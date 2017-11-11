namespace CarDealer.Services.Interfaces
{
    using CarDealer.Services.Models;
    using CarDealer.Services.Models.Cars;
    using System.Collections.Generic;

    public interface ICarService
    {
        IEnumerable<CarModel> ByMake(string make);
        IEnumerable<CarModel> AllCars();
        IEnumerable<CarWithPartsModel> WithParts();
        void Create(string make, string model, long travelledDistance, IEnumerable<int> parts);
    }
}

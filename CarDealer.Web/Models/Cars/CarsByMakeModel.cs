using CarDealer.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealer.Web.Models.Cars
{
    public class CarsByMakeModel
    {
        public string Make { get; set; }
        public IEnumerable<CarModel> Cars { get; set; }
    }
}

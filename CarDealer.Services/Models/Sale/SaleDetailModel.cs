using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.Services.Models
{
    public class SaleDetailModel : SaleDetailBaseModel
    {
        public double Price { get; set; }
        public double PriceWithDiscount { get; set; }
        public double DiscountPercentage { get; set; }

    }
}

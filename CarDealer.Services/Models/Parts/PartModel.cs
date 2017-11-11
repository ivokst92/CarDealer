namespace CarDealer.Services.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class PartModel : PartBaseModel
    {

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public string Supplier { get; set; }

    }
}

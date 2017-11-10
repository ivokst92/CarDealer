namespace CarDealer.Services.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class PartDetailsModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public int SupplierId { get; set; }
    }
}

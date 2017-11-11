namespace CarDealer.Services.Models.Parts
{
    using System.ComponentModel.DataAnnotations;

    public class PartBaseModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}

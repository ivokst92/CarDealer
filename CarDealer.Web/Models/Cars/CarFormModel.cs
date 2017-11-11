namespace CarDealer.Web.Models.Cars
{
    using CarDealer.Services.Models.Parts;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CarFormModel
    {
        [Required]
        [MaxLength(50)]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Display(Name = "Travelled Distance")]
        [Range(0, long.MaxValue, ErrorMessage = "{0} must be a positive number")]
        public long TravelledDistance { get; set; }
        
        public IEnumerable<int> SelectedPats { get; set; }

        [Display(Name ="Parts")]
        public IEnumerable<SelectListItem> Parts { get; set; }
    }
}

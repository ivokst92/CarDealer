using CarDealer.Services.Models.Supplier;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarDealer.Web.Models.Parts
{
    public class PartFormModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must me positive number")]
        public double Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price must me positive number")]
        public int Quantity { get; set; }

        [Display(Name = "Supplier")]
        public int SupplierId { get; set; }

        public IEnumerable<SelectListItem> Suppliers { get; set; }

        public bool IsEdit { get; set; }
    }
}

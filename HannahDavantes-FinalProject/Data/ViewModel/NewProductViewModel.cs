using HannahDavantes_FinalProject.Data.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.ViewModel {
    public class NewProductViewModel {
        [Display(Name = "Name")]
        [StringLength(100, ErrorMessage = "Too many character")]
        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }

        [Display(Name = "Brand Name")]
        [StringLength(50, ErrorMessage = "Too many characters")]
        [Required(ErrorMessage = "Product brand name is required")]
        [DataType(DataType.MultilineText)]
        public string Brand { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Product category is required")]
        public ProductCategory Category { get; set; }

        [Display(Name = "Size")]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Size should be greater than 0")]
        public double SizeNumber { get; set; }

        [Required(ErrorMessage = "Size unit is required")]
        public ProductSizeUnit SizeUnit { get; set; }

        [Display(Name = "Photo")]
        [DataType(DataType.Upload)]
        public IFormFile PhotoFile { get; set; }

        [Display(Name = "Descripion")]
        [Required(ErrorMessage = "Product description is required")]
        [StringLength(300, ErrorMessage = "Too many characters")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        [Range(double.Epsilon, double.MaxValue, ErrorMessage = "Size should be greater than 0")]
        public double Price { get; set; }
    }
}

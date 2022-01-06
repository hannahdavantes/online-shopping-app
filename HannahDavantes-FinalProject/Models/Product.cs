using HannahDavantes_FinalProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Models {
    public class Product {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public ProductCategory Category { get; set; }
        public int SizeNumber { get; set; }
        public ProductSizeUnit SizeUnit { get; set; }
        public string Photo { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

    }
}

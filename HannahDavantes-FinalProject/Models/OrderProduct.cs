using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

//Bridging table between Order and Product since they have n:m relationship
namespace HannahDavantes_FinalProject.Models {
    public class OrderProduct {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }



        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }


        public int OrderId { get; set; }
        [ForeignKey("ProductId")]
        public Order Order { get; set; }
    }
}

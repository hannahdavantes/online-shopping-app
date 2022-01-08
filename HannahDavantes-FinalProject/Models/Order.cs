using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Models {
    public class Order {
        [Key]
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string LoggedInUserId { get; set; }

        //n:m relationsip
        public List<OrderProduct> OrderProducts { get; set; }
    }
}

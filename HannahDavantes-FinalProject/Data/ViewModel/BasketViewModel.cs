using HannahDavantes_FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Data.ViewModel {

    /// <summary>
    /// This controls data to display on Basket
    /// </summary>
    public class BasketViewModel {
        public Basket Basket { get; set; }
        public double BasketTotalPrice { get; set; }
    }
}

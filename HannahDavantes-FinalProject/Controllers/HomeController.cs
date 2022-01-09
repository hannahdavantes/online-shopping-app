using HannahDavantes_FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HannahDavantes_FinalProject.Controllers {
    /// <summary>
    /// This class represents the controller that returns the home page
    /// </summary>
    public class HomeController : Controller {
        /// <summary>
        /// This method will return the Home page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index() {
            return View();
        }
    }
}

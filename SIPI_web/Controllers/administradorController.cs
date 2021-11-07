using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIPI_web.Controllers
{
    public class administradorController : Controller
    {
        public IActionResult homeAdministrador()
        {
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SAP.Controllers.Admin
{
    [Authorize]
    [Route("admin")]
    public class AdministrationController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
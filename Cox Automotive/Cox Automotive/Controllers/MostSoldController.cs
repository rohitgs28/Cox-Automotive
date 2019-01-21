using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cox_Automotive.Data;
using Cox_Automotive.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cox_Automotive.Controllers
{
    public class MostSoldController : Controller
    {

        private CalculateMostSold _calculatemostsold { get; set; }

        public MostSoldController(CalculateMostSold calculatemostsold)
        {
            _calculatemostsold = calculatemostsold;
        }
        public IActionResult MostSoldCar()
        {
            var records = new ListOfRecords();
            records.listofRecords =  _calculatemostsold.FetchmostSold();
            return View(records);
        }
    }
}
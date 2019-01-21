using System;
using System.Web;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cox_Automotive.Models;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Http;
using Cox_Automotive.Extensions;
using Cox_Automotive.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Net.Http.Headers;

namespace Cox_Automotive.Controllers
{
    public class ViewController : Controller
    {
        private IHostingEnvironment hostingEnvironment;
        private CoxData _coxdata { get; set; }

            
        public ViewController(IHostingEnvironment hostingEnvironment, CoxData coxData)
        {
            this.hostingEnvironment = hostingEnvironment;
            _coxdata = coxData;
        }

        public IActionResult Index()
        {
            return View();
        }
        public Microsoft.AspNetCore.Mvc.JsonResult FetchRecords(string myfile)
        {

           // string filename = file.FileName;
            var result = new List<Csvfile>();
            result = _coxdata.FetchRecords(myfile);
            return Json(new { data = result });


        }
    }
}
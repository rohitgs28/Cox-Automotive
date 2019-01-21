// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Cox_Automotive.Data;
using Cox_Automotive.Extensions;
using Cox_Automotive.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace AspNetCoreFileUploading
{
  public class UploaderController : Controller
  {
    private IHostingEnvironment hostingEnvironment;
    private CoxData _coxdata { get; set; }

        public UploaderController(IHostingEnvironment hostingEnvironment, CoxData coxData)
         {
            this.hostingEnvironment = hostingEnvironment;
            _coxdata = coxData;
        }


    public IActionResult Index()
        {
            return View();

        }
    [HttpPost]
    public async Task<IActionResult> Index(IList<IFormFile> files)
    {
      foreach (IFormFile source in files)
      {
        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

        filename = this.EnsureCorrectFilename(filename);

                var path = Path.Combine(
                                      Directory.GetCurrentDirectory(), "wwwroot\\uploads\\",
                                      source.FileName);
                using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
                    await source.CopyToAsync(output);


                DataTable dt = ReadFile.ReadCsvFile(path.ToString(), filename);
                ListOfRecords MyListOfRecords = new ListOfRecords();
                MyListOfRecords.listofRecords = GetListByDataTable(dt);
                _coxdata.InsertIntoDb(dt);

            }
            Cox_Automotive.Models.JsonResult result = new Cox_Automotive.Models.JsonResult();
            result.status = true;
            result.message = "File uploaded successfully";
            return Json(result);
           
    }
        private List<Csvfile> GetListByDataTable(DataTable dt)
        {
            try {
                List<Csvfile> ListOfRecords = new List<Csvfile>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Csvfile rowRecord = new Csvfile();

                    rowRecord.GivenFilename = dt.Rows[i]["GivenFilename"].ToString();
                    rowRecord.DealNumber = Convert.ToInt64(dt.Rows[i]["DealNumber"]);
                    rowRecord.CustomerName = dt.Rows[i]["CustomerName"].ToString();
                    rowRecord.DealershipName = dt.Rows[i]["DealershipName"].ToString();
                    rowRecord.Vehicle = dt.Rows[i]["Vehicle"].ToString();
                    rowRecord.Price = Convert.ToDecimal(dt.Rows[i]["Price"]);
                    rowRecord.Date = Convert.ToDateTime(dt.Rows[i]["Date"]);


                    ListOfRecords.Add(rowRecord);
                }
                return ListOfRecords;
            }

            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains("\\"))
        filename = filename.Substring(filename.LastIndexOf("\\") + 1);

      return filename;
    }

    private string GetPathAndFilename(string filename)
    {
      string path = this.hostingEnvironment.WebRootPath + "\\uploads\\";

      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);

      return path + filename;
    }
  }
}
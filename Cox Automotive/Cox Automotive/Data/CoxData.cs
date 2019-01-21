using Cox_Automotive.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace Cox_Automotive.Data
{
    public class CoxData:DataBase
{
        public CoxData(IConfiguration configuration) : base(configuration)
        {
        }

        public void InsertIntoDb(DataTable CsvDT)
        {
            try { 
            if (CsvDT != null)
            {
                //creating object of SqlBulkCopy  
                SqlBulkCopy objbulk = new SqlBulkCopy(DBConnection);
                objbulk.DestinationTableName = "tbl_DealerTrack";
                objbulk.ColumnMappings.Add("GivenFilename", "GivenFilename");
                objbulk.ColumnMappings.Add("CustomerName", "CustomerName");
                objbulk.ColumnMappings.Add("DealNumber", "DealNumber");
                objbulk.ColumnMappings.Add("DealershipName", "DealershipName");
                objbulk.ColumnMappings.Add("Vehicle", "Vehicle");
                objbulk.ColumnMappings.Add("Price", "Price");
                objbulk.ColumnMappings.Add("Date", "Date_Added");
                //inserting bulk Records into DataBase   
                objbulk.WriteToServer(CsvDT);
            }
            }
            catch(Exception ex)
            {

            }
        }

        public List<Csvfile> FetchRecords(string filename)
        {
            try { 
            var results = new List<Csvfile>();
                using (var conn = new SqlConnection(DBConnection))
                { 
                    SqlCommand command = new SqlCommand("FetchRecords", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    conn.Open();
                    SqlParameter myfilename = command.Parameters.AddWithValue("@myfilename", filename);
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        Csvfile Csvfile = new Csvfile();
                        Csvfile.GivenFilename = dr["GivenFilename"].ToString();
                        Csvfile.DealNumber = Convert.ToInt32(dr["DealNumber"]);
                        Csvfile.CustomerName = dr["CustomerName"].ToString();
                        Csvfile.DealershipName = dr["DealershipName"].ToString();
                        Csvfile.Vehicle = dr["Vehicle"].ToString();
                        Csvfile.Price = Convert.ToDecimal(dr["Price"]);
                        Csvfile.Date = Convert.ToDateTime(dr["Date_Added"]);
                        results.Add(Csvfile);
                    }
                    conn.Close();

                }
            return results;
            }
            catch(Exception ex)
            {
                throw (ex);
            }
        }

    }
}

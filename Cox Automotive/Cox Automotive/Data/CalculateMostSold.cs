using Cox_Automotive.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Cox_Automotive.Data
{
    public class CalculateMostSold :DataBase
        {



        public CalculateMostSold(IConfiguration configuration) : base(configuration)
        {
            
        }
        public List<Csvfile> FetchmostSold()
        {
            try
            {
                var results = new List<Csvfile>();
                using (var conn = new SqlConnection(DBConnection))
                {
                    SqlCommand command = new SqlCommand("CalculateMostSold", conn);
                    command.CommandType = CommandType.StoredProcedure;
                    DataTable dt = new DataTable();
                    conn.Open();
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

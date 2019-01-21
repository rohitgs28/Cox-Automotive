using Cox_Automotive.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Cox_Automotive_Test
{
    public static class HealperClass
    {

       

        public static bool AreTablesTheSame(DataTable tbl1, DataTable tbl2)
        {
            if (tbl1.Rows.Count != tbl2.Rows.Count || tbl1.Columns.Count != tbl2.Columns.Count)
                return false;


            for (int i = 0; i < tbl1.Rows.Count; i++)
            {
                for (int c = 0; c < tbl1.Columns.Count; c++)
                {
                    if (!Equals(tbl1.Rows[i][c], tbl2.Rows[i][c]))
                        return false;
                }
            }
            return true;
        }
        public static DataTable GenerateCsvDatattable()
        {
            DataTable dtCsv = new DataTable();
            dtCsv.Columns.Add("GivenFileName");
            dtCsv.Columns.Add("DealNumber");
            dtCsv.Columns.Add("CustomerName");
            dtCsv.Columns.Add("DealershipName");
            dtCsv.Columns.Add("Vehicle");
            dtCsv.Columns.Add("Price");
            dtCsv.Columns.Add("Date");
            DataRow dr = dtCsv.NewRow();
            dr[0] = "test.csv";
            dr[1] = "5469";
            dr[2] = "Milli Fulton";
            dr[3] = "Sun of Saskatoon";
            dr[4] = "2017 Ferrari 488 Spider";
            dr[5] = "429987";
            dr[6] = "6/19/2018";
            dtCsv.Rows.Add(dr);
            return dtCsv;
        }
       



        
    }
}

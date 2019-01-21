using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cox_Automotive.Extensions
{
    public class ReadFile
    {
       
        public static DataTable ReadCsvFile(string FileSaveWithPath, string filename)
        {
            try
            {
                DataTable dtCsv = new DataTable();
                string Fulltext;


                using (StreamReader sr = new StreamReader(FileSaveWithPath))
                {
                    while (!sr.EndOfStream)
                    {
                        Fulltext = sr.ReadToEnd().ToString();
                        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                        for (int i = 0; i < rows.Count() - 1; i++)
                        {
                            rows[i] = rows[i].Replace("\r", "");
                            if (i != 0)
                            {


                                //string mystring = rows[i].Substring(j);
                                MatchCollection matchList = Regex.Matches(rows[i], "\".*?\"");
                                var list = matchList.Cast<Match>().Select(match => match.Value).ToList();
                                foreach (var item in list)
                                {

                                    string NewValue = item.Replace(",", "");
                                    NewValue = NewValue.Replace("\"", "");
                                    rows[i] = rows[i].Replace(item, NewValue);

                                }
                            }
                            string[] rowValues = rows[i].Split(','); //split each row with comma to get individual values  
                            {
                                if (i == 0)
                                {
                                    for (int j = 0; j < rowValues.Count(); j++)
                                    {
                                        if (j == 0)
                                        {
                                            dtCsv.Columns.Add("GivenFileName");
                                            dtCsv.Columns.Add(rowValues[j]);
                                        }
                                        else
                                        {
                                            dtCsv.Columns.Add(rowValues[j]); //add headers  }
                                        }
                                    }
                                }
                                else
                                {

                                    DataRow dr = dtCsv.NewRow();
                                    for (int k = 0; k < rowValues.Count(); k++)
                                    {
                                        if (k == 0)
                                        {
                                            dr[k] = filename;
                                            dr[k + 1] = rowValues[k].ToString();
                                        }
                                        else { 
                                         dr[k+1] = rowValues[k].ToString();
                                        }
                                    }
                                    dtCsv.Rows.Add(dr); //add other rows  
                                }
                            }
                        }
                    }
                }

                return dtCsv;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
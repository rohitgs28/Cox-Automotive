using Cox_Automotive.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Cox_Automotive_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitTest1();
            
         
        }

        private static void UnitTest1()
        {
            DataTable dtcsv = HealperClass.GenerateCsvDatattable();
            string projectDirectory = Environment.CurrentDirectory;
            projectDirectory = projectDirectory.Replace("bin\\Debug\\netcoreapp2.1", "");
            DataTable dto = MethodsToTest.ReadCsvFile(projectDirectory + "test.csv", "test.csv");
            bool value = HealperClass.AreTablesTheSame(dtcsv, dto);

            if (value == true)
            {
                Console.WriteLine("Unit test 1 passed");
                Console.ReadKey();
            }
        }
    }
}

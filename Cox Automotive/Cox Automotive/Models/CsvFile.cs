using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cox_Automotive.Models
{
    public class Csvfile
{
        public string GivenFilename { get; set; }
        public Int64 DealNumber { get; set; }
        public string CustomerName { get; set; }
        public string DealershipName { get; set; }
        public string Vehicle { get; set; }
        public Decimal Price { get; set; }
        public DateTime Date { get; set; }
    }
}

using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cox_Automotive.Data
{
    public abstract class DataBase
{
        IConfiguration _config;
        public DataBase(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string DBConnection
        {
            get
            {
                return _config["ConnectionStrings:DefaultConnection"];
            }
        }
    }
}

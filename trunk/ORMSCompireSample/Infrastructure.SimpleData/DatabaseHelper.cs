using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Simple.Data;
using Simple.Data.Ado;
using Simple.Data.SqlServer;
namespace Infrastructure.SimpleData
{
    static class DatabaseHelper
    {
        public static dynamic Open()
        {
            var con = ConfigurationManager.ConnectionStrings["SDSample"].ConnectionString;
            return Simple.Data.Database.Opener.OpenConnection(ConfigurationManager.ConnectionStrings["SDSample"].ConnectionString);
        }

        public static void Reset()
        {
            
        }
    }
}

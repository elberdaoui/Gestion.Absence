using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace gestionAbs
{
    class Queries
    {
        public SqlConnection cnx = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();
        public SqlDataReader dtr;
        public DataTable dt = new DataTable();

        public void Connect()
        {
            if(cnx.State == ConnectionState.Closed || cnx.State == ConnectionState.Broken)
            {
                cnx.ConnectionString = "Data Source=DESKTOP-0PAGHK2;Initial Catalog=absGestion;Integrated Security=True";
                cnx.Open();
            }
        }

        public void Disconnect()
        {
            if(cnx.State == ConnectionState.Open)
            {
                cnx.Close();
            }
        }
    }
}

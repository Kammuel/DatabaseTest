using System;
using System.Collections.Generic;
//make sure we are 'using' the sql client
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseTest
{
    /// <summary>
    /// Contains helper methods for working with the database
    /// </summary>
    /// //no reason to make this an instance class, so static?
    static class VendorDB
    {
        //create a way to connet to the database itself
        //SqlConnection is the return type
        private static SqlConnection GetConnection()
        {
            var con = new SqlConnection();
            //we are going to have to set the connection string
            //the '@' will ignore all backslashes/escape sequences
            //Data source is our server name
            //Initial catalog is our database
            //Integrated security - means use windows authentication for security
            //for the last, could use username = "" password = ""
            //never trust your employees/coworkers.  EVER.
            con.ConnectionString = @"Data Source=localhost;Initial Catalog=AP;Integrated Security=True";
            //it is empty because we do not know the connection to the database yet
            return con;

            //^could also be written as:
            //var con = new Sqlconnection(@"Data Source=localhost;Initial Catalog=AP;Integrated Security=True");
            //return con;

            //even shorter:
            //return new Sqlconnection(@"Data Source=localhost;Initial Catalog=AP;Integrated Security=True");
        }

        public static List<Vendor> GetAllVendors()
        {
            //to get rid of red squiggly^
            //for if we forget to come back here
            //do not use placeholder return values

            //1) get a connection to the database
            SqlConnection con = GetConnection();

            //2) prepare SQL command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //the @ will allow us to query on multiple lines
            cmd.CommandText = @"SELECT VendorID, VendorName, VendorCity, VendorState FROM Vendors";

            //3) open connection
            //can't interact with a closed connection
            con.Open();//nice and easy, yeah?

            //4) execute query
            //wooooooooooooooooooooooooooooooo
            SqlDataReader reader = cmd.ExecuteReader();

            //we are going to add the vendornames to our list
            var list = new List<Vendor>();
            while(reader.Read())         //like a hasNext(), iterating through each row
            {
                Vendor tempVendor = new Vendor();

                //must cast these objects to a certain data type
                tempVendor.VendorId = (int)reader["VendorID"];
                tempVendor.Name = (string)reader["VendorName"];
                tempVendor.City = (string)reader["VendorCity"];
                tempVendor.State = (string)reader["VendorCity"];

                list.Add(tempVendor);
            }

            //close connection
            con.Close();
            //or con.Dispose();

            return list;
        }
    }
}

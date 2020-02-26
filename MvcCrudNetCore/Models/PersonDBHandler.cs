using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MvcCrudNetCore.Models
{
    public class PersonDBHandler
    {
        private SqlConnection con;
        private IConfiguration _configuration;
        public PersonDBHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private void connection()
        {
            string constring = _configuration.GetConnectionString("PeopleDBConn");
            con = new SqlConnection(constring);
        }
        public bool InsertPerson(PersonModel iList)
        {
            connection();
            string query = "INSERT INTO Person VALUES('" + iList.Name + "','" + iList.Address + "','" + iList.Phone + "','" + iList.Email + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<PersonModel> GetPersonList()
        {
            connection();
            List<PersonModel> iList = new List<PersonModel>();

            string query = "SELECT * FROM Person";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            adapter.Fill(dt);
            con.Close();

            foreach (DataRow dr in dt.Rows)
            {
                iList.Add(new PersonModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    Name = Convert.ToString(dr["Name"]),
                    Address = Convert.ToString(dr["Address"]),
                    Phone = Convert.ToString(dr["Phone"]),
                    Email = Convert.ToString(dr["Email"])
                });
            }
            return iList;
        }

        public bool UpdatePerson(PersonModel iList)
        {
            connection();
            string query = "UPDATE Person SET Name = '" + iList.Name + "', Address = '" + iList.Address + "', Phone = '" + iList.Phone + "', Email = '" + iList.Email + "' WHERE ID = " + iList.ID;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeletePerson(int id)
        {
            connection();
            string query = "DELETE FROM Person WHERE ID = " + id;
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}

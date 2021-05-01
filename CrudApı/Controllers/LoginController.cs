using CrudApı.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CrudApı.Controllers
{
    public class LoginController : ApiController
    { //
        SqlConnection conn;
        private void SqlConn()
        {
            string conString = ConfigurationManager.ConnectionStrings["DBCS"].ToString();
            conn = new SqlConnection(conString);
        }

       
        //
        [HttpGet]
        public UsersTable UseLogin(LoginTable login)
        {
            // String pass = "a2135";
            //String email ="x@gmail.com";
            SqlConn();
            conn.Open();
             Response response = new Response();
            SqlCommand cmd = new SqlCommand("Select * From UsersTable Where Email=@UEmail And Password=@UPass ", conn);
            cmd.Parameters.AddWithValue("@UEmail", login.Email);
            cmd.Parameters.AddWithValue("@UPass",login.Password);
            
            SqlDataReader rdr = cmd.ExecuteReader();
            UsersTable NewUser = new UsersTable();
            if (rdr.Read())
            {
                
                NewUser.Id = Convert.ToInt32(rdr["Id"]);
                NewUser.Name = rdr["Name"].ToString();
                NewUser.Surname = rdr["Surname"].ToString();
                NewUser.Address = rdr["Address"].ToString();
                NewUser.Email = rdr["Email"].ToString();
                NewUser.Mobile = rdr["Mobile"].ToString();
                NewUser.Password = rdr["Password"].ToString();

                return NewUser;
                conn.Close();
            }
            else
            {
                
            }
            conn.Close();
            return NewUser;

        }



    }
}

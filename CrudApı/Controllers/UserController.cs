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
    public class UserController : ApiController
    {
        SqlConnection conn;
        private void SqlConn()
        {
            string conString = ConfigurationManager.ConnectionStrings["DBCS"].ToString();
            conn = new SqlConnection(conString);
        }
      
        public IEnumerable<UsersTable> GetUsersList()
        {
            List<UsersTable> UserData = new List<UsersTable>();
            SqlConn();
            SqlCommand cmd = new SqlCommand("Select * From UsersTable", conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                UsersTable user = new UsersTable();
                user.Id = Convert.ToInt32(rdr["Id"]);
                user.Name = rdr["Name"].ToString();
                user.Surname = rdr["Surname"].ToString();
                user.Email = rdr["Email"].ToString();
                user.Mobile = rdr["Mobile"].ToString();
                user.Address = rdr["Address"].ToString();
                user.Password = rdr["Password"].ToString();
                UserData.Add(user);
            }
            conn.Close();
            return UserData;

        }
       
        [HttpGet]
        public List<UsersTable> GetUser(int EmpID)
        {


            List<UsersTable> UserData = new List<UsersTable>();

            SqlConn();
            SqlCommand cmd = new SqlCommand("Select * From UsersTable Where Id=@UId ", conn);
            cmd.Parameters.AddWithValue("@UId", EmpID);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                UsersTable NewUser = new UsersTable();
                NewUser.Id = Convert.ToInt32(rdr["Id"]);
                NewUser.Name = rdr["Name"].ToString();
                NewUser.Surname = rdr["Surname"].ToString();
                NewUser.Address = rdr["Address"].ToString();
                NewUser.Email = rdr["Email"].ToString();
                NewUser.Mobile = rdr["Mobile"].ToString();
                NewUser.Password = rdr["Password"].ToString();
                UserData.Add(NewUser);
            }
            else
            {

            }
            conn.Close();
            return UserData;

        }



        [HttpPost]
        public Response SaveUser(UsersTable user)
        {
            SqlConn();


            Response response = new Response();
            try
            {
                conn.Open();

                SqlCommand cmdR = new SqlCommand("Select * From UsersTable Where Mobile='" + user.Mobile.ToString() + "'", conn);
                SqlDataReader rdr = cmdR.ExecuteReader();
                if (rdr.Read())
                {
                    conn.Close();
                    response.Message = "Customer Mobile No. Already Exist!";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Name))
                {
                    response.Message = "Customer Name is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Address))
                {
                    response.Message = "Customer Address is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Email))
                {
                    response.Message = "Customer Email is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Mobile))
                {
                    response.Message = "Customer Mobile No is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    response.Message = "Password is Required";
                    response.Status = 0;
                }
                else
                {
                    conn.Close();
                    conn.Open();
                    string sSQL = "insert into UsersTable (Name,Surname,Address,Email,Mobile,Password)values(@Name,@Surname,@Address,@Email,@Mobile,@Password)";
                    var cmd = new SqlCommand(sSQL, conn);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Surname", user.Surname);
                    cmd.Parameters.AddWithValue("@Address", user.Address);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    int temp = 0;

                    temp = cmd.ExecuteNonQuery();
                    if (temp > 0)
                    {
                        conn.Close();
                        response.Message = "Customer Registration Successfully.";
                        response.Status = 1;

                    }
                    else
                    {
                        conn.Close();
                        response.Message = "Customer Registration Failed.";
                        response.Status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                response.Message = ex.Message;
                response.Status = 0;
            }
            finally
            {
                conn.Close();
            }
            return response;
        }


        [HttpPut]
        public Response UpdateUser(UsersTable user)
        {
            SqlConn();
            conn.Open();

            Response response = new Response();
            try
            {
                if (string.IsNullOrEmpty(user.Name))
                {
                    response.Message = "Customer Name is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Surname))
                {
                    response.Message = "Customer Surname is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Address))
                {
                    response.Message = "Customer Address is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Email))
                {
                    response.Message = "Customer Email is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Mobile))
                {
                    response.Message = "Customer Mobile No is Required";
                    response.Status = 0;
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    response.Message = "Password is Required";
                    response.Status = 0;
                }
                else
                {
                    string sSQL = "Update UsersTable set Name=@Name,Surname=@Surname,Address=@Address,Email=@Email,Password=@Password Where Mobile=@Mobile";
                    var cmd = new SqlCommand(sSQL, conn);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Surname", user.Surname);
                    cmd.Parameters.AddWithValue("@Address", user.Address);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Mobile", user.Mobile);
                    int temp = 0;

                    temp = cmd.ExecuteNonQuery();
                    if (temp > 0)
                    {
                        conn.Close();
                        response.Message = "Record is Updated Successfully.";
                        response.Status = 1;

                    }
                    else
                    {
                        conn.Close();
                        response.Message = "Record Updation Failed ";
                        response.Status = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                response.Message = ex.Message;
                response.Status = 0;
            }
            finally
            {
                conn.Close();
            }
            return response;
        }


        [HttpDelete]
        public Response DeleteUser(int EmpID)
        {
            SqlConn();
            conn.Open();

            Response response = new Response();
            try
            {
                string sSQL = "Delete from UsersTable Where Id=@Id";
                var cmd = new SqlCommand(sSQL, conn);
                cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(EmpID));
                int temp = 0;

                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                {
                    conn.Close();
                    response.Message = "Record is Deleted Successfully.";
                    response.Status = 1;

                }
                else
                {
                    conn.Close();
                    response.Message = "Record Delition Failed ";
                    response.Status = 0;
                }

            }
            catch (Exception ex)
            {
                conn.Close();
                response.Message = ex.Message;
                response.Status = 0;
            }
            finally
            {
                conn.Close();
            }
            return response;
        }


        
        [HttpPost]
        public Response UseLogin(LoginTable login)
        {
            //    String pass = "a213";
            // String email ="veli@gmail.com";
            /*           SqlConn();
                       conn.Open();
                       Response response = new Response();
                       SqlCommand cmd = new SqlCommand("Select Id From UsersTable Where Email=@UEmail And Password=@UPass", conn);
                       cmd.Parameters.AddWithValue("@UEmail", login.Email);
                       cmd.Parameters.AddWithValue("@UPass", login.Password);
            */
            //    SqlDataReader rdr = cmd.ExecuteReader();

            //    UsersTable NewUser = new UsersTable();
            // if (rdr.Read())
            //{
            //   rdr.Read();
            //    NewUser.Id = Convert.ToInt32(rdr["Id"]);
            //  NewUser.Name = rdr["Name"].ToString();
            // NewUser.Surname = rdr["Surname"].ToString();
            // NewUser.Address = rdr["Address"].ToString();
            // NewUser.Email = rdr["Email"].ToString();v
            // NewUser.Mobile = rdr["Mobile"].ToString();
            // NewUser.Password = rdr["Password"].ToString();
            //  rdr.Close(); 
            // return NewUser;
            //  conn.Close();
            //  }
            Response response = new Response();

            try
            {
                SqlConn();
                conn.Open();

                SqlCommand cmd = new SqlCommand("Select Id From UsersTable Where Email=@UEmail And Password=@UPass", conn);
                cmd.Parameters.AddWithValue("@UEmail", login.Email);
                cmd.Parameters.AddWithValue("@UPass", login.Password);
                SqlDataReader rdr = cmd.ExecuteReader();

                rdr.Read();
                response.Id = Convert.ToInt32(rdr["Id"]);
                rdr.Close();

                conn.Close();
                response.Message = "Başarıyla Giriş Yapıldı";
                response.Status = 1;
                return response;
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
                response.Status = 0;
                return response;
            }

        }

        //

        [HttpGet]
        public UsersTable userget(int EmpID)
        {
            UsersTable NewUser = new UsersTable();
            try
            { 
            SqlConn();
            SqlCommand cmd = new SqlCommand("Select * From UsersTable Where Id=@UId ", conn);
            cmd.Parameters.AddWithValue("@UId", EmpID);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
             
                NewUser.Id = Convert.ToInt32(rdr["Id"]);
                NewUser.Name = rdr["Name"].ToString();
                NewUser.Surname = rdr["Surname"].ToString();
                NewUser.Address = rdr["Address"].ToString();
                NewUser.Email = rdr["Email"].ToString();
                NewUser.Mobile = rdr["Mobile"].ToString();
                NewUser.Password = rdr["Password"].ToString();
            rdr.Close();
           
            conn.Close();
            return NewUser;
            }
            catch
            {
                return NewUser;
            }

        }





















    }

}

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
    public class WorkController : ApiController
    {
        SqlConnection conn;
        private void SqlConn()
        {
            string conString = ConfigurationManager.ConnectionStrings["DBCS"].ToString();
            conn = new SqlConnection(conString);
        }

        [HttpPost]
        public Response SaveWork(WorkTable work)
        {
            SqlConn();

            Response response = new Response();
            try
            {

                conn.Open();
                string sSQL = "insert into Works (UserId,Tittle,Explanation,Category,Hıw,EducationStatus,Experience,CompanyName,CompanyPhone,Address,AddressLongitude,AddressLatitude,InfoEmployer)values(@UserId,@Tittle,@Explanation,@Category,@Hıw,@EducationStatus,@Experience,@CompanyName,@CompanyPhone,@Address,@AddressLongitude,@AddressLatitude,@InfoEmployer)";
                var cmd = new SqlCommand(sSQL, conn);
                cmd.Parameters.AddWithValue("@UserId", work.Id);
                cmd.Parameters.AddWithValue("@Tittle", work.Tittle);
                cmd.Parameters.AddWithValue("@Explanation", work.Explanation);
                cmd.Parameters.AddWithValue("@Category", work.Category);
                cmd.Parameters.AddWithValue("@Hıw", work.Hıw);
                cmd.Parameters.AddWithValue("@EducationStatus", work.EducationStatus);
                cmd.Parameters.AddWithValue("@Experience", work.Experience);
                cmd.Parameters.AddWithValue("@CompanyName", work.CompanyName);
                cmd.Parameters.AddWithValue("@CompanyPhone", work.CompanyPhone);
                cmd.Parameters.AddWithValue("@Address", work.Address);
                cmd.Parameters.AddWithValue("@AddressLongitude", work.AddressLongitude);
                cmd.Parameters.AddWithValue("@AddressLatitude", work.AddressLatitude);
                cmd.Parameters.AddWithValue("@InfoEmployer", work.InfoEmployer);

                int temp = 0;

                temp = cmd.ExecuteNonQuery();
                if (temp > 0)
                {
                    conn.Close();
                    response.Message = "Work Added Successfully.";
                    response.Status = 1;

                }
                else
                {
                    conn.Close();
                    response.Message = "Works Add Failed.";
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

        [HttpGet]
        public string deneme()
        {
            string a = "çalışıyooooo";
            return a;


        }

        [HttpGet]
        public IEnumerable<WorkTable> GetWorkList()
        {
            List<WorkTable> WorkData = new List<WorkTable>();
            SqlConn();
            SqlCommand cmd = new SqlCommand("Select * From Works", conn);
            conn.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                WorkTable work = new WorkTable();
                work.Id = Convert.ToInt32(rdr["UserId"]);
                work.Tittle= rdr["Tittle"].ToString();
                work.Explanation = rdr["Explanation"].ToString();
                work.Category = rdr["Category"].ToString();
                work.Hıw = rdr["Hıw"].ToString();
                work.EducationStatus = rdr["EducationStatus"].ToString();
                work.Experience = rdr["Experience"].ToString();
                work.CompanyName = rdr["CompanyName"].ToString();
                work.Address = rdr["Address"].ToString();
                work.AddressLongitude = Convert.ToDouble(rdr["AddressLongitude"]);
                work.AddressLatitude = Convert.ToDouble(rdr["AddressLatitude"]);
                work.CompanyPhone = rdr["CompanyPhone"].ToString();
                work.InfoEmployer = rdr["InfoEmployer"].ToString();
                WorkData.Add(work);
            }
            conn.Close();
            return WorkData;

        }


    }
}

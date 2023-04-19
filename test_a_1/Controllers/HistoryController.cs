using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;

namespace test_a_1.Controllers
{
    public class HistoryController : ApiController
    {
        [HttpGet]
        [Route("api/history/gethistory")]
        public IHttpActionResult GetHistory()
        {
            List<History> historyList = new List<History>();

            //calling java API to get history
            string apiURL = "http://example.com/api/history";
            using (WebClient webClient = new WebClient())
            {
                var response = webClient.DownloadString(apiURL);
                historyList = JsonConvert.DeserializeObject<List<History>>(response);
            }

            //Fetching request information from DB
            string connectionString = "Data Source=ServerName;Initial Catalog=DBName;User Id=UserName;Password=PassWord;";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Requests", con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    historyList.Add(new History
                    {
                        RequestId = reader["RequestId"].ToString(),
                        RequestName = reader["RequestName"].ToString(),
                        RequestStatus = reader["RequestStatus"].ToString()
                    });
                }
            }

            return Ok(historyList);
        }
    }

    public class History
    {
        public string RequestId { get; set; }
        public string RequestName { get; set; }
        public string RequestStatus { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using test_a_1;

namespace test_a_1.Controllers
{
    public class HistoryController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage GetHistory()
        {
            // Fetch data from java API
            var javaApiResponse = GetDataFromJavaAPI();

            // Fetch data from DB
            var historyData = GetDataFromDB();

            // Merge both result
            historyData = MergeResult(javaApiResponse, historyData);

            return Request.CreateResponse(HttpStatusCode.OK, historyData);
        }

        public DataTable GetDataFromJavaAPI()
        {
            var javaApiDataTable = new DataTable();
            // Logic to fetch data from java API
            return javaApiDataTable;
        }

        public DataTable GetDataFromDB()
        {
            var dbDataTable = new DataTable();
            string connectionString = "your_db_connection_string";

            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "select * from history";
                SqlDataAdapter adapter = new SqlDataAdapter(query, cnn);
                adapter.Fill(dbDataTable);
            }
            return dbDataTable;
        }

        public DataTable MergeResult(DataTable javaApiDataTable, DataTable dbDataTable)
        {
            DataTable mergedDataTable = new DataTable();
            mergedDataTable.Merge(javaApiDataTable);
            mergedDataTable.Merge(dbDataTable);
            return mergedDataTable;
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace SDApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SDAppControler : ControllerBase
    {
        private IConfiguration _configuration ;
        public SDAppControler(IConfiguration configuration) {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("GetMedicine")]
        public JsonResult GetMedicine()
        {
            string query = "select * from dbo.medicine";
            System.Data.DataTable table = new System.Data.DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SDAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult(table);
        }


        [HttpPost]
        [Route("AddMedicine")]
        public JsonResult AddMedicine([FromForm] string newCode, [FromForm] string newMedicine, [FromForm] string newDescr, [FromForm] byte newImg, [FromForm] string newDose)
        {
            string query = "insert into dbo.medicine values(@code,@drug_name,@drug_descr,@drug_img,@dosing)";
            System.Data.DataTable table = new System.Data.DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SDAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@code", newCode);
                    myCommand.Parameters.AddWithValue("@drug_name", newMedicine);
                    myCommand.Parameters.AddWithValue("@drug_descr", newDescr);
                    myCommand.Parameters.AddWithValue("@drug_img", newImg);
                    myCommand.Parameters.AddWithValue("@dosing", newDose);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult("Added Successfully");
        }

        [HttpDelete]
        [Route("DeleteMedicine")]
        public JsonResult DeleteMedicine(int id)
        {
            string query = "delete from dbo.medicine where id=@id";
            System.Data.DataTable table = new System.Data.DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SDAppDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }
            return new JsonResult("Deleted Successfully");
        }
    }
}

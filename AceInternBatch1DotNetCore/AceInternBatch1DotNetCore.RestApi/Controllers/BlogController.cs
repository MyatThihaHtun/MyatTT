using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Reflection.Metadata;

namespace AceInternBatch1DotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        public BlogController()
        {
            //_connectionStringBuilder = new SqlConnectionStringBuilder();
            //_connectionStringBuilder.DataSource = ".";
            //_connectionStringBuilder.InitialCatalog = "AceInternBatch1DotNetCore";
            //_connectionStringBuilder.UserID = "sa";
            //_connectionStringBuilder.Password = "sa@123";

            _connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AceInternBatch1DotNetCore",
                UserID = "sa",
                Password = "sa@123"
            };
        }
            
        [HttpGet]
        public IActionResult GetBlogs()
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var lst = db.Query<TblBlog>(Quaries.BlogList).ToList();
            //string a = lst[0].Call();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<TblBlog>(Quaries.BlogById, new { BlogId = id}).FirstOrDefault();
            if (item is null)
                return NotFound("No data found.");
       
            return Ok(item);
        }
        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Quaries.BlogCreate, blog);
            string message = result > 0 ? "Saving Successful." : "Saving Failed.";
            return Ok(message);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Quaries.BlogUpdate, new { BlogId = id });
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }
        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok("PatchBlog");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result = db.Execute(Quaries.BlogDelete, new { BlogId = id });
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }
    }

    public class TblBlog
    {
        public int BlogID { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
    }

    public static class Quaries
    {
        public static string BlogList { get; } = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog]";

        public static string BlogById { get; } = @"  SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
  FROM [dbo].[Tbl_Blog] Where BlogId = @BlogId";
        
        public static string BlogCreate { get; } = @"INSERT INTO[dbo].[Tbl_Blog]
        ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           , @BlogAuthor
           , @BlogContent)";

        public static string BlogDelete { get; } = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId = @BlogId";

        public static string BlogUpdate { get; } = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = 'title'
      ,[BlogAuthor] = 'author'
      ,[BlogContent] = 'content'
 WHERE BlogId = @BlogId";
    }

}



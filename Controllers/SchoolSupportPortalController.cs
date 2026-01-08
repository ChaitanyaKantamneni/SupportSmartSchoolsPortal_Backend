using Microsoft.AspNetCore.Mvc;
using SchoolSupportPortal.DAL;
using SchoolSupportPortal.Models;

namespace SchoolSupportPortal.Controllers
{
    public class SchoolSupportPortalController:ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SchoolSupportPortalDAL dbop;
        private readonly ILogger<SchoolSupportPortalController> _logger;

        public SchoolSupportPortalController(
            IConfiguration configuration,
            ILogger<SchoolSupportPortalController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            var connectionString = _configuration.GetConnectionString("DefaultConnection")
                                   ?? throw new ArgumentNullException("Connection string not found");

            dbop = new SchoolSupportPortalDAL(connectionString);
        }

        [HttpPost("Tbl_SchoolDetailss_CRUD_Operations")]
        public IActionResult Tbl_SchoolDetailss_CRUD_Operations([FromBody] SchoolDetails school)
        {
            try
            {
                var result = dbop.Tbl_SchoolDetails_CRUD_Operations(school);

                if (result == null || result.Count == 0)
                {
                    return BadRequest(new { StatusCode = 400, Message = "No result returned or operation failed." });
                }

                // Check for any error status
                var error = result.FirstOrDefault(x => x.Status?.ToLower().Contains("error") == true);
                if (error != null)
                {
                    return BadRequest(new { StatusCode = 400, Message = error.Status });
                }

                return Ok(new { StatusCode = 200, Success = true, Message = result.First().Status, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { StatusCode = 400, Success = false, Message = "Internal server error.", Error = ex.Message });
            }
            //basic change in controller by chaitanya
        }
    }
}

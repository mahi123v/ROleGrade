using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YourNamespace.Data;
using RolesGrade.Models;
using System.Reflection.Metadata.Ecma335;

namespace RolesGrade.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly ILogger<DepartmentController> _logger;
        private readonly ApplicationDbContext _context;
        public DepartmentController(ILogger<DepartmentController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("department")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {

                var departments = await _context.DEPARTMENT1234.ToListAsync();

                if (departments == null || departments.Count == 0)
                {

                    return NotFound("No employees found.");
                }


                // Return the list of employees if found
                return Ok(departments);
            }
            catch (Exception ex)
            {
                // Log any exceptions and return a 500 Internal Server Error
                _logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("department/{departmentId}")]
        public async Task<IActionResult> GetDepartmentById(int departmentId)
        {
            var departments = await _context.DEPARTMENT1234.FirstOrDefaultAsync(e => e.Id == departmentId);
            return Ok(departments);
        }

        [HttpPost]
        [Route("department")]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            try
            {
                if(department == null)
                {
                    return BadRequest("department not found");
                }
                var departments = await _context.DEPARTMENT1234.AddAsync(department);
                await _context.SaveChangesAsync();
                return Ok(departments);
            }
            catch(Exception ex )
            {
                _logger.LogError($" failed create an Employee : {ex.Message}");
                return StatusCode(500, $" internal server error : {ex.Message}");
            }
           
        }



    }
};

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YourNamespace.Data;
using RolesGrade.Models;

namespace RolesGrade.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly ApplicationDbContext _context;

        // Constructor with Dependency Injection
        public EmployeeController(ILogger<EmployeeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/employee
        [HttpGet]
        [Route("employee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                // Check if Employee121 table is null or empty
                var employees = await _context.Employee121.ToListAsync();

                if (employees == null || employees.Count == 0)
                {
                    // Return NotFound if no employees are found
                    return NotFound("No employees found.");
                }
                

                // Return the list of employees if found
                return Ok(employees);
            }
            catch (Exception ex)
            {
                // Log any exceptions and return a 500 Internal Server Error
                _logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("employee")]
        public async Task<IActionResult> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)

                { return BadRequest(); }

             
           
                 employee.CreatedAt = DateTime.UtcNow;  
                employee.UpdatedAt = DateTime.UtcNow;

                var existingEmployee = await _context.Employee121.FirstOrDefaultAsync(e => e.Email == employee.Email);
                if (existingEmployee != null)
                    return Conflict($"An employee with the email {employee.Email} already exists.");
                var employees = await _context.Employee121.AddAsync(employee);
                await _context.SaveChangesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError($" failed create an Employee : {ex.Message}");
                return StatusCode(500, $" internal server error : {ex.Message}");
            }

        }

        [HttpGet]
        [Route("employee/{employeeId}")]
        public async Task<IActionResult>EmployeeGetById(int employeeId)
        {
            var employees = await _context.Employee121.FirstOrDefaultAsync(e => e.Id == employeeId);
            return Ok(employees);
        }

        [HttpDelete]
        [Route("employee/{employeeId}")]

        public async Task<IActionResult>DeleteEmployee(int employeeId)
        {
            var employees = await _context.Employee121.FirstOrDefaultAsync(e => e.Id == employeeId);
            _context.Employee121.Remove(employees);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}

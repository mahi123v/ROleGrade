using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using YourNamespace.Data;
using RolesGrade.Models;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using System.Text;


namespace RolesGrade.Controllers
{
    public class SalaryController : ControllerBase
    {
        private readonly ILogger<SalaryController> _logger;
        private readonly ApplicationDbContext _context;

        public SalaryController(ILogger<SalaryController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        [Route("employeesalary")]
        public async Task<IActionResult> GetAllEmployeeSalary()
        {
            try
            {
                var salaries = await _context.SALARY.ToListAsync();
                if (salaries == null || salaries.Count == 0)
                {
                    return NotFound();
                }
                return Ok(salaries);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpDelete]
        [Route("employeesalary/{salaryId}")]
        public async Task<IActionResult> DeleteSalary(int salaryId)
        {
            var salaries = await _context.SALARY.FirstOrDefaultAsync(s => s.Id == salaryId);
            _context.SALARY.Remove(salaries);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("employeesalary/{salaryId}")]
        public async Task<IActionResult> GetSalaryById(int salaryId)
        {
            var salaries = await _context.SALARY.FirstOrDefaultAsync(s => s.Id == salaryId);
            return Ok(salaries);
        }

        [HttpGet]
        [Route("salaryslip/csv")]
        public async Task<IActionResult> GenerateSalarySlipCsv(DateTime? startDate =null,DateTime? endDate = null)
        {
            startDate ??= new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            endDate ??= startDate.Value.AddMonths(1).AddDays(-1);
            var salaries = _context.SALARY.Include(s => s.Employee)
                .Where(s => s.StartDate >= startDate && s.EndDate <= endDate)
                .Select(s => new
            {
               
                s.Employee.Id,
                s.Employee.FirstName,
                s.Employee.LastName,
                s.Employee.Department.Name,
                s.Employee.Salary.Amount,
                s.Employee.Salary.StartDate,
                s.Employee.Salary.EndDate
            }).ToList();

            StringBuilder csvContent = new StringBuilder();
            csvContent.AppendLine("EmployeeId, FirstName, LastName, Name, Amount,StartDate,EndDate");
            foreach (var salary in salaries)
            {
                csvContent.AppendLine($"{salary.Id}, {salary.FirstName},{salary.LastName},{salary.Name},{salary.Amount},{salary.StartDate},{salary.EndDate}");
            }
            var fileBytes = Encoding.UTF8.GetBytes(csvContent.ToString());
            return File(fileBytes, "text/csv", "SalarySlips.csv");
        }


    }
}

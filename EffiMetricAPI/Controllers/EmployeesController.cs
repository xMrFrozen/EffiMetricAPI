using EffiMetricAPI.Data;
using EffiMetricAPI.Models;
using EffiMetricAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EffiMetricAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EmployeesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(string sortBy = "score")
        {
            var query = _context.Employees.Include(e => e.Tasks).AsQueryable();

            if (sortBy.ToLower() == "money") query = query.OrderByDescending(e => e.totalEarnings);
            else query = query.OrderByDescending(e => e.efficiencyScore);

            return await query.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                fullName = employeeDto.Name,
                efficiencyScore = employeeDto.Score,
                totalEarnings = employeeDto.Earned,
                Position = employeeDto.Position
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEmployees), new { id = employee.Id }, employee);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var employees = await _context.Employees.Include(e => e.Tasks).ToListAsync();

            if (!employees.Any()) return Ok("No data.");

            var summary = new
            {
                TotalEmployees = employees.Count,
                TotalDistributedMoney = employees.Sum(e => e.totalEarnings),
                AverageEfficiency = employees.Average(e => e.efficiencyScore),
                TopPerformer = employees.OrderByDescending(e => e.efficiencyScore)
                    .Select(e => new { e.fullName, e.efficiencyScore })
                    .FirstOrDefault()
            };

            return Ok(summary);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null) return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("dashboard-highlights")]
        public async Task<IActionResult> GetHighlights()
        {
            var employees = await _context.Employees.ToListAsync();

            var highlights = new
            {
                Champions = employees.OrderByDescending(e => e.efficiencyScore).Take(3)
                    .Select(e => new { e.fullName, e.efficiencyScore, e.Rank }),

                NeedSupport = employees.OrderBy(e => e.efficiencyScore).Take(3)
                    .Select(e => new { e.fullName, e.efficiencyScore, e.Rank }),

                CompanyVault = employees.Sum(e => e.totalEarnings)
            };

            return Ok(highlights);
        }
    }
}

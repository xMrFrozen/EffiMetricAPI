using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EffiMetricAPI.Models;
using EffiMetricAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EffiMetricAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkTasksController : ControllerBase
    {
        private readonly AppDbContext _context;
        public WorkTasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{taskID}/complete")]
        public async Task<IActionResult> CompleteTask(int taskID, double actualHours)
        {
            var task = await _context.WorkTasks.FindAsync(taskID);
            if (task == null) return NotFound();

            var employee = await _context.Employees.Include(e => e.Tasks)
                .FirstOrDefaultAsync(e => e.Id == task.employeeId);

            if (employee == null) return NotFound();

            ///////////////////////////

            task.isCompleted = true;
            task.completedHours = actualHours;
            task.CompletedAt = DateTime.Now;

            double timeEfficiency = task.estimatedHours / actualHours;
            if(timeEfficiency > 2) timeEfficiency = 2;

            double taskScore = (task.difLevel * 10) * timeEfficiency;

            double rankMultiplier = employee.efficiencyScore switch
            {
                >= 90 => 1.5,
                >= 75 => 1.2,
                _ => 1.0,
            };

            double baseRatePerDif = 50.0;
            double earnedMoney = (task.difLevel * baseRatePerDif) * timeEfficiency * rankMultiplier;

            var completedTasks = employee.Tasks.Where(t => t.isCompleted).ToList();
            employee.efficiencyScore = completedTasks.Average(t => (t.difLevel * 10) * (t.estimatedHours / t.completedHours));

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Task Completed!",
                EfficiencyScore = taskScore,
                MoneyEarned = earnedMoney,
                NewGeneralScore = employee.efficiencyScore
            });
        }
    }
}

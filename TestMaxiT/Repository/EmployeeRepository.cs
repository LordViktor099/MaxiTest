using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaxiT.Models.Data;
using TestMaxiT.Models.Entities;

namespace TestMaxiT.Repository
{
    public class EmployeeRepository : ControllerBase, IEmployeeRepository<Employee> 
    {
        private MyAppContext _dbContext;

        public EmployeeRepository(MyAppContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var existingEmployee = await _dbContext.Employee.FromSqlInterpolated($"EXEC FindEmployee {id}").ToListAsync();
            if ( existingEmployee.Count == 0)
            {
                return NotFound();
            }
            return Ok(existingEmployee);
        }

        public async Task<ActionResult<Employee>> Create(Employee entity)
        {
            var savedEntity = await _dbContext.Employee.FromSqlInterpolated($"EXEC CreateEmployee {entity.FirstName},{entity.LastName},{entity.Birthday},{entity.EmployeeNumber},{entity.CURP},{entity.SSN},{entity.PhoneNumber},{entity.Nationality}").ToListAsync();
            return savedEntity[0];
        }

        public async Task<ActionResult<Employee>> Update(int id, Employee entity)
        {
            var existingEmployee = await _dbContext.Employee.FromSqlInterpolated($"EXEC FindEmployee {id}").ToListAsync();
            if ( existingEmployee.Count == 0)
            {
                return NotFound();
            }

            var updatedEmployee = await _dbContext.Employee.FromSqlInterpolated($"EXEC UpdateEmployee {entity.FirstName},{entity.LastName},{entity.Birthday},{entity.EmployeeNumber},{entity.CURP},{entity.SSN},{entity.PhoneNumber},{entity.Nationality},{id}").ToListAsync();
            return Ok(updatedEmployee[0]);
        }
        public async Task<ActionResult> Delete(int id)
        {
            var existingEmployee = await _dbContext.Employee.FromSqlInterpolated($"EXEC FindEmployee {id}").ToListAsync();
            if ( existingEmployee.Count == 0 )
            {
                return NotFound();
            }

            await _dbContext.Database.ExecuteSqlRawAsync($"EXEC DeleteEmployee {id}");
            return NoContent();

        }
    }
}

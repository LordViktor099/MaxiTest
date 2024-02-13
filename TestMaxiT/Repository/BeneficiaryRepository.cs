using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestMaxiT.Models.Data;
using TestMaxiT.Models.Entities;

namespace TestMaxiT.Repository
{
    public class BeneficiaryRepository : ControllerBase, IBeneficiaryRepository<Beneficiary> 
    {
        private MyAppContext _dbContext;

        public BeneficiaryRepository(MyAppContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ActionResult<Beneficiary>> Get(int id)
        {
            var existingBeneficiary = await _dbContext.Employee.FromSqlInterpolated($"EXEC FindBeneficiary {id}").ToListAsync();
            if ( existingBeneficiary.Count == 0)
            {
                return NotFound();
            }
            return Ok(existingBeneficiary);
        }

        public async Task<ActionResult<Beneficiary>> Create(Beneficiary entity, int employeeId)
        {
            var existingEmployee = await _dbContext.Employee.FromSqlInterpolated($"EXEC FindEmployee {employeeId}").ToListAsync();
            if ( existingEmployee.Count == 0)
            {
                return BadRequest();
            }

            var savedEntity = await _dbContext.Beneficiary.FromSqlInterpolated($"EXEC CreateBeneficiary {employeeId},{entity.FirstName},{entity.LastName},{entity.Birthday},{entity.EmployeeNumber},{entity.CURP},{entity.SSN},{entity.PhoneNumber},{entity.Nationality}").ToListAsync();
            return savedEntity[0];
        }

        public async Task<ActionResult> Update(int id, Beneficiary entity)
        {
            var existingEmployee = await _dbContext.Beneficiary.FromSqlInterpolated($"EXEC FindBeneficiary {id}").ToListAsync();
            if ( existingEmployee.Count == 0)
            {
                return NotFound();
            }

            var updatedEmployee = await _dbContext.Beneficiary.FromSqlInterpolated($"EXEC UpdateBeneficiary {entity.FirstName},{entity.LastName},{entity.Birthday},{entity.EmployeeNumber},{entity.CURP},{entity.SSN},{entity.PhoneNumber},{entity.Nationality},{id}").ToListAsync();
            return Ok(updatedEmployee);
        }
        public async Task<ActionResult> Delete(int id)
        {
            var existingEmployee = await _dbContext.Beneficiary.FromSqlInterpolated($"EXEC FindBeneficiary {id}").ToListAsync();
            if ( existingEmployee.Count == 0 )
            {
                return NotFound();
            }

            await _dbContext.Database.ExecuteSqlRawAsync($"EXEC DeleteBeneficiary {id}");
            return NoContent();

        }
    }
}

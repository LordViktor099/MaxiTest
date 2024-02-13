using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TestMaxiT.Models.Dtos;
using TestMaxiT.Models.Entities;
using TestMaxiT.Repository;

namespace TestMaxiT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private IEmployeeRepository<Employee> repository;
        private readonly IMapper mapper;
        private IValidator<Employee> validator;

        public EmployeeController(IEmployeeRepository<Employee> repository, IMapper mapper, IValidator<Employee> validator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        [HttpGet(Name = "GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployee([FromQuery] int employeeId)
        {
            var employee = await repository.Get(employeeId);
            return employee;
        }

        [HttpPost(Name = "CreateEmployee")]
        public async Task<ActionResult<Employee>> CreateEmployee(EmployeeDto employee)
        {
            var employeeInst = mapper.Map<Employee>(employee); 
            ValidationResult validationResult = await validator.ValidateAsync(employeeInst);
            if( !validationResult.IsValid)
            {
                return BadRequest();
            }

            var savedEmployee = await repository.Create(employeeInst);
            return savedEmployee;
        }

        [HttpPut(Name = "UpdateEmployee")]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromQuery] int employeeId, [FromBody] EmployeeDto employee)
        {
            var updatedEmployee = await repository.Update(employeeId, mapper.Map<Employee>(employee));
            return updatedEmployee;
        }

        [HttpDelete(Name = "DeleteEmployee")]
        public async Task<ActionResult> DeleteEmployee([FromQuery] int employeeId)
        {
            return await repository.Delete(employeeId);
        }

    }
}

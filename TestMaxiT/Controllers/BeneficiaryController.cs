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
    public class BeneficiaryController : Controller
    {
        private IBeneficiaryRepository<Beneficiary> repository;
        private readonly IMapper mapper;
        private IValidator<Beneficiary> validator;

        public BeneficiaryController(IBeneficiaryRepository<Beneficiary> repository, IMapper mapper, IValidator<Beneficiary> validator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
        }

        [HttpGet(Name = "GetBeneficiary")]
        public async Task<ActionResult<Beneficiary>> GetBeneficiary([FromQuery] int beneficiaryId)
        {
            var beneficiary = await repository.Get(beneficiaryId);
            return beneficiary;
        }

        [HttpPost(Name = "CreateBeneficiary")]
        public async Task<ActionResult<Beneficiary>> CreateBeneficiary([FromBody]BeneficiaryDto beneficiary, [FromQuery] int employeeId)
        {
            var beneficiaryInst = mapper.Map<Beneficiary>(beneficiary); 
            ValidationResult validationResult = await validator.ValidateAsync(beneficiaryInst);
            if( !validationResult.IsValid)
            {
                return BadRequest();
            }

            var savedBeneficiary = await repository.Create(beneficiaryInst, employeeId);
            return savedBeneficiary;
        }

        [HttpDelete(Name = "DeleteBeneficiary")]
        public async Task<ActionResult> DeleteBeneficiary([FromQuery] int beneficiaryId)
        {
            return await repository.Delete(beneficiaryId);
        }
    }
}

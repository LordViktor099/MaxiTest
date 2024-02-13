using FluentValidation;
using TestMaxiT.Models.Entities;

namespace TestMaxiT.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator() {
            RuleFor(x => x.Birthday).Must(BeEighteen);
        }

        private bool BeEighteen(DateTime birthday)
        {
            var currentDateTime = DateTime.Now;

            if ( birthday > currentDateTime )
            {
                return false;
            }

            TimeSpan span = currentDateTime - birthday;
            int years = (new DateTime(1, 1, 1) + span).Year - 1;

            return years >= 18;
        }
    }
}

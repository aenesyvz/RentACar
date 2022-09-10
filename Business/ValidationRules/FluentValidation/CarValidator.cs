using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator: AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(x => x.CarName).NotEmpty();
            RuleFor(x => x.BrandId).NotEmpty();
            RuleFor(x => x.ColorId).NotEmpty();
            RuleFor(x => x.DailyPrice).NotEmpty();
           
        }
    }
}

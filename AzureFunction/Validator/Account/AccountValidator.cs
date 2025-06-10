using FluentValidation;
using HellowWord.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureFunction.Validator.Account
{
    public class AccountValidator : AbstractValidator<AccountDTO> 
    {
        public AccountValidator()
        {
            RuleFor(account => account.Address1_Line1).NotNull();
            RuleFor(account => account.Name).NotNull().MinimumLength(2).MaximumLength(100);
        }
    }
}

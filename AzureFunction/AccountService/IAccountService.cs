using HellowWord.DTO;
using HellowWord.Validation;

namespace HellowWord.AccountService
{
    public interface IAccountService
    {
         List<AccountDTO> GetAccounts(IValidationContainer validationContainer);
         Task<AccountDTO> CreateAccount(AccountDTO accountDTO, IValidationContainer validationContainer);
        Task<AccountDTO> UpdateAccount(AccountDTO accountDTO, IValidationContainer validationContainer);
        Task<string> DeleteAccount(AccountDTO accountDTO, IValidationContainer validationContainer);
    }
}

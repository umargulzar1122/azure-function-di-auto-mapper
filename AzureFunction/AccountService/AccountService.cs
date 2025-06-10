using AutoMapper;
using HellowWord.DTO;
using HellowWord.Validation;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using YourNamespace;


namespace HellowWord.AccountService
{
    public class AccountService(ServiceClient service, IMapper mapper) : IAccountService
    {
        private readonly ServiceClient _service = service;
        private readonly IMapper _mapper = mapper;

        public async Task<AccountDTO> CreateAccount(AccountDTO accountDTO, IValidationContainer validationContainer)
        {
            try
            {
                var account1 = _mapper.Map<Account>(accountDTO); 
                account1.Attributes.Remove("accountid");
                var data = await _service.CreateAndReturnAsync(account1);
                return _mapper.Map<AccountDTO>(data);
            }
            catch (Exception ex)
            {
                validationContainer.AddMessage(Enum.MessageTypeEnum.Error, ex.Message);
                return new AccountDTO();
            }

        }       

        public List<AccountDTO> GetAccounts(IValidationContainer validationContainer)
        {
            try
            {
                ServiceContext serviceContext = new(_service);
                var accounts = serviceContext.AccountSet.Select(s => new Account
                {
                    Name = s.Name,
                    Address1_Line1 = s.Address1_Line1,
                    PrimaryContactId = s.PrimaryContactId,
                    AccountId = s.AccountId
                })
                    .ToList();
                return _mapper.Map<List<AccountDTO>>(accounts);
            }
            catch (Exception ex)
            {
                validationContainer.AddMessage(Enum.MessageTypeEnum.Error, ex.Message);
                return [];
            }

        }

        public async Task UpdateAccount(AccountDTO accountDTO, IValidationContainer validationContainer)
        {
            try
            {
                //var account = _mapper.Map<Account>(accountDTO);
                var account = new Account();
                account.Name = $"Test {DateTime.Now}";
                account.AccountId = Guid.Parse(accountDTO.AccountId);
                 await _service.UpdateAsync(account);
                validationContainer.AddMessage(Enum.MessageTypeEnum.Success, $"Account with Id {accountDTO.AccountId} is successfully updated");
            }
            catch (Exception ex)
            {
                validationContainer.AddMessage(Enum.MessageTypeEnum.Error, ex.Message);                 
            }
        }
        public async Task<string> DeleteAccount(AccountDTO accountDTO, IValidationContainer validationContainer)
        {
            try
            {
                var account = _mapper.Map<Account>(accountDTO);
                await _service.DeleteAsync(Account.EntityLogicalName,account.Id);
                return "Deleted Successfully";
            }
            catch (Exception ex)
            {
                validationContainer.AddMessage(Enum.MessageTypeEnum.Error, ex.Message);
                return string.Empty;
            }
        }
    }
}

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
                var account = _mapper.Map<Account>(accountDTO);
                var data = await _service.CreateAndReturnAsync(account);
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
                    PrimaryContactId = s.PrimaryContactId
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

        public async Task<AccountDTO> UpdateAccount(AccountDTO accountDTO, IValidationContainer validationContainer)
        {
            try
            {
                var account = _mapper.Map<Account>(accountDTO);
                await _service.UpdateAsync(account);
                return accountDTO;
            }
            catch (Exception ex)
            {
                validationContainer.AddMessage(Enum.MessageTypeEnum.Error, ex.Message);
                return new AccountDTO();
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

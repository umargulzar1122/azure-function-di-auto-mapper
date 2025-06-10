using FluentValidation;
using HellowWord.AccountService;
using HellowWord.DTO;
using HellowWord.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace AzureFunction;

public class Dataverse
{
    private readonly ILogger<Dataverse> _logger;
    private readonly IAccountService _accountService;
    private readonly IValidationContainer _validationContainer;
    private readonly IValidator<AccountDTO> _accountValidator;

    public Dataverse(IAccountService accountService, IValidator<AccountDTO> accountValidator, ILogger<Dataverse> logger, IValidationContainer validationContainer)
    {
        _logger = logger;
        _accountService = accountService;
        _validationContainer = validationContainer;
        _accountValidator = accountValidator;
    }

    [Function("GetAccounts")]
    public IActionResult GetAccounts([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        var result = _accountService.GetAccounts(_validationContainer);
        if (_validationContainer.IsError)
        {
            return new BadRequestObjectResult(_validationContainer);
        }
        return new OkObjectResult(result);
    }

    [Function("CreateAccount")]
    public async Task<IActionResult> CreateAccount([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var accountDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountDTO>(requestBody, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate
        });
        var validationResult = await _accountValidator.ValidateAsync(accountDTO);
        if (validationResult.IsValid == false)
        {
            return new BadRequestObjectResult(validationResult.Errors);
        }
        var result = await _accountService.CreateAccount(accountDTO, _validationContainer);
        if (_validationContainer.IsError)
        {
            return new BadRequestObjectResult(_validationContainer);
        }

        return new OkObjectResult(result);
    }

    [Function("UpdateAccount")]
    public async Task<IActionResult> UpdateAccount([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var accountDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountDTO>(requestBody, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        });
        await _accountService.UpdateAccount(accountDTO, _validationContainer);
        if (_validationContainer.IsError)
        {
            return new BadRequestObjectResult(_validationContainer);
        }

        return new OkObjectResult(_validationContainer);
    }

    [Function("DeleteAccount")]
    public async Task<IActionResult> DeleteAccount([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequest req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

        var accountDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<AccountDTO>(requestBody, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
        });
        var result = await _accountService.DeleteAccount(accountDTO, _validationContainer);
        if (_validationContainer.IsError)
        {
            return new BadRequestObjectResult(_validationContainer);
        }

        return new OkObjectResult(result);
    }
}
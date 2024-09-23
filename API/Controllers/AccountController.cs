using API.DTO.Account.Rerquests;
using BLL.Services.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountController : APIBaseController
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LogIn([FromBody] LogInDTORequest request, [FromServices] LogInDTORequestValidator validator)
    {
        var badRequest = await ValidateRequest(request, validator);
        if (badRequest is not null) return badRequest;


        var token = _accountService.LogIn(request.username, request.password);


        return Ok(new{access_token = token});
    }
}

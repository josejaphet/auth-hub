﻿using Application.Features.Auth.Login;
using Application.Features.Auth.Register;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(request.UserName,
                                          request.Email,
                                          request.Password,
                                          request.PhoneNumber, 
                                          request.Roles);

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(new CommonResponse()
        {
            Data = result.Value,
            Message = result.Message,
            IsSuccess = result.IsSuccess,
        }) : BadRequest(new CommonResponse
        {
            Data = null,
            Message = result.Error.ToString(),
            IsSuccess = result.IsFailure
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.UserName, 
                                       request.Password);

        var result = await _mediator.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(new CommonResponse()
        {
            Data = result.Value,
            Message = result.Message,
            IsSuccess = result.IsSuccess,
        }) : BadRequest(new CommonResponse
        {
            Data = null,
            Message = result.Error.ToString(),
            IsSuccess = result.IsFailure
        });
    }
}

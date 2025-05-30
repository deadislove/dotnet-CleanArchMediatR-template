using CleanArchMediatR.Template.Application.Commands.Auth;
using CleanArchMediatR.Template.Application.DTOs.Request;
using CleanArchMediatR.Template.Application.DTOs.Request.Auth;
using CleanArchMediatR.Template.Application.DTOs.Response.Auth;
using CleanArchMediatR.Template.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMediatR.Template.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        public IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] BaseRequest<RegisterDto> requestModel)
        {
            InjectMetaIfMissing(requestModel);

            RegisterResponseDto response = await this.mediator.Send(new RegisterCommand
            {
                Username = requestModel.Payload.username,
                Password = requestModel.Payload.password
            });

            return Ok(ResponseHelper.Success(response));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] BaseRequest<LoginDto> requestModel)
        {
            InjectMetaIfMissing(requestModel);

            LoginResponseDto response = await this.mediator.Send(new LoginCommand { 
                Username = requestModel.Payload.username,
                Password = requestModel.Payload.password
            });

            return Ok(ResponseHelper.Success(response));
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] BaseRequest<RefreshTokenDto> requestModel)
        {
            InjectMetaIfMissing(requestModel);

            RefreshTokenResponseDto response = await this.mediator.Send(new RefreshTokenCommand(requestModel.Payload.RefreshToken));

            return Ok(ResponseHelper.Success(response));
        }
    }
}

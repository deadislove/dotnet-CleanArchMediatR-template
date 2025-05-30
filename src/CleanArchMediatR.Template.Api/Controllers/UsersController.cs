using Asp.Versioning;
using CleanArchMediatR.Template.Application.Commands.Users;
using CleanArchMediatR.Template.Application.DTOs.Request;
using CleanArchMediatR.Template.Application.DTOs.Request.Users;
using CleanArchMediatR.Template.Application.DTOs.Response.Users;
using CleanArchMediatR.Template.Application.Helpers;
using CleanArchMediatR.Template.Application.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMediatR.Template.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UsersController : BaseApiController
    {
        public IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() { 
            List<UserResponseDto> data = await this.mediator.Send(new GetAllUsersQuery());

            return Ok(ResponseHelper.Success(data));
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(string userId) {
            UserResponseDto data = await this.mediator.Send(new GetUserQuery(userId));

            return Ok(ResponseHelper.Success(data));
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] BaseRequest<UsersDto> requestModel) {

            InjectMetaIfMissing(requestModel);

            UserResponseDto data = await this.mediator.Send(new CreateUserCommand { 
                userName = requestModel.Payload.userName,
                password = requestModel.Payload.password
            });

            return Ok(ResponseHelper.Success(data));
        }

        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody] BaseRequest<UsersDto> requestModel) {

            InjectMetaIfMissing(requestModel);

            bool data = await this.mediator.Send(new UpdateUserCommand { 
                id = requestModel.Payload.id,
                userName = requestModel.Payload.userName,
                password = requestModel.Payload.password
            });


            return Ok(ResponseHelper.Success(data));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (Guid.TryParse(userId, out Guid userIdGuid))
            {
                bool data = await this.mediator.Send(new DeleteUserCommand(userIdGuid));
                return Ok(ResponseHelper.Success(data));
            }
            else {
                return BadRequest(ResponseHelper.Fail<bool>("User ID tpye is not UUID."));
            }
            
        }
    }
}

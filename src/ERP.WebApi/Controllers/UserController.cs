using ERP.Application.Modules.Users;
using ERP.Domain.Enums;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : BaseController
    {
        public UserController(IMediator _mediator) : base(_mediator)
        { }

        [CustomRoleAuthorizeFilter(PermissionEnum.UserAdd)]
        [HttpPost]
        public async Task<CustomActionResult> RegisterUser(RegisterUserCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.UserEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateUser(UpdateUserCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.UserEdit)]
        [HttpPost]
        public async Task<CustomActionResult> ResetUserPassword(ResetUserPasswordCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.UserEdit)]
        [HttpPost]
        public async Task<CustomActionResult> BlockUser(BlockUserCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.UserEdit)]
        [HttpPost]
        public async Task<CustomActionResult> ActivateUser(ActivateUserCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<CustomActionResult> Login(LoginReq req)
        {
            var result = await _mediator.Send<LoginRes>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.UserView)]
        [HttpPost]
        public async Task<CustomActionResult> GetAllUsers(GetAllUsersReq req)
        {
            var id = await _mediator.Send<GetAllUsersRes>(req);
            return new CustomActionResult(true, null, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.UserView)]
        [HttpPost]
        public async Task<CustomActionResult> GetUserById(GetUserById req)
        {
            var id = await _mediator.Send<UserViewModel>(req);
            return new CustomActionResult(true, null, null, id);
        }
    }
}
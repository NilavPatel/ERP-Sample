using ERP.Application.Modules.Designations.Commands;
using ERP.Application.Modules.Designations.Queries;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Designations;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class DesignationController : BaseController
    {
        public DesignationController(IMediator _mediator) : base(_mediator)
        { }

        [HttpPost]
        public async Task<CustomActionResult> GetAllDesignations(GetAllDesignationsReq req)
        {
            var result = await _mediator.Send<GetAllDesignationsRes>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DesignationView)]
        [HttpPost]
        public async Task<CustomActionResult> GetDesignationById(GetDesignationByIdReq req)
        {
            var result = await _mediator.Send<Designation>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DesignationAdd)]
        [HttpPost]
        public async Task<CustomActionResult> CreateDesignation(CreateDesignationCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DesignationEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateDesignation(UpdateDesignationCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }
        
        [CustomRoleAuthorizeFilter(PermissionEnum.DesignationDelete)]
        [HttpPost]
        public async Task<CustomActionResult> DeleteDesignation(DeleteDesignationCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record removed sucessfully." }, null, result);
        }
    }
}
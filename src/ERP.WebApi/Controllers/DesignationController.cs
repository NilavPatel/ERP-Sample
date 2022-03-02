using ERP.Application.Modules.Designations;
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

        [CustomRoleAuthorizeFilter(PermissionEnum.DesignationView)]
        [HttpPost]
        public async Task<CustomActionResult> GetAllDesignations(GetAllDesignationsReq req)
        {
            var result = await _mediator.Send<GetAllDesignationsRes>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DesignationView)]
        [HttpPost]
        public async Task<CustomActionResult> GetDesignationById(GetDesignationById req)
        {
            var result = await _mediator.Send<Designation>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DesignationAdd)]
        [HttpPost]
        public async Task<CustomActionResult> CreateDesignation(CreateDesignation req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DesignationEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateDesignation(UpdateDesignation req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }
    }
}
using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.Queries
{
    public sealed record GetAllRolesForUsersQuery() : IRequest<Result<List<AppRole>>>;

    internal sealed class GetAllRolesForUsersQueryHandler(
    RoleManager<AppRole> roleManager) : IRequestHandler<GetAllRolesForUsersQuery, Result<List<AppRole>>>
    {
        public async Task<Result<List<AppRole>>> Handle(GetAllRolesForUsersQuery request, CancellationToken cancellationToken)
        {
            List<AppRole> roles = await roleManager.Roles.OrderBy(p => p.Name).ToListAsync(cancellationToken);

            return roles;
        }
    }
}

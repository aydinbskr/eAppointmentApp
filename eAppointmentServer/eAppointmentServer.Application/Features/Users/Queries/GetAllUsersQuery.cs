using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
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
    public sealed record GetAllUsersQuery() : IRequest<Result<List<GetAllUsersQueryResponse>>>;

    internal sealed class GetAllUsersQueryHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IUserRoleRepository userRoleRepository
    ) : IRequestHandler<GetAllUsersQuery, Result<List<GetAllUsersQueryResponse>>>
    {
        public async Task<Result<List<GetAllUsersQueryResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<AppUser> users = await userManager.Users.OrderBy(p => p.FirstName).ToListAsync(cancellationToken);

            List<GetAllUsersQueryResponse> response =
                users.Select(s => new GetAllUsersQueryResponse()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    FullName = s.FullName,
                    UserName = s.UserName,
                    Email = s.Email
                }).ToList();

            foreach (var item in response)
            {
                List<AppUserRole> userRoles = await userRoleRepository.Where(p => p.UserId == item.Id).ToListAsync(cancellationToken);

                List<Guid> stringRoles = new();
                List<string?> stringRoleNames = new();

                foreach (var userRole in userRoles)
                {
                    AppRole? role =
                        await roleManager
                        .Roles
                        .Where(p => p.Id == userRole.RoleId)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (role is not null)
                    {
                        stringRoles.Add(role.Id);
                        stringRoleNames.Add(role.Name);
                    }
                }

                item.RoleIds = stringRoles;
                item.RoleNames = stringRoleNames;
            }

            return response;
        }
    }

    public sealed record GetAllUsersQueryResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public List<Guid> RoleIds { get; set; } = new();
        public List<string?> RoleNames { get; set; } = new();
    };
}

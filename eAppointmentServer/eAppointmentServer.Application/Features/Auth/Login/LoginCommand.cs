using eAppointmentServer.Application.Services;
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

namespace eAppointmentServer.Application.Features.Auth.Login
{
    public sealed record LoginCommand(
        string UserNameOrEmail, string Password):IRequest<Result<LoginCommandResponse>>;

    public sealed record LoginCommandResponse(string Token);

    public sealed class LoginCommandHandler(UserManager<AppUser> userManager,IJWTProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            AppUser? appUser = await userManager.Users.FirstOrDefaultAsync(p =>
            p.UserName == request.UserNameOrEmail ||
            p.Email == request.UserNameOrEmail, cancellationToken);

            if(appUser is null)
            {
                return Result<LoginCommandResponse>.Failure("User not found");
            }

            bool isPasswordCorrect = await userManager.CheckPasswordAsync(appUser, request.Password);
            if(!isPasswordCorrect)
            {
                return Result<LoginCommandResponse>.Failure("Password is wrong");
            }

            string token = jwtProvider.CreateToken(appUser);


            return Result<LoginCommandResponse>.Succeed(new LoginCommandResponse(token));
        }
    }
}

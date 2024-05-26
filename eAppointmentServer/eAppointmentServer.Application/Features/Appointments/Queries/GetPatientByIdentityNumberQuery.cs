using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.Queries
{
    public sealed record GetPatientByIdentityNumberQuery(
    string IdentityNumber) : IRequest<Result<Patient>>;

    internal sealed class GetPatientByIdentityNumberQueryHandler(
    IPatientRepository patientRepository) : IRequestHandler<GetPatientByIdentityNumberQuery, Result<Patient>>
    {
        public async Task<Result<Patient>> Handle(GetPatientByIdentityNumberQuery request, CancellationToken cancellationToken)
        {
            Patient? patient =
                await patientRepository
                .GetByExpressionAsync(p => p.IdentityNumber == request.IdentityNumber, cancellationToken);

            return patient;
        }
    }
}

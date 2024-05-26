using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.Queries
{
    public sealed record GetAllAppointmentsByDoctorIdQuery(
    Guid DoctorId) : IRequest<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>;

    public sealed record GetAllAppointmentsByDoctorIdQueryResponse(
    Guid Id,
    DateTime StartDate,
    DateTime EndDate,
    string Title,
    Patient Patient);

    internal sealed class GetAllAppointmentsByDoctorIdQueryHandler(
    IAppointmentRepository appointmentRepository) : IRequestHandler<GetAllAppointmentsByDoctorIdQuery, Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentsByDoctorIdQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments =
                await appointmentRepository
                .Where(p => p.DoctorId == request.DoctorId)
                .Include(p => p.Patient)
                .ToListAsync(cancellationToken);


            List<GetAllAppointmentsByDoctorIdQueryResponse> response =
                appointments.Select(s =>
                new GetAllAppointmentsByDoctorIdQueryResponse(
                    s.Id,
                    s.StartDate,
                    s.EndDate,
                    s.Patient!.FullName,
                    s.Patient))
                .ToList();

            return response;
        }
    }
}

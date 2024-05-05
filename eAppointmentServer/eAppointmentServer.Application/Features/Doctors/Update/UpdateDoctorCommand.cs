using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.Update
{
    public sealed record UpdateDoctorCommand(
    Guid Id,
    string FirstName,
    string LastName,
    int DepartmentValue) : IRequest<Result<string>>;

    internal sealed class UpdateDoctorCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await doctorRepository.GetByExpressionWithTrackingAsync(p => p.Id == request.Id, cancellationToken);

            if (doctor is null)
            {
                return Result<string>.Failure("Doctor not found");
            }

            mapper.Map(request, doctor);

            doctorRepository.Update(doctor);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doctor update is successful";
        }
    }
}

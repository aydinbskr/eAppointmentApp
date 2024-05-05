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

namespace eAppointmentServer.Application.Features.Doctors.Create
{
    public sealed record CreateDoctorCommand(string FirstName,string LastName,int DepartmentValue) : IRequest<Result<string>>;

    internal sealed class CreateDoctorCommandHandler(
    IDoctorRepository doctorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<CreateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor doctor = mapper.Map<Doctor>(request);

            await doctorRepository.AddAsync(doctor, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doctor create is successul";
        }
    }
}

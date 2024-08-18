using Announcement.Application.Services;
using Announcement.Application.Services.AnnouncementServices;
using Announcement.Application.Services.AnnouncementServices.AnnouncementServicesInterfaces;
using Announcement.Application.Validations;
using Announcement.Domain.Models;
using Announcement.Domain.Models.RequestDtos;
using Announcement.Persistence.Repositories;
using Announcement.Persistence.Repositories.AnnouncementRepository;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Announcement.Infrastructure.Extensions
{
    public static class ServicesRegistrationExtension
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IAnnouncementService, AnnouncementService>();
            services.AddScoped<IBaseService<AnnouncementEntity>, BaseService<AnnouncementEntity>>();

            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IBaseRepository<AnnouncementEntity>, BaseRepository<AnnouncementEntity>>();

            services.AddScoped<IValidator<CreateAnnouncementRequestDto>, CreateAnnouncementRequestDtoValidator>();
            services.AddScoped<IValidator<UpdateAnnouncementRequestDto>, UpdateAnnouncementRequestDtoValidator>();
        }
    }
}
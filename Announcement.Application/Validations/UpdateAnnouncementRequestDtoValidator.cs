using Announcement.Domain.Models.RequestDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcement.Application.Validations
{
    public class UpdateAnnouncementRequestDtoValidator : AbstractValidator<UpdateAnnouncementRequestDto>
    {
        public UpdateAnnouncementRequestDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Invalid announcement ID.");

            RuleFor(x => x.Title)
             .NotEmpty().WithMessage("Title is required.")
             .Length(1, 100).WithMessage("Title cannot be less than 1 and exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .Length(10, 1000).WithMessage("Description cannot be less than 5 and exceed 100 characters.");
        }
    }
}

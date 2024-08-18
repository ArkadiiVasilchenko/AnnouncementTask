using Announcement.API.Controllers;
using Announcement.Application.Services.AnnouncementServices.AnnouncementServicesInterfaces;
using Announcement.Domain.Models;
using Announcement.Domain.Models.RequestDtos;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace Announcement.UnitTests
{
    public class AnnouncementControllerTests
    {
        private readonly AnnouncementController _controller;
        private readonly IAnnouncementService _announcementService;

        public AnnouncementControllerTests()
        {
            _announcementService = A.Fake<IAnnouncementService>();
            _controller = new AnnouncementController(_announcementService);
        }

        [Fact]
        public void GetAnnouncements_ShouldReturnNotFound_WhenNoAnnouncementsExist()
        {
            // Arrange
            A.CallTo(() => _announcementService.Read())
                .Returns(new List<AnnouncementEntity>());

            // Act
            var result = _controller.GetAnnouncements() as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);
            result.Value.Should().Be("No announcements found.");
        }

        [Fact]
        public void GetAnnouncements_ShouldReturnOk_WhenAnnouncementsExist()
        {
            // Arrange
            var announcements = new List<AnnouncementEntity>
            {
                new AnnouncementEntity ("Test Announcement1","Test Description1"),
                new AnnouncementEntity ("Test Announcement2","Test Description2"),
                new AnnouncementEntity ("Test Announcement3","Test Description3"),
            };

            A.CallTo(() => _announcementService.Read())
                .Returns(announcements);

            // Act
            var result = _controller.GetAnnouncements() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetAnnouncements_ShouldReturnInternalServerError_WhenExceptionIsThrown()
        {
            // Arrange
            A.CallTo(() => _announcementService.Read())
                .Throws(new Exception("Test Exception"));

            // Act
            var result = _controller.GetAnnouncements() as ObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("An error occurred while processing the request.");
        }

        [Fact]
        public async Task CreateAnnouncement_ShouldReturnOk_WhenRequestIsValid()
        {
            // Arrange
            var requestDto = new CreateAnnouncementRequestDto
            {
                Title = "TestTitle",
                Description = "TestDescription"
            };

            // Act
            var result = await _controller.CreateAnnouncement(requestDto);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
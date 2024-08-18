using Announcement.Application.Services.AnnouncementServices;
using Announcement.Application.Services.AnnouncementServices.AnnouncementServicesInterfaces;
using Announcement.Domain.Models.RequestDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Announcement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService announcementService;
        public AnnouncementController(IAnnouncementService announcementService)
        {
            this.announcementService = announcementService;
        }

        [HttpPost]
        [Route("CreateAnnouncement")]
        public async Task<IActionResult> CreateAnnouncement([FromBody] CreateAnnouncementRequestDto requestDto)
        {
            try 
            {
                await announcementService.СreateAsync(requestDto);
                return Ok();
            }
            catch(FluentValidation.ValidationException ex)
            {
                var modelStateDictionary = new ModelStateDictionary();
                foreach (var error in ex.Errors)
                {
                    modelStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(modelStateDictionary);
            }
        }
            
        [HttpGet]
        [Route("GetAnnouncements")]
        public IActionResult GetAnnouncements()
        {
            try
            {
                var result = announcementService.Read();

                if (result == null || !result.Any())
                {
                    return NotFound("No announcements found.");
                }
                else return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        [HttpGet]
        [Route("GetAnnouncementById/{id}")]
        public async Task<IActionResult> GetAnnouncementById([FromRoute] int id)
        {
            try
            {
                var result = await announcementService.ReadByIdAsync(id);

                if (result == null)
                {
                    return NotFound("No announcement found.");
                }
                else return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        [HttpDelete]
        [Route("DeleteAnnouncement/{id}")]
        public async Task<IActionResult> DeleteAnnouncement([FromRoute] int id)
        {
            try
            {
                await announcementService.DeleteAsync(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        [Route("UpdateAnnouncement")]
        public async Task<IActionResult> UpdateAnnouncement([FromBody] UpdateAnnouncementRequestDto requestDto)
        {
            try
            {
                await announcementService.UpdateAsync(requestDto);
                return Ok();
            }
            catch (FluentValidation.ValidationException ex)
            {
                var modelStateDictionary = new ModelStateDictionary();
                foreach (var error in ex.Errors)
                {
                    modelStateDictionary.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(modelStateDictionary);
            }
            catch(NullReferenceException)
            {
                return NotFound("No announcement found.");
            }
        }
    }
}
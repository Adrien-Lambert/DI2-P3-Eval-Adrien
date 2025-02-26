using backend.Data.DTO;
using backend.Data.Mappers;
using backend.Logic.Enums;
using backend.Logic.Services;
using backend.Services.EncryptionStrategy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace backend.Controllers
{
    /// <summary>
    /// Controller for managing application operations such as creation, retrieval....
    /// </summary>
    [ApiController]
    [Route("api/applications")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly ILogger<ApplicationController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController"/> class.
        /// </summary>
        /// <param name="applicationService">The application service for operations.</param>
        /// <param name="logger">The logger instance.</param>
        public ApplicationController(IApplicationService applicationService, ILogger<ApplicationController> logger)
        {
            _applicationService = applicationService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new application.
        /// </summary>
        /// <param name="applicationDto">The application data for creation.</param>
        /// <returns>The HTTP response with the created application details.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromBody] ApplicationCreationDto applicationDto)
        {
            _logger.LogInformation("Processing request to create a new application.");

            if (applicationDto == null)
            {
                _logger.LogWarning("Application creation request failed due to invalid data.");
                return BadRequest("Invalid application data");
            }

            try
            {
                var application = ApplicationMapper.ToApplication(applicationDto);

                if ((application.ApplicationType != ApplicationType.PROFESSIONAL) && (application.ApplicationType != ApplicationType.PUBLIC))
                {
                    _logger.LogWarning("Application creation request failed due to unknown application type");
                    return BadRequest("Unknown application type, must be 0 (PUBLIC) or 1 (PROFESSIONAL)");
                }

                var createdApplication = await _applicationService.CreateApplication(application);

                _logger.LogInformation("Application created successfully with ID {ApplicationId}.", createdApplication.ApplicationId);
                return CreatedAtAction(nameof(GetApplicationById), new { applicationId = createdApplication.ApplicationId }, ApplicationMapper.ToApplicationReadDTO(createdApplication));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update error while creating application.");
                return Conflict("An application with the same name already exists.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the request.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        /// <summary>
        /// Retrieves an application by its ID.
        /// </summary>
        /// <param name="applicationId">The application ID.</param>
        /// <returns>The application details or a not found status.</returns>
        [HttpGet("{applicationId:int}")]
        public async Task<IActionResult> GetApplicationById(int applicationId)
        {
            _logger.LogInformation("Processing request to get application with ID {ApplicationId}.", applicationId);

            var application = await _applicationService.GetApplicationById(applicationId);
            if (application == null)
            {
                _logger.LogWarning("Application with ID {ApplicationId} not found.", applicationId);
                return NotFound();
            }

            _logger.LogInformation("Application with ID {ApplicationId} retrieved successfully.", applicationId);
            return Ok(ApplicationMapper.ToApplicationReadDTO(application));
        }

        /// <summary>
        /// Retrieves all applications.
        /// </summary>
        /// <returns>A list of all applications or a no content status if none are found.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllApplications()
        {
            _logger.LogInformation("Processing request to get all applications.");

            var applications = await _applicationService.GetAllApplications();
            if (applications == null || !applications.Any())
            {
                _logger.LogWarning("No applications found.");
                return NoContent();
            }

            _logger.LogInformation("Applications retrieved successfully, total count: {Count}.", applications.Count);
            return Ok(applications.Select(ApplicationMapper.ToApplicationReadDTO));
        }
    }
}

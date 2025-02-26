using backend.Data.DTO;
using backend.Data.Mappers;
using backend.Logic.Enums;
using backend.Logic.Services;
using backend.Services.EncryptionStrategy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace backend.Controllers
{
    /// <summary>
    /// Controller for managing password operations such as creation, retrieval....
    /// </summary>
    [ApiController]
    [Route("api/passwords")]
    public class PasswordController : ControllerBase
    {
        private readonly IPasswordService _passwordService;
        private readonly IApplicationService _applicationService;
        private readonly ILogger<PasswordController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordController"/> class.
        /// </summary>
        /// <param name="passwordService">The password service for operations.</param>
        /// <param name="logger">The logger instance.</param>
        public PasswordController(IPasswordService passwordService, IApplicationService applicationService, ILogger<PasswordController> logger)
        {
            _passwordService = passwordService;
            _applicationService = applicationService;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new password.
        /// </summary>
        /// <param name="passwordDto">The password data for creation.</param>
        /// <returns>The HTTP response with the created password details.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePassword([FromBody] PasswordCreationDto passwordDto)
        {
            _logger.LogInformation("Processing request to create a new password.");

            if (passwordDto == null)
            {
                _logger.LogWarning("Password creation request failed due to invalid data.");
                return BadRequest("Invalid password data");
            }

            try
            {
                var password = PasswordMapper.ToPassword(passwordDto);
                IEncryptionStrategy encryptionStrategy = null;

                _logger.LogInformation("Processing request to get linked application with ID {ApplicationId}.", password.ApplicationId);

                var application = await _applicationService.GetApplicationById(password.ApplicationId);
                if (application == null)
                {
                    _logger.LogWarning("Application with ID {ApplicationId} not found.", password.ApplicationId);
                    return NotFound();
                }

                if (application.ApplicationType == ApplicationType.PROFESSIONAL)
                {
                    encryptionStrategy = new RSAEncryption();
                }
                else
                {
                    encryptionStrategy = new AESEncryption();
                }

                var createdPassword = await _passwordService.CreatePassword(password, encryptionStrategy);

                _logger.LogInformation("Password created successfully with ID {PasswordId}.", createdPassword.PasswordId);
                return CreatedAtAction(nameof(GetPasswordById), new { passwordId = createdPassword.PasswordId }, PasswordMapper.ToPasswordReadDTO(createdPassword));
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database update error while creating password.");
                return Conflict("An password with the same name already exists.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while processing the request.");
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        /// <summary>
        /// Retrieves an password by its ID.
        /// </summary>
        /// <param name="passwordId">The password ID.</param>
        /// <returns>The password details or a not found status.</returns>
        [HttpGet("{passwordId:int}")]
        public async Task<IActionResult> GetPasswordById(int passwordId)
        {
            _logger.LogInformation("Processing request to get password with ID {PasswordId}.", passwordId);
            IEncryptionStrategy encryptionStrategy = null;

            var password = await _passwordService.GetPasswordById(passwordId);
            if (password == null)
            {
                _logger.LogWarning("Password with ID {PasswordId} not found.", passwordId);
                return NotFound();
            }

            _logger.LogInformation("Processing request to get linked application with ID {ApplicationId}.", password.ApplicationId);

            var application = await _applicationService.GetApplicationById(password.ApplicationId);
            if (application == null)
            {
                _logger.LogWarning("Application with ID {ApplicationId} not found.", password.ApplicationId);
                return NotFound();
            }
            password.Application = application;

            if (application.ApplicationType == ApplicationType.PROFESSIONAL)
            {
                encryptionStrategy = new RSAEncryption();
            }
            else
            {
                encryptionStrategy = new AESEncryption();
            }

            password.EncryptedPassword = encryptionStrategy.Decrypt(password.EncryptedPassword);


            _logger.LogInformation("Password with ID {PasswordId} retrieved successfully.", passwordId);
            return Ok(PasswordMapper.ToPasswordReadDTO(password));
        }

        /// <summary>
        /// Retrieves all passwords.
        /// </summary>
        /// <returns>A list of all passwords or a no content status if none are found.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllPasswords()
        {
            _logger.LogInformation("Processing request to get all passwords.");
            IEncryptionStrategy encryptionStrategy = null;

            var passwords = await _passwordService.GetAllPasswords();
            if (passwords == null || !passwords.Any())
            {
                _logger.LogWarning("No passwords found.");
                return NoContent();
            }

            foreach (var password in passwords)
            {
                _logger.LogInformation("Processing request to get linked application with ID {ApplicationId}.", password.ApplicationId);

                var application = await _applicationService.GetApplicationById(password.ApplicationId);
                if (application == null)
                {
                    _logger.LogWarning("Application with ID {ApplicationId} not found.", password.ApplicationId);
                    return NotFound(); 
                }
                password.Application = application;

                if (application.ApplicationType == ApplicationType.PROFESSIONAL)
                {
                    encryptionStrategy = new RSAEncryption();
                }
                else
                {
                    encryptionStrategy = new AESEncryption();
                }

                password.EncryptedPassword = encryptionStrategy.Decrypt(password.EncryptedPassword);
            }

            _logger.LogInformation("Passwords retrieved successfully, total count: {Count}.", passwords.Count);
            return Ok(passwords.Select(PasswordMapper.ToPasswordReadDTO));
        }

        /// <summary>
        /// Deletes an password by its ID.
        /// </summary>
        /// <param name="passwordId">The password ID.</param>
        /// <returns>The HTTP response indicating the result of the deletion.</returns>
        [HttpDelete("{passwordId:int}")]
        public async Task<IActionResult> DeletePassword(int passwordId)
        {
            _logger.LogInformation("Processing request to delete password with ID {PasswordId}.", passwordId);

            var result = await _passwordService.DeletePassword(passwordId);
            if (!result)
            {
                _logger.LogWarning("Password with ID {PasswordId} not found.", passwordId);
                return NotFound();
            }

            _logger.LogInformation("Password with ID {PasswordId} deleted successfully.", passwordId);
            return NoContent();
        }
    }
}

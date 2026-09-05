
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevelopersController : ControllerBase
{
    private readonly DeveloperService _developerService;

    public DevelopersController(
        DeveloperService developerService)
    {
        _developerService = developerService;
    }

    [HttpGet("documentation")]
    public async Task<ActionResult<List<DeveloperLearning>>>
        GetDocumentationUsers()
    {
        return Ok(
            await _developerService.GetDocumentationUsersAsync()
        );
    }

    [HttpGet("documentation-and-ai")]
    public async Task<ActionResult<List<DeveloperLearning>>>
        GetDocumentationAndAIUsers()
    {
        return Ok(
            await _developerService.GetDocumentationAndAIUsersAsync()
        );
    }

    [HttpGet("ai-trust/{trust}")]
    public async Task<ActionResult<List<DeveloperLearning>>>
        GetByAITrust(string trust)
    {
        return Ok(
            await _developerService.GetByAITrustAsync(trust)
        );
    }

    [HttpGet("experience/{level}")]
    public async Task<ActionResult<List<DeveloperLearning>>>
        GetByExperienceLevel(string level)
    {
        return Ok(
            await _developerService.GetByExperienceLevelAsync(level)
        );
    }

    [HttpGet("backend-ai")]
    public async Task<ActionResult<List<DeveloperLearning>>>
        GetBackendAIUsers()
    {
        return Ok(
            await _developerService.GetBackendAIUsersAsync()
        );
    }
}
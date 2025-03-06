using Microsoft.AspNetCore.Mvc;
using RolesGrade.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class RoleGradeController : ControllerBase
{
    private readonly ILogger<RoleGradeController> _logger;
    private readonly IRoleGradeService _service;

    public RoleGradeController(ILogger<RoleGradeController> logger, IRoleGradeService service)
    {
        _logger = logger;
        _service = service;
    }

    #region Grade

    [HttpPost]
    [Route("grade")]
    public async Task<IActionResult> CreateGrade([FromBody] Grade grade)
    {
        try
        {
            if (grade == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid request");
                return BadRequest();
            }



            var response = await _service.CreateGrade(grade);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to create grade: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("grade/{businessId}/{gradeId}")]
    public async Task<IActionResult> GetGradeById(Guid businessId, string gradeId)
    {
        try
        {
            var response = await _service.GetGradeById(businessId, gradeId);
            return response != null ? Ok(response) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get grade by ID: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("grade/{businessId}")]
    public async Task<IActionResult> GetAllGrades(Guid businessId)
    {
        try
        {
            var response = await _service.GetAllGrades(businessId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get all grades: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("grade")]
    public async Task<IActionResult> UpdateGrade([FromBody] Grade grade)
    {
        try
        {
            if (grade == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid request");
                return BadRequest();
            }

            var response = await _service.UpdateGrade(grade);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update grade: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("grade/{businessId}/{gradeId}")]
    public async Task<IActionResult> DeleteGrade(Guid businessId, string gradeId)
    {
        try
        {
            var response = await _service.DeleteGrade(gradeId, businessId);
            return response ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete grade: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    #endregion

    #region Roles

    [HttpPost]
    [Route("role")]
    public async Task<IActionResult> CreateRole([FromBody] Role role)
    {
        try
        {
            if (role == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid request");
                return BadRequest();
            }



            var response = await _service.CreateRole(role);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to create role: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("role/{businessId}/{roleId}")]
    public async Task<IActionResult> GetRoleById(Guid businessId, string roleId)
    {
        try
        {
            var response = await _service.GetRoleById(businessId, roleId);
            return response != null ? Ok(response) : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get role by ID: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("role/{businessId}")]
    public async Task<IActionResult> GetAllRoles(Guid businessId)
    {
        try
        {
            var response = await _service.GetAllRoles(businessId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to get all roles: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut]
    [Route("role")]
    public async Task<IActionResult> UpdateRole([FromBody] Role role)
    {
        try
        {
            if (role == null || !ModelState.IsValid)
            {
                _logger.LogWarning($"Invalid request");
                return BadRequest();
            }

            var response = await _service.UpdateRole(role);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update role: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete]
    [Route("role/{businessId}/{roleId}")]
    public async Task<IActionResult> DeleteRole(Guid businessId, string roleId)
    {
        try
        {
            var response = await _service.DeleteRole(businessId, roleId);
            return response ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete role: {ex.Message}");
            return StatusCode(500, ex.Message);
        }
    }

    #endregion
}

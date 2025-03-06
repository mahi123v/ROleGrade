using Microsoft.EntityFrameworkCore;
using RolesGrade.Interfaces;
using YourNamespace.Data;

public class RoleGradeService : IRoleGradeService
{
    private readonly ApplicationDbContext _context;

    public RoleGradeService(ApplicationDbContext context)
    {
        _context = context;
    }

    #region GRADE

    public async Task<bool> CheckIfGradeUsed(Guid businessId, string gradeId)
    {
        return await _context.Grade.AnyAsync(g => g.BusinessId == businessId && g.GradeID == gradeId && g.Status == 1);
    }

    public async Task<Grade> CreateGrade(Grade grade)
    {

        await _context.Grade.AddAsync(grade);
        await _context.SaveChangesAsync();
        return grade;
    }

    public async Task<Grade> GetGradeById(Guid businessId, string gradeId)
    {
        return await _context.Grade.FirstOrDefaultAsync(g => g.BusinessId == businessId && g.GradeID == gradeId);
    }

    public async Task<List<Grade>> GetAllGrades(Guid businessId)
    {
        return await _context.Grade.Where(g => g.BusinessId == businessId).ToListAsync();
    }

    public async Task<Grade> UpdateGrade(Grade grade)
    {
        _context.Grade.Update(grade);
        await _context.SaveChangesAsync();
        return grade;
    }

    public async Task<bool> DeleteGrade(string gradeId, Guid businessId)
    {
        var grade = await GetGradeById(businessId, gradeId);
        if (grade != null)
        {
            _context.Grade.Remove(grade);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    #endregion

    #region Roles

    public async Task<Role> CreateRole(Role role)
    {
        await _context.Role.AddAsync(role);
        await _context.SaveChangesAsync();
        return role;
    }
    public async Task<Role> UpdateRole(Role role)
    {
        await _context.Role.AddAsync(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteRole(Guid businessId, string roleId)
    {
        var role = await GetRoleById(businessId, roleId);
        if (role != null)
        {
            _context.Role.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<Role> GetRoleById(Guid businessId, string roleId)
    {
        return await _context.Role.FirstOrDefaultAsync(g => g.BusinessId == businessId && g.RoleId == roleId);
    }

    public async Task<List<Role>> GetAllRoles(Guid businessId)
    {
        return await _context.Role.Where(g => g.BusinessId == businessId).ToListAsync();
    }

    #endregion
}

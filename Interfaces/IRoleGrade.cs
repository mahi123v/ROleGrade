namespace RolesGrade.Interfaces
{

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRoleGradeService
    {
        Task<Grade> CreateGrade(Grade gradeVM);
        Task<Grade> GetGradeById(Guid businessId, string gradeId);
        Task<List<Grade>> GetAllGrades(Guid businessId);
        Task<Grade> UpdateGrade(Grade gradeVM);
        Task<bool> DeleteGrade(string gradeId, Guid businessId);
        //Task<bool> CheckIfGradeNameExists(Guid businessId, string gradeName, string gradeId = null);
        Task<bool> CheckIfGradeUsed(Guid businessID, string gradeId);

        Task<Role> CreateRole(Role roleVM);
        Task<Role> GetRoleById(Guid businessId, string roleId);
        Task<List<Role>> GetAllRoles(Guid businessId);
        Task<Role> UpdateRole(Role roleVM);
        Task<bool> DeleteRole(Guid businessId, string roleId);


    }
}

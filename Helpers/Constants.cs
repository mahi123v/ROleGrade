namespace RolesGrade.Helpers
{
    public class Constants
    {
        public enum Status
        {
            Active = 1, Inactive = 2, Deleted = 3
        }
        public enum ApiError
        {
            OrganisationLevelNameExists = 70000,
            OrganisationLevelInUse = 70100,
            OrganisationLevelNumberExists = 70200,

            OrganisationUnitNameExists = 71000,
            OrganisationUnitInUse = 71100,

            OrganisationGradeNameExists = 72000,
            OrganisationGradeInUse = 72200,

            OrganisationRoleNameExists = 73000,
            OrganisationRoleInUse = 73300,
        }
    }
}

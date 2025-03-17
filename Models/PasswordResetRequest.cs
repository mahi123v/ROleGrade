namespace RolesGrade.Models
{
    public class PasswordResetRequest
    {


        public string Email { get; set; }          // For Forgot Password (email to send reset token)
        public string ResetToken { get; set; }    // For Reset Password (received token for validation)
        public string NewPassword { get; set; }
    }
}

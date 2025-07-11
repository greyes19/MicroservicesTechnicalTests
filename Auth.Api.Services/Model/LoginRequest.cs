namespace Auth.Api.Services.Model
{
    /// <summary>
    /// The login request.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Get or set the user name
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Get or set the password
        /// </summary>
        public string Password { get; set; }
    }
}

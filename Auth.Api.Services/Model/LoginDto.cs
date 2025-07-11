namespace Auth.Api.Services.Model
{
    /// <summary>
    /// The login dto.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Get or set the bearer token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// Get or set expires at
        /// </summary>
        public DateTime ExpiresAt { get; set; }
    }
}

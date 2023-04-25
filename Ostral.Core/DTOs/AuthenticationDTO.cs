namespace Ostral.Core.DTOs
{
	public class AuthenticationDTO
	{
		public string Token { get; set; } = string.Empty;

		public string RefreshToken { get; set; } = string.Empty;
	}
}

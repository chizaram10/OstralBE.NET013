using Ostral.Core.Interfaces;

namespace Ostral.Core.Results
{
	public class Result<T> : IResult
	{
		public bool Success { get; set; }
		public IEnumerable<string> Errors { get; set; } = Array.Empty<string>();
		public T? Data { get; set; }
	}
}

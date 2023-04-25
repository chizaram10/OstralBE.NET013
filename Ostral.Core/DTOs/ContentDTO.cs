
namespace Ostral.Core.DTOs
{
    public class ContentDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public double Duration { get; set; }
        public bool IsDownloadable { get; set; }
		public string ContentUrl { get; set; } = string.Empty;
	}
}

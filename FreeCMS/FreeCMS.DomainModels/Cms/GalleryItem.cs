namespace FreeCMS.DomainModels.Cms
{
	public class GalleryItem
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateTime CreationDate { get; set; }
		public string? Address { get; set; }
		public int GalleryId { get; set; }
		public virtual Gallery? Gallery { get; set; }
	}
}

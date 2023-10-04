namespace FreeCMS.DomainModels.Cms
{
	public class Slide
	{
		public int Id { get; set; }
		public string Text { get; set; }
		public DateTime CreationDate { get; set; }
		public string Image { get; set; }
		public string Link { get; set; }
		public int ShowPriority { get; set; }
	}

}

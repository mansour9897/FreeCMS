namespace FreeCMS.Areas.Cms.ViewModels.Topic
{
	public class TopicCheckbox
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsSelected { get; set; }
		public List<TopicCheckbox> Children { get; set; }
	}
}

namespace FreeCMS.DomainModels.Cms
{
	public class PostTopic
	{
		public int PostId { get; set; }
		public int TopicId { get; set; }
		public virtual Post? Post { get; set; }
		public virtual Topic? Topic { get; set; }
	}
}
